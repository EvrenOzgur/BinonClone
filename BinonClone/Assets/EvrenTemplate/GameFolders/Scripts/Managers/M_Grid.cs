using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class M_Grid : MonoBehaviour
{
    public static Action<Vector3> OnGridSucceedControl;
    // --------- Grid ----------
    public GridItem GridItemPrefab;

    public int GridLenghtI = 10;
    public int GridLenghtJ = 10;
    public GridItem[,] GridArray;

    private void OnEnable()
    {
        M_Observer.OnGameCreate += GameCreate;
        OnGridSucceedControl += SucceedControl;
    }
    private void OnDisable()
    {
        M_Observer.OnGameCreate -= GameCreate;
        OnGridSucceedControl -= SucceedControl;

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
        List<int> _succeedScoreList = new List<int>();  
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
                _succeedScoreList.Add(i);

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
                _succeedScoreList.Add(j);

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
        if (_succeedScoreList.Count != 0)
        {
            int _scoreUp = 0;
            for (int i = 0; i < _succeedScoreList.Count; i++)
            {
                _scoreUp += (i * 10);
            }
            M_Level.OnSetScore?.Invoke(_scoreUp);
        }
        GameContinueControl();
    }
    public void GameContinueControl()
    {
        List<int> _continueControlList = new List<int>();
        int _emptySlotCount = 0;
        for (int i = 0; i < M_Piece.I.PieceSlots.Length; i++)
        {
            if (_continueControlList.Count != 0) break;
            if (M_Piece.I.PieceSlots[i].isFull)
            {
                Piece _piece = Instantiate(M_Piece.I.PieceSlots[i].CurrentPiece) ;
                for (int x = 0; x < GridLenghtI; x++)
                {
                    if (_continueControlList.Count != 0) break;

                    for (int y = 0; y < GridLenghtJ; y++)
                    {
                        if (_continueControlList.Count != 0) break;
                        _piece.transform.position = new Vector3(x,y,0);
                        int _counter = 0;
                        for (int j = 0; j < _piece.PieceChilds.Length; j++)
                        {
                            int _controlX = Mathf.RoundToInt(_piece.PieceChilds[j].transform.position.x);
                            int _controlY = Mathf.RoundToInt(_piece.PieceChilds[j].transform.position.y);
                            if (PieceInGridControl(_controlX,_controlY))
                            {
                                if (GridArray[_controlX,_controlY].IsFull == false)
                                {
                                    _counter++;
                                }
                            }
                        }
                       
                        if (_counter == _piece.PieceChilds.Length)
                        {
                            _continueControlList.Add(_counter);
                        }

                    }
                }
                Destroy(_piece.gameObject);
                
            }
            else
            {
              
                _emptySlotCount++;
            }
        }
       
        if (_emptySlotCount == M_Piece.I.PieceSlots.Length )
        {
            return;
        }
        else
        {
           
            if (_continueControlList.Count == 0)
            {
                M_Observer.OnGameFail?.Invoke();
                print("Fail");
            }
        }
       
    }
    private bool PieceInGridControl(int controlX, int controlY)
    {
        if (controlX <= GridLenghtI - 1 &&
            controlX >= 0 &&
            controlY <= GridLenghtJ - 1 &&
            controlY >= 0 
          )
        {
            return true;
        }
        return false;
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
