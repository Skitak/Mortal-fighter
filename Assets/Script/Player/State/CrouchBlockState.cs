using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchBlockState : BlockState {

	public CrouchBlockState (BasicPlayer player) : base(player){
		this.ignoredActions = new string[] {
		"light normal standing",
		"heavy normal standing",
		"light normal crouch",
		"heavy normal crouch",
		"anti-air"
		};
	}
	public override void Enter(){
		player.animator.SetBool("crouch", true);
		if (isAlreadyBlocking){
			isAlreadyBlocking = false;
			return;
		}
		player.animator.SetBool("block", true);
		SetEntryTimers();
	}

	public override void Exit(){
		player.animator.SetBool("crouch", false);
		if (isAlreadyBlocking)
			return;
		player.animator.SetBool("block", false);
		SetExitTimers();
	}
	

	protected override void Move(){
		float Yaxis = Input.GetAxis("vertical " + player.playerNumber);
		if (Yaxis >= 0){
			isAlreadyBlocking = true;
			player.ChangeState(new BlockState(player));
		}
	}

	protected override void Abilities(){
		if (Input.GetButtonUp("block " + player.playerNumber))
			player.ChangeState(new CrouchState(player));
	}
}
