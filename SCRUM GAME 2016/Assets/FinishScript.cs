using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{

	public Canvas finishCanvas;
	AsyncOperation async;
	public Button nextButton;
	public Button tryAgainButton;
	public Text errorMessage;
	public Button loadNextLevel;
	public Image progressBar;
	public Image progressBarAgain;
	public GameObject endGroup;
	public RectTransform storyGroup;
	bool sceneActivation = false;

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

			if(PlayerWalk.UfoCounter != 2 && SceneManager.GetActiveScene().name == "game") {
				errorMessage.text= "... but you sadly missed " + (2-PlayerWalk.UfoCounter) + " UFO-parts.";
				errorMessage.gameObject.SetActive (true);
				loadNextLevel.gameObject.SetActive (false);
				tryAgainButton.gameObject.SetActive (true);
				progressBar = progressBarAgain;
				sceneActivation = true;
			}

			finishCanvas.gameObject.SetActive (true);
			if (PrefsHelper.getHighestScore() < PlayerWalk.score) {
				PrefsHelper.saveHighscore (PlayerWalk.score);
			}
		}
	}

	public void showMenu() {
		SceneManager.LoadScene ("Menue");
	}

	public void tryAgain() {
		StartCoroutine ("LevelLoad", (string) SceneManager.GetActiveScene().name);
	}


	public void nextLevel() {
		finishCanvas.gameObject.SetActive (false);
		storyGroup.gameObject.SetActive (true);
		StartCoroutine ("LevelLoad", (string)"game2");
	}


	IEnumerator LevelLoad (string level)
	{
		async = SceneManager.LoadSceneAsync (level);
		async.allowSceneActivation = sceneActivation;
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
}
