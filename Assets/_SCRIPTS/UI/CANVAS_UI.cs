using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CANVAS_UI : MonoBehaviour
{
    [SerializeField] Image _imgSolGosterge, _imgSagGosterge,_imgSolGosterge2;
    [SerializeField] TMP_Text _txtCurrent, _txtNext,_txtCoin;
    [SerializeField] Button _btnExit, _btnReset, _btnMainClick;
    [SerializeField] Image[] _imgsFail;
    Player _player;
    //Tween _sagCizgi;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        foreach (var item in _imgsFail)
        {
            item.DOFade(0, 0);
        }
    }
    private void Start()
    {
        SetButtonHandles();
    }

    private void SetButtonHandles()
    {
        _btnExit.onClick.AddListener(() => { GameManager.instantiate.Exit(); });
        _btnMainClick.onClick.AddListener(() => { _player.Toogle(); });
        _btnReset.onClick.AddListener(() => { GameManager.instantiate.Restart(); });
    }

    public void SetAltGosterge(float val, Color color)
    {
        _imgSolGosterge.fillAmount = val;
        _imgSolGosterge2.fillAmount = val;
        _imgSolGosterge.color = color;
        _imgSolGosterge2.color = color;
    }

    public void SetLevelGosterge(float val,  int currentLevel)
    {
      _imgSagGosterge.fillAmount = val;
      //  if (_sagCizgi != null)
      //  {
      //      _sagCizgi.Kill();
      //      _sagCizgi = null;
      //  }
      //_sagCizgi=  _imgSagGosterge.DOFillAmount(val, 0.5f).SetEase(Ease.OutCubic);
       
        _txtCurrent.text = currentLevel.ToString();
        _txtNext.text = (currentLevel + 1).ToString();
    }

    public void SetCoin(int count)
    {
        _txtCoin.text = "" + count;
    }

    public void AddMistake()
    {
        foreach (var item in _imgsFail)
        {
           
            if (item.color.a == 0)
            {
                item.DOFade(1, 0.6f).SetEase(Ease.OutQuint);
                item.transform.DOScale(new Vector3(2, 2, 2), 0.6f).From().SetEase(Ease.OutQuint);
                item.transform.DORotate(new Vector3(0, 0, -109), 0.6f).From().SetEase(Ease.OutQuint);
                break;
            }
        }
    }
}
