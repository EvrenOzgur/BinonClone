using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void SpawnPieces()
    {
        for (int i = 0; i < PieceSlots.Length; i++)
        {
            int _randomPieceIndex = Random.Range(0,PiecePrefabs.Length);
            Piece _piece = Instantiate(PiecePrefabs[_randomPieceIndex] , PieceSlots[i].transform);
            _piece.transform.localPosition = Vector3.zero;
            PieceSlots[i].CurrentPiece = _piece;
        }
    }
}