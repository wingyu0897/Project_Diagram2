using UnityEngine;

public class CellSpawner : MonoSingleton<CellSpawner>
{
    [SerializeField] private int _maxCellCount;
	[SerializeField] private GameObject testCellPrefab;

	private int _cellCount = 0;

	private void Awake()
	{
		CreateCells(_maxCellCount);
	}

    private void CreateCells(int count)
	{
		Bounds bound = Camera.main.GetBounds();
		Vector2 position;

		for (int i = 0; i < count; ++i)
		{
			position = bound.GetRandomPoint();
			Instantiate(testCellPrefab, position, Quaternion.identity);
			_cellCount++;
		}
	}

	public void ModifyCellCount(int count)
	{
		_cellCount += count;
		_cellCount = Mathf.Max(_cellCount, 0);

		if (_cellCount < _maxCellCount)
			CreateCells(_maxCellCount - _cellCount);
	}
}
