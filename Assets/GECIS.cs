using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GECIS : MonoBehaviour
{
    [SerializeField] GameObject _vfx;
    [SerializeField] Vector3 _step1, _step2, _stepTown, _stepFar;
    [SerializeField] float _durationStep1 = 0.3f, _durationStep2 = 0.3f;

    ParticleSystem _particleSystem;
    public static GECIS ST;

    private void Awake()
    {
        ST = this;
        DontDestroyOnLoad(gameObject);
        _vfx.SetActive(false);
        _vfx.transform.DOLocalMove(_stepTown, 0);
        _particleSystem = _vfx.GetComponent<ParticleSystem>();
    }


    public void In()
    {
        _vfx.SetActive(true);
        _particleSystem.Play();
        _vfx.transform.DOLocalMove(_stepTown, 0).OnComplete(() =>
        {
            _vfx.transform.DOLocalMove(_step1, _durationStep1).SetEase(Ease.OutCubic).OnComplete(() =>
                    {
                        _vfx.transform.DOLocalMove(_step2, _durationStep2).SetEase(Ease.OutCubic);
                    });
        });

    }

    public void Out()
    {
        _vfx.SetActive(true);

        _vfx.transform.DOLocalMove(_step2, 0).OnComplete(() =>
        {
            _vfx.transform.DOLocalMove(_step1, _durationStep1).SetEase(Ease.OutCubic).OnComplete(() =>
            {
                _vfx.transform.DOLocalMove(_stepFar, _durationStep2).SetEase(Ease.OutCubic).OnComplete(() =>
                {
                    _particleSystem.Stop();
                    _vfx.SetActive(false);
                });
            });
        });
    }
    public bool IsRuning() { return _vfx.activeInHierarchy; }
    
}
