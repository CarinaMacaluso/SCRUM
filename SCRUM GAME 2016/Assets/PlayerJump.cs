using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{

	public double jumpStrength = 50;
	public int jumpCounter = 0;
	public static bool jetPack = false;
	public float Fuel = 50;
	public float fuelLeft;
	public static bool onGround = true;
	Rigidbody rb;
	public int jetPackCounter;
	float upForce = 0;
	float downForce = 0;

	void Start ()
	{
		fuelLeft = Fuel;
		jetPackCounter = 0;
		rb = GetComponent<Rigidbody> ();
	}

	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!onGround) {
			if (Physics.Raycast (transform.position, Vector3.down, 0.5f)) {
				onGround = true;
				jumpCounter = 0;
				upForce = 0;
				transform.position = new Vector3 (transform.position.x, -0.2f, transform.position.z);
				print ("down");
				if (fuelLeft <= Fuel) {
					fuelLeft += Time.deltaTime;
				}
			} 
		} else {
			if (jetPack == true) {
				if (Input.GetKey (KeyCode.Space)) { 
					print ("pressed Space");
					rb.AddForce (new Vector3 (0, 100000, 0)); 
				} 
			} else { 
				if (Input.GetKeyDown (KeyCode.Space)) {
					upForce = 1f;
					downForce = 1f;
					print ("Changed Force to " + upForce);
					onGround = false;
				}
			}
		}

		if (upForce > 0) {
			transform.Translate (new Vector3 (0, upForce, 0));
			upForce -= 0.05f;
			print ("In Air: " + upForce);
		} else if (!onGround & transform.position.y > -0.2) {
			transform.Translate (new Vector3 (0, -downForce, 0));
			downForce -= 0.05F;
			print ("In Air: " + downForce);
		}
		if (jetPack == true) {
			jetPackCounter++;
			if (jetPackCounter == 250) {
				jetPack = false;
				jetPackCounter = 0;
				print ("JetPack is inactive.");
			}

		}
	}







}

