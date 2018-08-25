using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : PlayerState {

	private int stunLeft = 0;
	public HitStunState (BasicPlayer player, int hitstun) : base(player){
		stunLeft = hitstun;
	}

	public override void Enter(){
		player.animator.SetBool("damaged", true);
	}

	public override void Exit(){
		player.animator.SetBool("damaged", false);
	}

	public override void Update() {
		if (--stunLeft == 0){
			player.ChangeState(new NormalState(player));
		}
	}
}
