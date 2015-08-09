using UnityEngine;
using System.Collections;

public class KillOnTouch : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("GAme Over!");
			Application.LoadLevel("game over");
		}
	}

	void Update(){
		transform.Rotate (1.5f, .5f, 4f);
	}
}
