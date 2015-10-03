using UnityEngine;
using System.Collections;
using BladeCast;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

	float speed = 3f;
	PlayerProps myProp;
	GameObject waterBucket;
	float axisX = 0f, axisY = 0f;
	Rigidbody2D rb;
	public int playerIndex = 0;
	Vector2 originalPosition;
	bool isRepositioning = true;

	Vector3[] list = new []{
		new Vector3 (5f, 2f, -2.1f),
		new Vector3 (-5f, 2f, -2.1f),
//		new Vector3 (5f, 0f, -2.1f),
//		new Vector3 (-5f, 0f, -2.1f),
		new Vector3 (5f, -2f, -2.1f),
		new Vector3 (-5f, -2f, -2.1f)
	};
	// Use this for initialization
	void Start ()
	{
		myProp = gameObject.GetComponent<PlayerProps> ();
		rb = GetComponent<Rigidbody2D> ();
		BCMessenger.Instance.RegisterListener ("move_axis", 0, this.gameObject, "HandleMoveDirection");
		GetComponentInChildren<TextMesh> ().text = playerIndex.ToString ();
		print (playerIndex);
		originalPosition = list[playerIndex-1];

	}


	void HandleMoveDirection (ControllerMessage msg)
	{
		if (playerIndex == msg.ControllerSource) {
			if (msg.Payload.HasField ("move_axis")) {
				float.TryParse (msg.Payload.GetField ("axisX").ToString (), out axisX);
				float.TryParse (msg.Payload.GetField ("axisY").ToString (), out axisY);
			}
		}

		if (!GameManager.Instance.canPlayerMove)
			axisX = axisY = 0;
	}

	float vel = 4f;

	void FixedUpdate ()
	{
		if (!isRepositioning && GameManager.Instance.canPlayerMove) {
			Vector3 movement = new Vector3 (axisX * vel, axisY * vel, 0);
			rb.velocity = movement;
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll)
	{

		if (coll.gameObject.tag.Equals ("Player")) {
			PlayerProps otherProp = coll.gameObject.GetComponent<PlayerProps> ();

			// bounce in inverse direction
//			print ("bounc]e up");

//			rb.AddForce (coll.contacts [0].normal * 50f, ForceMode2D.Impulse);

			// check if other person has ball 
			// if yes the exchange it
			Sound.Instance.PlayBump();
			if (myProp.isHoldingBall) {
				GameObject ball = GameObject.FindGameObjectWithTag ("Ball");
				Ball ballScript = ball.GetComponent<Ball> ();
				ballScript.ScaleToZero (coll.gameObject.transform);
				myProp.isHoldingBall = false;
			}
		}
	}

	public void Reposition ()
	{
		Sound.Instance.PlayPing ();
		transform.position = list[playerIndex-1];
		isRepositioning = true;
		gameObject.GetComponent<PlayerProps> ().isHoldingBall = false;
		transform.localScale = new Vector3 (0f, 0f, 1f);
		iTween.ScaleTo (gameObject,
		               iTween.Hash (
			"x", 1,
			"y", 1,
			"time", 1,
			"easetype", iTween.EaseType.easeOutBack,
			"oncomplete", "OnCompleteReposition"));
	}
	

	void OnCompleteReposition ()
	{
		isRepositioning = false;
	}
}
