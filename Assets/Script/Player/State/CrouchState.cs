using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : NormalState {

	public CrouchState (BasicPlayer player) : base(player){
		// First is the input, second is the animation's name
		availableActions = new Dictionary<string, string>(){
			{"heavy normal", "heavy normal crouching"},
			{"light normal", "light normal crouching"},
			{"throw", "throw standing"},
			{"anti-air", "anti-air"},
			{"over-head", "over-head"},
		};
	}

	public override void Block(){
		if (!player.blockTimer.IsFinished()){
			player.ChangeState(new CrouchBlockState(player));
		}
	}

	public override void Crouch(){}

	public override void Stand(){
		player.animator.SetBool("crouch", false);
		player.ChangeState(new NormalState(player));
	}

	public override void Attack(string action){
		string animation = availableActions[action];
		if (animation == null)
			return;
		CMS.Ability abilityInformations =  GetAbilityInformations(action);
		abilityInformations.name = animation;
		player.ChangeState(new HitCrouchState(player, abilityInformations));
		return;
	}
}
