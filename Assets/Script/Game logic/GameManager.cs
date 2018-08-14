using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	// Use this for initialization
	void Start () {
		setInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setInstance(){
		if (instance != null)
			Debug.Log(" /!\\ Multiple GameManager instances !");
		instance = this;
		DontDestroyOnLoad(this);
	}
}
