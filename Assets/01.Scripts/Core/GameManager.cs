using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CellSpawner _cellSpawner;

    private void Awake()
    {
        _cellSpawner = GetComponent<CellSpawner>();

        InitializeGame();
    }

    private void InitializeGame()
    {
        _cellSpawner.Initialize();

        Debug.Log("Initialized");
    }
}
