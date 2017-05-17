using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour {
	public GameObject MatrixDetector;
	void Start () {
    for (int y = 0; y < 8; y++) {
      for (int x = 0; x < 8; x++) {
        var currentClone = Instantiate(MatrixDetector, new Vector3(-10.5f + x * 3, -0.35f, -10.5f + y * 3), Quaternion.identity);
				currentClone.GetComponent<MetaData>().row = y;
				currentClone.GetComponent<MetaData>().column = x;
    }}
	}
}
