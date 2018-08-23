using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour {

	public GameObject player;
	private CMS.Ability abilityInformations;
	public CMS.Ability AbilityInformations{
		get { return abilityInformations;}
		set {
			abilityInformations = value;
			FetchAbilityInformations(); 
		}
	}

	private void FetchAbilityInformations(){
		double value;
		abilityInformations.informations.TryGetValue("damage",out value);
		damages = Mathf.RoundToInt((float) value); 
		abilityInformations.informations.TryGetValue("hit stun",out value);
		hitStun = Mathf.RoundToInt((float) value);
	}
	int damages;
	int hitStun;
	public string action;
	private void OnTriggerEnter(Collider other) {
		// Debug.Log("hit");
		if (other.gameObject.tag != "Player" || player == other.gameObject)
			return;
		other.gameObject.GetComponent<BasicPlayer>().Hit(damages,hitStun, action);
		Debug.Log("Hit by - " + action + " : " + damages);
	}

}
