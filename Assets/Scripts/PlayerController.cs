using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public static PlayerController self;
	
	private const int HORIZONTAL = 1000;
	private const int VERTICAL = 1001;
	
	private const int NONE_VERTICAL = -1;
	private const int NONE_HORIZONTAL = -2;
	private const int LEFT = 1;
	private const int RIGHT = 2;
	private const int DOWN = 3;
	private const int UP = 4;
	
	private float vertical_timer = 0;
	private float horizontal_timer = 0;
	private int code_vertical_timer = NONE_VERTICAL;
	private int code_horizontal_timer = NONE_HORIZONTAL;
	private float current_vertical_speed = 0;
	private float z_rotation = 0;
	private float x_rotation = 0;
	private bool inBarrel = false;
	private float barrelDegrees = 0;
	private float barrelSign = 1;
	private bool inHit = false;
	private int vibrationTime = 0;
	private int vibrationSign = -1;
	private float inicialZRotation = 0;
	
	
	public static int score = 0;
	
	int lifes = 3;
	
	public float speed_z = 40;
	public float speed_barrel_boost = 60;
	public float max_x_rotation = 25;
	public float min_x_rotation = 85;
	public float bounce_rotation = 10;
	public float speed_z_rotation = 35;
	public float speed_x_rotation = 15;
	public Rigidbody rb;
	
	public GameObject BulletPrefab;
	public float bulletSpeed = 60f;
	
	float lastBarrel;
	public float timeBetweenBarrels = 2f;
	
	float lastShot;
	
	public float tiempoRefresco = 1f;
	
	//UI ELEMENTS
	public Image life1;
	public Image life2;
	public Image life3;
	public Text scoreText;
	
	//SOUNDS
	public AudioSource planeShot;
	public AudioSource alert;
	private Dictionary<float, int> alertDictionary =
		new Dictionary<float, int>();
	
	public void PlayAlert (float id){
		if (!alertDictionary.ContainsKey (id)) {
			alertDictionary.Add (id, 0);
		}
		alert.Play ();
	}
	
	public void StopAlert (float id){
		alertDictionary.Remove (id);
		if (alertDictionary.Count == 0) {
			alert.Stop ();
		}
	}
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		lastShot = Time.time - tiempoRefresco;
		if (PlayerController.self == null) {
			PlayerController.self = this;
		}
	}
	
	void Fire()
	{
		planeShot.Play ();
		GameObject bulletClone = (GameObject) Instantiate(BulletPrefab, transform.position, transform.rotation*Quaternion.Euler(0,180,0));
	}
	
	//Control the movements timer
	void ControlTimer(int code){
		switch(code){
		case LEFT:
		case RIGHT:
		case NONE_HORIZONTAL:
			if (code_horizontal_timer == code){
				horizontal_timer += Time.deltaTime;
			}else{
				horizontal_timer = 0;
				code_horizontal_timer = code;
			}
			break;
		case UP:
		case DOWN:
		case NONE_VERTICAL:
			if (code_vertical_timer == code){
				vertical_timer += Time.deltaTime;
			}else{
				vertical_timer = 0;
				code_vertical_timer = code;
			}
			break;
		}
	}
	
	float TimerFactor(int direction){
		if (direction == VERTICAL) {
			return Mathf.Log (vertical_timer+1);
		} else {
			return Mathf.Log (horizontal_timer+1);
		}
	}
	
	float NearVerticalFactor(){
		return (max_x_rotation - x_rotation)/30f;
	}
	
	float DownFactor(){
		if (x_rotation > 0)
			return Mathf.Sqrt (x_rotation);
		return 1f;
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		
		transform.Translate (Vector3.forward * Time.deltaTime * speed_z);
		if (inBarrel) {
			transform.Translate (Vector3.forward * Time.deltaTime * speed_barrel_boost);
		}
		//HORIZONTAL MOVEMENTS
		float horizontal_movement = 0;
		if (moveHorizontal < 0 && z_rotation > -90) { //LEFT IS PRESSED
			ControlTimer (LEFT); // Control timer
			horizontal_movement = speed_z_rotation * Time.deltaTime;
		} else if (moveHorizontal > 0 && z_rotation < 90) { //RIGHT IS PRESSED
			ControlTimer (RIGHT); // Control timer
			horizontal_movement = -speed_z_rotation * Time.deltaTime;
		} else if (moveHorizontal == 0) { //NEITHER LEFT NOR RIGHT IS PRESSED
			ControlTimer (NONE_HORIZONTAL);
			horizontal_movement = z_rotation / 2 * Time.deltaTime;
		}
		transform.RotateAround (transform.position, transform.forward, horizontal_movement);
		z_rotation -= horizontal_movement;
		
		//VERTICAL MOVEMENTS
		float vertical_movement = 0;
		if (moveVertical < 0 && x_rotation < max_x_rotation) { //DOWN IS PRESSED
			ControlTimer (DOWN); // Control timer
			vertical_movement = NearVerticalFactor () * TimerFactor (VERTICAL) * -speed_x_rotation * Time.deltaTime;
			
		}
		if (moveVertical > 0) { //UP IS PRESSED
			ControlTimer (UP); //Control timer
			vertical_movement = DownFactor() * TimerFactor (VERTICAL) * speed_x_rotation * Time.deltaTime;
			
		} else if (moveVertical == 0) { //NIERTHER UP NOR DOWN IS PRESSED
			ControlTimer (NONE_VERTICAL);
			vertical_movement = TimerFactor (VERTICAL) * x_rotation / 1.8f * Time.deltaTime;
		}
		
		//THIS DID NOT WORK, IF I HAVE TIME I WILL TAKE I LOOK IN A WHILE
		/*if (vertical_movement < 0) {
			if (vertical_movement / Time.deltaTime > current_vertical_speed) {
				vertical_movement = current_vertical_speed * Time.deltaTime;
			} else {
				current_vertical_speed = vertical_movement / Time.deltaTime;
			}
		} else {
			if (vertical_movement / Time.deltaTime < current_vertical_speed) {
				vertical_movement = current_vertical_speed * Time.deltaTime;
			} else {
				current_vertical_speed = vertical_movement / Time.deltaTime;
			}
		}*/
		
		transform.RotateAround (transform.position, Quaternion.Euler (0, 90f, 0) * transform.forward, vertical_movement);
		x_rotation -= vertical_movement;
		transform.RotateAround (transform.position, Vector3.up, Time.deltaTime * z_rotation / 1.7f);
		
		
		if (inBarrel) {
			float barrel = -400 * Time.deltaTime;
			transform.RotateAround (transform.position, transform.forward, barrel*barrelSign);
			barrelDegrees -= barrel;
			if (barrelDegrees >= 360) {
				transform.RotateAround (transform.position, transform.forward, (360f-barrelDegrees));
				barrelDegrees = 0;
				inBarrel = false;
			}
		}
		
		if (inHit) {
			if(vibrationTime == 3){
				inHit = false;
				vibrationTime = 0;
			}
			if(Mathf.Abs(inicialZRotation - z_rotation) > 20){
				vibrationTime += 1;
				vibrationSign *= -1;
			}
			transform.RotateAround (transform.position, transform.forward, 700*Time.deltaTime*vibrationSign);
			z_rotation -= 300*Time.deltaTime*vibrationSign;
			
		}
		
		if (Input.GetKeyDown ("space")) {
			float timeNow = Time.time;
			if ((timeNow - lastBarrel) > timeBetweenBarrels && !inBarrel) {
				lastBarrel = timeNow;
				inBarrel = true;
				if(z_rotation > 0){
					barrelSign = 1;
				} else {
					barrelSign = -1;
				}
			}
		}
		
		if (Input.GetButtonDown ("Fire1")) {
			float timeNow = Time.time;
			if ((timeNow - lastShot) > tiempoRefresco){
				lastShot = timeNow;
				Fire ();
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "EnemyBullet") {
			if(lifes == 3){
				life3.enabled = false;
			}else if(lifes == 2){
				life2.enabled =false;
			} else{
				Application.LoadLevel("game over");
			}
			inHit = true;
			inicialZRotation = z_rotation;
			lastBarrel = Time.time;
			lifes -= 1;
		}
		
	}
	
	public static void IncreaseScore(int plus){
		score += plus;
		self.scoreText.text = "Score: " + score;
	}
}
