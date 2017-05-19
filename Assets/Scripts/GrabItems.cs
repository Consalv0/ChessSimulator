using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItems : MonoBehaviour {
	public Transform Soul;

	Transform defaultSoul;

	void Start() {
		GetComponent<ControllerMovement>().Soul = Soul;
		defaultSoul = Soul;
	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, 10)) {
				if (hit.transform.GetComponent<PieceData>() != null) {
					Soul = hit.transform;
				}
			} else {
				Soul = defaultSoul;
				transform.position = defaultSoul.position;
				transform.rotation = defaultSoul.rotation;
			}
			GetComponent<ControllerMovement>().Soul = Soul;
		}

		if (Input.GetButtonDown("Fire2")) {
			Soul = defaultSoul;
			GetComponent<ControllerMovement>().Soul = defaultSoul;
		}
	}
}
