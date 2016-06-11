using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Timer : MonoBehaviour {

    public float time = 5;
    public float remainingTime;
    public bool timerSet = false;
    private GameObject TimerText;
    private Text text;


    void Start()
    {
        TimerText = GameObject.FindWithTag("TimerText");
        text = TimerText.GetComponent<Text>();

    }


    // Use this for initialization
    void OnTriggerEnter (Collider other) {
	    if(other.gameObject.tag == "Player")
        {
            remainingTime = time;
            timerSet = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(timerSet == true)
        {
            text.text = remainingTime.ToString("F2");
            remainingTime -= Time.deltaTime;
        }

        if(remainingTime <= 0)
        {
            timerSet = false;
            text.text = " ";

        }
	}
}
