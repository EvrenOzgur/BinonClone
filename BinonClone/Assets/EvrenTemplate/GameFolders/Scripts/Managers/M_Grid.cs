using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Grid : MonoBehaviour
{
    // --------- Grid ----------
    public GridItem GridItemPrefab;

    [HideInInspector] public int GridLenghtI = 10;
    [HideInInspector] public int GridLenghtJ = 10;
    GridItem[,] GridArray;

    private void OnEnable()
    {
        M_Observer.OnGameCreate += GameCreate;
    }
    private void OnDisable()
    {
        M_Observer.OnGameCreate -= GameCreate;

    }

    private void GameCreate()
    {
        GridCreate();
    }

    private void GridCreate()
    {
        GridArray = new GridItem[GridLenghtI, GridLenghtJ];
        for (int i = 0; i < GridLenghtI; i++)
        {
            for (int j = 0; j < GridLenghtJ; j++)
            {
                GridItem _gridItem = Instantiate(GridItemPrefab , transform);
                _gridItem.transform.position = new Vector3(i,j,0);
                _gridItem.IndexI = i;
                _gridItem.IndexJ = j;
                GridArray[i, j] = _gridItem;
            }
        }
    }
}
