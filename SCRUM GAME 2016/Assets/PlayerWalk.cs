

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

	// Use this for initialization
	void Start ()
	{
		score = 0;
		health = 100; 
		isShieldActive = false; 
		shieldCounter = 0; 
		currentLine = 0; 
		UfoCounter = 0;
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
		}
	}

	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Coin") {
			Destroy (coll.gameObject);
			increaseScore (10);
		} else if (coll.gameObject.tag == "HealthItem") {
			Destroy (coll.gameObject);
			health = 100;
			healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500 * (health / 100), 350);
		} else if (coll.gameObject.tag == "Shield") {
			Destroy (coll.gameObject);
			isShieldActive = true;
			print ("Shield is active."); 
		} else if ((coll.gameObject.tag == "Obstacle" || coll.gameObject.tag == "Enemy") && isShieldActive == true) {
			coll.gameObject.GetComponent<Collider> ().enabled = false; 
		} else if (coll.gameObject.tag == "JetPack") {
			Destroy (coll.gameObject);
			PlayerJump.jetPack = true;
		} else if (coll.gameObject.tag == "Enemy") {
			print (coll.gameObject.name);
			gameOver ();
		} else if (coll.gameObject.tag == "Obstacle") {
			print ("Outch. health: " + health);
			changeHealth (10);
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
		healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500f * (health / 100f), 400f);

	}

	void gameOver ()
	{
		if (PrefsHelper.getHighestScore() < score) {
			PrefsHelper.saveHighscore (score);
		}
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
