﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	public GameObject quitMenu;
	public Button tryAgainButton;
	public Button exitButton;
	public Text score;
	public Text highScore;
	public Image progressBar;
	AsyncOperation async;

	// Use this for initialization
	void Start () {
		score.text = "Score: " + PlayerWalk.score;
		highScore.text = "Highscore: " + PlayerPrefs.GetInt ("score");
		tryAgainButton = tryAgainButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();	}

	public void ExitPress()
	{
		quitMenu.gameObject.SetActive(true);
		tryAgainButton.enabled = false;
		exitButton.enabled = false;
	}

	public void NoPress ()
	{
		quitMenu.gameObject.SetActive(false);
		tryAgainButton.enabled = true;
		exitButton.enabled = true;
	}

	public IEnumerator StartLevel()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync ("game");
		while (!async.isDone) {
			progressBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500 * async.progress, 20);
			yield return async;
		}
	}

	public void ExitGame ()
	{
		Application.Quit (); 
	}
}
