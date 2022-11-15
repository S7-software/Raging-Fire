using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] bool gecis = false;
    private void Start()
    {
        int currenLevel = SaveSystem.GetCurrentLevel();
        if (!gecis) return;
        STScene.GoTo(currenLevel);
    }
}
