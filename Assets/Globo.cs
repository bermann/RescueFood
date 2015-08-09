using UnityEngine;
using System.Collections;

public class Globo : MonoBehaviour {

	float verticalSpeed = 3f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up*verticalSpeed*Time.deltaTime);
		if (transform.position.y >= 160)
			Destroy (gameObject);
	}


}
