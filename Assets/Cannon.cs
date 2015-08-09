using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	Vector3 BallisticVel( Transform target)  {
		Vector3 dir = (target.position + target.forward * targetSpeed) - transform.position;  // get target direction
		float dist = dir.magnitude ;  // get horizontal distance
		// calculate the velocity magnitude
		float vel = 200f;
		return vel * dir.normalized;
	}
	
	public Transform myTarget;  // drag the target here
	public GameObject cannonball;  // drag the cannonball prefab here
	public float targetSpeed = 40;
	float minDist = 3000f;

	float lastShot = -1f;

	void Update(){
		if (Vector3.Distance(transform.position, myTarget.position) < minDist && (Time.time - lastShot) > 1f){  // press b to shoot
			lastShot = Time.time;
			GameObject ball = (GameObject) Instantiate(cannonball, transform.position, Quaternion.identity);
			Rigidbody ballRB = (Rigidbody) ball.GetComponent<Rigidbody>();
			ballRB.velocity = BallisticVel(myTarget);
			Destroy(ball, 5);
		}
	}
}
