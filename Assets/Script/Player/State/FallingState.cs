using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState {
	int groundedHitStunTime = 15;
	bool isGrounded = false;
 	public override void Update(){
		if (player.controller.isGrounded && --groundedHitStunTime == 0)
			player.ChangeState(new NormalState(player));
	}

	public FallingState (BasicPlayer player) : base(player){
	}

	public override void Enter(){
		float otherXPos = player.other.transform.position.x;
		float playerXPos = player.transform.position.x;
		float xDirection = otherXPos > playerXPos ? -1 : 1;
		player.inAirMobility.direction.x = xDirection;
		player.animator.SetBool("damaged", true);
	}

	public override void Exit(){
		player.animator.SetBool("damaged", false);

	}
}
