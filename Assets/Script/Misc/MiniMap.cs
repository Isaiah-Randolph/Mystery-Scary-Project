using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject player_blip;
	// Update is called once per frame
	void FixedUpdate () {
		Quaternion rot = player.transform.rotation;
		player_blip.transform.localRotation = rot;
	}
}
