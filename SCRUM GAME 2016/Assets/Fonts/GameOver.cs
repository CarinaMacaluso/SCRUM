using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	public Canvas quitMenu;
	public Button tryAgainButton;
	public Button exitButton;
	public Text score;
	public Text highScore;

	// Use this for initialization
	void Start () {
		score.text = "Score: " + PlayerWalk.score;
		highScore.text = "Highscore: " + PlayerPrefs.GetInt ("score");
		tryAgainButton = tryAgainButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		quitMenu.enabled = false;
	}

	public void ExitPress()
	{
		quitMenu.enabled = true;
		tryAgainButton.enabled = false;
		exitButton.enabled = false;
	}

	public void NoPress ()
	{
		quitMenu.enabled = false;
		tryAgainButton.enabled = true;
		exitButton.enabled = true;
	}

	public void StartLevel()
	{
		SceneManager.LoadScene ("game");
	}

	public void ExitGame ()
	{
		Application.Quit (); 
	}
}
