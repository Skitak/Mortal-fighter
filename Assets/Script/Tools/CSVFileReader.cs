using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
 
public class CSVFileReader : MonoBehaviour 
{
	public TextAsset csvFile;
	private Dictionary<string, float> csvFileVariables;
	public void Start()
	{
		//string[,] grid = SplitCsvGrid(csvFile.text);
		//Debug.Log("size = " + (1+ grid.GetUpperBound(0)) + "," + (1 + grid.GetUpperBound(1))); 
 
		//DebugOutputGrid(grid);
		 fetchCMSFromFile(csvFile);
	}
 
	// outputs the content of a 2D array, useful for checking the importer
	static public void DebugOutputGrid(string[,] grid)
	{
		string textOutput = ""; 
		for (int y = 0; y < grid.GetUpperBound(1); y++) {	
			for (int x = 0; x < grid.GetUpperBound(0); x++) {
 
				textOutput += grid[x,y]; 
				textOutput += "|"; 
			}
			textOutput += "\n"; 
		}
		Debug.Log(textOutput);
	}
 
	// splits a CSV file into a 2D string array
	static public string[,] SplitCsvGrid(string csvText)
	{
		string[] lines = csvText.Split("\n"[0]); 
 
		// finds the max width of row
		int width = 0; 
		for (int i = 0; i < lines.Length; i++)
		{
			string[] row = SplitCsvLine( lines[i] ); 
			width = Mathf.Max(width, row.Length); 
		}
 
		// creates new 2D string grid to output to
		string[,] outputGrid = new string[width + 1, lines.Length + 1]; 
		for (int y = 0; y < lines.Length; y++)
		{
			string[] row = SplitCsvLine( lines[y] ); 
			for (int x = 0; x < row.Length; x++) 
			{
				outputGrid[x,y] = row[x]; 
				
				// This line was to replace "" with " in my output. 
				// Include or edit it as you wish.
				outputGrid[x,y] = outputGrid[x,y].Replace("\"\"", "\"");
			}
		}
 
		return outputGrid; 
	}
 
	// splits a CSV row 
	static public string[] SplitCsvLine(string line)
	{
		return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
		@"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)", 
		System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
		select m.Groups[1].Value).ToArray();
	}

	static public CMS fetchCMSFromFile(TextAsset csvFile){
		CMS cms = new CMS(csvFile);
		fetchCMSMovements(cms);
		fetchCMVAbilities(cms);
		return cms;
	}

	private static void fetchCMSMovements(CMS cms){
		string[] lines = cms.File.text.Split("\n"[0]);
		string[] ignoredLines = {"Jump Frames Total","Dash Forward Total Frames" };
		Debug.Log("Récupération des variables de mouvement --------------------------------");
		for (int i = 1; i <= 12; ++i)
		{
			string[] row = SplitCsvLine( lines[i] );
			if (!ignoredLines.Contains(row[0])){
				cms.movements.Add(row[0],double.Parse(row[1]));
				Debug.Log(row[0] + " : " + row[1]); 
			}
		}
	}

	private static void fetchCMVAbilities(CMS cms){

		string[] lines = cms.File.text.Split("\n"[0]);
		string[] ignoredAbilities = {"Total Frame","Input", "Hit Level" };
		string[] row = SplitCsvLine( lines[14] );
		string[] abilitiesInfos = new string[row.Length - 1];
		Debug.Log("Récupération des variables de combat ----------------------------");
		for (int i = 1; i < row.Length - 1; ++i) {
				abilitiesInfos[i] = row[i];
				//Debug.Log(abilitiesInfos[i]);
		}
		for (int i = 15; i < lines.Length; ++i) {
			row = SplitCsvLine( lines[i] );
			CMS.Ability ability = new CMS.Ability();
			for (int j = 1; j < row.Length - 1; ++j) {
				if (!ignoredAbilities.Contains(abilitiesInfos[j])){
					ability.informations.Add(abilitiesInfos[j], double.Parse(row[j]));
				}
			}
			cms.ablilities.Add(row[0], ability);
			Debug.Log(ability.informations.ToString());
		}
	}


}