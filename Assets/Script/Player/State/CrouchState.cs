using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : PlayerState {

	public CrouchState (BasicPlayer player) : base(player){
		// First is the name, second is the input
		availableActions = new Tuple<string,string>[]{
			new Tuple<string, string>("heavy normal crouching","heavy normal"),
			new Tuple<string, string>("light normal crouching","light normal"),
			new Tuple<string, string>("throw standing","throw"),
			new Tuple<string, string>("anti-air","anti-air"),
			new Tuple<string, string>("over-head","over-head"),
		};
	}

	protected override void Block(){
		if (Input.GetButton("block " + player.playerNumber) && !player.blockTimer.IsFinished()){
			player.ChangeState(new CrouchBlockState(player));
		}
	}
}
