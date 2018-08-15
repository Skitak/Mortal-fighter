using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : PlayerState {

	// Use this for initialization
	public HitState (BasicPlayer player) : base(player){}

	public override void Enter(){
		player.animator.SetTrigger("hit");
	}

	public override void Exit(){
	}
	public override void Update(float delta) {
		
	}
}
