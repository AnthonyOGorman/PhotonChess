using UnityEngine;

[System.Serializable]
public class Pawn : ChessPiece
{
	public Pawn(Vector2Int pos, PieceColor color)
	{
		position = pos;
		Color = color;
		gameObject = Resources.Load("Prefabs/" + (color == PieceColor.BLACK ? "Black" : "White") + " Pawn", typeof(GameObject)) as GameObject;
	}

	public override PieceType Type => PieceType.PAWN;

	public override int[,] Evaluator(int [,] board, int boardSize) 
	{
		// Not accounting for first move yet.
		Vector2Int[] vector2s = new Vector2Int[]
		{
			new Vector2Int(-1,1),
			new Vector2Int(0,1),
			new Vector2Int(1,1)
		};

		int[,] eval = new int[boardSize,boardSize];

		for( int i = 0; i < vector2s.Length; i++)
		{
			// Can base it off player's forward in the future
			var forward = new Vector2Int(1, 1);
			if (Color == PieceColor.BLACK)
				forward = new Vector2Int(-1, -1);

			var cellPos = (vector2s[i] * forward) + position;
			var x = cellPos.x;
			var y = cellPos.y;

			if ((cellPos.x >= 0 && cellPos.x < boardSize) && (cellPos.y >= 0 && cellPos.y < boardSize))
			{
				eval[cellPos.x, cellPos.y] = 1;
			}
		}

		return eval;
	}
}
