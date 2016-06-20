using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	public GameObject quitMenu;
	public Button startText;
	public Button exitText;

	// Use this for initialization
	void Start () {
	
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
	}
	
	public void ExitPress()
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

	public void StartLevel()
	{
		SceneManager.LoadScene ("game");

	}

	public void ExitGame ()
	{
		Application.Quit (); 
	}
}
