using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState {
	InAirInfos inAirInfos;
	Vector3 direction;
	bool isGrounded = false;
	bool isDashing = false;
	int startupFrames;
	int totalFrames;
	int recoverFrames = 0;
 	public override void Update(){
		if (--startupFrames == 0){
			inAirInfos.direction = direction * player.speed;
			isDashing = true;
		}
		else if (startupFrames < 0 && --totalFrames == 0)
			isDashing = false;
		else if (totalFrames < 0 && recoverFrames-- == 0)
			player.ChangeState(new InAirState(player, inAirInfos));
		if (player.controller.isGrounded)
			isGrounded = true;
     	fly();
	}

	public DashState (BasicPlayer player, Vector3 direction) : base(player){
		this.direction = direction;
		inAirInfos.canDash = false;
		inAirInfos.direction = Vector3.zero;
	}
	public DashState (BasicPlayer player, Vector3 direction, InAirInfos inAirInfos) : base(player){
		this.direction = direction;
		this.inAirInfos = inAirInfos;
		this.inAirInfos.canDash = false;
	}
	void fly(){
		player.controller.Move(Vector3.up * Time.deltaTime);
		inAirInfos.direction.y -= isDashing ? 0 : InAirState.gravity * Time.deltaTime;
		player.controller.Move(inAirInfos.direction* Time.deltaTime);
	}

	public override void Enter(){
		FetchCMSInformations(GetDashDirection());
		player.animator.SetBool("dash", true);
	}

	public override void Exit(){
		player.animator.SetBool("dash", false);		
	}

	string GetDashDirection(){
		float xDirection = direction.x;
		float xPosition = player.transform.position.x;
		float xOtherPosition = player.other.transform.position.x - xPosition;
		if ((xOtherPosition < 0 && xDirection > 0) || (xOtherPosition > 0 && xDirection < 0) )
			return "backward";
		else
			return "forward";
	}
	void FetchCMSInformations(string direction){
		totalFrames = Mathf.RoundToInt((float) 
			player.CmsInfos.movements["dash "+ direction +" total frames"]);
		startupFrames = Mathf.RoundToInt((float)
			player.CmsInfos.movements["jump frames startup"]);
		if (direction == "backwards"){
			recoverFrames = Mathf.RoundToInt((float)
				player.CmsInfos.movements["dash "+ direction + "recovery"]);
		}
		totalFrames -= startupFrames + recoverFrames;
	}
}
