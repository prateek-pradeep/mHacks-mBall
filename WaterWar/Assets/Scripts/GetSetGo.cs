using UnityEngine;
using System.Collections;

public class GetSetGo : MonoBehaviour
{
	void OnEnable ()
	{
		transform.localScale = new Vector3 (0f, 0f, 1f);
		iTween.ScaleTo (gameObject,
		               iTween.Hash (
			"x", 1,
			"y", 1,
			"time", 0.5,
			"easetype", iTween.EaseType.easeOutElastic
		));

		Invoke ("OnCompleteDone", 1f);
	}

	void OnCompleteDone ()
	{
		gameObject.SetActive (false);
	}

	void OnDisable ()
	{
		transform.localScale = new Vector3 (0f, 0f, 1f);
	}
}
