using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState {

	public JumpState (BasicPlayer player) : base(player){}

    public override void Update(float delta) {
        player.Motion(delta);
        Attack();
    }

    public void Attack(){
        
		bool isAttacking = player.animator.GetBool("hit");
		if (isAttacking)
			return;
		if (Input.GetButtonDown("Heavy normal")){
			player.animator.SetBool("aerial heavy normal", true);
			isAttacking = true;
		}
			
		if (Input.GetButtonDown("Light normal")){
			player.animator.SetBool("aerial light normal", true);
			isAttacking = true;
		}
			
		if (Input.GetButtonDown("Throw")){
			player.animator.SetBool("aerial throw", true);
			isAttacking = true;
		}
		player.animator.SetBool("hit", isAttacking);
    }
}
