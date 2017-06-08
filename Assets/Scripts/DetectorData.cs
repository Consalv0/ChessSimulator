using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorData : MonoBehaviour {
	public int row;
	public int column;
	public bool acceptsMove = false;
	public GameObject assignedPiece;

	public Instantiation mainInstantiation;

	void OnTriggerStay(Collider piece) {
		if (piece.GetComponent<PieceData>() == null) return;
    if (piece.GetComponent<PieceData>().isPicked) return;
		var detectors = mainInstantiation.detectors;
		var activeDetector = mainInstantiation.activeDetector;

		if (acceptsMove && piece.gameObject == mainInstantiation.activePiece) {
			for (int i = 0; i < detectors.GetLength(0); i++) {
				for (int j = 0; j < detectors.GetLength(0); j++) {
					detectors[i, j].GetComponent<DetectorData>().acceptsMove = false;
				}
			}
			// Set New Base To The Piece and Add It To The Board
			mainInstantiation.board[column, row] = piece.gameObject;
			piece.GetComponent<PieceData>().Base = transform.gameObject;
			// Remove Old Position
			mainInstantiation.activePiece = null;
			mainInstantiation.board[activeDetector.GetComponent<DetectorData>().column, activeDetector.GetComponent<DetectorData>().row] = null;
			activeDetector.GetComponent<DetectorData>().assignedPiece = null;
			activeDetector = null;
			// Remove Board to Captured Piece
			if (assignedPiece == null) {
				assignedPiece = piece.gameObject;
			} else if (piece.gameObject != assignedPiece) {
				assignedPiece.GetComponent<PieceData>().Base = null;
				assignedPiece = piece.gameObject;
			}
			mainInstantiation.turn = !mainInstantiation.turn;
		}
  }

	void OnTriggerExit(Collider piece) {
		if (piece.GetComponent<PieceData>() == null) return;
        if (!piece.GetComponent<PieceData>().isPicked) return;
  
		var pieceData = piece.GetComponent<PieceData>();
		var detectors = mainInstantiation.detectors;

		if (mainInstantiation.activePiece == null && piece.GetComponent<PieceData>().Base != null && mainInstantiation.board[column, row] == piece.gameObject) {
			if (mainInstantiation.isWhite(piece.gameObject) == "true" && mainInstantiation.turn) return;
			if (mainInstantiation.isWhite(piece.gameObject) == "false" && !mainInstantiation.turn) return;

			mainInstantiation.activePiece = piece.gameObject;
			int[,] posibleMoves = mainInstantiation.getPosibleMoves(pieceData.type, pieceData.color, column, row);

			// Check IF There's Possible Moves
			if (posibleMoves == null) {
				mainInstantiation.activePiece = null;
				return;
			}

			mainInstantiation.activeDetector = transform.gameObject;
			for (var i = 0; i < posibleMoves.GetLength(0); i++) {
				// Find Detectors Within Posible Moves
				if (mainInstantiation.numberInBoard(posibleMoves[i, 0], posibleMoves[i, 1])) {
					detectors[posibleMoves[i, 0], posibleMoves[i, 1]].GetComponent<DetectorData>().acceptsMove = true;
				}
			}
		}
  }

	void Update() {
		if (!acceptsMove) {
			if ((row + column) % 2 != 0) {
				GetComponent<Renderer>().material = mainInstantiation.WoodTexture;
			} else {
				GetComponent<Renderer>().material = mainInstantiation.CoralTexture;
			}
		} else if (acceptsMove) {
			GetComponent<Renderer>().material = mainInstantiation.JadeTexture;
		}
	}
}
