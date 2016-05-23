using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{

	public double jumpStrength = 50;
	public int jumpCounter = 0;
	public bool jetPack = false;
	public float Fuel = 50;
	public float fuelLeft;
	public static bool onGround = true;
	float upForce = 0;
	float downForce = 0;

	void Start ()
	{
		fuelLeft = Fuel;
	}

	
	// Update is called once per frame
	void Update ()
	{
		if (!onGround) {
			if (Physics.Raycast(transform.position, Vector3.down, 0.5f)) {
				onGround = true;
				jumpCounter = 0;
				upForce = 0;
				transform.position = new Vector3(transform.position.x,-0.2f,transform.position.z);
				print ("down");
				if (fuelLeft <= Fuel) {
					fuelLeft += Time.deltaTime;
				}
			} else if (jetPack) {
				flyWithJetPack ();
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Space)) { 
				upForce = 3;
				downForce = 3;
				print ("Changed Force to " + upForce);
				onGround = false;
			}
		}

		if (upForce > 0) {
			transform.Translate (new Vector3 (0, upForce, 0));
			upForce -= 0.5f;
			print ("In Air: " + upForce);
		} else if (!onGround & transform.position.y > -0.2) {
			transform.Translate (new Vector3 (0, -downForce, 0));
			downForce -= 0.5F;
			print ("In Air: " + downForce);
		}
	}

	void flyWithJetPack ()
	{

		if (fuelLeft >= 0) {
			fuelLeft -= Time.deltaTime * 10;
		} 
	}




}

