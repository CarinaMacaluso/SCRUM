

using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{
	public float moveSpeed;
	public static int score;
	public Text coinText;
	public Text ufoText;
	public Image jetPackImage;
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
	public AudioClip[] sounds;
	enum SoundClips {collide, shield, coin, life, jump};
	public Transform shielddescription; 
	public Text healthDescription;
	public Text jumpDescription; 

	public float time = 5;
	public float remainingTime;
	public bool timerSet = false;
	private GameObject TimerText;
	private Text text;


	// Use this for initialization
	void Start ()
	{

		gameFinished = false;
		score = 0;
		health = 100; 
		isShieldActive = false; 
		shieldCounter = 0; 
		currentLine = 0; 
		UfoCounter = 0;	

		TimerText = GameObject.FindWithTag ("TimerText");
		text = TimerText.GetComponent<Text> ();
		jetPackImage.gameObject.SetActive (false);
		shielddescription.gameObject.SetActive (false);
		healthDescription.gameObject.SetActive (false); 
		//jumpDescription.gameObject.SetActive (false); 
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
				GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.jump]; 
				GetComponent<AudioSource> ().Play (); 
				 
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
				shielddescription.gameObject.SetActive (false);
			}

			if (timerSet == true) {
				remainingTime -= Time.deltaTime;
				text.text = remainingTime.ToString ("F2");
//				text.text = "Hallo";
			}

			if (remainingTime <= 0) {
				timerSet = false;
				text.text = "Timer";
			}

		
		}
	}

	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Coin") {
			Destroy (coll.gameObject);
			increaseScore (10);
			GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.coin]; 
			GetComponent<AudioSource> ().Play (); 

		} else if (coll.gameObject.tag == "HealthItem") {
			Destroy (coll.gameObject);
			health = 100;
			healthDescription.text = "100%";
			healthDescription.gameObject.SetActive (true);
			Invoke ("hideHealthDescription", 3.0f);
			healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500 * (health / 100), 350);
			GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.life]; 
			GetComponent<AudioSource> ().Play ();  
		} else if (coll.gameObject.tag == "Shield") {
			Destroy (coll.gameObject);
			isShieldActive = true;
			print ("Shield is active.");
			GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.shield]; 
			GetComponent<AudioSource> ().Play (); 
			shielddescription.gameObject.SetActive (true);

			remainingTime = time;
			timerSet = true;
		} else if ((coll.gameObject.tag == "Obstacle" || coll.gameObject.tag == "Enemy") && isShieldActive == true) {
			coll.gameObject.GetComponent<Collider> ().enabled = false;
			GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.collide]; 
			GetComponent<AudioSource> ().Play (); 
		} else if (coll.gameObject.tag == "JetPack") {
			Destroy (coll.gameObject);
			jetPackImage.gameObject.SetActive (true);
			//jumpDescription.gameObject.SetActive (true); 
			PlayerJump.jetPackFuel +=350;

			print ("JetPack eingesammelt.");
		} else if (coll.gameObject.tag == "Enemy") {
			print (coll.gameObject.name);
			gameOver ();
		} else if (coll.gameObject.tag == "Obstacle") {
			print ("Outch. health: " + health);
			changeHealth (10);
			GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.collide]; 
			GetComponent<AudioSource> ().Play (); 		 
		} else if (coll.gameObject.tag == "Ufo") {
			Destroy (coll.gameObject);
			UfoCounter += 1;
			ufoText.text = UfoCounter + "/2";
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
		healthDescription.text = "-"+damage;
		healthDescription.gameObject.SetActive (true);
		Invoke ("hideHealthDescription", 3.0f);
		health -= damage;
		if (health <= 0) {
			gameOver ();
		}
		healthBar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500f * (health / 100f), 350f);
	}


	void hideHealthDescription() {
		healthDescription.gameObject.SetActive (false);
	}
		

	void gameOver ()
	{
		if (PrefsHelper.getHighestScore () < score) {
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
