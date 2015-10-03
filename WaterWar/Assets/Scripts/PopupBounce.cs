using UnityEngine;
using System.Collections;

public class PopupBounce : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnEnable ()
	{
		transform.localScale = new Vector3 (0f, 0f, 1f);

		iTween.ScaleTo (gameObject,
		                iTween.Hash (
			"x", 0.7,
			"y", 0.7,
			"time", 3,
			"easetype", iTween.EaseType.easeOutElastic));
	}

	void OnDiasable ()
	{
		transform.localScale = new Vector3 (0f, 0f, 1f);
	}

}
