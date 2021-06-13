using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public const int boardX = 8;
	public const int boardY = 8;
	public const float tileDistance = 1.5f;

	public List<ChessPiece> pieces;

	private InputManager inputManager;
	private Camera mainCamera;

	private ChessPiece selected = null;

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
			Vector2 gridPosition = GridHelper.LocalToGrid(transform.position - hit.point, tileDistance);


			// Might look into a better way to store and search. (Only 32 pieces so not urgent)
			ChessPiece nextSelected = pieces.Find(x => x.position == gridPosition);

			// Proof of concept movment (subject to change)
			if(nextSelected == null)
			{
				if(selected != null)
				{
					// Would like to make the grid position update game objects transform by itself in the future.
					selected.position = gridPosition;
					selected.obj.transform.localPosition = GridHelper.GridToLocal(gridPosition, tileDistance);
				}
			}
			else
			{
				selected = nextSelected;
			}

		}
	}

	// Start is called before the first frame update
	void Start()
	{
		foreach( ChessPiece piece in pieces )
		{
			piece.obj = GameObject.Instantiate(piece.obj, transform, false);
			piece.obj.transform.localPosition = GridHelper.GridToLocal(piece.position, tileDistance);
		}
	}

	// Update is called once per frame
	void Update()
	{
	}

	// Display the tiles
	private void OnDrawGizmos()
	{
		for (int x = 0; x < boardX; x++)
		{
			for (int y = 0; y < boardY; y++)
			{
				// Selected tile in red
				if( selected != null )
				{
					if (selected.position.x == x && selected.position.y == y)
						Gizmos.color = Color.red;
					else
						Gizmos.color = Color.white;
				}

				// Quick and dirty tile gizmos display
				Gizmos.DrawWireCube(transform.position + GridHelper.GridToLocal(new Vector2(x,y), tileDistance), new Vector3(1.5f, 0.1f, 1.5f));
			}
		}

	}
}
