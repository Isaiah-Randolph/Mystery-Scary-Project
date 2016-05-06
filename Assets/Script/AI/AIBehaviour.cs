using UnityEngine;
using System.Collections; 
using System.Collections.Generic;


public class AIBehaviour : MonoBehaviour {
	private AIsight sight;
	public GameObject player;
	private PlayerState playerstate;

	//For search behaviour
	public Transform[] searchPoints;
	private int searchPoint = 0;
	[SerializeField] [Range(0f, 3600f)] private float m_searchtimer;
	public float timeLeft = 0f;
	public bool searching = false;

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
			chase ();
		// If player is heard...
		else if (sight.playerIsHeard && playerstate.isAlive)
			//... search for player
			search ();
		//else..
		else
			//... patrol the floor for player
			patrol ();
	}

	//Patrol functions
	void patrol() {
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (navAgent.remainingDistance < 0.5f)
			GotoNextPoint();
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

	//Chase functions
	void chase () {
		navAgent.destination = player.transform.position;
	}

	//Search functions
	void search () {
		//If still in searh timer...
		if (SearchTime ()) {
			//... search for player 
			GotoNextSearchPoint ();
			searching = true;
		} else {
			//... stop searching for player
			GetComponent<AIsight>().playerIsHeard = false;
			searching = false;
		}
	}

	//Get waypoints nearest to player
	public void GetSearchPoints () {
		List<Transform> transformList = new List<Transform>();
		Collider[] body = Physics.OverlapSphere(player.transform.position, 10.0f);

		foreach (Collider coll in body) {
			if (coll.tag == "waypoint") {
				transformList.Add (coll.transform);
			}
		}
		searchPoints = transformList.ToArray();
	}

	//Same as patrol function
	private void GotoNextSearchPoint() {
		// Returns if no points have been set up and stop searching
		if (searchPoints.Length == 0) {
			GetComponent<AIsight>().playerIsHeard = false;
			searching = false;
			return;
		}
		// Set the agent to go to the currently selected destination.
		navAgent.destination = searchPoints[searchPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		searchPoint = (searchPoint + 1) % searchPoints.Length;
	}

	//start the search timer from the AIsense script
	public void StartSearchTimer() {
		timeLeft = m_searchtimer;
	}

	//Countdown the timer
	private bool SearchTime() {
		//Countdown the time
		timeLeft -= Time.deltaTime;
		// if times up...
		if (timeLeft <= 0f)
			//timer stop
			return false;
		//timer continue
		return true;
	}
}
