using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState {
	public static float gravity = 30.0f;
	InAirInfos inAirInfos;

	public InAirState (BasicPlayer player, InAirInfos inAirInfos) : base(player){
		// First is the name, second is the input
		availableActions = new Dictionary<string, string>(){
			{"heavy normal", "heavy normal mid-air"},
			{"light normal", "light normal mid-air"},
			{"throw", "throw mid-air"}
		};

		this.inAirInfos = inAirInfos;
	}

	public override void Update(){
     	player.controller.Move(Vector3.up * Time.deltaTime);
		inAirInfos.direction.y -= gravity * Time.deltaTime;
		player.controller.Move(inAirInfos.direction* Time.deltaTime);
		if (player.controller.isGrounded)
			player.ChangeState(new NormalState(player));
	}
	public override void Enter(){
	}

	public override void Exit(){

	}
	public override void Damaged(int damages, int hitStun){
		player.Health -= damages;
		if (player.Health > 0)
			player.ChangeState(new FallingState(player, inAirInfos));
	}

	public override void Attack(string action){
		string animation = availableActions[action];
		if (animation == null)
			return;
		CMS.Ability abilityInformations =  GetAbilityInformations(action);
		abilityInformations.name = animation;
		player.ChangeState(new InAirHitState(player, inAirInfos, abilityInformations));
		return;
	}
}

public struct InAirInfos {
	public Vector3 direction;
	public bool canDash;

	public InAirInfos(Vector3 direction, bool canDash){
		this.canDash = canDash;
		this.direction = direction;
	}
	
}