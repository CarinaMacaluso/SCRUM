using UnityEngine;
using System.Collections;

public class loadOutro : MonoBehaviour {

	void OnTriggerEnter (Collider Other){
		if (Other.tag == "Player"){
			Application.LoadLevel ("OutroScreen");
		}
	}
}
