using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItems : MonoBehaviour {
	public Transform Soul;
    public GameObject pickedElement;
    public GameObject MainSource;

    Transform defaultSoul;

	void Start() {
        Instantiation MainInstantiation = MainSource.GetComponent<Instantiation>();
        GetComponent<ControllerMovement>().Soul = Soul;
		defaultSoul = Soul;
	}

	void Update () {
        // When Primary Button IS Pressed:
		if (Input.GetButtonDown("Fire1")) {
            Instantiation MainInstantiation = MainSource.GetComponent<Instantiation>();
            // Cast A Ray AND Return a element
            RaycastHit element;
			if (Physics.Raycast(transform.position, transform.forward, out element, 50)) {
                // Do nothing IF the element IS NOT a Chess Piece
				if (element.transform.GetComponent<PieceData>() != null) {
                    var piece = element.transform;
                    var data = piece.GetComponent<PieceData>();
                    // Do nothing IF There's NO Posibles Moves to Do in a Piece That Have Base
                    if (piece.GetComponent<PieceData>().Base != null) {
                        int[,] posibleMoves = MainInstantiation.getPosibleMoves(data.type, data.color, data.Base.GetComponent<DetectorData>().column, data.Base.GetComponent<DetectorData>().row);
                      if (posibleMoves == null) return;
                      if (MainInstantiation.isWhite(piece.gameObject) == "true" && MainInstantiation.turn) return;
                      if (MainInstantiation.isWhite(piece.gameObject) == "false" && !MainInstantiation.turn) return;
                    }
                    
                    // Assign the Hitted Piece
                    Soul = piece;
                    pickedElement = piece.gameObject;
				}
			} else {
				Soul = defaultSoul;
				transform.position = defaultSoul.position;
				transform.rotation = defaultSoul.rotation;
            }
            GetComponent<ControllerMovement>().Soul = Soul;
        }

		if (Input.GetButtonDown("Fire2")) {
            pickedElement.GetComponent<PieceData>().isPicked = false;
            pickedElement = null;
			Soul = defaultSoul;
			GetComponent<ControllerMovement>().Soul = defaultSoul;
        }
        if (pickedElement != null)
          pickedElement.GetComponent<PieceData>().isPicked = true;
    }
}
