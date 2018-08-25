using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState {
 	Vector3 moveDirection = Vector3.zero;
	int frameStartup, frameTotal;
	float apex;
	float gravity = 30.0f;
 	public override void Update() {
     	player.controller.Move(Vector3.up * Time.deltaTime);
		moveDirection.y -= gravity * Time.deltaTime;
		player.controller.Move(moveDirection* Time.deltaTime);
		if (player.controller.isGrounded)
			player.ChangeState(new NormalState(player));
 	}

	public JumpState (BasicPlayer player) : base(player){
		// First is the name, second is the input
		availableActions = new Tuple<string,string>[]{
			new Tuple<string, string>("heavy normal mid-air","heavy normal"),
			new Tuple<string, string>("light normal mid-air","light normal"),
			new Tuple<string, string>("throw mid-air","throw")
		};
	}

	public override void Enter(){
		player.animator.SetBool("grounded", false);
		FetchCMSInformations();
		float direction = Input.GetAxis("horizontal " + player.playerNumber);
		moveDirection = new Vector3(direction * player.speed, apex, 0);
	}

	private void FetchCMSInformations(){
		// Jump height, and other things
		double value;
		player.CmsInfos.movements.TryGetValue("jump height apex", out value);
		// apex = (float) value;
		apex = 11f;
		player.CmsInfos.movements.TryGetValue("jump frames startup", out value);
		frameStartup = Mathf.RoundToInt((float) value);
		player.CmsInfos.movements.TryGetValue("jump frames total", out value);
		frameTotal = Mathf.RoundToInt((float) value);
	}

	public override void Exit(){

	}
}
