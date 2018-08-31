using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState {
	public InAirState (BasicPlayer player) : base(player){
		// First is the input, second is the name
		availableActions = new Dictionary<string, string>(){
			{"heavy normal", "heavy normal mid-air"},
			{"light normal", "light normal mid-air"},
			{"throw", "throw mid-air"}
		};
	}

	public override void Update(){
		if (player.inAirMobility.isGrounded){
			player.ChangeState(new NormalState(player));
			player.inAirMobility.enabled = false;
		}
	}
	public override void Enter(){
	}

	public override void Exit(){
	}
	public override void Damaged(int damages, int hitStun){
		player.Health -= damages;
		if (player.Health > 0)
			player.ChangeState(new FallingState(player));
	}

	public override void Attack(string action){
		string animation = availableActions[action];
		if (animation == null)
			return;
		CMS.Ability abilityInformations =  GetAbilityInformations(action);
		abilityInformations.name = animation;
		player.ChangeState(new InAirHitState(player, abilityInformations));
		return;
	}

	public override void Jump(Vector3 direction){
		Dash(direction);
	}
	public override void Dash(Vector3 direction){
		if (player.inAirMobility.canDash)
			player.ChangeState(new DashState(player, direction));
	}
}