using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class BlockState : PlayerState {
	private string[] ignoredActions = {
		"light normal",
		"heavy normal",
		"anti-air",
		"over-head"
		};
	public BlockState (BasicPlayer player) : base(player){}

	public override void Update (float delta){
		move();
		abilities();
	}

	public override void Enter(){
		player.animator.SetBool("block", true);
		player.freeBlockTimer.Play();
		player.blockTimer.Pause();
		player.blockBufferTimer.Reset();

	}

	public override void Exit(){
		player.animator.SetBool("block", false);
		player.freeBlockTimer.Reset();
		player.blockTimer.Pause();
		player.blockBufferTimer.Play();
	}

	private void move(){
		// If crouching Then change state to crouchblock
	}

	private void abilities(){
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
