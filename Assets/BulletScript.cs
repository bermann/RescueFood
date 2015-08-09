using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	float speed = 60f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (-1*(Vector3.forward-Vector3.up*0.1f) * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Globo") {
			//GOT IT!
			GameObject globo = other.transform.FindChild("Globo").gameObject;
			GameObject regalo = other.transform.FindChild("Regalo").gameObject;
			globo.SetActive(false);
			regalo.GetComponent<Rigidbody>().useGravity = true;
			PlayerController.IncreaseScore (80);
		} else if (other.gameObject.tag == "EnemyBase"){
			other.gameObject.GetComponent<Base>().vida -=1;
			Debug.Log(other.gameObject.GetComponent<Base>().vida);
			if (other.gameObject.GetComponent<Base>().vida == 0){
				PlayerController.IncreaseScore (200);
				Destroy (other.gameObject);
			}
		} else if (other.gameObject.tag == "EnemyCannon"){
			other.gameObject.GetComponent<Cannon>().Die();
		}
	}
}
