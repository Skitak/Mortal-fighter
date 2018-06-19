using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MenuScreen {
	public MenuScreen selectionMenu;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey){
			GameManager.instance.gameObject.GetComponent<MenuManager>()
			.swapToMenu(selectionMenu);
		}
	}
}
