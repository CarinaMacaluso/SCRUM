using UnityEngine;
using System.Collections;

public class TriggerfuerBaum : MonoBehaviour {

	public Animation anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "TriggerBaum") {
			anim.Play ();

		}
	}
}
