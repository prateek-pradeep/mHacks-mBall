using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour
{

	void OnMouseDown ()
	{

		iTween.ScaleTo (gameObject,
		                iTween.Hash (
			"x", 0.7,
			"y", 0.7,
			"time", 0.5f,
			"oncomplete","Complete",
			"easetype",iTween.EaseType.easeOutElastic));
		Sound.Instance.PlayBing2 ();


	}

	void Complete()
	{
		Application.LoadLevel ("Gameplay");
	}
}
