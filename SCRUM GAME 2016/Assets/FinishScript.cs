using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{

	public Canvas finishCanvas;
	AsyncOperation async;
	public Button nextButton;
	public Image progressBar;
	public GameObject endGroup;
	public RectTransform storyGroup;

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
		endGroup.gameObject.SetActive (false);
		storyGroup.gameObject.SetActive (true);
		StartCoroutine ("LevelLoad", (string)"game2");
	}


	IEnumerator LevelLoad (string level)
	{
		async = SceneManager.LoadSceneAsync (level);
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
}
