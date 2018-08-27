using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState {
	int frameStartup, frameTotal;
	float apex;
	Vector3 direction;
	InAirInfos inAirInfos;
 	public override void Update() {
		if (--frameStartup == 0){
			player.animator.SetBool("grounded", false);
			player.ChangeState(new InAirState(player, inAirInfos));
		}
 	}

	public JumpState (BasicPlayer player, Vector3 direction) : base(player){
		this.direction = direction;
	}

	public override void Enter(){
		FetchCMSInformations();
		Vector3 moveDirection = new Vector3(direction.x * player.speed, apex, 0);
		inAirInfos = new InAirInfos(moveDirection, true);
		player.animator.SetTrigger("jump");
	}

	private void FetchCMSInformations(){
		// Jump height, and other things
		apex = (float) player.CmsInfos.movements["jump height apex"];
		 Debug.Log(apex);
		frameStartup = Mathf.RoundToInt((float) player.CmsInfos.movements["jump frames startup"]);
		 Debug.Log(frameStartup);
		frameTotal = Mathf.RoundToInt((float) player.CmsInfos.movements["jump frames total"]);
		 Debug.Log(frameTotal);
	}	

	public override void Exit(){

	}
}
