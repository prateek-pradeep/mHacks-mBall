using UnityEngine;
using System.Collections;

public enum BallTransfer
{
	None=0,
	Holding,
	Animating,
}

public class Ball : MonoBehaviour
{
	public Transform parentPlayer;
	public float rotationsPerMinute = 10f;
	Vector2 originalScale = Vector2.zero;
	Vector3 originalPosition = Vector2.zero;
	bool isAnimating = false;
	BallTransfer ballTransfer = BallTransfer.None;

	void Start ()
	{
		originalPosition = new Vector3 (0.057f, -0.425f, -2.45f);
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0f, 0f, -6.0f * rotationsPerMinute * Time.deltaTime);
		originalScale = transform.localScale;

		if (parentPlayer != null) {
			transform.position = new Vector3 (parentPlayer.position.x, parentPlayer.position.y, transform.position.z);
		}
	}

	public void ResetPosition ()
	{
		print (originalPosition);
		transform.position = originalPosition;
		parentPlayer = null;
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag.Equals ("Player")) {
			parentPlayer = coll.gameObject.transform;
			PlayerProps playerProps = coll.gameObject.GetComponent<PlayerProps> ();
			playerProps.isHoldingBall = true;
			ballTransfer = BallTransfer.Holding;
			Sound.Instance.PlayCollect();
		} 
	}

	public void ScaleToZero (Transform newParent)
	{
		if (ballTransfer == BallTransfer.Animating)
			return;
		ballTransfer = BallTransfer.Animating;
		parentPlayer = newParent;

		Invoke ("ChangeToHolding", 2);
	}

	void ChangeToHolding ()
	{
		ballTransfer = BallTransfer.Holding;
	}
}
