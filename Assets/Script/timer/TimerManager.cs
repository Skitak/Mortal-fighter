using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
	private static ArrayList timers = new ArrayList (); 

	void Update () {
		foreach (Timer t in timers){
			t.Update(Time.deltaTime);
		}
	}
		
	public static void SetupTimer(Timer t){
		timers.Add (t);
	}
}