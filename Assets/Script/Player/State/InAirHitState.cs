using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirHitState : HitState {

	public InAirHitState (BasicPlayer player, CMS.Ability ability): base(player, ability){}
	protected override void ReturnToPreviousState(){
		player.ChangeState(new InAirState(player));
	}
}
