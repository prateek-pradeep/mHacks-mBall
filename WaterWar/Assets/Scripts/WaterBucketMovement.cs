using UnityEngine;
using System.Collections;

public class WaterBucketMovement : MonoBehaviour
{
	public bool isFloating = false;
	public Transform parent;

	float offsetPosition = 0.2f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isFloating) {
			transform.position = new Vector3 (parent.position.x + offsetPosition, parent.position.y + offsetPosition, transform.position.z);
		}
	}
}
