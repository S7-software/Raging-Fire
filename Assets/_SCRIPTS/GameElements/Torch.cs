using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Torch : MonoBehaviour
{
    [SerializeField] GameObject[] _effect;
    [SerializeField] GameObject _torchModel;
    [SerializeField] Material _materialOfCoin;
    bool _yaniyor = false,_coinVar=false;
    public bool _yaniyorD = false;
    Collider _myCollider;
    Tween _myTween;
    Material _materialOfModel;
    MeshRenderer _myMeshRenderer;
    private void Awake()
    {
        _myMeshRenderer = _torchModel.GetComponent<MeshRenderer>();
        _myCollider = GetComponent<Collider>();
        _materialOfModel = _myMeshRenderer.material;
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

    public void AddCoin() { SetCoin(true);_coinVar = true; }
   public void SetCoin(bool var)    {_myMeshRenderer.material = var ? _materialOfCoin : _materialOfModel;     }
    public bool IsBurning() { return _yaniyor; }
    public bool IsCoin() { return _coinVar; }
}
