using UnityEngine;

[System.Serializable]
public class ChessPiece
{
	public enum Type { KING, QUEEN, ROOK, BISHOP, KNIGHT, PAWN };
	public enum Color { BLACK, WHITE };

	public GameObject obj;
	public Type type;
	public Color color;
	public Vector2 position;
}
