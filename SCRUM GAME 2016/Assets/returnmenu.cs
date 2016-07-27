using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class returnmenu : MonoBehaviour {

	public Button returnmenue;


	// Use this for initialization
	void Start () {
	
		returnmenue = returnmenue.GetComponent<Button> (); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartLevel () {
		Application.LoadLevel ("Menue");
		
	}
}
