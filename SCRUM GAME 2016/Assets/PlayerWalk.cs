ausing UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{

	public float moveSpeed;
	public float rotationSpeed;
	public float jumpStrength;
	Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
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
	}

}
