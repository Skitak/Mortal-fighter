using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMS {

	public class Ability {
		public Dictionary<string, double> informations;
		public Ability(){
			informations = new Dictionary<string, double>();
		}
	}
	private TextAsset file;
	public Dictionary<string, double> movements;
	public Dictionary<string, Ability> abilities;

	public CMS(TextAsset file){
		File = file;
		movements = new Dictionary<string, double>();
		abilities = new Dictionary<string, Ability>();
	}

	public TextAsset File {
		get {
			return file;
		}
		set {
			file = value;
		}
	}
}
