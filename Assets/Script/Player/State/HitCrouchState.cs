using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCrouchState : HitState {
	public HitCrouchState (BasicPlayer player, CMS.Ability ability) : base(player, ability){}

	protected override void ReturnToPreviousState(){
		player.ChangeState(new CrouchState(player));
	}
}
