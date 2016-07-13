using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
	public float jumpStrength;
	public float jetPackStrength;
	public static bool jetPack = false;
	public static bool onGround = true;
	public static bool spacePressed = false;
	public Image jetPackImage;
	Rigidbody rb;
	int jetPackCounter;

	void Start ()
	{
		jetPackCounter = 0;
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!onGround) {
			if (Physics.Raycast (transform.position, Vector3.down, 1f)) {
				onGround = true;

				//transform.position = new Vector3 (transform.position.x, -0.2f, transform.position.z);
				print ("down");
			} 
		} else {
			if (spacePressed) {
				rb.velocity = new Vector3 (0, 0, 0);
				rb.velocity = new Vector3 (0, jumpStrength, 0);
				//print (rb.velocity.y);
				onGround = false;
				spacePressed = false;
			}
		}

		if (jetPack == true) {
			if (Input.GetKey (KeyCode.J)) {
				//rb.AddForce (new Vector3 (0, jetPackStrength, 0), ForceMode.Acceleration); 
				rb.velocity = new Vector3 (0, jumpStrength, 0);

			}
			if (Physics.Raycast (transform.position, Vector3.down, 1f)) {
				onGround = true;
				//rb.velocity = new Vector3 (0, 0, 0);
				print ("Jetpack on ground");
			} 

			print (transform.position.y);
		}

			 
		if (jetPack == true) {
			jetPackCounter++;
			if (jetPackCounter == 350) {
				jetPack = false;
				jetPackImage.gameObject.SetActive (false);
				jetPackCounter = 0;
				print ("JetPack is inactive.");
			}
		}
		

	}
}

