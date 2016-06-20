using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{

	public GameObject quitMenu;
	public Button startText;
	public Button exitText;
	public Image progressBar;

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
	}

	IEnumerator LevelLoad (string map)
	{
		AsyncOperation async = Application.LoadLevelAsync (map);
		while (!async.isDone) {
			progressBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500 * async.progress, 20);
			yield return null;
		}
	}
		
	public void ExitGame ()
	{
		Application.Quit (); 
	}
}
