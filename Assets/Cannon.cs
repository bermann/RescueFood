using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	
	Vector3 BallisticVel( Transform target)  {
		Vector3 dir = (target.position + target.forward * targetSpeed) - transform.position;  // get target direction
		float dist = dir.magnitude ;  // get horizontal distance
		// calculate the velocity magnitude
		float vel = 150f;
		return vel * dir.normalized;
	}
	
	public Transform myTarget;  // drag the target here
	public GameObject cannonball;  // drag the cannonball prefab here
	public AudioSource shot;
	public float targetSpeed = 40;
	float minDist = 200f;
	
	float lastShot = -3f;

	float timeOfDeath = 0f;

	void Update(){
		if (gameObject.activeSelf && Vector3.Distance (transform.position, myTarget.position) < minDist && (Time.time - lastShot) > 3f) {  // press b to shoot
			AudioSource newShot = gameObject.AddComponent<AudioSource> ();
			newShot.clip = shot.clip;
			newShot.Play ();
			lastShot = Time.time;
			GameObject ball = (GameObject)Instantiate (cannonball, transform.position, Quaternion.identity);
			Rigidbody ballRB = (Rigidbody)ball.GetComponent<Rigidbody> ();
			ballRB.velocity = BallisticVel (myTarget);
			Destroy (ball, 5);
		} else if (!gameObject.activeSelf) {
			if ((Time.time - timeOfDeath) > 5f && timeOfDeath != 0f){
				gameObject.SetActive (true);
			}
		}
	}

	public void Die() {
		gameObject.SetActive (false);
	}
}
