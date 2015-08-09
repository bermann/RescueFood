using UnityEngine;
using System.Collections;

public class Globo : MonoBehaviour {

	float verticalSpeed = 8f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up*verticalSpeed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "PlayerBullet") {
			//GOT IT!
			GameObject globo = transform.FindChild("Globo").gameObject;
			GameObject regalo = transform.FindChild("Regalo").gameObject;
			globo.SetActive(false);
			regalo.GetComponent<Rigidbody>().useGravity = true;
		}
	}
}
