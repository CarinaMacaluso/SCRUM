using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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

	IEnumerator LevelLoad (string map)
	{
	 async = SceneManager.LoadSceneAsync (map);
		async.allowSceneActivation = false;
		while (!async.isDone) {
			progressBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500 * async.progress, 20);
			print ("Progress: " + async.progress);
			if (Mathf.Round(async.progress*100) == 89 && nextButton.gameObject.activeInHierarchy == false) {
				print ("setActive");
				nextButton.gameObject.SetActive (true);
			}
			yield return null;
		}
	}

	public void setSceneActivaion() {
		print ("setSceneActivaion");
		async.allowSceneActivation = true;
	}

	public void ExitGame ()
	{
		Application.Quit (); 
	}
}
