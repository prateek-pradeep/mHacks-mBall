using UnityEngine;
using System.Collections;

public class PingPogUpDown : MonoBehaviour
{

	public float yPos;
	// Use this for initialization
	void Start ()
	{
		iTween.MoveTo (gameObject,
		               iTween.Hash (
			"y", yPos,
			"time", 2,
			"easetype",iTween.EaseType.easeInOutBack,
			"looptype", iTween.LoopType.pingPong
		));

	}
	

}
