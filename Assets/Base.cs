using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

	public float vida = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Die(){
		PlayerController.IncreaseScore (200);
		Destroy (gameObject);
	}

}
