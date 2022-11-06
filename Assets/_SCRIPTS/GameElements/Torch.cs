using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Torch : MonoBehaviour
{
    [SerializeField] GameObject[] _effect;
    [SerializeField] GameObject _torchModel;
    bool _yaniyor = false;
    public bool _yaniyorD = false;
    Collider _myCollider;
    Tween _myTween;
    private void Awake()
    {
        _myCollider = GetComponent<Collider>();
        _myCollider.enabled = !_yaniyorD;
        RotateRandom(0);
    }
    private void Start()
    {
        SetTorch(_yaniyorD);
        StartCoroutine(CorRotate());
    }

    public void SetTorch(bool yaniyor)
    {
        if (_yaniyor) return;
        _yaniyor = yaniyor;
        _myCollider.enabled = !yaniyor;
        _myTween.Kill();
        _myTween = null;
        foreach (var item in _effect)
        {
            item.SetActive(_yaniyor);
        }
        
    }
    IEnumerator CorRotate()
    {
        while (!_yaniyor)
        {
            float duration = Random.Range(1f, 4f);
            RotateRandom(duration);
            duration += Random.Range(3f, 6f);
            yield return new WaitForSeconds(duration);
        }
    }
    void RotateRandom(float duration)
    {
        if (_yaniyor) return;
        _myTween = null;
       _myTween= _torchModel.transform.DOLocalRotate(new Vector3(0, 0, Random.Range(0f, 361f)), duration);
    }
}
