using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour {


	public GameObject startMenu;
	private GameObject actualMenu, previousMenu;
	Rigidbody rb;

	void Start() {
		actualMenu = startMenu;	
	}

	public void swapToMenu(GameObject newMenu){
	}
}
