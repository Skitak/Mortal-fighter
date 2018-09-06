using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public BasicPlayer player;
	public Text text;
	private bool isUsingController;
	public int controllerNumber = 1;
	string horizontal, vertical, block, feint, jump;
	string[] keyboardActions = {
		"heavy normal",
		"light normal",
		"throw",
		"anti-air",
		"over-head"
	};
	Dictionary<string, string[]> keyboardCombinations = new Dictionary<string, string[]>() {
	};
	string[] controllerActions = {
		"heavy normal",
		"light normal"
	};
	Dictionary<string, string[]> controllerCombinations = new Dictionary<string, string[]>() {
		{"throw", new string[]{"light normal","jump"}},
		{"anti-air", new string[]{"jump", "block"}},
		{"over-head", new string[]{"block", "heavy normal"}}
	};
	
	void Start() {
		horizontal = "horizontal " + controllerNumber;
		vertical = "vertical " + controllerNumber;
		block = "block " + controllerNumber;
		feint = "feint " + controllerNumber;
		jump = "jump " + controllerNumber;
		isUsingController = Input.GetJoystickNames().Length >= controllerNumber;

	}
	// Update is called once per frame
	void Update () {
		if (isUsingController){
			ControllerAction();
			ControllerMotion();
		}
		else{
			KeyboardAction();
			KeyboardMotion();
		}
			
	}
	void ControllerMotion (){
		float xAxisValue = Input.GetAxis(horizontal);
		float yAxisValue = Input.GetAxis(vertical);
		if (xAxisValue != 0)
			player.Move(xAxisValue);
		else
			player.animator.SetFloat("move", 0f);
		if (Input.GetButtonDown(jump))
			player.Jump(new Vector3(xAxisValue, yAxisValue));
		else if (yAxisValue < 0)
			player.Crouch();
		else 
			player.Stand();
	}
	void ControllerAction(){
		foreach(KeyValuePair<string, string[]> entry in controllerCombinations){
			if (Input.GetButtonDown(entry.Value[0] + " " + controllerNumber) && 
					Input.GetButtonDown(entry.Value[1] + " " + controllerNumber)){
				player.Attack(entry.Key);
				DebugAttack(entry.Key);
				return;
			}
		}
		foreach (string action in controllerActions){
			if (Input.GetButtonDown(action + " " + controllerNumber)){
				player.Attack(action);
				DebugAttack(action);
				return;
			}
		}
		if (Input.GetButtonDown(block))
			player.Block();
		else if (Input.GetButtonUp(block))
			player.Unblock();
	}

	void KeyboardMotion (){
		float xAxisValue = Input.GetAxis(horizontal);
		float yAxisValue = Input.GetAxis(vertical);
		if (xAxisValue != 0)
			player.Move(xAxisValue);
		else
			player.animator.SetFloat("move", 0f);
		if (Input.GetButtonDown(vertical))
			player.Jump(new Vector3(xAxisValue, yAxisValue));
		else if (yAxisValue < 0)
			player.Crouch();
		else 
			player.Stand();
	}

	void KeyboardAction(){
		// Not usefull yet
		// foreach(KeyValuePair<string, string[]> entry in keyboardCombinations){
		// 	if (Input.GetButtonDown(entry.Value[0] + " " + controllerNumber) && 
		// 			Input.GetButtonDown(entry.Value[1] + " " + controllerNumber)){
		// 		player.Attack(entry.Key);
		// 		return;
		// 	}
		// }
		foreach (string action in keyboardActions){
			if (Input.GetButtonDown(action + " " + controllerNumber)){
				player.Attack(action);
				DebugAttack(action);
				return;
			}
		}
		if (Input.GetButtonDown(block))
			player.Block();
		else if (Input.GetButtonUp(block))
			player.Unblock();
	}

	void DebugAttack(string attack){
		text.text = attack;
	}
}
