using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UI__RESULT : MonoBehaviour
{
    [Header("Set")]
    [SerializeField] float _duration = 0.4f;
    [Header("Variables")]
    [SerializeField] TMP_Text _txtLevel,_txtCoin;
    [SerializeField] Button _btnMainMenu, _btnNext, _btnRestart, _btnAds;
    [SerializeField] Image[] _stars;
    [SerializeField] Transform _trnsTable;
    [SerializeField] Image _panel;
    CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _stars[0].DOFade(0,0);
        _stars[1].DOFade(0,0);
        _stars[2].DOFade(0,0);
        _panel.DOFade(0,0);
    }
  

    void SetStars(int count,float delay,bool together)
    {
        float delayMassiv = 0.6f;
        for (int i = 0; i < count; i++)
        {
            SetStar(_stars[i], 0.6f, delay);
           if(!together) delay += delayMassiv;
            if (count > 3) break;
        }

    }

    void SetStar(Image star,float duration,float delay)
    {
        star.transform.DOLocalMoveY(10f, duration).From().SetEase(Ease.OutCubic).SetDelay(delay);
        star.DOFade(1, duration).SetEase(Ease.OutCubic).SetDelay(delay);
    }

    public void SetResult(int stars, bool starsTogether,int coin,int level )
    {
        SetButtons(stars);
        STUIAnim.In(_panel, _canvasGroup, _trnsTable, _duration);
        //_panel.DOFade(0.1f, _duration).SetEase(Ease.OutBack);
        //_canvasGroup.DOFade(1, _duration).SetEase(Ease.OutBack);
        //_trnsTable.DOMoveY(-200, _duration).From().SetEase(Ease.OutBack);
        //_trnsTable.DOScale(Vector3.zero, _duration).From().SetEase(Ease.OutBack);
        _txtCoin.text = "+" + coin;
        _txtLevel.text ="LEVEL " + level;
        SetStars(stars, .3f,starsTogether);
      
    }

    private void SetButtons(int stars)
    {
        if (stars > 0)
        {
            _btnNext.onClick.AddListener(() => { DOTween.KillAll(); STScene.GoToNext(); });
            _btnRestart.onClick.AddListener(() => { DOTween.KillAll(); STScene.Restart(); });
            _btnMainMenu.onClick.AddListener(() =>
            {
                STUIAnim.Out(_panel, _canvasGroup, _trnsTable, _duration, gameObject);
                Invoke("Menu", _duration -.1f);
            });

            _btnAds.gameObject.SetActive(UnityEngine.Random.Range(0, 2) == 1);
           
        }
        else
        {
            _btnNext.interactable = false;
            _btnRestart.onClick.AddListener(() => { STScene.Restart(); });
            _btnMainMenu.onClick.AddListener(() =>
            {
                STUIAnim.Out(_panel, _canvasGroup, _trnsTable, _duration, gameObject);
                Invoke("Menu", _duration - 0.1f);
            });
            _btnAds.gameObject.SetActive(false);
        }
    }
    void Menu() { GameManager.instantiate.CloneUI_MAIN_MENU(); }
}
