using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour {
	public GameObject MatrixDetector;
	public GameObject Pawn, Rook, Knight, Bishop, Queen, King;
	public Material WhiteTexture;
	public Material BlackTexture;
	public Material CoralTexture;
	public Material WoodTexture;
	public Material JadeTexture;
	public GameObject activePiece = null;
	public GameObject activeDetector = null;

	public GameObject[,] detectors;
	public GameObject[,] board;

	public GameObject[] whitePieces;
	public GameObject[] blackPieces;

	void Start () {
		detectors = new GameObject[8,8];
		whitePieces = new GameObject[16];
		blackPieces = new GameObject[16];
		board  = new GameObject[8,8];

		WhiteTexture = (Material)Resources.Load("Materials/WhiteMarble", typeof(Material));
		BlackTexture = (Material)Resources.Load("Materials/BlackMarble", typeof(Material));
		CoralTexture = (Material)Resources.Load("Materials/CoralMarble", typeof(Material));
		WoodTexture = (Material)Resources.Load("Materials/WoodMarble", typeof(Material));
		JadeTexture = (Material)Resources.Load("Materials/Jade", typeof(Material));

		placeDetectors();
		setPieces();
		placePieces();
	}

	public void placeDetectors() {
		for (int y = 0; y < 8; y++) {
			for (int x = 0; x < 8; x++) {
				var currentClone = Instantiate(MatrixDetector, new Vector3(-10.5f + x * 3, -0.45f, -10.5f + y * 3), Quaternion.identity);
				currentClone.GetComponent<DetectorData>().row = y;
				currentClone.GetComponent<DetectorData>().column = x;
				currentClone.GetComponent<DetectorData>().MainSource = this.gameObject;
				detectors[y, x] = currentClone;
			}
		}
	}

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
					board[j, i] = whitePieces[j];
					whitePieces[j].GetComponent<PieceData>().Base = detectors[i, j];
				}
				if (i == 0) {
					whitePieces[j + 8].transform.position = detectors[i, j].transform.position;
					board[j, i] = whitePieces[j + 8];
					whitePieces[j + 8].GetComponent<PieceData>().Base = detectors[i, j];
				}
				if (i == 6) {
					blackPieces[j].transform.position = detectors[i, j].transform.position;
					board[j, i] = blackPieces[j];
					blackPieces[j].GetComponent<PieceData>().Base = detectors[i, j];
				}
				if (i == 7) {
					blackPieces[j + 8].transform.position = detectors[i, j].transform.position;
					board[j, i] = blackPieces[j + 8];
					blackPieces[j + 8].GetComponent<PieceData>().Base = detectors[i, j];
				}
		} }
	}

	public void setPieceData(GameObject piece, string type, string color) {
		piece.GetComponent<PieceData>().type = type;
		piece.GetComponent<PieceData>().color = color;
		piece.GetComponent<PieceData>().MainSource = this.gameObject;
	}

	public void setPieces() {
		GameObject clone;
		for (var i = 0; i < whitePieces.GetLength(0); i++) {
			if (i < 8) {
				clone = Instantiate(Pawn, Vector3.zero, Quaternion.Euler(0, -7.5f, 0));
				setPieceData(clone, "pawn", "white");
				whitePieces[i] = clone;
			} else if (i == 8 || i == 15) {
				clone = Instantiate(Rook, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "rook", "white");
				whitePieces[i] = clone;
			} else if (i == 9 || i == 14) {
				clone = Instantiate(Knight, Vector3.zero, Quaternion.Euler(0, -90, 0));
				setPieceData(clone, "knight", "white");
				whitePieces[i] = clone;
			} else if (i == 10 || i == 13) {
				clone = Instantiate(Bishop, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "bishop", "white");
				whitePieces[i] = clone;
			} else if (i == 11) {
				clone = Instantiate(Queen, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "queen", "white");
				whitePieces[i] = clone;
			} else {
				clone = Instantiate(King, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "king", "white");
				whitePieces[i] = clone;
			}
		}

		for (var i = 0; i < blackPieces.GetLength(0); i++) {
			if (i < 8) {
				clone = Instantiate(Pawn, Vector3.zero, Quaternion.Euler(0, -7.5f, 0));
				setPieceData(clone, "pawn", "black");
				blackPieces[i] = clone;
			} else if (i == 8 || i == 15) {
				clone = Instantiate(Rook, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "rook", "black");
				blackPieces[i] = clone;
			} else if (i == 9 || i == 14) {
				clone = Instantiate(Knight, Vector3.zero, Quaternion.Euler(0, 90, 0));
				setPieceData(clone, "knight", "black");
				blackPieces[i] = clone;
			} else if (i == 10 || i == 13) {
				clone = Instantiate(Bishop, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "bishop", "black");
				blackPieces[i] = clone;
			} else if (i == 11) {
				clone = Instantiate(Queen, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "queen", "black");
				blackPieces[i] = clone;
			} else {
				clone = Instantiate(King, Vector3.zero, Quaternion.identity);
				setPieceData(clone, "king", "black");
				blackPieces[i] = clone;
			}
		}
	}

	public string isWhite(GameObject piece) {
		if (piece == null)
			return "NaN";
		if (piece.GetComponent<PieceData>().color == "white") {
			return "true";
		}
		if (piece.GetComponent<PieceData>().color == "black") {
			return "false";
		}

		return "NaN";
	}

	public string isWhite(string piece) {
		if (piece == "white") {
			return "true";
		}
		if (piece == "black") {
			return "false";
		}

		return "NaN";
	}

	public bool numberInBoard(int x, int y = 0) {
		if (x >= 0 && x < board.GetLength(0)) {
			if (y >= 0 && y < board.GetLength(1)) {
				return true;
			}
		}
		return false;
	}

	// Create All Posible Movements
	public int[,] getPosibleMoves(string type, string color, int y, int x) {
		int[,] moves = new int[1,1];
		// Movements for Black Pawn
		if (type == "pawn" && color == "white") {
			moves = new int[4,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}
			// Simple Step Foward
			if (numberInBoard(0 + y, 1 + x)) {
				if (board[0 + y, 1 + x] == null) {
					moves[0,0] = 1 + x; moves[0,1] = 0 + y;
				}
			}
			// Sided Step IF White Piece
			if (numberInBoard(1 + y, 1 + x)) {
				if (isWhite(board[1 + y, 1 + x]) == "false") {
					moves[1,0] = 1 + x; moves[1,1] = 1 + y;
				}
			}
			if (numberInBoard(-1 + y, 1 + x)) {
				if (isWhite(board[-1 + y, 1 + x]) == "false") {
					moves[2,0] = 1 + x; moves[2,1] = -1 + y;
				}
			}
			// Doble Step IF Inicial Position
			if (x == 1) {
				moves[3,0] = 2 + x; moves[3,1] = 0 + y;
			}
		}

		// Movements for White Pawn
		if (type == "pawn" && color == "black") {
			moves = new int[4,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}
			// Simple Step Foward
			if (numberInBoard(0 + y, -1 + x)) {
				if (board[0 + y, -1 + x] == null) {
					moves[0,0] = -1 + x; moves[0,1] = 0 + y;
				}
			}
			// Sided Step IF Black Piece
			if (numberInBoard(1 + y, -1 + x)) {
				if (isWhite(board[1 + y, -1 + x]) == "true") {
					moves[1,0] = -1 + x; moves[1,1] = 1 + y;
				}
			}
			if (numberInBoard(-1 + y, -1 + x)) {
				if (isWhite(board[-1 + y, -1 + x]) == "true") {
					moves[2,0] = -1 + x; moves[2,1] = -1 + y;
				}
			}
			// Doble Step IF Inicial Position
			if (x == 6) {
				moves[3,0] = -2 + x; moves[3,1] = 0 + y;
			}
		}

		// Movements for Rook
		if (type == "rook") {
			moves = new int[16,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}

			// Make Moves in X Axis
			// Make Moves From the Piece to the Top
			for (var i = x; i >= 0; i--) {
				if (i == x) continue;
				if (isWhite(board[y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[y, i]) != isWhite(color)
							&& isWhite(board[y, i]) != "NaN") {
						moves[i,0] = i; moves[i,1] = y;
						break;
					}
					moves[i,0] = i; moves[i,1] = y;
				}
			}
			// Make Moves From the Piece to the Bottom
			for (var i = x; i < 8; i++) {
				if (i == x) continue;
				if (isWhite(board[y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[y, i]) != isWhite(color)
							&& isWhite(board[y, i]) != "NaN") {
						moves[i,0] = i; moves[i,1] = y;
						break;
					}
					moves[i,0] = i; moves[i,1] = y;
				}
			}

			// Make Moves in Y Axis
			// Make Moves From the Piece to the Left
			for (var i = y; i >= 0; i--) {
				if (i == y) continue;
				if (isWhite(board[i, x]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i, x]) != isWhite(color)
							&& isWhite(board[i, x]) != "NaN") {
						moves[i+8,0] = x; moves[i+8,1] = i;
						break;
					}
					moves[i+8,0] = x; moves[i+8,1] = i;
				}
			}
			// Make Moves From the Piece to the Rigth
			for (var i = y; i < 8; i++) {
				if (i == y) continue;
				if (isWhite(board[i, x]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i, x]) != isWhite(color)
							&& isWhite(board[i, x]) != "NaN") {
						moves[i+8,0] = x; moves[i+8,1] = i;
						break;
					}
					moves[i+8,0] = x; moves[i+8,1] = i;
				}
			}
		}

		// Movements for Knight
		if (type == "knight") {
			moves = new int[8,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}

			// Set Posible Positions
			if (numberInBoard(-2 + y, 1 + x)) {
				if (isWhite(board[-2 + y, 1 + x]) != isWhite(color)) {
					moves[0,0] = 1 + x; moves[0,1] = -2 + y;
				}
			}
			if (numberInBoard(2 + y, 1 + x)) {
				if (isWhite(board[2 + y, 1 + x]) != isWhite(color)) {
					moves[1,0] = 1 + x; moves[1,1] = 2 + y;
				}
			}
			if (numberInBoard(-2 + y, -1 + x)) {
				if (isWhite(board[-2 + y, -1 + x]) != isWhite(color)) {
					moves[2,0] = -1 + x; moves[2,1] = -2 + y;
				}
			}
			if (numberInBoard(2 + y, -1 + x)) {
				if (isWhite(board[2 + y, -1 + x]) != isWhite(color)) {
					moves[3,0] = -1 + x; moves[3,1] = 2 + y;
				}
			}
			if (numberInBoard(1 + y, -2 + x)) {
				if (isWhite(board[1 + y, -2 + x]) != isWhite(color)) {
					moves[4,0] = -2 + x; moves[4,1] = 1 + y;
				}
			}
			if (numberInBoard(1 + y, 2 + x)) {
				if (isWhite(board[1 + y, 2 + x]) != isWhite(color)) {
					moves[5,0] = 2 + x; moves[5,1] = 1 + y;
				}
			}
			if (numberInBoard(-1 + y, -2 + x)) {
				if (isWhite(board[-1 + y, -2 + x]) != isWhite(color)) {
					moves[6,0] = -2 + x; moves[6,1] = -1 + y;
				}
			}
			if (numberInBoard(-1 + y, 2 + x)) {
				if (isWhite(board[-1 + y, 2 + x]) != isWhite(color)) {
					moves[7,0] = 2 + x; moves[7,1] = -1 + y;
				}
			}
		}

		// Movements for Bishop
		if (type == "bishop") {
			moves = new int[32,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}

			// Make Moves From the Piece to the Negative Bottom
			for (var i = x; i >= 0; i--) {
				if (i == x) continue;
				if (!numberInBoard(i, x - i + y)) continue;
				if (isWhite(board[x - i + y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[x - i + y, i]) != isWhite(color)
							&& isWhite(board[x - i + y, i]) != "NaN") {
						moves[i,0] = i; moves[i,1] = x - i + y;
						break;
					}
					moves[i,0] = i; moves[i,1] = x - i + y;
				}
			}
			// Make Moves From the Piece to the Positive Bottom
			for (var i = y; i >= 0; i--) {
				if (i == y) continue;
				if (!numberInBoard(y - i + x, i)) continue;
				if (isWhite(board[i, y - i + x]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i, y - i + x]) != isWhite(color)
							&& isWhite(board[i, y - i + x]) != "NaN") {
						moves[i+16,0] = y - i + x; moves[i+16,1] = i;
						break;
					}
					moves[i+16,0] = y - i + x; moves[i+16,1] = i;
				}
			}
			// Make Moves From the Piece to the Positive Top
			for (var i = x; i < 8; i++) {
				if (i == x) continue;
				if (!numberInBoard(i, i - x + y)) continue;
				if (isWhite(board[i - x + y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i - x + y, i]) != isWhite(color)
							&& isWhite(board[i - x + y, i]) != "NaN") {
						moves[i+8,0] = i; moves[i+8,1] = i - x + y;
						break;
					}
					moves[i+8,0] = i; moves[i+8,1] = i - x + y;
				}
			}
			// Make Moves From the Piece to the Negative Top
			for (var i = x; i >= 0; i--) {
				if (i == x) continue;
				if (!numberInBoard(i, i - x + y)) continue;
				if (isWhite(board[i - x + y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i - x + y, i]) != isWhite(color)
							&& isWhite(board[i - x + y, i]) != "NaN") {
						moves[i+24,0] = i; moves[i+24,1] = i - x + y;
						break;
					}
					moves[i+24,0] = i; moves[i+24,1] = i - x + y;
				}
			}
		}

		// Movements for Queen
		if (type == "queen") {
			moves = new int[48,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}

			// Make Moves From the Piece to the Negative Bottom
			for (var i = x; i >= 0; i--) {
				if (i == x) continue;
				if (!numberInBoard(i, x - i + y)) continue;
				if (isWhite(board[x - i + y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[x - i + y, i]) != isWhite(color)
							&& isWhite(board[x - i + y, i]) != "NaN") {
						moves[i,0] = i; moves[i,1] = x - i + y;
						break;
					}
					moves[i,0] = i; moves[i,1] = x - i + y;
				}
			}
			// Make Moves From the Piece to the Positive Bottom
			for (var i = y; i >= 0; i--) {
				if (i == y) continue;
				if (!numberInBoard(y - i + x, i)) continue;
				if (isWhite(board[i, y - i + x]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i, y - i + x]) != isWhite(color)
							&& isWhite(board[i, y - i + x]) != "NaN") {
						moves[i+16,0] = y - i + x; moves[i+16,1] = i;
						break;
					}
					moves[i+16,0] = y - i + x; moves[i+16,1] = i;
				}
			}
			// Make Moves From the Piece to the Positive Top
			for (var i = x; i < 8; i++) {
				if (i == x) continue;
				if (!numberInBoard(i, i - x + y)) continue;
				if (isWhite(board[i - x + y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i - x + y, i]) != isWhite(color)
							&& isWhite(board[i - x + y, i]) != "NaN") {
						moves[i+8,0] = i; moves[i+8,1] = i - x + y;
						break;
					}
					moves[i+8,0] = i; moves[i+8,1] = i - x + y;
				}
			}
			// Make Moves From the Piece to the Negative Top
			for (var i = x; i >= 0; i--) {
				if (i == x) continue;
				if (!numberInBoard(i, i - x + y)) continue;
				if (isWhite(board[i - x + y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i - x + y, i]) != isWhite(color)
							&& isWhite(board[i - x + y, i]) != "NaN") {
						moves[i+24,0] = i; moves[i+24,1] = i - x + y;
						break;
					}
					moves[i+24,0] = i; moves[i+24,1] = i - x + y;
				}
			}

			// Make Moves in X Axis
			// Make Moves From the Piece to the Top
			for (var i = x; i >= 0; i--) {
				if (i == x) continue;
				if (isWhite(board[y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[y, i]) != isWhite(color)
							&& isWhite(board[y, i]) != "NaN") {
						moves[i+32,0] = i; moves[i+32,1] = y;
						break;
					}
					moves[i+32,0] = i; moves[i+32,1] = y;
				}
			}
			// Make Moves From the Piece to the Bottom
			for (var i = x; i < 8; i++) {
				if (i == x) continue;
				if (isWhite(board[y, i]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[y, i]) != isWhite(color)
							&& isWhite(board[y, i]) != "NaN") {
						moves[i+32,0] = i; moves[i+32,1] = y;
						break;
					}
					moves[i+32,0] = i; moves[i+32,1] = y;
				}
			}

			// Make Moves in Y Axis
			// Make Moves From the Piece to the Left
			for (var i = y; i >= 0; i--) {
				if (i == y) continue;
				if (isWhite(board[i, x]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i, x]) != isWhite(color)
							&& isWhite(board[i, x]) != "NaN") {
						moves[i+40,0] = x; moves[i+40,1] = i;
						break;
					}
					moves[i+40,0] = x; moves[i+40,1] = i;
				}
			}
			// Make Moves From the Piece to the Rigth
			for (var i = y; i < 8; i++) {
				if (i == y) continue;
				if (isWhite(board[i, x]) == isWhite(color)) {
					break;
				} else {
					if (isWhite(board[i, x]) != isWhite(color)
							&& isWhite(board[i, x]) != "NaN") {
						moves[i+40,0] = x; moves[i+40,1] = i;
						break;
					}
					moves[i+40,0] = x; moves[i+40,1] = i;
				}
			}
		}

		// Movements for King
		if (type == "king") {
			moves = new int[12,2];
			// Set Position of All Movents to -1. -1
			for (var i = 0; i < moves.GetLength(0); i++) {
				moves[i,0] = -1; moves[i,1] = -1;
			}

			for (var i = -1; i < 2; i++) {
				for (var j = -1; j < 2; j++) {
					if (numberInBoard(i + y, j + x)) {
						if (isWhite(board[i + y, j + x]) != isWhite(color)) {
							moves[(i+1)*3 + j+1,0] = j + x;
							moves[(i+1)*3 + j+1,1] = i + y;
					} }
			} }
		}

		if (arrayForAll(moves, -1)) {
			return null;
		} else {
			return moves;
		}
	}

	private bool arrayForAll(int[,] array, int val) {
		bool isNull = true;
		for (int i = 0; i < array.GetLength(0); i++) {
			for (int j = 0; j < array.GetLength(1); j++) {
				if (array[i, j] != val) isNull = false;
			}
		}
    return isNull;
  }
}
