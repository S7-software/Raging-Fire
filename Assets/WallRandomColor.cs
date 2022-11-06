using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRandomColor : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] Material[] _materials;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (_materials.Length == 0) return;
        meshRenderer.material = _materials[Random.Range(0, _materials.Length)];
    }
}
