using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStunState : PlayerState {

	private int stunLeft = 0;
	public HitStunState (BasicPlayer player, int hitstun) : base(player){
		stunLeft = hitstun;
	}

	public override void Update(float delta) {
		if (--stunLeft == 0){
			player.ChangeState(new NormalState(player));
		}
	}
}
