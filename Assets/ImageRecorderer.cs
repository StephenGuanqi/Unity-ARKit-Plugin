using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class ImageRecorderer : MonoBehaviour {

	private int recordMode = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeRecordingState() {
		if (recordMode == 0) {
			recordMode = 1;
			GameObject.Find("Button").GetComponentInChildren<Text>().text = "is recording.....";
			UnityARSessionNativeInterface.GetARSessionNativeInterface ().setARRecordingState (recordMode);
		} else {
			recordMode = 0;
			GameObject.Find("Button").GetComponentInChildren<Text>().text = "Start Recording";
			UnityARSessionNativeInterface.GetARSessionNativeInterface ().setARRecordingState (recordMode);
		}
	}

	
}
