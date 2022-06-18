using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class M_Grid : MonoBehaviour
{
    // --------- Grid ----------
    public GridItem GridItemPrefab;

    public int GridLenghtI = 10;
    public int GridLenghtJ = 10;
    public GridItem[,] GridArray;

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
                GridItem _gridItem = Instantiate(GridItemPrefab, transform);
                _gridItem.transform.position = new Vector3(i, j, 0.1f);
                _gridItem.IndexI = i;
                _gridItem.IndexJ = j;
                GridArray[i, j] = _gridItem;
            }
        }
    }
    public void SucceedControl(Vector3 fingerupPos)
    {
        int _succeedControlI = 0;
        int _succeedControlJ = 0;
        List<GridItem> _deleteGridItem = new List<GridItem>();
        for (int i = 0; i < GridLenghtI; i++)
        {
            for (int j = 0; j < GridLenghtJ; j++)
            {
                if (GridArray[i, j].IsFull)
                {
                    _succeedControlJ++;
                }
            }
            if (_succeedControlJ == GridLenghtJ)
            {
                for (int j = 0; j < GridLenghtJ; j++)
                {
                    if (_deleteGridItem.Contains(GridArray[i, j]) == false)
                    {
                        _deleteGridItem.Add(GridArray[i, j]);

                    }
                }
            }
            _succeedControlJ = 0;

        }
        for (int j = 0; j < GridLenghtJ; j++)
        {
            for (int i = 0; i < GridLenghtI; i++)
            {
                if (GridArray[i, j].IsFull)
                {
                    _succeedControlI++;
                }
            }
            if (_succeedControlI == GridLenghtI)
            {
                for (int i = 0; i < GridLenghtI; i++)
                {
                    if (_deleteGridItem.Contains(GridArray[i, j]) == false)
                    {
                        _deleteGridItem.Add(GridArray[i, j]);

                    }
                }
            }
            _succeedControlI = 0;
        }
        if (_deleteGridItem.Count != 0)
        {
            _deleteGridItem = _deleteGridItem.OrderBy(qq => Vector3.Distance(qq.transform.position, fingerupPos)).ToList();
            for (int i = 0; i < _deleteGridItem.Count; i++)
            {
                float _delayTime =( 1f * i / _deleteGridItem.Count) /2f;
                GridItem _gridItem = _deleteGridItem[i];
                PieceChild _pieceChild = _gridItem.CurrentPieceChild;
                _pieceChild.transform.DOScale(new Vector3(0, 0, 1), 0.25f).SetDelay(_delayTime);
                _pieceChild.transform.DORotate(new Vector3(0, 0, 720), 0.25f, RotateMode.FastBeyond360).SetDelay(_delayTime);
                _gridItem.IsFull = false;
                _gridItem.CurrentPieceChild = null;

            }
        }
    }

    public static M_Grid II;

    public static M_Grid I
    {
        get
        {
            if (II == null)
            {
                GameObject _g = GameObject.Find("M_Grid");
                if (_g != null)
                {
                    II = _g.GetComponent<M_Grid>();
                }
            }

            return II;
        }
    }
}
