using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Coins : MonoBehaviour {

    public int coins = 0;
    private GameObject CoinText;
    private Text text;

	// Use this for initialization
	void Start () {
        CoinText = GameObject.FindWithTag("CoinText");
        text = CoinText.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(other.gameObject);
            text.text = coins.ToString();
        }
        
	}
}
