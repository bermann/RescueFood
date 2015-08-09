using UnityEngine;
using System.Collections;

public class Coleccionable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			PlayerController.IncreaseScore(20);
			Destroy (gameObject);
		}
	}
}
