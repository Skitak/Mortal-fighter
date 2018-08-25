using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : PlayerState {

	public NormalState (BasicPlayer player) : base(player){}

	public override void Enter(){
		// First is the name, second is the input
		availableActions = new Tuple<string,string>[]{
			new Tuple<string, string>("heavy normal standing","heavy normal"),
			new Tuple<string, string>("light normal standing","light normal"),
			new Tuple<string, string>("throw standing","throw"),
			new Tuple<string, string>("anti-air","anti-air"),
			new Tuple<string, string>("over-head","over-head"),
		};
	}
}
