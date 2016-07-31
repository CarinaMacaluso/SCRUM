using UnityEngine;
using UnityEngine.UI;


public class PauseGame : MonoBehaviour
{
	public Transform pauseCanvas;
	public CameraScript script;
	public AudioSource playercamera;







	void Start () {

		AudioListener.pause = false;



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
					playercamera.mute = true; 
					AudioListener.pause = true;
				



			

					 
				} else {
					Time.timeScale = 1.0f;
					PlayerWalk.gamePaused = false;
					pauseCanvas.gameObject.SetActive (false);
					playercamera.mute = false; 
					AudioListener.pause = false; 

					 
				
				}
			}
		}
	}


}
