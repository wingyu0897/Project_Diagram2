using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CellManager _cellSpawner;

    private void Awake()
    {
        _cellSpawner = GetComponent<CellManager>();

        InitializeGame();
    }

    private void InitializeGame()
    {
        _cellSpawner.Initialize();

        Debug.Log("Initialized");
    }
}
