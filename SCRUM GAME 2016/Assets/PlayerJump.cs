using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
	public float jumpStrength;
	public float jetPackStrength;
	public static int jetPackFuel = 0;
	public static bool onGround = true;
	public static bool spacePressed = false;
	public Image jetPackImage;
	public Transform jumpDescription; 
	Rigidbody rb;


	void Start ()
	{
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

		if (jetPackFuel > 0) {
			if (Input.GetKey (KeyCode.J)) {
				//rb.AddForce (new Vector3 (0, jetPackStrength, 0), ForceMode.Acceleration); 
				rb.velocity = new Vector3 (0, jumpStrength, 0);
				//jetPackImage.fillAmount = 0;
			}

			if (Physics.Raycast (transform.position, Vector3.down, 1f)) {
				onGround = true;
				//rb.velocity = new Vector3 (0, 0, 0);
				print ("Jetpack on ground");
			} 

			jetPackFuel--;
			if (jetPackFuel <= 0) {
				jetPackImage.gameObject.SetActive (false);
				//jumpDescription.gameObject.SetActive (false);
				print ("JetPack is inactive.");
			}
			print ("Fuel left: " + jetPackFuel);
		}
	}
}

