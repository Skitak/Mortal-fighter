﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour {

	public GameObject player;
	public int damages;
	public string action;
	private void OnTriggerEnter(Collider other) {
		// Debug.Log("hit");
		if (other.gameObject.tag != "Player" || player == other.gameObject)
			return;
		other.gameObject.GetComponent<BasicPlayer>().Health -= damages;
		Debug.Log("Hit by - " + action + " : " + damages);
	}

}