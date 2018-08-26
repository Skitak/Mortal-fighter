using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : PlayerState {

	public NormalState (BasicPlayer player) : base(player){
		// First is the input, second is the animation's name
		availableActions = new Dictionary<string, string>(){
			{"heavy normal", "heavy normal standing"},
			{"light normal", "light normal standing"},
			{"throw", "throw standing"},
			{"anti-air", "anti-air"},
			{"over-head", "over-head"},
		};}

	public override void Enter(){
		player.animator.SetBool("grounded", true);
	}

	public override void Move(float axisValue){
		Vector3 movement = new Vector3(axisValue * Time.deltaTime * player.speed, 0, 0);
		player.controller.Move(movement);
		if (player.isFacingRight)
			player.animator.SetFloat("move", Mathf.Abs(axisValue));
		if (player.ShouldTurn(axisValue)){
			player.controller.transform.Rotate(0f,180f,0f);
			player.isFacingRight = player.isFacingRight ? false : true ;
		}
	}
	public override void Block(){
		if (!player.blockTimer.IsFinished()){
			player.ChangeState(new BlockState(player));
		}
	}
	public override void Crouch(){
		player.animator.SetBool("crouch", true);
		player.ChangeState(new CrouchState(player));
	}

	public override void Jump(float direction){
		player.ChangeState(new JumpState(player, direction));
	}
}
