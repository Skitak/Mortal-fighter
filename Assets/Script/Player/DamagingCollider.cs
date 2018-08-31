using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class DamagingCollider : MonoBehaviour {
	Dictionary<string, int> priorities = new Dictionary<string, int>() {
		{"throw standing", 1},
		{"throw mid-air", 2},
		{"anti-air", 3},
		{"heavy normal standing", 4},
		{"heavy normal crouching", 4},
		{"over-head", 5},
		{"light normal standing", 6},
		{"light normal crouching", 6},
		{"heavy normal mid-air", 7},
		{"light normal mid-air", 8}
	};
	Dictionary<string, string[]> prioritiesExceptions = new Dictionary<string, string[]>(){
		{"anti-air", new string[]{ "heavy normal mid-air", "light normal mid-air"}} 
	};
	string[] canceledMoves = new string[] {
		"throw standing", "throw mimd-air"
	};
	void AddActionPrioritiesExceptions(){
		prioritiesExceptions.Add("anti-air", 
		new string[]{ "heavy normal mid-air", "light normal mid-air"});		
	}

	public BoxCollider boxCollider;
	public BasicPlayer player;
	int damages;
	int hitStun;
	string action;

	private void OnTriggerEnter(Collider other) {
		if (other.transform.IsChildOf(player.transform))
			return;
		if (other.tag == "damagingHitbox")
			other.gameObject.GetComponent<DamagingCollider>().Hit(damages,hitStun, action);	
		else if (other.tag == "Player")
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
		string[] linkedExceptions = prioritiesExceptions[hisAction];
		if (linkedExceptions != null && linkedExceptions.Contains(action))
			return true;
		return false;
	}
	public void ChangeHitInformations(CMS.Ability ability){
		this.damages =  Mathf.RoundToInt((float) ability.informations["damage"]);
		this.hitStun = Mathf.RoundToInt((float) ability.informations["hit stun"]);
		this.action = ability.name;
	}

}
