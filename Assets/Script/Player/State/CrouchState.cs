using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : PlayerState {

	public CrouchState (BasicPlayer player) : base(player){}

    public override void Update(float delta) {
        player.Motion(delta);
        Attack();
    }

    public void Attack(){
        
		bool isAttacking = player.animator.GetBool("hit");
		if (isAttacking)
			return;
            
		if (Input.GetButtonDown("Heavy normal")){
			player.animator.SetBool("crouch heavy normal", true);
			isAttacking = true;
		}

        if (Input.GetButtonDown("Anti-air")){
			player.animator.SetBool("anti-air", true);
			isAttacking = true;
		}
			
		if (Input.GetButtonDown("Over-head")){
			player.animator.SetBool("over-head", true);
			isAttacking = true;
		}
			
		if (Input.GetButtonDown("Light normal")){
			player.animator.SetBool("crouch light normal", true);
			isAttacking = true;
		}
			
		if (Input.GetButtonDown("Throw")){
			player.animator.SetBool("throw", true);
			isAttacking = true;
		}
		player.animator.SetBool("hit", isAttacking);
    }
}
