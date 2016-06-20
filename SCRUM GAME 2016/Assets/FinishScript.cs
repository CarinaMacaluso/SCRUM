using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{

	public Canvas finishCanvas;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Player") {
			PlayerWalk.gameFinished = true;
			finishCanvas.gameObject.SetActive (true);
			if (PrefsHelper.getHighestScore() < PlayerWalk.score) {
				PrefsHelper.saveHighscore (PlayerWalk.score);
			}
		}
	}

	public void showMenu() {
		SceneManager.LoadScene ("Menue");
	}

	public void nextLevel() {
		//TODO: next Level
	}
}
