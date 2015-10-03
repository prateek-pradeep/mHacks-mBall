using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState
{
	None=0,
	GetSetGo,
	Play,
	TouchDown,
	ResetPlayer,
	Congratulation,
}

public  class GameManager : MonoBehaviour
{

	public static GameManager Instance { get; private set; }
	
	private void Awake ()
	{
		if (Instance != null) {
			DestroyImmediate (gameObject);
			return;
		}
		Instance = this;
	}

	float maxScore = 3;
	public GetSetGoParent getSetGoParent ;
	public GameObject touchDown;
	public  bool canPlayerMove = false;
	public GameObject waitingConnect;
	public  bool[] connectedPlayer = new bool[4] {
		false,
		false,
		false,
		false
//		false,
//		false
	};
	public PlayerConnection connection;
	public Text blueScore;
	public Text yellowScore;
	public GameObject Congratulation;
	float yellowTeamScore = 0, blueTeamScore = 0;
	GameState gameState = GameState.None;
	public Team winTeam;

	public void GameStateChangeTo (GameState newState)
	{
		gameState = newState;
	}


	// Use this for initialization
	void Update ()
	{

		switch (gameState) {
		case GameState.GetSetGo:
			waitingConnect.SetActive (false);
			touchDown.SetActive (false);
			StartGame ();
			GameStateChangeTo (GameState.None);
			break;
		case GameState.Play:
			canPlayerMove = true;
			break;
		case GameState.TouchDown:
			Sound.Instance.PlayTouchdown ();
			canPlayerMove = false;
			touchDown.SetActive (true);
			GameStateChangeTo (GameState.None);
			break;
		case GameState.ResetPlayer:
			connection.ResetGame ();
			if (gameState != GameState.Congratulation && (yellowTeamScore == maxScore || blueTeamScore == maxScore)) {
				gameState = GameState.Congratulation;
			} else
				GameStateChangeTo (GameState.GetSetGo);
			break;
		case GameState.Congratulation:
			touchDown.SetActive (false);
			Congratulation.SetActive (true);
			connection.WinCondition ();
			Sound.Instance.PlayWin ();
			GameStateChangeTo (GameState.None);
			break;
		}


	}

	public  void UpdateConnectedPlayerList (int index)
	{
		connectedPlayer [index] = true;

		for (int i=0; i<connectedPlayer.Length; i++) {
			if (!connectedPlayer [i])
				return;
		}

		//Start Game counter
		GameStateChangeTo (GameState.GetSetGo);
	}

	void StartGame ()
	{
		getSetGoParent.StartGetSetGo ();
	}

	public void TouchDown (Team winTeam)
	{
		gameState = GameState.TouchDown;
		this.winTeam = winTeam;
		if (winTeam == Team.Blue) {

			yellowTeamScore++;
			yellowScore.text = yellowTeamScore.ToString ();
		} else {

			blueTeamScore++;
			blueScore.text = blueTeamScore.ToString ();
		}
	}
}

