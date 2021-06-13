
using UnityEngine;

public class GridHelper
{
	public static Vector3 GridToLocal(Vector2Int gridPosition, float tileDistance)
	{
		Vector3 vecOut = new Vector3();
		vecOut.x = gridPosition.x * tileDistance + tileDistance / 2;
		vecOut.z = gridPosition.y * tileDistance + tileDistance / 2;
		return vecOut;
	}

	public static Vector2Int LocalToGrid(Vector3 localPosition, float tileDistance)
	{
		Vector2Int vecOut = new Vector2Int();
		vecOut.x = (int)Mathf.Floor(Mathf.Abs(localPosition.x) / tileDistance);
		vecOut.y = (int)Mathf.Floor(Mathf.Abs(localPosition.z) / tileDistance);
		return vecOut;
	}
}
