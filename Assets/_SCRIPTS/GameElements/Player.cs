using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _tiklanma;
    public Transform _pointA, _pointB, _player, _bunuEsitle, _buna;
    public Transform[] _playerStafs;
    public bool _isPointA = true;
    public float _duration = 1f;
    public Ease _ease;

    public float time = 0.5f;
    public float str = 0.5f;
    public int vib = 10;
    public float rnd = 0.5f;

    Tween _myTween;
    private void Awake()
    {

    }
    void Start()
    {
        RunFrom(_pointA, _duration);
    }

    private void Update()
    {
        if (GameManager.instantiate._testOn)
        {
            CheckTorch();
            if (GameManager.instantiate.GetBolumBitti()) Finish();
        }
    }



    void CheckTorch()
    {
        Collider[] touchs = Physics.OverlapBox(_isPointA ? _pointB.position : _pointA.position, new Vector3(.2f, .2f, 1f));
        bool founded = false;
        foreach (var item in touchs)
        {
       
            if (item.gameObject.tag == Tags.TouchPoint.ToString())
            {
          

                item.gameObject.tag = Tags.OnTouchPonit.ToString();
                item.GetComponent<Torch>().SetTorch(true);
                ChangePoint();
                founded = true;
                GameManager.instantiate.SetNevLocForCamera(item.gameObject.transform.parent.InverseTransformPoint(item.gameObject.transform.position));
                Transform currentPoint = GetCurrenPoint(true);
                FixLocationPoint(currentPoint,item.transform); 
                GameManager.instantiate.SetYeniMesaleYak();
                break;
            }
        }
        if (!founded&&!GameManager.instantiate._testOn)
        {
            GameManager.instantiate.ShakeScreen(time, str, vib, rnd);
            GameManager.instantiate.SetHataliTiklama();
        }
    }

    private void FixLocationPoint(Transform currentPoint, Transform transform)
    {
        Vector3 vector3 = new Vector3(
            transform.position.x,
            transform.position.y,
            currentPoint.position.z
            );

        currentPoint.DOMove(vector3, 0.2f).SetEase(Ease.InOutCubic);
    }
    

    void RunFrom(Transform point, float duration)
    {
        //float x = point.transform.rotation.x;
        //float y = point.transform.rotation.y;

      SetupPlayerAssets(point == _pointA);
        _myTween = null;

        //_myTween = point.DORotate(new Vector3(x, y, point == _pointA ? 180 : -180), duration).SetLoops(-1, LoopType.Incremental).SetEase(_ease);
        _myTween = GameManager.instantiate.GetTween(point, point == _pointA);


    }

   
    void Finish() {
       GameManager.instantiate.SetBtnActivePause(false);
        StopTween();
        _pointA.transform.rotation = new Quaternion(0, 0, 0, 0);
        _pointB.transform.rotation = new Quaternion(0, 0, 0, 0);
        _myTween = null;
        Transform point = _isPointA ? _pointA : _pointB;
        _myTween = point.DORotate(new Vector3(point.transform.rotation.x,
            point.transform.rotation.y,
            point == _pointA ? 180 : -180),
            .3f)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
    void StopTween() { _myTween.Kill(); }
    void ChangePoint()
    {
       // GameManager.instantiate.ShakeScreen(0.2f, str, vib, rnd);
        Transform point = _isPointA ? _pointB : _pointA;
        StopTween();
        RunFrom(point, _duration);
        _isPointA = !_isPointA;

    }
    public void Toogle()
    {
        if (!GameManager.instantiate.GetTiklanabilir()) return;
        TiklanmaEfect();
        CheckTorch();
        if (GameManager.instantiate.GetBolumBitti()) Finish();


    }

    void SetupPlayerAssets(bool isPointA)
    {
        foreach (var item in _playerStafs)
        {
            item.transform.SetParent(_player);
        }
        _pointA.transform.SetParent(_player);
        _pointB.transform.SetParent(_player);
        _pointA.transform.rotation = new Quaternion(0, 0, 0, 0);
        _pointB.transform.rotation = new Quaternion(0, 0, 0, 0);

        if (isPointA)
            _pointB.transform.SetParent(_pointA);
        else
            _pointA.transform.SetParent(_pointB);
        foreach (var item in _playerStafs)
        {
            if (isPointA)
                item.transform.SetParent(_pointA);
            else
                item.transform.SetParent(_pointB);
        }
        



    }

  

    Transform GetCurrenPoint(bool yes)
    {
        if (yes) return _isPointA ? _pointA : _pointB;

        return !_isPointA ? _pointA : _pointB;
    }



    
        

    

    void TiklanmaEfect()
    {
        foreach (var item in _tiklanma)
        {
            item.Play();
        }
        
    }
}
