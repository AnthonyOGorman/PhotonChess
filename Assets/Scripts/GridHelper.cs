
using UnityEngine;

public class GridHelper
{
	public static Vector3 GridToLocal(Vector2 gridPosition, float tileDistance)
	{
		Vector3 vecOut = new Vector3();
		vecOut.x = gridPosition.x * tileDistance + tileDistance / 2;
		vecOut.z = gridPosition.y * tileDistance + tileDistance / 2;
		return vecOut;
	}

	public static Vector2 LocalToGrid(Vector3 localPosition, float tileDistance)
	{
		Vector2 vecOut = new Vector2();
		vecOut.x = Mathf.Floor(Mathf.Abs(localPosition.x) / tileDistance);
		vecOut.y = Mathf.Floor(Mathf.Abs(localPosition.z) / tileDistance);
		return vecOut;
	}
}
