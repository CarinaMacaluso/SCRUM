

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{
	public int score;
	public Text healthText;
	public Text coinText;
	public int health;
	public bool isShieldActive;
	public int shieldCounter;
	public GameObject centerLine;
	public GameObject leftLine;
	public GameObject rightLine;
	public int currentLine;

	// Use this for initialization
	void Start ()
	{
		score = 0;
		health = 100; 
		isShieldActive = false; 
		shieldCounter = 0; 
		currentLine = 0; 

	}

	// Update is called once per frame

	void Update ()
	{
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
		}
	}

	void FixedUpdate() {
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
			score += 10;
			coinText.text = score.ToString ();
		} else if (coll.gameObject.tag == "HealthItem") {
			Destroy (coll.gameObject);
			health = 100;
			healthText.text = health.ToString (); 
		} else if (coll.gameObject.tag == "Shield") {
			Destroy (coll.gameObject);
			isShieldActive = true;
			print ("Shield is active."); 
		} else if (coll.gameObject.tag == "Obstacle" && isShieldActive == true) {
			coll.gameObject.GetComponent<Collider> ().enabled = false; 
		} else if (coll.gameObject.tag == "JetPack") {
			Destroy (coll.gameObject);
			PlayerJump.jetPack = true;
		} else if (coll.gameObject.tag == "Enemy") {
			print (coll.gameObject.name);
			SceneManager.LoadScene ("Menue");
			//transform.position = new Vector3(transform.position.x,0,transform.position.z);
		} else if (coll.gameObject.tag == "Obstacle") {
			health -= 10;
			print ("Outch. health: " + health);
			healthText.text = health.ToString (); 

		}

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
    