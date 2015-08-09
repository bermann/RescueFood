using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	Vector3 BallisticVel( Transform target, float angle)  {
		Vector3 dir = target.position - transform.position;  // get target direction
		float h = dir.y;  // get height difference
		dir.y = 0;  // retain only the horizontal direction
		float dist = dir.magnitude ;  // get horizontal distance
		var a = angle * Mathf.Deg2Rad;  // convert angle to radians
		dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
		dist += h / Mathf.Tan(a);  // correct for small height differences
		// calculate the velocity magnitude
		float vel = 200f;
		return vel * dir.normalized;
	}
	
	public Transform myTarget;  // drag the target here
	public GameObject cannonball;  // drag the cannonball prefab here
	float shootAngle = 45f;  // elevation angle
	float minDist = 300f;

	float lastShot = -1f;

	void Update(){
		if (Vector3.Distance(transform.position, myTarget.position) < minDist && (Time.time - lastShot) > 1f){  // press b to shoot
			lastShot = Time.time;
			GameObject ball = (GameObject) Instantiate(cannonball, transform.position, Quaternion.identity);
			Rigidbody ballRB = (Rigidbody) ball.GetComponent<Rigidbody>();
			ballRB.velocity = BallisticVel(myTarget, shootAngle);
			Destroy(ball, 5);
		}
	}
}
