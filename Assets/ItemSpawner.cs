﻿using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

	public GameObject[] itemList;

	float lastSpawned;
	float spawnTime = 10f;

	float minX = 188f;
	float maxX = 898f;

	float minZ = 30f;
	float maxZ = 460f;

	// Use this for initialization
	void Start () {
		lastSpawned = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - lastSpawned) > spawnTime) {
			lastSpawned = Time.time;
			int num = Random.Range(0, itemList.Length);
			float posY = ((GameObject)itemList[num]).transform.position.y;
			GameObject ball = (GameObject) Instantiate((GameObject)itemList[num], new Vector3(Random.Range(minX,maxX), posY, Random.Range(minZ,maxZ)), Quaternion.identity);
		}
	}
}