using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class BlockState : PlayerState {
	protected string[] ignoredActions;
	
	//Is used for the transition between blockstate and crouchblockstate
	protected static bool isAlreadyBlocking = false;
	public BlockState (BasicPlayer player) : base(player){
		ignoredActions = new string[] {
		"light normal standing",
		"heavy normal standing",
		"light normal mid-air",
		"heavy normal mid-air",
		"anti-air",
		"over-head"
		};
	}

	public override void Update (){
		Move();
		Abilities();
	}

	public override void Enter(){
		if (isAlreadyBlocking){
			isAlreadyBlocking = false;
			return;
		}
		player.animator.SetBool("block", true);
		SetEntryTimers();
	}

	protected void SetEntryTimers(){
		player.freeBlockTimer.Play();
		if (!player.blockTimer.IsFinished())
			player.blockTimer.Pause();
		player.blockBufferTimer.Reset();
	}

	public override void Exit(){
		if (isAlreadyBlocking)
			return;
		player.animator.SetBool("block", false);
		SetExitTimers();
	}

	protected void SetExitTimers(){
		player.freeBlockTimer.Reset();
		if (!player.blockTimer.IsFinished())
			player.blockTimer.Pause();
		player.blockBufferTimer.Play();
	}
	protected virtual void Move(){
		float Yaxis = Input.GetAxis("vertical " + player.playerNumber);
		if (Yaxis < 0){
			isAlreadyBlocking = true;
			player.ChangeState(new CrouchBlockState(player));
		}
	}

	protected virtual void Abilities(){
		if (Input.GetButtonUp("block " + player.playerNumber))
			player.ChangeState(new NormalState(player));
	}

	public override void Hit(int damages, int hitStun, string action){
		if (player.IsEnemyBehind() ||  !ignoredActions.Contains(action)){
			player.Damaged(damages, hitStun);
		}
		else{
			Debug.Log("Blocked mathafaka");
		}
	}
}
