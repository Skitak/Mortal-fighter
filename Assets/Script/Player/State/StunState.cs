using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : PlayerState {

	private int stunLeft = 0;
	private string action; 
	public StunState (BasicPlayer player, int hitstun, string action) : base(player){
		stunLeft = hitstun;
		this.action = action;
	}

	public override void Enter(){
		player.animator.SetBool(action, true);
	}

	public override void Exit(){
		player.animator.SetBool(action, false);
	}

	public override void Update() {
		if (--stunLeft == 0){
			player.ChangeState(new NormalState(player));
		}
	}
}
