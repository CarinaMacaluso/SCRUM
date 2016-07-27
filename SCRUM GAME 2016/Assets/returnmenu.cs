using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class returnmenu : MonoBehaviour {

	public Button menureturn;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReturnToMenu () {
		Application.LoadLevel ("Menue"); 
	}
}
