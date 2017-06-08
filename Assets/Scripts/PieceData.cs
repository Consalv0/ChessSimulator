using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData : MonoBehaviour {
	public string type;
	public string color;
	public GameObject mainSource;
	public bool isMoving = false;
	public GameObject Base;
  public bool isPicked = false;

	float distanceOfBase;
	string localColor;
  Vector3 CenterOfMass = new Vector3(0, -0.5f, 0);

	void Setup () {
    GetComponent<Rigidbody>().centerOfMass = CenterOfMass;
	}

	void Update() {
		distanceOfBase = Vector3.Distance(transform.position, getBasePosition() + new Vector3(0, 0.5f, 0));
		if (localColor != color) {
			localColor = color;
			if (color == "white") {
				GetComponent<Renderer>().material = mainSource.GetComponent<Instantiation>().WhiteTexture;
			} else {
				GetComponent<Renderer>().material = mainSource.GetComponent<Instantiation>().BlackTexture;
			}
		}

		if (isMoving == false && Base != null && !Base.GetComponent<DetectorData>().acceptsMove) {
			if (distanceOfBase > 0.5f) {
				var basePosition = Base.transform.position;
				transform.position = Vector3.Lerp(transform.position, new Vector3(basePosition.x, transform.position.y, basePosition.z), Time.deltaTime);
			}
			var thisQuat = transform.eulerAngles;
			if (thisQuat.x < -18 || thisQuat.x > 18 || thisQuat.z < -18 || thisQuat.z > 18) {
				var rigidBody = GetComponent<Rigidbody>();
				rigidBody.angularVelocity = new Vector3(0, rigidBody.angularVelocity.y, 0);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, thisQuat.y, 0), Time.deltaTime * 4f);
			}
		}

		if (distanceOfBase > 50 && isMoving == true) {
			transform.position = getBasePosition();
			transform.rotation = Quaternion.identity;
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		if (transform.gameObject == mainSource.GetComponent<Instantiation>().activePiece) {
			GetComponent<Rigidbody>().mass = 100;
			isMoving = true;
		} else {
			if (Base == null) {
				GetComponent<Rigidbody>().mass = 0.01f;
			} else {
				GetComponent<Rigidbody>().mass = 10;
			}
			isMoving = false;
		}

    if (isPicked)
      transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	public Vector3 getBasePosition() {
		if (Base != null) {
			return Base.transform.position;
		}
		return Vector3.zero;
	}
}
