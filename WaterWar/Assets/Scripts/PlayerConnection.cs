using UnityEngine;
using System.Collections;
using BladeCast;

public class PlayerConnection : MonoBehaviour
{


	Vector3[] list = new []{
		new Vector3 (5f, 2f, -2.1f),
		new Vector3 (-5f, 2f, -2.1f),
//		new Vector3 (5f, 0f, -2.1f),
//		new Vector3 (-5f, 0f, -2.1f),
		new Vector3 (5f, -2f, -2.1f),
		new Vector3 (-5f, -2f, -2.1f)
	};
	Vector3[] winList = new []{
		new Vector3 (2f, -3f, -2.1f),
		new Vector3 (2f, -3f, -2.1f),
//		new Vector3 (0f, -3f, -2.1f),
//		new Vector3 (0f, -3f, -2.1f),
		new Vector3 (-2f, -3f, -2.1f),
		new Vector3 (-2f, -3f, -2.1f)
	};
	public GameObject[] playerList = new GameObject[4];
	// Use this for initialization
	void Start ()
	{
		BCMessenger.Instance.RegisterListener ("connect", 0, gameObject, "HandleControllerRegister");
	}

	void HandleControllerRegister (ControllerMessage msg)
	{		
		int controllerIndex = msg.ControllerSource;
		if (controllerIndex > 4) {
			print ("Too many controllers - bugger off");
			return;
		} 
		playerList [controllerIndex - 1].transform.position = list [controllerIndex - 1];

		playerList [controllerIndex - 1].SetActive (true);

		GameManager.Instance.UpdateConnectedPlayerList (controllerIndex - 1);

		PlayerMovement plMove = playerList [controllerIndex - 1].GetComponent<PlayerMovement> ();
		plMove.Reposition ();

	}

	public void WinCondition ()
	{
		// if yellow wins
		if (GameManager.Instance.winTeam == Team.Yellow) {
			for (int i=0; i<playerList.Length; i++) {
				if (i % 2 == 0) {
					playerList [i].transform.position = winList [i];
					iTween.ScaleTo (playerList [i],
					               iTween.Hash (
						"x", 1.3,
						"y", 1.3,
						"time", 1,
						"easetype", iTween.EaseType.easeOutBack,
						"looptype", iTween.LoopType.pingPong));
										
				} else
					playerList [i].SetActive (false);
			}
		}

		if (GameManager.Instance.winTeam == Team.Blue) {
			for (int i=0; i<playerList.Length; i++) {
				if (i % 2 == 1) {
					playerList [i].transform.position = winList [i];
					iTween.ScaleTo (playerList [i],
					               iTween.Hash (
						"x", 1.3,
						"y", 1.3,
						"time", 1,
						"easetype", iTween.EaseType.easeOutBack,
						"looptype", iTween.LoopType.pingPong));
				} else
					playerList [i].SetActive (false);
			}

		}
		
		GameObject.FindGameObjectWithTag ("Ball").SetActive (false);

			
	}

	public void ResetGame ()
	{
		for (int i=0; i<playerList.Length; i++)
			playerList [i].GetComponent<PlayerMovement> ().Reposition ();
			
		GameObject.FindGameObjectWithTag ("Ball").GetComponent<Ball> ().ResetPosition ();
	}
}
