using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameEditor : MonoBehaviour
{
    public bool _calisiyor = false;
    private void Awake()
    {
        if (_calisiyor) return;
       
    }
}
