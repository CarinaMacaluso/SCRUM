﻿using UnityEngine;
using System.Collections;

public class loadMenu : MonoBehaviour {

	public void NextLevelButton(int index)
	{ 
		Application.LoadLevel (index);
	}
	public void NextLevelButton(string Menue)
	{
		Application.LoadLevel (Menue);
	}
}