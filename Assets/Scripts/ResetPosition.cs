using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {
	public Vector3 position = new Vector3(0, 0, 0);

	void OnTriggerExit(Collider element) {
		element.transform.position = position;
		if (element.GetComponent<Rigidbody>() != null) {
			element.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}
