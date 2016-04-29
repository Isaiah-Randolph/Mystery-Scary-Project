using UnityEngine;
using System.Collections;

public class int_peek : MonoBehaviour {
	public bool inTrigger = false;

	private GameObject player;
	private PlayerController playerscript;

	public Transform Headpos;
	public Transform Bodypos;
	private Transform initPos;
	private	float starttime;

	private bool entering = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerscript = player.GetComponent<PlayerController> ();
		initPos = player.transform;
	}

	void Update () {
		// to set toggle action and action starttime
		if (inTrigger && Input.GetButtonDown ("Interact")) {
			if (!playerscript.m_IsInteracting) {
				playerscript.m_IsInteracting = true;
				starttime = Time.time;
				entering = true;
			} else {
				playerscript.m_IsInteracting = false;
				starttime = Time.time;
				entering = false;
			}
		}

		// call the peeking function
		if (inTrigger) {
			playerscript.Peeks (Headpos, starttime);
			playerscript.RotatePlayer (initPos, Bodypos, starttime, 1.0f, entering);
		} 
	}

	void OnTriggerEnter(Collider body) {
		if (body.gameObject.tag == "Player") {
			initPos = player.transform;
		}
	}

	//Check if player in trigger
	void OnTriggerStay(Collider body) {
		if (body.gameObject.tag == "Player") {
			inTrigger = true;
		}
	}

	void OnTriggerExit (Collider body) {
		if (body.gameObject.tag == "Player") {
			inTrigger = false;
		}
	}
}
