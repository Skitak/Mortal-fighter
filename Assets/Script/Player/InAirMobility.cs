using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirMobility : MonoBehaviour {
	public float gravity = 30.0f;
	BasicPlayer player;
	[HideInInspector]
	public Vector3 direction;
	[HideInInspector]
	public bool canDash;
	[HideInInspector]
	public bool useGravity = true;
	[HideInInspector]
	public bool isGrounded = false;


	void Start() {	
		this.player = this.GetComponent<BasicPlayer>();
	}

	void Update(){
		if (player == null)
			return;
     	player.controller.Move(Vector3.up * Time.deltaTime);
		if (useGravity)
			direction.y -= gravity * Time.deltaTime;
		player.controller.Move(direction* Time.deltaTime);
		isGrounded = player.controller.isGrounded;
	}

	public void SetDirection(Vector3 direction){
		this.direction = direction;
		isGrounded = false;
	}

}