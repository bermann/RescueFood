using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed_z = 10;
	public float rotate_speed = 10;
	public float rotation_on_turn = 1000;
	public Rigidbody rb;

	private float z_rotation = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		transform.Translate(Vector3.forward * Time.deltaTime * speed_z);

		if (moveHorizontal < 0 && z_rotation>-90) {
			transform.RotateAround (transform.position,transform.forward,rotation_on_turn*Time.deltaTime);
			z_rotation -= rotation_on_turn*Time.deltaTime;
		} else if(moveHorizontal > 0 && z_rotation<90) {
			transform.RotateAround (transform.position,transform.forward,-rotation_on_turn*Time.deltaTime);
			z_rotation += rotation_on_turn*Time.deltaTime;
		}
		transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * z_rotation/1.7f);



		/*if (moveHorizontal < 0) {
			transform.Rotate (0, -Time.deltaTime * rotate_speed, -(1f+z_rotation)*rotation_on_turn);
			z_rotation = -1;
		} else if(moveHorizontal > 0) {
			transform.Rotate (0f, Time.deltaTime * rotate_speed, (1f-z_rotation)*rotation_on_turn);
			z_rotation = 1;
		} else {
			transform.Rotate (0, Time.deltaTime * rotate_speed, -z_rotation*rotation_on_turn*1f);
			z_rotation = 0;
		}*/
	}
}
