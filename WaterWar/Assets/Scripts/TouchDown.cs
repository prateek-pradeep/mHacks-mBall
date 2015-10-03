using UnityEngine;
using System.Collections;

public class TouchDown : MonoBehaviour
{
	public Team teamColor;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Player")) {
			PlayerProps playerProps = other.gameObject.GetComponent<PlayerProps> ();
			if (playerProps.isHoldingBall && teamColor == playerProps.playerTeam) {
				GameManager.Instance.TouchDown (teamColor);

			}
		}
	}
}
