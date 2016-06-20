using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class PrefsHelper : MonoBehaviour
{

	public const string PREFS_KEY = "scores";
	public const char DELIMITER = '|';
	public const char DELIMITER_2 = '$';

	public static int getHighestScore ()
	{
		int highestScore = 0;
		try {
			string[] scores = getHighscores ().Split (DELIMITER);	
			highestScore = Int32.Parse (scores [0].Split (DELIMITER_2) [1]);
		} catch (Exception e) {
			Debug.Log (e);
		}
		print ("highestScore " + highestScore);
		return highestScore;
	}


	public static void saveHighscore (int score)
	{
		string currentDate = System.DateTime.Now.ToString ("dd.MM.yy") + " - " + System.DateTime.Now.ToString ("hh:mm");
		 

		string earlierScores = getHighscores ();
		string stringToSave = currentDate + DELIMITER_2 + score;
		PlayerPrefs.SetString (PREFS_KEY, stringToSave + DELIMITER + earlierScores);
		PlayerPrefs.Save ();
		print ("In Prefs: " + getHighscores ());
	}

	public static string getHighscores ()
	{
		return PlayerPrefs.GetString (PREFS_KEY);
	}


	public static string[] getHighscoresArray ()
	{
		return getHighscores ().Split (DELIMITER);
	}


	[Obsolete ("Only use, if only the currently highest score is to be saved.", true)]
	public static Dictionary<string,int> getHighScoreDic ()
	{
		Dictionary<string,int> highscores = new Dictionary<string, int> ();
		string[] scoreArray = PlayerPrefs.GetString (PREFS_KEY).Split (DELIMITER);
		//string[] scoreArray = createMockup ().Split (DELIMITER);
		print (scoreArray.Length);
		if (scoreArray.Length != 1) {
			for (int i = 0; i < scoreArray.Length - 1; i++) {
				string[] results = scoreArray [i].Split (DELIMITER_2);
				string key = results [0];
				int val = Int32.Parse (results [1]);
				highscores.Add (key, val);
			}
		}
		return highscores;
	}



	public static string createMockup ()
	{
		string result = "";
		for (int i = 0; i < 20; i++) {
			result += "06/20/16 - 04:1" + i + DELIMITER_2 + (int)UnityEngine.Random.Range (20, 500) + DELIMITER;
		}
		print (result);
		return result;
	}

	public static string getScoresAsList ()
	{
		string text = "";
		string[] scores = getHighscoresArray ();
		for (int i = 0; i < scores.Length - 1; i++) {
			text += scores [i].Split (DELIMITER_2) [0] + ": " + Int32.Parse(scores [i].Split (DELIMITER_2) [1]) + "\n";
		}
		return text;
	}
}


