using UnityEngine;
using System.Collections;

public class AIsight : MonoBehaviour {
	public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
	public bool playerInSight;                      // Whether or not the player is currently sighted.
	public bool playerIsHeard;
	public Vector3 personalLastSighting;            // Last place this enemy spotted the player.

	private SphereCollider col;                     // Reference to the sphere collider trigger component.
	private GameObject player;                      // Reference to the player.
	private Vector3 previousSighting;               // Where the player was sighted last frame.

	// Use this for initialization
	void Start () {
		col = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");

		//set the current player last sighting as initial.
		personalLastSighting = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		onHeard ();
	}

	void onHeard() {
		// ... and if the player is within hearing range...
		if (Vector3.Distance (player.transform.position, transform.position) <= col.radius) {
			// if player is not sneaking and not in sight
			if (!player.GetComponent<PlayerController> ().m_IsSneaking && !playerInSight) {	
				// ... set the last personal sighting of the player to the player's current position.
				personalLastSighting = player.transform.position;
				// player is heard by AI
				playerIsHeard = true;
			}
		}
	}
	void OnTriggerStay(Collider body) {
		if (body.gameObject.tag == "Player"){
			print ("trigger stay");
			// By default the player is not in sight.
			playerInSight = false;

			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = body.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < fieldOfViewAngle * 0.5f)
			{
				RaycastHit hit;

				// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
				{
				// ... and if the raycast hits the player...
					if(hit.collider.gameObject == player)
					{
						// ... the player is in sight.
						playerInSight = true;

						// Set the last global sighting is the players current position.
						personalLastSighting = player.transform.position;
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider body){
		if (body.gameObject.tag == "Player") {
			print ("triggerexit");
			playerInSight = false;
			playerIsHeard = false;
		}
	}
}