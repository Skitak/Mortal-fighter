using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	private MenuScreen actualMenu;
	public MenuScreen initialMenu;
	private void Start() {
		actualMenu = initialMenu;
	}
	// Menus will come with a state pattern like interface :
	// onMenuOpen and onMenuClose functions
	public void swapToMenu(MenuScreen newMenu){
		actualMenu.gameObject.SetActive(false);
		newMenu.gameObject.SetActive(true);
		actualMenu = newMenu;
	}

	public void goToPreviousMenu(){
		swapToMenu(actualMenu.previousMenu);
	}

	void Update() {
		if (Input.GetButtonDown("return"))
			goToPreviousMenu();	
	}
}
