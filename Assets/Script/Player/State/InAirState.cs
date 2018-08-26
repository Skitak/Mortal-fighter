using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : PlayerState {
	public static float gravity = 30.0f;
	InAirInfos inAirInfos;

	public InAirState (BasicPlayer player, InAirInfos inAirInfos) : base(player){
		// First is the name, second is the input
		availableActions = new Tuple<string,string>[]{
			new Tuple<string, string>("heavy normal mid-air","heavy normal"),
			new Tuple<string, string>("light normal mid-air","light normal"),
			new Tuple<string, string>("throw mid-air","throw")
		};

		this.inAirInfos = inAirInfos;
	}

	public override void Update(){
     	player.controller.Move(Vector3.up * Time.deltaTime);
		inAirInfos.direction.y -= gravity * Time.deltaTime;
		player.controller.Move(inAirInfos.direction* Time.deltaTime);
		if (player.controller.isGrounded)
			player.ChangeState(new NormalState(player));
	}
	public override void Enter(){
	}

	public override void Exit(){

	}
	public override void Damaged(int damages, int hitStun){
		player.Health -= damages;
		if (player.Health > 0)
			player.ChangeState(new FallingState(player, inAirInfos));
	}
}

public struct InAirInfos {
	public Vector3 direction;
	public bool canDash;

	public InAirInfos(Vector3 direction, bool canDash){
		this.canDash = canDash;
		this.direction = direction;
	}
}