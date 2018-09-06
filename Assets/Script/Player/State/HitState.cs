using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : PlayerState {

	CMS.Ability ability;
	protected int startup, active, recovery, blockStun;
	public HitState (BasicPlayer player, CMS.Ability ability) : base(player){
		this.ability = ability;
	}

	public override void Enter(){
		player.damagingCollider.boxCollider.enabled = false;
		player.animator.SetTrigger(ability.name);
		player.damagingCollider.ChangeHitInformations(ability);
		FetchAbilityInformations();
	}
	
	private void FetchAbilityInformations(){
		startup = Mathf.RoundToInt((float) ability.informations["startup"]);
		active = Mathf.RoundToInt((float) ability.informations["active"]);
		recovery = Mathf.RoundToInt((float) ability.informations["recovery"]);
		blockStun = Mathf.RoundToInt((float) ability.informations["block stun"]) ;
	}

	public override void Exit(){

	}
	public override void Update() {
		if (--startup == 0)
			player.damagingCollider.boxCollider.enabled = true;
		else if (startup < 0 && --active == 0)
			player.damagingCollider.boxCollider.enabled = false;
		else if (active < 0 && --recovery == 0){
			ReturnToPreviousState();
		}
	}
	//Is used in inherited classes which doesn't change state to normal
	protected virtual void ReturnToPreviousState(){
		player.ChangeState(new NormalState(player));
	}
	public override void Block(){
		if (startup <= 0)
			return;
		player.animator.SetTrigger("feint");
		ReturnToPreviousState();
	}

	public override void Blocked(){
		player.ChangeState(new StunState(player, blockStun, "blocked"));
	}
}
