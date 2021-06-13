using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public const int boardSize = 8;
	public const float tileDistance = 1.5f;

	public List<ChessPiece> pieces = new List<ChessPiece>();

	private InputManager inputManager;
	private Camera mainCamera;

	private ChessPiece selected = null;

	private int [,] board = new int[boardSize, boardSize];

	// Debug output for now
	private int[,] movement = null;

	private void Awake()
	{
		inputManager = InputManager.Instance;
		mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		inputManager.OnPressDown += PressDown;
	}
	
	private void OnDisable()
	{
		inputManager.OnPressDown -= PressDown;
	}

	// User selects tile to pick-up or place piece
	public void PressDown(Vector2 pos)
	{
		Ray ray = mainCamera.ScreenPointToRay(pos);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{
			Debug.DrawLine(ray.origin, hit.point, Color.red, 5);
			Vector2Int gridPosition = GridHelper.LocalToGrid(transform.position - hit.point, tileDistance);


			// Might look into a better way to store and search. (Only 32 pieces so not urgent)
			//ChessPiece nextSelected = pieces.Find(x => x.position == gridPosition);

			ChessPiece nextSelected = null;
			int index = board[gridPosition.x, gridPosition.y]; // Is in bounds check? Its own function?
			if (index != -1)
				nextSelected = pieces[index];


			// Proof of concept movment (subject to change)
			if (nextSelected == null)
			{
				if(selected != null)
				{
					// Would like to make the grid position update game objects transform by itself in the future.

					int oldIndex = board[selected.position.x, selected.position.y];
					board[selected.position.x, selected.position.y] = -1;
					board[gridPosition.x, gridPosition.y] = oldIndex;

					selected.position = gridPosition;
					selected.gameObject.transform.localPosition = GridHelper.GridToLocal(gridPosition, tileDistance);

					selected = null;
					movement = null;
				}
			}
			else
			{
				selected = nextSelected;

				// Temp debug arr
				movement = selected.Evaluator(board, boardSize);
			}

		}
	}

	// Start is called before the first frame update
	void Start()
	{
		// Test for evaluation
		pieces.Add(new Pawn(new Vector2Int(0, 1), ChessPiece.PieceColor.WHITE));
		pieces.Add(new Pawn(new Vector2Int(1, 1), ChessPiece.PieceColor.WHITE));

		pieces.Add(new Pawn(new Vector2Int(0, 6), ChessPiece.PieceColor.BLACK));
		pieces.Add(new Pawn(new Vector2Int(1, 6), ChessPiece.PieceColor.BLACK));

		// -1 = blank, else index of piece.
		for (int x = 0; x < boardSize; x++)
		{
			for (int y = 0; y < boardSize; y++)
			{
				board[x, y] = -1;
			}
		}

		for(int i = 0; i < pieces.Count; i++)
		{
			pieces[i].gameObject = GameObject.Instantiate(pieces[i].gameObject, transform, false);
			pieces[i].gameObject.transform.localPosition = GridHelper.GridToLocal(pieces[i].position, tileDistance);
			board[pieces[i].position.x, pieces[i].position.y] = i;
		}
	}

	// Update is called once per frame
	void Update()
	{
	}

	// Display the tiles
	private void OnDrawGizmos()
	{
		for (int x = 0; x < boardSize; x++)
		{
			for (int y = 0; y < boardSize; y++)
			{
				// Selected tile in red / movement yellow
				if( movement != null && movement[x,y] == 1)
				{
					Gizmos.color = Color.yellow;
				}
				else if (selected != null)
				{
					if (selected.position.x == x && selected.position.y == y)
					{
						Gizmos.color = Color.red;
					}
					else
					{
						Gizmos.color = Color.white;
					}
				}
				else
				{
					Gizmos.color = Color.white;
				}

				// Quick and dirty tile gizmos display
				Gizmos.DrawCube(transform.position + GridHelper.GridToLocal(new Vector2Int(x,y), tileDistance), new Vector3(1.5f, 0.1f, 1.5f));
			}
		}

	}
}
