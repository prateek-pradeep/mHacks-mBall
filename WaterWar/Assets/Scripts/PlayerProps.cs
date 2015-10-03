using UnityEngine;
using System.Collections;

public enum Team{
	Blue=0,
	Yellow=1
}

public class PlayerProps : MonoBehaviour {
	public bool isHoldingBall = false;
	public Team playerTeam;



}
