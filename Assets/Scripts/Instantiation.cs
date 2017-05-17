using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour {
	public GameObject MatrixDetector;
	public Material WhiteTexture;
	public Material BlackTexture;
	public GameObject Pawn, Rook, Knight, Bishop, Queen, King;

	public GameObject[,] detectors;
	public GameObject[,] board;

	public GameObject[] whitePieces;
	public GameObject[] blackPieces;

	void Start () {
		detectors = new GameObject[8,8];
		whitePieces = new GameObject[16];
		blackPieces = new GameObject[16];
		board  = new GameObject[8,8];

		placeDetectors();
		setPieces();
		placePieces();
	}

	public void placeDetectors() {
		for (int y = 0; y < 8; y++) {
			for (int x = 0; x < 8; x++) {
				var currentClone = Instantiate(MatrixDetector, new Vector3(-10.5f + x * 3, -0.35f, -10.5f + y * 3), Quaternion.identity);
				currentClone.GetComponent<MetaData>().row = y;
				currentClone.GetComponent<MetaData>().column = x;
				detectors[y, x] = currentClone;
		}}
	}

	// public static void printBoard() {
	// 	// Bool to Switch Grid Color
	// 	bool placeBlack = false;
	// 	for (var i = 0; i < board.GetLength(0); i++) {
	// 		for (var j = 0; j < board.GetLength(1); j++) {
	// 			// Set Black and White Grid When There's NO Pieces
	// 			if (placeBlack && board[i, j] == null) {
	// 			} else if (board[i, j] == null) {
	// 			} else {
	// 				board[i, j].;
	// 			}
	// 				placeBlack = !placeBlack;
	// 		}
	// 		placeBlack = !placeBlack;
	// 	}
	// }

	public void placePieces() {
		// Clean Board
		for (var i = 0; i < board.GetLength(0); i++) {
			for (var j = 0; j < board.GetLength(1); j++) {
				board[i, j] = null;
		} }

		// Place Pieces
		for (var i = 0; i < board.GetLength(0); i++) {
			for (var j = 0; j < board.GetLength(1); j++) {
				if (i == 1) {
					whitePieces[j].transform.position = detectors[i, j].transform.position;
					board[i, j] = whitePieces[j];
				}
				if (i == 0) {
					whitePieces[j + 8].transform.position = detectors[i, j].transform.position;
					board[i, j] = whitePieces[j + 8];
				}
				if (i == 6) {
					blackPieces[j].transform.position = detectors[i, j].transform.position;
					board[i, j] = blackPieces[j];
				}
				if (i == 7) {
					blackPieces[j + 8].transform.position = detectors[i, j].transform.position;
					board[i, j] = blackPieces[j + 8];
				}
		} }
	}

	public void setPieces() {
		GameObject clone;
		for (var i = 0; i < whitePieces.GetLength(0); i++) {
			if (i < 8) {
				clone = Instantiate(Pawn, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = WhiteTexture;
				whitePieces[i] = clone;
			} else if (i == 8 || i == 15) {
				clone = Instantiate(Rook, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = WhiteTexture;
				whitePieces[i] = clone;
			} else if (i == 9 || i == 14) {
				clone = Instantiate(Knight, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = WhiteTexture;
				whitePieces[i] = clone;
			} else if (i == 10 || i == 13) {
				clone = Instantiate(Bishop, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = WhiteTexture;
				whitePieces[i] = clone;
			} else if (i == 11) {
				clone = Instantiate(Queen, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = WhiteTexture;
				whitePieces[i] = clone;
			} else {
				clone = Instantiate(King, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = WhiteTexture;
				whitePieces[i] = clone;
			}
		}

		for (var i = 0; i < blackPieces.GetLength(0); i++) {
			if (i < 8) {
				clone = Instantiate(Pawn, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = BlackTexture;
				blackPieces[i] = clone;
			} else if (i == 8 || i == 15) {
				clone = Instantiate(Rook, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = BlackTexture;
				blackPieces[i] = clone;
			} else if (i == 9 || i == 14) {
				clone = Instantiate(Knight, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = BlackTexture;
				blackPieces[i] = clone;
			} else if (i == 10 || i == 13) {
				clone = Instantiate(Bishop, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = BlackTexture;
				blackPieces[i] = clone;
			} else if (i == 11) {
				clone = Instantiate(Queen, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = BlackTexture;
				blackPieces[i] = clone;
			} else {
				clone = Instantiate(King, new Vector3(0,0,0), Quaternion.identity);
				clone.GetComponent<Renderer>().material = BlackTexture;
				blackPieces[i] = clone;
			}
		}
	}
}
