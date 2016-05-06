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
	Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		score = 0;
		health = 100; 
		isShieldActive = false; 
		shieldCounter = 0; 

	}

	// Update is called once per frame
	void Update ()
	{
		float h = Input.GetAxis ("Horizontal");		//key 'A' or 'D' / Arrow left/right
		float v = Input.GetAxis ("Vertical"); 		//key 'W' or 'S' / Arrow up/down
		transform.Translate (new Vector3 (0, 0, v) * Time.deltaTime * moveSpeed);
		transform.Rotate (0, h * rotationSpeed, 0);

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

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Coin") {
			Destroy (coll.gameObject);
			score++;
			print (score);
			scoreText.text = "Coin "+score; 
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
    