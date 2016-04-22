using UnityEngine;
using System.Collections;

public class FollowTargetHeight : MonoBehaviour {
	public Transform target;
	public Vector3 offset = new Vector3(0f, 7.5f, 0f);


	private void LateUpdate()
	{
		Vector3 pos = new  Vector3(transform.position.x, target.position.y, transform.position.z);
		transform.position = pos + offset;
	}
}
