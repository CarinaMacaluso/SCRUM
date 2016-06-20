using System;
using UnityEngine;

public class RandomGameObjects : MonoBehaviour
{
	public GameObject parent;

	public GameObject leftLane;
	public GameObject centerLane;
	public GameObject rightLine;


	public GameObject rocks1;
	public GameObject rocks2;
	public GameObject rocks3;

	private GameObject[] lanes;
	private GameObject[] rocks;
	public int DENSITY;

	float length;

	void Start ()
	{
		lanes = new GameObject[] {leftLane, centerLane, rightLine};
		rocks = new GameObject[] {rocks1,rocks2,rocks3};
		length = centerLane.transform.lossyScale.z*10;
		print ("length: "+ length);

		for (int i = 0; i < DENSITY; i++) {
			int randomLane = (int)UnityEngine.Random.Range (0, lanes.Length);
			int randomObject = (int)UnityEngine.Random.Range (0, rocks.Length);
			float posX = lanes [randomLane].transform.position.x;
			float posZ = UnityEngine.Random.Range (100,length-200);

			GameObject instace = (GameObject) Instantiate (rocks [randomObject], new Vector3 (posX, 0, posZ),Quaternion.identity);
			instace.transform.parent = parent.gameObject.transform;
		}
	}

}

