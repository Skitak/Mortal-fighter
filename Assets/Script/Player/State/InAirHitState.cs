using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirHitState : HitState {

	public InAirHitState (BasicPlayer player, CMS.Ability ability): base(player, ability){}
	public override void Update() {
		if (--animationFrames == 0){
			player.ChangeState(new InAirState(player));
		}
	}

	public override void Block(){
		if (startup <= 0)
			return;
		player.animator.SetTrigger("feint");
		player.ChangeState(new InAirState(player));
	}
}
