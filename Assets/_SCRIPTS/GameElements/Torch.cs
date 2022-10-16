using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject[] _effect;
     bool _yaniyor = false;
    public bool _yaniyorD = false;
    Collider _myCollider;
    private void Awake()
    {
        _myCollider = GetComponent<Collider>();
        _myCollider.enabled = !_yaniyorD;
    }
    private void Start()
    {
        SetTorch(_yaniyorD);
    }

    public void SetTorch(bool yaniyor)
    {
        if (_yaniyor) return;
        _yaniyor = yaniyor;
        _myCollider.enabled = !yaniyor;
        foreach (var item in _effect)
        {
            item.SetActive(_yaniyor);
        }
        
    }
}
