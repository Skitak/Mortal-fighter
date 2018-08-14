using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState {

	protected BasicPlayer player;
	public PlayerState(BasicPlayer player){
		this.player = player;
	}
	public virtual void Update (float delta){
		player.Motion(delta);
	}

	public virtual void Enter(){}

	public virtual void Exit(){}

	public virtual void Hit(int damages, float hitStun){
		player.Health -= damages;
		player.ChangeState(new HitStunState(player));
	}
}
