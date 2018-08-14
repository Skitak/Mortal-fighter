using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningState : PlayerState {

	public BeginningState (BasicPlayer player) : base(player){}

	public override void Enter(){
		player.slider.maxValue = 100;
		player.slider.value = 100;
	}
}
