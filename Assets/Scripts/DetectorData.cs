using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorData : MonoBehaviour {
	public int row;
	public int column;
	public bool acceptsMove = false;
	public GameObject assignedPiece;
	public GameObject MainSource;

	void OnTriggerStay(Collider piece) {
		if (piece.GetComponent<PieceData>() == null) return;
        if (piece.GetComponent<PieceData>().isPicked) return;
		Instantiation MainInstantiation = MainSource.GetComponent<Instantiation>();
		var detectors = MainInstantiation.detectors;
		var activeDetector = MainInstantiation.activeDetector;

		if (acceptsMove && piece.gameObject == MainInstantiation.activePiece) {
			for (int i = 0; i < detectors.GetLength(0); i++) {
				for (int j = 0; j < detectors.GetLength(0); j++) {
					detectors[i, j].GetComponent<DetectorData>().acceptsMove = false;
				}
			}
			// Set New Base To The Piece and Add It To The Board
			MainInstantiation.board[column, row] = piece.gameObject;
			piece.GetComponent<PieceData>().Base = transform.gameObject;
			// Remove Old Position
			MainInstantiation.activePiece = null;
			MainInstantiation.board[activeDetector.GetComponent<DetectorData>().column,
			 						activeDetector.GetComponent<DetectorData>().row] = null;
			activeDetector.GetComponent<DetectorData>().assignedPiece = null;
			activeDetector = null;
			// Remove Board to Captured Piece
			if (assignedPiece == null) {
				assignedPiece = piece.gameObject;
			} else if (piece.gameObject != assignedPiece) {
				assignedPiece.GetComponent<PieceData>().Base = null;
				assignedPiece = piece.gameObject;
			}
			MainInstantiation.turn = !MainInstantiation.turn;
		}
  }

	void OnTriggerExit(Collider piece) {
		if (piece.GetComponent<PieceData>() == null) return;
        if (!piece.GetComponent<PieceData>().isPicked) return;
        Instantiation MainInstantiation = MainSource.GetComponent<Instantiation>();
		var pieceData = piece.GetComponent<PieceData>();
		var detectors = MainInstantiation.detectors;

		if (MainInstantiation.activePiece == null && piece.GetComponent<PieceData>().Base != null && MainInstantiation.board[column, row] == piece.gameObject) {
			if (MainInstantiation.isWhite(piece.gameObject) == "true" && MainInstantiation.turn) return;
			if (MainInstantiation.isWhite(piece.gameObject) == "false" && !MainInstantiation.turn) return;

			MainInstantiation.activePiece = piece.gameObject;
			int[,] posibleMoves = MainInstantiation.getPosibleMoves(pieceData.type, pieceData.color, column, row);

			// Check IF There's Possible Moves
			if (posibleMoves == null) {
				MainInstantiation.activePiece = null;
				return;
			}

			MainInstantiation.activeDetector = transform.gameObject;
			for (var i = 0; i < posibleMoves.GetLength(0); i++) {
				// Find Detectors Within Posible Moves
				if (MainInstantiation.numberInBoard(posibleMoves[i, 0], posibleMoves[i, 1])) {
					detectors[posibleMoves[i, 0], posibleMoves[i, 1]].GetComponent<DetectorData>().acceptsMove = true;
				}
			}
		}
  }

	void Update() {
		Instantiation MainInstantiation = MainSource.GetComponent<Instantiation>();
		if (!acceptsMove) {
			if ((row + column) % 2 != 0) {
				GetComponent<Renderer>().material = MainInstantiation.WoodTexture;
			} else {
				GetComponent<Renderer>().material = MainInstantiation.CoralTexture;
			}
		} else if (acceptsMove) {
			GetComponent<Renderer>().material = MainInstantiation.JadeTexture;
		}
	}
}
