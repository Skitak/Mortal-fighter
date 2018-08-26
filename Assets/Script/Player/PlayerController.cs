using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public BasicPlayer player;
	public int controllerNumber = 1;
	string horizontal, vertical, block;
	string[] actions = {
		"heavy normal",
		"light normal",
		"throw",
		"anti-air",
		"over-head"
	};
	
	void Start() {
		horizontal = "horizontal " + controllerNumber;
		vertical = "vertical " + controllerNumber;
		block = "block " + controllerNumber;
	}
	// Update is called once per frame
	void Update () {
		Motion();
		Action();
	}

	void Motion (){
		float xAxisValue = Input.GetAxis(horizontal);
		float yAxisValue = Input.GetAxis(vertical);
		if (xAxisValue != 0)
			player.Move(xAxisValue);
		else
			player.animator.SetFloat("move", 0f);
		if (yAxisValue > 0)
			player.Jump(xAxisValue);
		else if (yAxisValue < 0)
			player.Crouch();
		else 
			player.Stand();
	}

	void Action(){
		foreach (string action in actions){
			if (Input.GetButtonDown(action + " " + controllerNumber)){
				player.Attack(action);
				return;
			}
		}
		if (Input.GetButtonDown(block))
			player.Block();
		else if (Input.GetButtonUp(block))
			player.Unblock();
	}
}
