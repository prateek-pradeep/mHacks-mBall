using UnityEngine;
using System.Collections;

public class RotateBounce : MonoBehaviour
{
	public bool isCongratulation = false;
	public Sprite blue ;
	public Sprite yellow;
	public SpriteRenderer sr;

	void OnEnable ()
	{
		if (isCongratulation) {
			if (GameManager.Instance.winTeam == Team.Blue)
				sr.sprite = blue;
			else
				sr.sprite = yellow;

		}

		iTween.PunchRotation (gameObject,
		                iTween.Hash (
			"z", 25,
			"time", 3,
			"easetype", iTween.EaseType.easeOutElastic,
			"oncomplete", "OnCeompleteRotate"));
	}

	void OnCeompleteRotate ()
	{
		if (!isCongratulation)
			GameManager.Instance.GameStateChangeTo (GameState.ResetPlayer);
	}
}
