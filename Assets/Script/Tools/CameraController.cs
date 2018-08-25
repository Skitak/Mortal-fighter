using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject[] players;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if (players == null){
			TryFetchPlayers();
			return;
		}
		float xPos = 0;
		foreach (GameObject player in players)
			xPos += player.transform.position.x;
		xPos /= 2;
		this.transform.position = new Vector3(xPos, this.transform.position.y, this.transform.position.z);
	}	

	void TryFetchPlayers(){
		players = GameObject.FindGameObjectsWithTag("Player");
		if (players.GetLength(0) != 2){
			players = null;
		}
	}
}
