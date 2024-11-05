using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] private int cellCount;
	[SerializeField] private GameObject testCellPrefab;

	private void Awake()
	{
		CreateCells();
	}

	[ContextMenu("Create Cells")]
    private void CreateCells()
	{
		Bounds bound = Camera.main.GetBounds();
		Vector2 position;

		for (int i = 0; i < cellCount; ++i)
		{
			position = bound.GetRandomPoint();
			Instantiate(testCellPrefab, position, Quaternion.identity);
		}
	}
}
