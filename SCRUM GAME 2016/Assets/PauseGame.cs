using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
	public Transform pauseCanvas;
	public CameraScript script;
	public PlayerWalk script2; 






	void Start () {

	


	}

	// Update is called once per frame
	void Update ()
	{
		if (!PlayerWalk.gameFinished) {
			if (Input.GetKeyDown (KeyCode.P)) {
				if (pauseCanvas.gameObject.activeInHierarchy == false) {
					PlayerWalk.gamePaused = true;
					pauseCanvas.gameObject.SetActive (true);
					Time.timeScale = 0f;
					script2.alienbewegung.mute = true; 
					script2.atmo.mute = true; 



			

					 
				} else {
					Time.timeScale = 1.0f;
					PlayerWalk.gamePaused = false;
					pauseCanvas.gameObject.SetActive (false);
					script2.alienbewegung.mute = false; 
					script2.atmo.mute = false; 
					 
				
				}
			}
		}
	}
}
