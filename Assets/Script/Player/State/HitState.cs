﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : PlayerState {

	CMS.Ability ability;
	protected int animationFrames;
	// Use this for initialization
	public HitState (BasicPlayer player, CMS.Ability ability) : base(player){
		this.ability = ability;
	}

	public override void Enter(){
		player.animator.SetTrigger(ability.name);
		FetchAbilityInformations();
		// animationFrames
	}
	
	private void FetchAbilityInformations(){
		int damages = Mathf.RoundToInt((float) ability.informations["damage"]); 
		int hitStun = Mathf.RoundToInt((float) ability.informations["hit stun"]);
		animationFrames = Mathf.RoundToInt((float) ability.informations["total frame"]);
		player.damagingCollider.ChangeHitInformations(damages,hitStun,ability.name);
	}

	public override void Exit(){

	}
	public override void Update() {
		if (--animationFrames == 0){
			player.ChangeState(new NormalState(player));
		}
	}
}
