using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private float distance_to_player;

	void Start() {
		distance_to_player = Vector3.Distance(transform.position, player.transform.position);
	}


	// Update is called once per frame
	void LateUpdate () {

		//transform.position = player.transform.position + new Vector3 (0f,0f,offset_z);
		
		transform.position = player.transform.position + player.transform.forward *-1 * distance_to_player+Vector3.up * 1.3f;
		transform.LookAt(player.transform.position);
	}
}
