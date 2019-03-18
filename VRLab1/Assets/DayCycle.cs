using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DayCycle : MonoBehaviour {
	public static float DAY_LENGTH = 5;
	public enum DAYPART { DAY, NIGHT};
	private float t;
	private DAYPART state; // DAY or NIGHT
	// Use this for initialization
	Light lt;
	void Start () {
		lt = GetComponent<Light> ();
		resetTimer ();
	}

	void resetTimer() {
		t = DAY_LENGTH;
		state = DAYPART.DAY;
	}

	void toggleLight() {
		switch (state) {
		case DAYPART.DAY:
			lt.intensity = 0.1f;
			state = DAYPART.NIGHT;
			break;
		case DAYPART.NIGHT:
			lt.intensity = 20f;
			state = DAYPART.DAY;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		if (t <= 0) {
			toggleLight ();
		}
	}
}
