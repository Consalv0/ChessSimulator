using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {
	public Vector3 position = new Vector3(0, 0, 0);

	void OnTriggerExit(Collider element) {
		if (element.GetComponent<Rigidbody>() != null) {
			if (element.GetComponent<PieceData>() != null) {
				if (element.GetComponent<PieceData>().Base != null) {
					element.transform.position = position;
					element.GetComponent<Rigidbody>().velocity = Vector3.zero;
				}
			} else {
				element.transform.position = position;
				element.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
		} else {
			element.transform.position = position;
		}
	}
}
