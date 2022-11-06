using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    private void Start()
    {
        int currenLevel = SaveSystem.GetCurrentLevel();

        STScene.GoTo(currenLevel);
    }
}
