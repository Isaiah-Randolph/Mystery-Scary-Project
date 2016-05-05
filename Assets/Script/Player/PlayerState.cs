using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

	public bool isAlive = true;
	public bool isMoving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Vertical") || Input.GetButton ("Horizontal")) {
			isMoving = true;
		} else {
			isMoving = false;
		}
	}
}
