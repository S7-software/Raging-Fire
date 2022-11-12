using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Level Ayarlari")]
    [SerializeField] float geriSayimTiklamaIcin = 1;
    [SerializeField] int currentLevel = 1;
    public bool _testOn =false;

    [Header("Level Ayarlari Player")]
    [SerializeField] int[] tweenIndexs;
    int _tweenIdex = 0;
    [SerializeField] PlayerTweens _playerTweens;
    List<PlayerTweens.TweenTypeValues> _tweenTypes;

    [Header("Componentler")]
    [SerializeField] Transform _cameraParent;
    [SerializeField] Transform _cameraMain;
    [SerializeField] float _yumusatma = 10f;
    [SerializeField] GameObject _UI_RESULT,_UI_PAUSE,_UI_SETTINGS,_UI_MAIN_MENU,_UI_LEVELS;

    bool _kamerayiTakipet = false;
    bool _bolumBitti = false;
    Vector3 _cameraNew;
    CANVAS_UI _canvas_UI;
    HAVAI_FISEK _havai_Fisek;
    float _geriSayimTiklamaIcin;
    public static GameManager instantiate;

    float _toplamMesaleSayisi, _toplananMesaleSayisi;
    Vector3 _tempCameraPos;
    public int _yildiz,_coinCollected=0,_ownCoin;
    private void Awake()
    {
        instantiate = this;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        _ownCoin = SaveSystem.GetOwnCoin();
        _canvas_UI = FindObjectOfType<CANVAS_UI>();
        _havai_Fisek = FindObjectOfType<HAVAI_FISEK>();
        _tweenTypes = _playerTweens.GetChosenTweens(tweenIndexs);
        SetLevelDegerler();
        _bolumBitti = false;
    }
    private void Start()
    {
        
        SetUI();
        SetGameObjectsValues();
        SetCoinOfTorch();
        SaveSystem.SetCurrentLevel(currentLevel);
        _yildiz = 3;
 
    }

    private void Update()
    {
        SolGosterge();
        ChangeLocationCamera();
    }

    //SET START
    private void SetUI()
    {
        float deger = _toplananMesaleSayisi / _toplamMesaleSayisi;
        _canvas_UI.SetLevelGosterge(deger, currentLevel);
        _canvas_UI.SetAltGosterge(1, Color.green);
        _canvas_UI.SetCoin(_ownCoin);
    }

    void SetCoinOfTorch()
    {
        bool coinAtandi = false;
        Torch[] torches = FindObjectsOfType<Torch>();
        while (!coinAtandi)
        {
            Torch torch = torches[UnityEngine.Random.Range(0, torches.Length)];
            if (!torch.IsBurning())
            {
                torch.AddCoin();
                coinAtandi = true;            }
        }
    }


    //LEVEL SONU
    IEnumerator BolumBitti(int kacYildiz, float delayShowUI)
    {
        SetBtnActivePause(false);
        _havai_Fisek.SetStartFinish(kacYildiz);
      GameObject result=  Instantiate(_UI_RESULT);
        yield return new WaitForSeconds(delayShowUI);
        result.GetComponent<UI__RESULT>().SetResult(kacYildiz, true, _coinCollected, 3);
        SaveSystem.SetMaxLevel(currentLevel + 1);
        SaveSystem.SetStarsOfLevel(currentLevel, kacYildiz);
    }


    //KAMERA
    void ChangeLocationCamera()
    {
        if (!_kamerayiTakipet) return;
        Vector3 temp = new Vector3(_cameraNew.x, _cameraNew.y, _cameraParent.position.z);
        _cameraParent.position = Vector3.Lerp(_cameraParent.position, temp, _yumusatma * Time.deltaTime);

    }
    public void SetNevLocForCamera(Vector3 newPos)

    {
        _cameraNew = newPos;
        _kamerayiTakipet = true;
    }

   void SetGameObjectsValues()
    {
         _tempCameraPos = _cameraMain.transform.localPosition;
        _toplamMesaleSayisi = FindObjectsOfType<Torch>().Length;
        _toplananMesaleSayisi = 1;

    }

    public void ShakeScreen(float time, float str, int vib, float rnd)
    {

        _cameraMain.transform.DOShakePosition(time, str, vib, rnd).OnComplete(() =>
        {
            _cameraMain.transform.localPosition = _tempCameraPos;
        });

    }

    //GAMEPLAY
    public void SetYeniMesaleYak()
    {
        _toplananMesaleSayisi++;
        _canvas_UI.SetLevelGosterge(_toplananMesaleSayisi / _toplamMesaleSayisi, currentLevel);
        if (_toplamMesaleSayisi == _toplananMesaleSayisi)
        {
           StartCoroutine( BolumBitti(_yildiz,2f));
            _bolumBitti = true;
        }
    }
    public bool GetBolumBitti() => _bolumBitti;
    public void CollectCoin() {
        _coinCollected++;
        _ownCoin++;
        SaveSystem.AddToOwnCoin(1);
        _canvas_UI.AddCoinWihtAnim(_ownCoin);
    }

    // GOSTERGE TIKLAMA VE HATA
    public void SetHataliTiklama()
    {
        _geriSayimTiklamaIcin = 0;
        _canvas_UI.AddMistake();
        _yildiz--;
        if (_yildiz == 0)
        {
            SetBtnActivePause(false);
            Player player = FindObjectOfType<Player>();
            player.Broke();
            GameObject result = Instantiate(_UI_RESULT);
            
            result.GetComponent<UI__RESULT>().SetResult(0,true,0,currentLevel);
        }

    }

    void SolGosterge()
    {
        if (_geriSayimTiklamaIcin >= geriSayimTiklamaIcin) return;
        _geriSayimTiklamaIcin += Time.deltaTime;
        float maxYesilDeger = 0.2431371f;
        float deger = ((_geriSayimTiklamaIcin>geriSayimTiklamaIcin?geriSayimTiklamaIcin:_geriSayimTiklamaIcin) /geriSayimTiklamaIcin );
        Color color = new Color(maxYesilDeger*deger,deger,0);
        _canvas_UI.SetAltGosterge(deger, color);

    }
    public bool GetTiklanabilir() { return _geriSayimTiklamaIcin >= geriSayimTiklamaIcin; }
    void SetLevelDegerler()
    {
        _geriSayimTiklamaIcin = geriSayimTiklamaIcin;
    }

    //UI
    public void Pause() {
        Instantiate(_UI_PAUSE);
        SetBtnActivePause(false);
    }
    public void SetBtnActivePause(bool aktif) { _canvas_UI.SetBtnActivePause(aktif); }
    public void Exit() { Application.Quit(); }
    public void CloneUI_LEVELS() { Instantiate(_UI_LEVELS); SetBtnActivePause(false); }
    public void CloneUI_MAIN_MENU() { Instantiate(_UI_MAIN_MENU); SetBtnActivePause(false); }
    public void CloneUI_SETTINGS() { Instantiate(_UI_SETTINGS); SetBtnActivePause(false); }
    //PlayerDonme
    //public Tween GetTween(Transform point,bool isPointA)
    //{
    //    float x = point.transform.rotation.x;
    //    float y = point.transform.rotation.y;
    //    Vector3 temp = new Vector3(x, y, isPointA ? donusDerece : -donusDerece);
    //  return  point.DORotate(temp, duration).SetLoops(-1, loopType).SetEase(ease);

    //}
    public Tween GetTween(Transform point, bool isPointA)
    {
        if (_tweenIdex >= _tweenTypes.Count) _tweenIdex = 0;
        PlayerTweens.TweenTypeValues values = _tweenTypes[_tweenIdex];

        float dnsDrc = values.donusDerece;
        float drtn = values.duration;
        Ease ease = values.ease;
        LoopType loopType = values.loopType;
        float x = point.transform.rotation.x;
        float y = point.transform.rotation.y;
        Vector3 temp = new Vector3(x, y, isPointA ? dnsDrc : -dnsDrc);
        _tweenIdex++;
        return point.DORotate(temp, drtn).SetLoops(-1, loopType).SetEase(ease);

    }
}

