using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JetPack : MonoBehaviour {

	public Image jetpack;



	// Use this for initialization
	void Start () {
	
		jetpack.fillAmount = 0; 

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.J)) {
			jetpack.fillAmount = 0;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "JetPack") {
			jetpack.fillAmount = 1;
		} 
		
	}
}
