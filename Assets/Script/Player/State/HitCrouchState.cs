using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCrouchState : HitState {
	public HitCrouchState (BasicPlayer player, CMS.Ability ability) : base(player, ability){}

	public override void Update() {
		if (--animationFrames == 0){
			player.ChangeState(new CrouchState(player));
		}
	}
}
