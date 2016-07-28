using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour {

	public Text score;
	public Text highScore;


	// Use this for initialization
	void Start () {
		score.text = "Score: " + PlayerWalk.score;
		highScore.text = "Highscore: " + PrefsHelper.getHighestScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
