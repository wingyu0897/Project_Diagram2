using UnityEngine;

public static class BoundaryCalculator
{
	#region Camera Extensions
	public static bool IsInBound(this Camera camera, Vector2 point)
	{
		return camera.GetBounds().Contains(point);
	}

	public static Bounds GetBounds(this Camera camera)
	{
		float vertical = camera.orthographicSize;
		float horizontal = vertical * camera.aspect;

		Bounds bound = new Bounds(camera.transform.position, new Vector3(horizontal, vertical) * 2f);
		return bound;
	}
	#endregion

	#region Bounds Extensions
	public static Vector2 GetRandomPoint(this Bounds bounds, Vector2 marginSize = default(Vector2))
	{
		return new Vector2(Random.Range(bounds.min.x + marginSize.x, bounds.max.x - marginSize.x), Random.Range(bounds.min.y + marginSize.y, bounds.max.y - marginSize.y));
	}
	#endregion
}
