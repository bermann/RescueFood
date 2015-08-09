using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed_z = 30;
	public float max_x_rotation = 15;
	public float bounce_rotation = 10;
	public float speed_z_rotation = 1000;
	public float speed_x_rotation = 4;
	public Rigidbody rb;

	public GameObject BulletPrefab;
	public float bulletSpeed = 60f;

	private float z_rotation = 0;
	private float x_rotation = 0;

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

		//HORIZONTAL MOVEMENTS
		if (moveHorizontal < 0 && z_rotation > -90) {
			transform.RotateAround (transform.position, transform.forward, speed_z_rotation * Time.deltaTime);
			z_rotation -= speed_z_rotation * Time.deltaTime;
		} else if (moveHorizontal > 0 && z_rotation < 90) {
			transform.RotateAround (transform.position, transform.forward, -speed_z_rotation * Time.deltaTime);
			z_rotation += speed_z_rotation * Time.deltaTime;
		} else if(moveHorizontal == 0) {
			transform.RotateAround (transform.position, transform.forward, z_rotation/100);
			z_rotation -= z_rotation/100;
		}

		//VERTICAL MOVEMENTS
		if (moveVertical < 0 && x_rotation < max_x_rotation) {
			transform.RotateAround (transform.position, Quaternion.Euler(0,90f,0) * transform.forward, -speed_x_rotation * Time.deltaTime);
			x_rotation += speed_x_rotation * Time.deltaTime;

		} else if(moveVertical == 0) {
			transform.RotateAround (transform.position, Quaternion.Euler(0,90f,0) * transform.forward, x_rotation/40);
			x_rotation -= x_rotation/40;
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
