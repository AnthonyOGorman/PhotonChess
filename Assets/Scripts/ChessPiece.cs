using UnityEngine;

[System.Serializable]
public abstract class ChessPiece
{
	public enum PieceType { KING, QUEEN, ROOK, BISHOP, KNIGHT, PAWN };
	public enum PieceColor { BLACK, WHITE };

	public GameObject gameObject;
	public abstract PieceType Type { get; }
	public PieceColor Color { get; protected set; }
	public Vector2Int position;

	public abstract int[,] Evaluator(int[,] board, int boardSize);
}
