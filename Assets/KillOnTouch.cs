using UnityEngine;
using System.Collections;

public class KillOnTouch : MonoBehaviour {
	
	public static KillOnTouch self;
	
	private float id;
	private bool alertIsPlaying = false;
	
	void Start(){
		id = Random.Range (0, 1);
		self = this;
	}
	
	void Update(){ 
		transform.Rotate (1.5f, .5f, 4f);
		if (Vector3.Distance (PlayerController.self.transform.position, transform.position) < 400) {
			if(!alertIsPlaying){
				PlayerController.self.PlayAlert(id);
				alertIsPlaying = true;
			}
			
		} else if (alertIsPlaying) {
			PlayerController.self.StopAlert(id);
			alertIsPlaying = false;
		}
	}
}
