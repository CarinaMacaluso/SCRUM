using UnityEngine;
using System.Collections;

public class animTrigger : MonoBehaviour {

	public Animator anim;
	private AudioSource audio;


	// Use this for initialization
	void Start () {
		anim.enabled = false;

		audio = GetComponent<AudioSource> (); 
		audio.mute = true; 
	}
	
	// Update is called once per frame
	void OnTriggerEnter (){
		anim.enabled = true;
		audio.mute = false; 
	}
}
