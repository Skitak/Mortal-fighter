using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	private MenuScreen actualMenu;
	public MenuScreen initialMenu;
	public MenuScreen exitMenu;
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
		if (exitMenu.gameObject.activeSelf)
			exitMenu.gameObject.SetActive(false);
		else swapToMenu(actualMenu.previousMenu);
	}

	void Update() {
		if (Input.GetButtonDown("Cancel"))
			goToPreviousMenu();	
	}

	public void exitGame(){
		Application.Quit();
	}

	// Will be changed for a pop-up menu api
	public void showExitMenu(){
		exitMenu.gameObject.SetActive(true);
	}
	
	public void playGame(){
		SceneManager.LoadScene(1);
	}
}
