using UnityEngine;
using System.Collections;

public class PlayerBhv : MonoBehaviour {

	public float AmbientSpeed = 300.0f;
	
	public float RotationSpeed = 100.0f;
	
	void FixedUpdate()
	{        
		Quaternion AddRot = Quaternion.identity;
		float roll = 0;
		float pitch = 0;
		float yaw = 0;
		roll = Input.GetAxis("Horizontal") * (Time.deltaTime * RotationSpeed);
		pitch = -Input.GetAxis("Vertical") * (Time.deltaTime * RotationSpeed);
		AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
		GetComponent<Rigidbody>().rotation *= AddRot;
		Vector3 AddPos = transform.forward;
		AddPos = GetComponent<Rigidbody>().rotation * AddPos;
		GetComponent<Rigidbody>().velocity = AddPos * (Time.deltaTime * AmbientSpeed);
	}
}
