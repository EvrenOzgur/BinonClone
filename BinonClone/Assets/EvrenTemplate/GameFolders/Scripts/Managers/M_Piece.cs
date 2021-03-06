using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class M_Piece: MonoBehaviour
{
    public PieceSlot[] PieceSlots;
    public Piece[] PiecePrefabs;
    private void OnEnable()
    {
        M_Observer.OnGameStart += GameStart;
    }
    private void OnDisable()
    {
        M_Observer.OnGameStart -= GameStart;

    }

   
    private void GameStart()
    {
        SpawnPieces();
    }

    private void Start()
    {
        
    }
    public void PieceSlotsControl()
    {
        bool _slotsEmpty = false;
        for (int i = 0; i < PieceSlots.Length; i++)
        {
            if (!PieceSlots[i].isFull)
            {
                _slotsEmpty = true;
            }
            else
            {
                _slotsEmpty = false;
                break;
            }
        }
        if (_slotsEmpty)
        {
            SpawnPieces();
        }

    }
    private void SpawnPieces()
    {
        for (int i = 0; i < PieceSlots.Length; i++)
        {
              int _randomPieceIndex = Random.Range(0,PiecePrefabs.Length);

            Piece _piece = Instantiate(PiecePrefabs[_randomPieceIndex] , PieceSlots[i].transform);
            _piece.transform.localPosition = PieceSlots[i].transform.localPosition + new Vector3(30,0,0);
            _piece.transform.DOLocalMove(Vector3.zero , 0.25f).SetEase(Ease.OutExpo);
            PieceSlots[i].CurrentPiece = _piece;
            PieceSlots[i].isFull = true;
        }
    }
    public static M_Piece II;

    public static M_Piece I
    {
        get
        {
            if (II == null)
            {
                GameObject _g = GameObject.Find("M_Piece");
                if (_g != null)
                {
                    II = _g.GetComponent<M_Piece>();
                }
            }

            return II;
        }
    }
}
