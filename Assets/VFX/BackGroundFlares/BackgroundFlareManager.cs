using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlareManager : MonoBehaviour {

public ParticleSystem BackgroundFlareSystem;
public int ParticlesViolence;



void Update ()
{
	Violence ();
}

void Violence ()
{
var main = BackgroundFlareSystem.main;

	//max Particules Number
	if (main.maxParticles < 100)
		main.maxParticles = (ParticlesViolence * 2);

	//Particles Spawn Number
	if (ParticlesViolence > 0)
		BackgroundFlareSystem.emissionRate = Mathf.RoundToInt( Mathf.Log (ParticlesViolence));
}


}
