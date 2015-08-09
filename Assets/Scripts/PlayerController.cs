using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed_z = 10;
	public float rotate_speed = 10;
	public float rotation_on_turn = 1000;
	public Rigidbody rb;

	public GameObject BulletPrefab;
	public float bulletSpeed = 60f;

	private float z_rotation = 0;

	float lastShot;

	public float tiempoRefresco = 2f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		lastShot = Time.time - tiempoRefresco;
	}

	void Fire() 
	{
		GameObject bulletClone = (GameObject) Instantiate(BulletPrefab, transform.position, transform.rotation*Quaternion.Euler(0,180,0));
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
		} else if(moveHorizontal == 0) {
			transform.RotateAround (transform.position, transform.forward, z_rotation/100);
			z_rotation -= z_rotation/100;
			
		}
		transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * z_rotation/1.7f);

		if (Input.GetButtonDown ("Fire1")) {
			float timeNow = Time.time;
			if ((timeNow - lastShot) > tiempoRefresco){
				lastShot = timeNow;
				Fire ();
			}
		}
	}
}
