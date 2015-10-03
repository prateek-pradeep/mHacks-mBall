using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{

	public TextMesh scoreTM;
	int score;
	// Use this for initialization
	void Start ()
	{
		score = 0;
	}
	
	public void UpdateScore ()
	{
		score++;
		scoreTM.text = score.ToString ();
	}
}
