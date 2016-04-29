using UnityEngine;
using System.Collections;

public class int_peek : MonoBehaviour {
	public bool inTrigger = false;

	private GameObject player;
	private PlayerController playerscript;

	public Transform Headpos;
	public Transform Bodypos;
	private	float starttime;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerscript = player.GetComponent<PlayerController> ();
	}

	void Update () {
		// to set toggle action and action starttime
		if (inTrigger && Input.GetButtonDown ("Interact")) {
			if (!playerscript.m_IsInteracting) {
				playerscript.m_IsInteracting = true;
				starttime = Time.time;
			} else {
				playerscript.m_IsInteracting = false;
				starttime = Time.time;
			}
		}

		// call the peeking function
		if (inTrigger) {
			playerscript.Peeks (Headpos, Bodypos, starttime);
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
