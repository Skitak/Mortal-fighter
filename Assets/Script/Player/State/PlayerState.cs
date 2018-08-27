using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PlayerState {

	protected BasicPlayer player;
	protected Dictionary<string,string> availableActions;
	public PlayerState(BasicPlayer player){
		this.player = player;
	}
	public virtual void Update (){
	}

	public virtual void Enter(){}

	public virtual void Exit(){}

	public virtual void Hit(int damages, int hitStun, string action){
		Damaged(damages, hitStun);
	}

	public virtual void Damaged(int damages, int hitStun){
		player.Health -= damages;
		if (player.Health > 0)
			player.ChangeState(new StunState(player, hitStun));
	}

	public virtual void Attack(string action){
		string animation;
		try{
			animation = availableActions[action];
		}
		catch (NullReferenceException e){

			return;
		}
		CMS.Ability abilityInformations =  GetAbilityInformations(action);
		abilityInformations.name = animation;
		player.ChangeState(new HitState(player, abilityInformations));
		return;
	}


	protected CMS.Ability GetAbilityInformations(string action){
		CMS.Ability ability;
		player.CmsInfos.abilities.TryGetValue(action, out ability);
		if (ability == null){
			Debug.Log("Action named " + action + " does not exists in cms file.");
			return new CMS.Ability(action);
		}
		return ability;
	}

	public virtual void Move(float axisValue){
	}

	public virtual void Crouch(){
	}

	public virtual void Jump(Vector3 direction){}

	public virtual void Stand(){}
	public virtual void Block(){}
	public virtual void Unblock(){}
	public virtual void Dash(Vector3 direction){}
}
