using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Level Ayarlari")]
    [SerializeField] float geriSayimTiklamaIcin = 1;
    [SerializeField] int currentLevel = 1;
    [Header("Level Ayarlari Player")]
    [SerializeField] int[] tweenIndexs;
    int _tweenIdex = 0;
    [SerializeField] PlayerTweens _playerTweens;
    [SerializeField] float donusDerece = 180f;
    [SerializeField] float duration = 1f;
    [SerializeField] LoopType loopType = LoopType.Incremental;
    [SerializeField] Ease ease = Ease.Linear;

    [Header("Componentler")]
    
    [SerializeField] Transform _cameraParent;
    [SerializeField] Transform _cameraMain;
    [SerializeField] float _yumusatma = 10f;
    bool _kamerayiTakipet = false;
    Vector3 _cameraNew;
    CANVAS_UI _canvas_UI;
    HAVAI_FISEK _havai_Fisek;
    float _geriSayimTiklamaIcin;
    public static GameManager instantiate;

    float _toplamMesaleSayisi, _toplananMesaleSayisi;
    Vector3 _tempCameraPos;
    private void Awake()
    {
        instantiate = this;
        _canvas_UI = FindObjectOfType<CANVAS_UI>();
        _havai_Fisek = FindObjectOfType<HAVAI_FISEK>();
        SetLevelDegerler();
    }
    private void Start()
    {
        
        SetUI();
        SetGameObjectsValues();
 
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
    }


    //LEVEL SONU
    void BolumBitti(int kacYildiz)
    {
        _havai_Fisek.SetStartFinish(kacYildiz);
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
            BolumBitti(3);
        }
    }



    //SOL GOSTERGE
    public void SetHataliTiklama()
    {
        _geriSayimTiklamaIcin = 0;
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
    public void Restart() { DOTween.KillAll(); SceneManager.LoadScene(0); }
    public void Exit() { Application.Quit(); }

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
        if (_tweenIdex >= tweenIndexs.Length) _tweenIdex = 0;

        float dnsDrc = _playerTweens.tweens[_tweenIdex].donusDerece;
        float drtn = _playerTweens.tweens[_tweenIdex].duration;
        Ease ease = _playerTweens.tweens[_tweenIdex].ease;
        LoopType loopType = _playerTweens.tweens[_tweenIdex].loopType;
        float x = point.transform.rotation.x;
        float y = point.transform.rotation.y;
        Vector3 temp = new Vector3(x, y, isPointA ? dnsDrc : -dnsDrc);
        _tweenIdex++;
        return point.DORotate(temp, drtn).SetLoops(-1, loopType).SetEase(ease);

    }
}

