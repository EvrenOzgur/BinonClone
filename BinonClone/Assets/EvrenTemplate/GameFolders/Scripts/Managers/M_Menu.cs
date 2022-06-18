using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class M_Menu : MonoBehaviour
{
    public GameObject MainMenuPanelPrefab;
    public GameObject GamePanelPrefab;
    public GameObject PausePanelPrefab;
    public GameObject CompletePanelPrefab;

    [HideInInspector] public GameObject CurrentPanel;

    private void Awake()
    {
        M_Observer.OnGameCreate += GameCreate;
        M_Observer.OnGameReady += GameReady;
        M_Observer.OnGameStart += GameStart;
        M_Observer.OnGamePause += GamePause;
        M_Observer.OnGameFail += GameFail;
        M_Observer.OnGameComplete += GameComplete;
        M_Observer.OnGameRetry += GameRetry;
        M_Observer.OnGameContinue += GameContinue;
        M_Observer.OnGameNextLevel += GameNextLevel;
        CurrentPanel = Instantiate(MainMenuPanelPrefab, transform);


    }



    private void OnDestroy()
    {
        M_Observer.OnGameCreate -= GameCreate;
        M_Observer.OnGameReady -= GameReady;
        M_Observer.OnGameStart -= GameStart;
        M_Observer.OnGamePause -= GamePause;
        M_Observer.OnGameFail -= GameFail;
        M_Observer.OnGameComplete -= GameComplete;
        M_Observer.OnGameRetry -= GameRetry;
        M_Observer.OnGameContinue -= GameContinue;
        M_Observer.OnGameNextLevel -= GameNextLevel;
    }


    private void GameCreate()
    {

    }

    private void GameReady()
    {
        //print("GameReady");
    }

    private void GameStart()
    {
        DeleteCurrentPanel();
        GameObject _g = Instantiate(GamePanelPrefab, transform);
        RectTransform _rt = _g.GetComponent<RectTransform>();
        _rt.DOAnchorPosX(540, 0.5f).SetEase(Ease.OutBack).OnComplete(() => CurrentPanel = _g);

    }
    private void GamePause()
    {
        DeleteCurrentPanel();
        GameObject _g = Instantiate(PausePanelPrefab, transform);
        RectTransform _rt = _g.GetComponent<RectTransform>();
        _rt.DOAnchorPosX(540, 0.5f).SetEase(Ease.OutBack).OnComplete(() => CurrentPanel = _g);
    }
    private void GameFail()
    {
        //print("GameFail");
    }

    private void GameComplete()
    {
        DeleteCurrentPanel();
        GameObject _g = Instantiate(CompletePanelPrefab, transform);
        RectTransform _rt = _g.GetComponent<RectTransform>();
        _rt.DOAnchorPosX(540, 0.5f).SetEase(Ease.OutBack).OnComplete(() => CurrentPanel = _g);
    }

    private void GameRetry()
    {
        DeleteCurrentPanel();
        GameObject _g = Instantiate(GamePanelPrefab, transform);
        RectTransform _rt = _g.GetComponent<RectTransform>();
        _rt.DOAnchorPosX(540, 0.5f).SetEase(Ease.OutBack).OnComplete(() => CurrentPanel = _g);
    }

    private void GameContinue()
    {
        DeleteCurrentPanel();
        GameObject _g = Instantiate(GamePanelPrefab, transform);
        RectTransform _rt = _g.GetComponent<RectTransform>();
        _rt.DOAnchorPosX(540, 0.5f).SetEase(Ease.OutBack).OnComplete(() => CurrentPanel = _g);
    }

    private void GameNextLevel()
    {
        DeleteCurrentPanel();
        GameObject _g = Instantiate(GamePanelPrefab, transform);
        RectTransform _rt = _g.GetComponent<RectTransform>();
        _rt.DOAnchorPosX(540, 0.5f).SetEase(Ease.OutBack).OnComplete(() => CurrentPanel = _g);
    }

    void DeleteCurrentPanel()
    {
        if (CurrentPanel != null)
        {
            RectTransform _rt = CurrentPanel.GetComponent<RectTransform>();
            //  float _a = 0;
            // DOTween.To(() => _a, _b => _a = _b, 1080, 0.5f).OnUpdate(()=>);
            _rt.DOAnchorPosX(1620, 0.25f).OnComplete(() =>
            {
                Destroy(CurrentPanel);
                CurrentPanel = null;
            });



        }
    }
}
