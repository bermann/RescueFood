using UnityEngine;
using System.Collections;

public class KillOnTouch : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log("GAme Over!");
		}
	}
}
