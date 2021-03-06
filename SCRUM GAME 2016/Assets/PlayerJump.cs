using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
	public float jumpStrength;
	public float highJumpStrength;
	public float jetPackStrength;
	public static int jetPackFuel = 0;
	public static bool onGround = true;
	public static bool spacePressed = false;
	public Image jetPackImage;
	public Transform jumpDescription; 
	Rigidbody rb;
	public static bool highJumpEnabled = false;
	public AudioClip[] sounds;
	enum SoundClips
	{jet};

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		if (spacePressed) {
			if (onGround) {
				rb.velocity = new Vector3 (0, 0, 0);
				rb.velocity = new Vector3 (0, jumpStrength, 0);
				highJumpEnabled = true;
				print ("normal");
			} else if(highJumpEnabled) {
				rb.velocity = new Vector3 (0, 0, 0);
				rb.velocity = new Vector3 (0, highJumpStrength, 0);
				highJumpEnabled = false;
				print ("high");
			}
			spacePressed = false;
			onGround = false;
		}



		if (Physics.Raycast (transform.position, Vector3.down, 0.5f) && rb.velocity.y < 0) {
			onGround = true;
			highJumpEnabled = false;
			print ("down");
		}   

		if (jetPackFuel > 0) {
			if (Input.GetKey (KeyCode.J)) {
				//rb.AddForce (new Vector3 (0, jetPackStrength, 0), ForceMode.Acceleration); 
				rb.velocity = new Vector3 (0, jumpStrength, 0);
				jetPackImage.fillAmount = 0;
				GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.jet]; 
				GetComponent<AudioSource> ().Play (); 
			}

			if (Physics.Raycast (transform.position, Vector3.down, 1f)) {
				onGround = true;
				//rb.velocity = new Vector3 (0, 0, 0);
				print ("Jetpack on ground");
			} 

			jetPackFuel--;
			if (jetPackFuel <= 0) {
				jetPackImage.gameObject.SetActive (false);
				jumpDescription.gameObject.SetActive (false);
				print ("JetPack is inactive.");
				GetComponent<AudioSource> ().clip = sounds [(int)SoundClips.jet]; 
				GetComponent<AudioSource> ().Pause (); 

			}
			print ("Fuel left: " + jetPackFuel);
		}
	}
}

