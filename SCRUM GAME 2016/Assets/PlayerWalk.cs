

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{

	public float moveSpeed;
	public float rotationSpeed;
	public float jumpStrength;
	public int score;
	public Text scoreText;
	public int health;
	public bool isShieldActive;
	public int shieldCounter;
	public GameObject centerLine;
	public GameObject leftLine;
	public GameObject rightLine;
	Rigidbody rb;
	public int currentLine;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		score = 0;
		health = 100; 
		isShieldActive = false; 
		shieldCounter = 0; 
		currentLine = 0; 

	}

	// Update is called once per frame

	void Update ()
	{

		transform.Translate (new Vector3 (0, 0, 1) * moveSpeed);

		if (Input.GetKeyDown ("left")) { // left
			print ("Left");
			if (currentLine == 0) {
				transform.position = new Vector3 (leftLine.transform.position.x, transform.position.y, transform.position.z);
				currentLine = 1; 
			} else if (currentLine == -1) {
				transform.position = new Vector3 (centerLine.transform.position.x, transform.position.y, transform.position.z);
				currentLine = 0; 
			}

		} else if (Input.GetKeyDown ("right")) { //right
			if (currentLine == 0) {
				transform.position = new Vector3 (rightLine.transform.position.x, transform.position.y, transform.position.z);
				currentLine = -1;
			} else if (currentLine == 1) {
				transform.position = new Vector3 (centerLine.transform.position.x, transform.position.y, transform.position.z);
				currentLine = 0;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) { 
			rb.AddForce (Vector3.up * 800F * jumpStrength);
		}

		if (isShieldActive == true) {
			shieldCounter++;
			if (shieldCounter == 1000) {
				isShieldActive = false;
				shieldCounter = 0;
				print ("Item vorbei"); 
			} 
		}
	}

	void OnCollisionEnter (Collision coll)
	{
		if (coll.gameObject.tag == "Coin") {
			Destroy (coll.gameObject);
			score++;
			print (score);
			scoreText.text = "Coin " + score; 
		} 

		if (coll.gameObject.tag == "HealthItem") {
			Destroy (coll.gameObject);
			health = 100;
			print (health);
		}
		if (coll.gameObject.tag == "Shield") {
			Destroy (coll.gameObject);
			isShieldActive = true;
			print ("Item beginnt"); 

		}

	}

}
    