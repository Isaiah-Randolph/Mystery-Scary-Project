using UnityEngine;
using System.Collections;

public class AIBehaviour : MonoBehaviour {
	private AIsight sight;
	public GameObject player;
	private PlayerState playerstate;

	//For patrolling behaviour
	public Transform[] patrolPoints;
	private int destPoint = 0;
	private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent> ();
		sight = GetComponent<AIsight> ();

		playerstate = player.GetComponent<PlayerState> ();

		//start patrolling
		GotoNextPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		//If player is seen and is alive...
		if (sight.playerInSight && playerstate.isAlive)
			//... chase player
			chasing ();
		// If player is heard...
		else if (sight.playerIsHeard && playerstate.isAlive)
			//... search for player
			searching ();
		//else..
		else
			//... patrol the floor for player
			patrolling ();
	}

	void patrolling() {
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (navAgent.remainingDistance < 0.5f)
			GotoNextPoint();
	}

	void chasing () {
		navAgent.destination = player.transform.position;
	}

	void searching () {
		navAgent.destination = sight.personalLastSighting;
	}

	private void GotoNextPoint() {
		// Returns if no points have been set up
		if (patrolPoints.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		navAgent.destination = patrolPoints[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % patrolPoints.Length;
	}
}
