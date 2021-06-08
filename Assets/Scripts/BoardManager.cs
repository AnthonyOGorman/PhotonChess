using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public int boardX = 8;
	public int boardY = 8;
	public float tileDistance = 1.5f;

	private InputManager inputManager;
	private Camera mainCamera;

	private Vector2 selected = Vector3.zero;

	private void Awake()
	{
		inputManager = InputManager.Instance;
		mainCamera = Camera.main;
	}

	private void OnEnable()
	{
		inputManager.OnMouseClickEvent += Click;
	}
	
	private void OnDisable()
	{
		inputManager.OnMouseClickEvent -= Click;
	}

	// Basic test to grab tile position
	public void Click(Vector2 pos)
	{
		Ray ray = mainCamera.ScreenPointToRay(pos);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{
			Debug.DrawLine(ray.origin, hit.point, Color.red, 5);

			// Will make some sort of grid manager / helper functions later.
			selected.x = Mathf.Floor(Mathf.Abs((transform.position.x - hit.point.x) / tileDistance));
			selected.y = Mathf.Floor(Mathf.Abs((transform.position.z - hit.point.z) / tileDistance));
		}
	}

	// Start is called before the first frame update
	void Start()
	{
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
				if (selected.x == x && selected.y == y)
					Gizmos.color = Color.red;
				else
					Gizmos.color = Color.white;

				// Quick and dirty tile display
				Gizmos.DrawWireCube(transform.position + new Vector3(x * tileDistance + tileDistance / 2, 0f, y * tileDistance + tileDistance / 2), new Vector3(1.5f, 0.1f, 1.5f));
			}
		}

	}
}
