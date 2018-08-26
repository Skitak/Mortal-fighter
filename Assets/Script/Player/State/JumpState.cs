using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState {
	int frameStartup, frameTotal;
	float apex;
	InAirInfos inAirInfos;
 	public override void Update() {
		if (--frameStartup == 0){
			player.animator.SetBool("grounded", false);
			player.ChangeState(new InAirState(player, inAirInfos));
		}
 	}

	public JumpState (BasicPlayer player) : base(player){
	}

	public override void Enter(){
		FetchCMSInformations();
		float direction = Input.GetAxis("horizontal " + player.playerNumber);
		Vector3 moveDirection = new Vector3(direction * player.speed, apex, 0);
		inAirInfos = new InAirInfos(moveDirection, true);
		player.animator.SetTrigger("jump");
	}

	private void FetchCMSInformations(){
		// Jump height, and other things
		double value;
		player.CmsInfos.movements.TryGetValue("jump height apex", out value);
		// apex = (float) value;
		apex = 11f;
		player.CmsInfos.movements.TryGetValue("jump frames startup", out value);
		frameStartup = Mathf.RoundToInt((float) value);
		player.CmsInfos.movements.TryGetValue("jump frames total", out value);
		frameTotal = Mathf.RoundToInt((float) value);
	}	

	public override void Exit(){

	}
}
