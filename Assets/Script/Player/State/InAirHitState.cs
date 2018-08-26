using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirHitState : InAirState {
	InAirInfos inAirInfos;
	CMS.Ability ability;

	public InAirHitState (BasicPlayer player, InAirInfos inAirInfos, CMS.Ability ability)
		: base(player,inAirInfos){
		this.ability = ability;
	}

	public override void Enter(){
		player.animator.SetTrigger(ability.name);
		FetchAbilityInformations();
	}

	private void FetchAbilityInformations(){
		int damages = Mathf.RoundToInt((float) ability.informations["damage"]); 
		int hitStun = Mathf.RoundToInt((float) ability.informations["hit stun"]);
		player.damagingCollider.ChangeHitInformations(damages,hitStun,ability.name);
	}

	public override void Exit(){

	}

	public override void Attack(string action){
	}
}
