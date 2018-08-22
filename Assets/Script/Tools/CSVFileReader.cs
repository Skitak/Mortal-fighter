using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
 
public class CSVFileReader : MonoBehaviour 
{
	public TextAsset csvFile;
	private Dictionary<string, float> csvFileVariables;
 
	// splits a CSV row 
	static public string[] SplitCsvLine(string line)
	{
		return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
		@"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)", 
		System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
		select m.Groups[1].Value).ToArray();
	}

	// Primary function, is used to initialize the cms
	static public CMS fetchCMSFromFile(TextAsset csvFile){
		CMS cms = new CMS(csvFile);
		fetchCMSMovements(cms);
		fetchCMVAbilities(cms);
		return cms;
	}

	private static void fetchCMSMovements(CMS cms){
		string[] lines = cms.File.text.Split("\n"[0]);
		string[] ignoredLines = {"jump frames total","dash forward total frames" };
		Debug.Log("Récupération des variables de mouvement --------------------------------");
		for (int i = 1; i <= 12; ++i)
		{
			string[] row = SplitCsvLine( lines[i].ToLower() );
			if (!ignoredLines.Contains(row[0])){
				cms.movements.Add(row[0],double.Parse(row[1]));
				Debug.Log(row[0] + " : " + row[1]); 
			}
		}
	}

	private static void fetchCMVAbilities(CMS cms){

		string[] lines = cms.File.text.Split("\n"[0]);
		string[] ignoredAbilities = {"total frame","input", "hit level" };
		string[] row = SplitCsvLine( lines[14] );
		string[] abilitiesInfos = new string[row.Length - 1];
		Debug.Log("Récupération des variables de combat ----------------------------");
		for (int i = 1; i < row.Length - 1; ++i) {
				abilitiesInfos[i] = row[i].ToLower();
				Debug.Log(abilitiesInfos[i]);
		}
		for (int i = 15; i < lines.Length; ++i) {
			row = SplitCsvLine( lines[i].ToLower() );
			CMS.Ability ability = new CMS.Ability();
			for (int j = 1; j < row.Length - 1; ++j) {
				if (!ignoredAbilities.Contains(abilitiesInfos[j])){
					ability.informations.Add(abilitiesInfos[j], double.Parse(row[j]));
				}
			}
			cms.abilities.Add(row[0], ability);
			Debug.Log(ability.informations.ToString());
		}

		for (int i = 0; i < cms.abilities.Count; ++i){
			Debug.Log(cms.abilities.Keys.ElementAt(i));
			// Debug.Log(cms.abilities[cms.abilities.Keys.ElementAt(i)]);
		}
	}


}