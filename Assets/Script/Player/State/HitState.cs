using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : PlayerState {

	CMS.Ability ability;
	int animationFrames;
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
		double value;
		ability.informations.TryGetValue("damage", out value);
		int damages = Mathf.RoundToInt((float) value); 
		ability.informations.TryGetValue("hit stun", out value);
		int hitStun = Mathf.RoundToInt((float) value);
		ability.informations.TryGetValue("total frame", out value);
		animationFrames = Mathf.RoundToInt((float) value);
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
