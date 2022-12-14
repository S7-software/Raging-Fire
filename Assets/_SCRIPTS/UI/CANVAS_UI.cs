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
    [SerializeField] Button  _btnPause, _btnMainClick;
    [SerializeField] Image[] _imgsFail;
    Player _player;

    bool _gecisOn = true;
    //Tween _sagCizgi;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        foreach (var item in _imgsFail)
        {
            item.DOFade(0, 0);
        }
        _btnPause.gameObject.SetActive(false);

    }
    private void Start()
    {
        SetButtonHandles();
    }
    private void Update()
    {
        if (GECIS.ST.IsRuning()) return;
        if (_gecisOn)
        {
            _gecisOn = false;
            _btnPause.gameObject.SetActive(true);
        }
    }

    private void SetButtonHandles()
    {
        _btnMainClick.onClick.AddListener(() => {
            if (_gecisOn) return;
            _player.Toogle();
        });
        _btnPause.onClick.AddListener(() => { GameManager.instantiate.Pause(); });
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
    public void SetBtnActivePause(bool aktif) { _btnPause.gameObject.SetActive(aktif); }
    public void SetCoin(int count)
    {
        _txtCoin.text = "" + count;
    }
    public void AddCoinWihtAnim(int coin)
    {
        _txtCoin.DOColor(Color.yellow, .2f);
        _txtCoin.gameObject.transform.DOScale(new Vector3(3f, 3f, 3f), .2f).OnComplete(() =>
        {
            SoundBox.instance.PlayOneShot(NamesOfSound.Coin);
            _txtCoin.text = "" + coin;
            _txtCoin.gameObject.transform.DOScale(Vector3.one, .4f).SetEase(Ease.InOutCubic);
            _txtCoin.DOColor(Color.white, .4f);
        });
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
