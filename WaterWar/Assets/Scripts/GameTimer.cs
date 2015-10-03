using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	Text score ;
	public int gameTime = 5;
	// Use this for initialization
	void Start ()
	{
		score = GetComponent<Text> ();
		score.text = gameTime.ToString();
		InvokeRepeating ("Timer", 1, 1F);
	}

	void Timer ()
	{
		if (gameTime == 1)
			CancelInvoke ("Timer");
		gameTime--;
		score.text = gameTime.ToString ();

	}
}
