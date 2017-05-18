using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData : MonoBehaviour {
	public string type;
	public string color;
	public GameObject MainSource;
  public Vector3 CenterOfMass = new Vector3(0, -0.5f, 0);
	public bool isMoving = false;
	public GameObject Base;
	public float distanceOfBase;

	private string localColor;

	void Setup () {
  	var rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CenterOfMass;
	}

	void Update() {
		distanceOfBase = Vector3.Distance(transform.position, Base.transform.position);
		if (localColor != color) {
			localColor = color;
			if (color == "white") {
				GetComponent<Renderer>().material = MainSource.GetComponent<Instantiation>().WhiteTexture;
			} else {
				GetComponent<Renderer>().material = MainSource.GetComponent<Instantiation>().BlackTexture;
			}
		}

		if (distanceOfBase > 50 && isMoving == true) {
			transform.position = getBasePosition();
			transform.rotation = Quaternion.identity;
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		if (distanceOfBase > 1.5f && isMoving == false) {
			transform.position = getBasePosition();
			transform.rotation = Quaternion.identity;
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		if (transform.gameObject == MainSource.GetComponent<Instantiation>().activePiece) {
			isMoving = true;
		} else {
			isMoving = false;
		}
	}

	public Vector3 getBasePosition() {
		if (Base != null) {
			return Base.transform.position;
		}
		return Vector3.zero;
	}
}
