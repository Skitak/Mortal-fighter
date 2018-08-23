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
		bool hit = false;
		string[] animations = new string[]{"heavy normal", "light normal", "anti-air", "over-head", "throw"};
		foreach (string animation in animations){
			if (Input.GetButtonDown(animation)){
				player.animator.SetTrigger(animation);
				hit = true;
			}
		}
		if (hit){
			player.ChangeState(new HitState(player));
			// player.damagingCollider.damages = 10;
		}
    }
}
