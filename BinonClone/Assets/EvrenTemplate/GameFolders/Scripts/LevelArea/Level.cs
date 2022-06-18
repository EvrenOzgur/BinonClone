using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    GameObject pickedObject;
    PieceSlot currentPieceSlot;
    Piece currentPiece;
    bool currentPieceCanMove;
  
    private void OnEnable()
    {
        M_Observer.OnGameCreate += GameCreate;
        M_Observer.OnGameStart += GameStart;
        M_Observer.OnGameReady += GameReady;
        M_Observer.OnGamePause += GamePause;
        M_Observer.OnGameContinue += GameContinue;
        M_Observer.OnGameFail += GameFail;
        M_Observer.OnGameComplete += GameComplete;
        M_Observer.OnGameRetry += GameRetry;
        M_Observer.OnGameNextLevel += GameNextLevel;
        FingerGestures.OnFingerDown += FingerGestures_OnFingerDown;
        FingerGestures.OnFingerMove += FingerGestures_OnFingerMove;
        FingerGestures.OnFingerUp += FingerGestures_OnFingerUp;



    }
    private void OnDisable()
    {
        M_Observer.OnGameCreate -= GameCreate;
        M_Observer.OnGameStart -= GameStart;
        M_Observer.OnGameReady -= GameReady;
        M_Observer.OnGamePause -= GamePause;
        M_Observer.OnGameContinue -= GameContinue;
        M_Observer.OnGameFail -= GameFail;
        M_Observer.OnGameComplete -= GameComplete;
        M_Observer.OnGameRetry -= GameRetry;
        M_Observer.OnGameNextLevel -= GameNextLevel;
        FingerGestures.OnFingerDown -= FingerGestures_OnFingerDown;
        FingerGestures.OnFingerMove -= FingerGestures_OnFingerMove;
        FingerGestures.OnFingerUp -= FingerGestures_OnFingerUp;

    }

   

  

  

    private void GameCreate()
    {
        print("GameStart");
    }

    private void GameStart()
    {
        print("GameStart");
    }
    private void GameReady()
    {
        print("GameReady");
    }
    private void GamePause()
    {
        print("GamePause");
    }
    private void GameContinue()
    {
        print("GameContinue");
    }
    private void GameFail()
    {
        print("GameFail");
    }
    private void GameComplete()
    {
        print("GameComplete");
    }
    private void GameRetry()
    {
        print("GameRetry");
    }
    private void GameNextLevel()
    {
        print("GameNextLevel");
    }



    //// ----------------------------- GamePlay----------------------------
    private void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        if (fingerIndex == 0)
        {
            pickedObject = PickObject(fingerPos);
            if (pickedObject!=null)
            {
                if (pickedObject.CompareTag("Slot"))
                {
                    currentPieceSlot = pickedObject.GetComponent<PieceSlot>();
                    if (currentPieceSlot.CurrentPiece != null)
                    {
                        currentPiece = currentPieceSlot.CurrentPiece;
                        currentPiece.transform.localScale *= 2;
                        currentPiece.transform.localPosition += new Vector3(0,1,0);
                        currentPieceCanMove = true;
                    }
                  
                }
            
            }
       
        }
    }
    private void FingerGestures_OnFingerMove(int fingerIndex, Vector2 fingerPos)
    {
        if (fingerIndex == 0)
        {
            if (currentPieceCanMove)
            {
                currentPiece.transform.position = GetWorldPos(fingerPos) + new Vector3(0,1,0);
            }
        }
    }
    private void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (fingerIndex == 0)
        {
            if (currentPieceCanMove)
            {
                currentPiece.transform.localPosition = Vector3.zero;
                currentPiece.transform.localScale /= 2;
                currentPieceCanMove = false;
                currentPiece = null;
                currentPieceSlot = null;
                pickedObject = null;
            }
        }
    }


    //RAYCAST ÝLE OBJE YAKALAMA.
    GameObject PickObject(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    // RAYCAST ÝLE TAÞIMA POZÝSYONU.
    Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // we solve for intersection with y = 0 plane
        float t = -ray.origin.z / ray.direction.z;

        return ray.GetPoint(t);
    }
}
