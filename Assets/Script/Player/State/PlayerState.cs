using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {

	protected BasicPlayer player;
	protected Tuple<string, string>[] availableActions = {};
	public PlayerState(BasicPlayer player){
		this.player = player;
	}
	public virtual void Update (){
		player.Motion();
		Ability();
	}

	public virtual void Enter(){}

	public virtual void Exit(){}

	public virtual void Hit(int damages, int hitStun, string action){
		player.Damaged(damages, hitStun);
	}

	protected void Ability(){
		Attack();
		Block();
	}

	protected void Attack(){
		foreach (Tuple<string, string> action in availableActions){
			if (Input.GetButtonDown(action.second + " " + player.playerNumber)){
				CMS.Ability abilityInformations =  GetAbilityInformations(action.second);
				abilityInformations.name = action.first;
				player.ChangeState(new HitState(player, abilityInformations));
				return;
			}
		}
	}

	protected virtual void Block(){
		if (Input.GetButton("block " + player.playerNumber) && !player.blockTimer.IsFinished()){
			player.ChangeState(new BlockState(player));
		}
	}

	CMS.Ability GetAbilityInformations(string action){
		CMS.Ability ability;
		player.CmsInfos.abilities.TryGetValue(action, out ability);
		if (ability == null){
			Debug.Log("Action named " + action + " does not exists in cms file of player " + player.playerNumber);
			return new CMS.Ability(action);
		}
		return ability;
	}
}
