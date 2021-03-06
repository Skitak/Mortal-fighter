﻿using System.Collections;
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
		if (isAlreadyBlocking)
			return;
		player.animator.SetBool("block", false);
		SetExitTimers();
	}

	public override void Stand(){
		isAlreadyBlocking = true;
		player.animator.SetBool("crouch", false);
		player.ChangeState(new BlockState(player));
	}

	public override void Unblock(){
		player.ChangeState(new CrouchState(player));
	}
}
