using UnityEngine;
using System.Collections;

public class CullCollider : MonoBehaviour {

	public enum cullCheckType {
		triggerCheck,
		continuous
	};

	public LayerMask cullingLayer;
	public cullCheckType checkType;

	/*
	===================================
	 */

	void OnTriggerEnter(Collider c) {
		if (checkType == cullCheckType.triggerCheck && (cullingLayer.value & c.gameObject.layer) > 0)
			c.GetComponent<Renderer>().enabled = true;
	}

	void OnTriggerExit(Collider c) {
		if ((cullingLayer.value & c.gameObject.layer) > 0)
			c.GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerStay(Collider c) {
		if (checkType == cullCheckType.continuous && (cullingLayer.value & c.gameObject.layer) > 0) {
			c.GetComponent<Renderer>().enabled = true;
		}
	}
}
