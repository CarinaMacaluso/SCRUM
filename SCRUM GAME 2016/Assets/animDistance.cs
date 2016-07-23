using UnityEngine;
using System.Collections;

public class animDistance : MonoBehaviour {
	private AnimationClip clipName;
	public float reachDistance;
	public GameObject Player;



	// Use this for initialization
	void Start () {
		

	}

	// Update is called once per frame
	void Update () {
		Debug.Log (Vector3.Distance (Player.transform.position, transform.position));


	}
}