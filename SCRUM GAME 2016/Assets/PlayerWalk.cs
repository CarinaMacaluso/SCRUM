﻿

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{
	public float moveSpeed;
	public static int score;
	public Text coinText;
	public int health;
	public bool isShieldActive;
	public int shieldCounter;
	public GameObject centerLine;
	public GameObject leftLine;
	public GameObject rightLine;
	public GameObject healthBar;
	public int currentLine;
	public static bool gamePaused = false;
	public static bool gameFinished = false;
	public int UfoCounter;
	public AudioSource coinsound;
	public AudioSource[] sounds;
	public AudioSource muenzen;
	public AudioSource alienbewegung;
	public AudioSource einsammeln;
	public AudioSource einsammelntank;
	public AudioSource atmo;
	public AudioSource collide;
	public AudioSource jump;

	public float time = 5;
	public float remainingTime;
	public bool timerSet = false;
	private GameObject TimerText;
	private Text text;





	// Use this for initialization
	void Start ()
	{
		score = 0;
		health = 100; 
		isShieldActive = false; 
		shieldCounter = 0; 
		currentLine = 0; 
		UfoCounter = 0;	
		coinsound.mute = true;
		sounds = GetComponents<AudioSource>();
		muenzen = sounds[0];
		alienbewegung = sounds[1];
		alienbewegung.mute = false; 
		einsammeln = sounds [2];
		einsammeln.mute = true; 
		einsammelntank = sounds [3];
		einsammelntank.mute = true; 
		atmo = sounds [4];
		atmo.mute = false; 
		collide.mute = true; 
		collide = sounds [5];
		jump = sounds [6];
		jump.mute = true; 

		TimerText = GameObject.FindWithTag("TimerText");
		text = TimerText.GetComponent<Text>();


	

	}

	// Update is called once per frame

	void Update ()
	{
		if (!gamePaused && !gameFinished) {
			deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
			if (Input.GetKeyDown ("left") || Input.GetKeyDown (KeyCode.A)) { // left
				if (currentLine == 0) {
					transform.position = new Vector3 (leftLine.transform.position.x, transform.position.y, transform.position.z);
					currentLine = 1; 
				} else if (currentLine == -1) {
					transform.position = new Vector3 (centerLine.transform.position.x, transform.position.y, transform.position.z);
					currentLine = 0; 
				}
			} else if (Input.GetKeyDown ("right") || Input.GetKeyDown (KeyCode.D)) { //right
				if (currentLine == 0) {
					transform.position = new Vector3 (rightLine.transform.position.x, transform.position.y, transform.position.z);
					currentLine = -1;
				} else if (currentLine == 1) {
					transform.position = new Vector3 (centerLine.transform.position.x, transform.position.y, transform.position.z);
					currentLine = 0;
				}
			} else if (Input.GetKeyDown (KeyCode.Space) && PlayerJump.onGround) { 
				PlayerJump.spacePressed = true;
				jump.mute = false;
				jump.Play (); 
			}
		}
	}

	void FixedUpdate ()
	{
		transform.Translate (new Vector3 (0, 0, -moveSpeed));

		if (isShieldActive == true) {
			shieldCounter++;
			if (shieldCounter == 250) {
				isShieldActive = false;
				shieldCounter = 0;
				print ("Shield is inactive");
			}

			if(timerSet == true)
			{
				remainingTime -= Time.deltaTime;
				text.text = remainingTime.ToString("F2");
//				text.text = "Hallo";
			}

			if(remainingTime <= 0)
			{
				timerSet = false;
				text.text = " ";
			} 
		}
	}

	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Coin") {
			Destroy (coll.gameObject);
			increaseScore (10);
			coinsound.mute = false; 
			coinsound.Play (); 
		} else if (coll.gameObject.tag == "HealthItem") {
			Destroy (coll.gameObject);
			health = 100;
			healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500 * (health / 100), 350);
			einsammeln.mute = false;
			einsammeln.Play (); 
		} else if (coll.gameObject.tag == "Shield") {
			Destroy (coll.gameObject);
			isShieldActive = true;
			print ("Shield is active.");
			einsammeln.mute = false; 
			einsammeln.Play ();

			remainingTime = time;
			timerSet = true;
		}
		else if ((coll.gameObject.tag == "Obstacle" || coll.gameObject.tag == "Enemy") && isShieldActive == true) {
			coll.gameObject.GetComponent<Collider> ().enabled = false;
		} else if (coll.gameObject.tag == "JetPack") {
			Destroy (coll.gameObject);
			PlayerJump.jetPack = true;
			einsammeln.mute = false;
			einsammeln.Play (); 
		} else if (coll.gameObject.tag == "Enemy") {
			print (coll.gameObject.name);
			gameOver ();
		} else if (coll.gameObject.tag == "Obstacle") {
			print ("Outch. health: " + health);
			changeHealth (10);
			collide.mute = false;
			collide.Play (); 
		} else if (coll.gameObject.tag == "Ufo") {
			Destroy (coll.gameObject);
			UfoCounter += 1;
			increaseScore (40);
			print ("UFO-Teil eingesammelt.");
		}
	}






	void increaseScore (int scoreToAdd)
	{
		score += scoreToAdd;
		coinText.text = score.ToString ();
	}


	void changeHealth (int damage)
	{
		health -= damage;
		if (health <= 0) {
			gameOver ();
		}
		healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500f * (health / 100f), 400f);

	}

	void gameOver ()
	{
		if (PrefsHelper.getHighestScore() < score) {
			PrefsHelper.saveHighscore (score);
		}
		GameOver.previousLevel = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene ("GameOver");
	}
		

	//Für FPS-Anzeige (oben links im Spielfenster)
	float deltaTime = 0.0f;

	void OnGUI ()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle ();

		Rect rect = new Rect (0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label (rect, text, style);
	}

}
