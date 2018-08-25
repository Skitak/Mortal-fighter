using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class DamagingCollider : MonoBehaviour {

	Dictionary<string, int> priorities = new Dictionary<string, int>();
	Dictionary<string, string[]> prioritiesExceptions = new Dictionary<string, string[]>();
	string[] canceledMoves = new string[] {
		"throw standing", "throw mimd-air"
	};

	private void Start() {
		AddActionPriorities();
		AddActionPrioritiesExceptions();	
	}

	void AddActionPriorities(){
		priorities.Add("throw standing", 1);
		priorities.Add("throw mid-air", 2);
		priorities.Add("anti-air", 3);
		priorities.Add("heavy normal standing", 4);
		priorities.Add("heavy normal crouching", 4);
		priorities.Add("over-head", 5);
		priorities.Add("light normal standing", 6);
		priorities.Add("light normal crouching", 6);
		priorities.Add("heavy normal mid-air", 7);
		priorities.Add("light normal mid-air", 8);
	}
	void AddActionPrioritiesExceptions(){
		prioritiesExceptions.Add("anti-air", 
		new string[]{ "heavy normal mid-air", "light normal mid-air"});		
	}
	public BasicPlayer player;
	int damages;
	int hitStun;
	string action;
	private void OnTriggerEnter(Collider other) {
		if (other.transform.IsChildOf(player.transform))
			return;
		if (other.tag == "damagingHitbox")
			other.gameObject.GetComponent<DamagingCollider>().Hit(damages,hitStun, action);	
		else if (other.tag == "blockingHitbox") // should call some other function
			other.gameObject.GetComponent<BasicPlayer>().Hit(damages,hitStun, action);
		else
			other.gameObject.GetComponent<BasicPlayer>().Hit(damages,hitStun, action);

	}

	private void Hit(int damages, int hitStun, string hisAction){
		// If we throw each other, we don't do shit
		if (IsMoveCanceled(hisAction))
			MoveCanceled();
		// Else if he has priority, I take the hit
		else if (HasPriority(hisAction))
			player.Damaged(damages, hitStun);
	}

	private bool IsMoveCanceled(string hisAction){
		return hisAction == action && canceledMoves.Contains(action);
	}

	// Here, sparkles n shit when their actions are canceled
	private void MoveCanceled(){}

	private bool HasPriority (string hisAction){
		int myActionRank, hisActionRank;
		priorities.TryGetValue(this.action, out myActionRank); 
		priorities.TryGetValue(hisAction, out hisActionRank);
		if (HasPriorityWithException(hisAction))
			return true;
		return myActionRank <= hisActionRank;
	}

	private bool HasPriorityWithException(string hisAction){
		string[] linkedExceptions;
		prioritiesExceptions.TryGetValue(hisAction, out linkedExceptions);
		if (linkedExceptions != null && linkedExceptions.Contains(action))
			return true;
		return false;
	}
	public void ChangeHitInformations(int damages, int hitStun, string action){
		this.damages = damages;
		this.hitStun = hitStun;
		this.action = action;
	}
}
