using UnityEngine;
using System.Collections;

public class pauseGame : MonoBehaviour {
	public Transform pauseCanvas;
	public CameraScript script;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P))
	
			{
			if (pauseCanvas.gameObject.activeInHierarchy == false) {
				pauseCanvas.gameObject.SetActive (true);

			} else {
				pauseCanvas.gameObject.SetActive (false);

			}
			}
	}
}
