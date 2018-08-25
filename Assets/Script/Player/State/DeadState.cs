using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : PlayerState {

	// Use this for initialization
	public DeadState (BasicPlayer player) : base(player){}

	public override void Enter(){
		player.animator.SetBool("dead", true);
		player.animator.SetTrigger("die");
	}

	public override void Exit() {
		Debug.Log("not ded");
	}
	public override void Update() {
		// not used because we override the basic behavior
		// which is trying to attack and move
	}
	public override void Hit(int damages, int hitStun, string action){
		// not used because we override the basic behavior
		// which is lowering the health of the player
	}
}
