using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
	public Transform pauseCanvas;
	public CameraScript script;

	// Update is called once per frame
	void Update ()
	{
		if (!PlayerWalk.gameFinished) {
			if (Input.GetKeyDown (KeyCode.P)) {
				if (pauseCanvas.gameObject.activeInHierarchy == false) {
					PlayerWalk.gamePaused = true;
					pauseCanvas.gameObject.SetActive (true);
					Time.timeScale = 0f;
				} else {
					Time.timeScale = 1.0f;
					PlayerWalk.gamePaused = false;
					pauseCanvas.gameObject.SetActive (false);
				}
			}
		}
	}
}
