using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState {
	InAirInfos inAirInfos;
	int groundedHitStunTime = 15;
	bool isGrounded = false;
 	public override void Update(){
		if (isGrounded && --groundedHitStunTime == 0){
			player.ChangeState(new NormalState(player));
		}
		else {
			player.controller.Move(Vector3.up * Time.deltaTime);
			inAirInfos.direction.y -= InAirState.gravity * Time.deltaTime;
			player.controller.Move(inAirInfos.direction* Time.deltaTime);
			if (player.controller.isGrounded && !isGrounded){
				isGrounded = true;
			}
		}
	}

	public FallingState (BasicPlayer player, InAirInfos inAirInfos) : base(player){
		this.inAirInfos = inAirInfos;
	}

	public override void Enter(){
		float otherXPos = player.other.transform.position.x;
		float playerXPos = player.transform.position.x;
		float xDirection = otherXPos > playerXPos ? -1 : 1;
		inAirInfos.direction.x = xDirection;
		player.animator.SetBool("damaged", true);
	}

	public override void Exit(){
		player.animator.SetBool("damaged", false);

	}
}
