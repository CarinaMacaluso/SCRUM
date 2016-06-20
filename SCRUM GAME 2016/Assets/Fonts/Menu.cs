using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	public GameObject quitMenu;
	public Button startText;
	public Button nextButton;
	public Button exitText;
	public Image progressBar;
	AsyncOperation async;
	public RectTransform MenuGroup;
	public RectTransform StoryGroup;
	public Text highscoreText;
	public RectTransform highScoreContainer;


	// Use this for initialization
	void Start ()
	{
	
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
	}

	public void ExitPress ()
	{
		quitMenu.gameObject.SetActive (true);
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress ()
	{
		quitMenu.gameObject.SetActive (false);
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel ()
	{
		StartCoroutine ("LevelLoad", (string)"game");
		MenuGroup.gameObject.SetActive (false);
		StoryGroup.gameObject.SetActive (true);

	}

	IEnumerator LevelLoad (string level)
	{
		AsyncOperation async = SceneManager.LoadSceneAsync (level);
		async.allowSceneActivation = false;
		while (!async.isDone) {
			progressBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500 * async.progress, 20);
			if (Mathf.Round(async.progress*100) == 90 && nextButton.gameObject.activeInHierarchy == false) {
				nextButton.gameObject.SetActive (true);
			}
			yield return null;
		}
	}

	public void setSceneActivaion() {
		print ("setSceneActivaion");
		async.allowSceneActivation = true;
	}

	[Obsolete("Only use, if only the currently highest score is to be saved.",true)]
	public void showHighscoresDictionary() {
		if (highScoreContainer.gameObject.activeInHierarchy == false) {
			highScoreContainer.gameObject.SetActive (true);

			string text= "";
			foreach (KeyValuePair<string,int> item in PrefsHelper.getHighScoreDic().OrderBy(key => key.Value)) {
				string entry = item.Key + ": " + item.Value;
				entry += "\n" + text;
				text = entry;
			}
			highscoreText.text = text;
			print (text);
		} else {
			highScoreContainer.gameObject.SetActive (false);
		}
	}

	public void showHighscores() {
		if (highScoreContainer.gameObject.activeInHierarchy == false) {
			highScoreContainer.gameObject.SetActive (true);

			highscoreText.text = PrefsHelper.getScoresAsList ();
		} else {
			highScoreContainer.gameObject.SetActive (false);
		}
	}

	public void clearHighscoreList() {
		PlayerPrefs.DeleteKey (PrefsHelper.PREFS_KEY);
		highscoreText.text = "-";
	}

	public void ExitGame ()
	{
		Application.Quit (); 
	}
}
