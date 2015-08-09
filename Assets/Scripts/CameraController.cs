using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public static CameraController self;
	
	public GameObject player;
	private float distance_to_player;
	
	private bool hit = false;
	private float timeHit = 0;
	
	void Start() {
		self = this;
		distance_to_player = Vector3.Distance(transform.position, player.transform.position);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		transform.position = player.transform.position + player.transform.forward *-1 * distance_to_player+Vector3.up * 5f;
		transform.LookAt (player.transform.position);
		if (hit) {
			transform.position = transform.position + new Vector3(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f));
			if((Time.time - timeHit)>0.7f){
				hit = false;
			}
		}
	}
	
	public void Hitted(){
		hit = true;
		timeHit = Time.time;
	}
}
