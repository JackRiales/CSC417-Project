using UnityEngine;
using System.Collections;

public class SunLight : MonoBehaviour {

	public Light lightReference;

	// How long do days last (seconds)
	public float dayTime = 480f;

	// How long do nights last (seconds)
	public float nightTime = 300f;

	// Is it daytime or nighttime right now
	private bool isDaytime = true;

	// Internal timer
	public float timer;

	/*
	===================================
	 */

	void Awake() {
		// Handle null light reference if possible
		if (!lightReference && GetComponent<Light> ())
			lightReference = GetComponent<Light>();

		// Set timer
		if (isDaytime)
			timer = dayTime;
		else
			timer = nightTime;
	}

	void Update() {
		// Increment and check the time
		checkDayTime ();

		// Control the lights
		if (isDaytime && !lightReference.enabled) {
			lightReference.enabled = true;
		} 

		else if (!isDaytime && lightReference.enabled) {
			lightReference.enabled = false;
		}

		// Do shit like sunsets here using lerp
	}

	/*
	===================================
	 */

	public int getCurrentTimeSeconds() {
		return Mathf.FloorToInt (timer);
	}

	public int getCurrentTimeMinutes() {
		return Mathf.FloorToInt (timer / 60f);
	}

	private void checkDayTime() {
		// Increment the time down frame delta-time
		timer -= Time.deltaTime;

		// If the timer's reached zero, it's time to change states
		if (timer <= 0) {
			isDaytime = !isDaytime;
			if (isDaytime)
				timer = dayTime;
			else
				timer = nightTime;
		}
	}
}
