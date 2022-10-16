using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerTweens : MonoBehaviour
{

    public TweenType[] tweens;

    public List<TweenType> GetChosenTweens( params int[] indexs)
    {
        List<TweenType> temp = new List<TweenType>();
        foreach (var item in indexs)
        {
            if (item > -1 && item < tweens.Length && tweens.Length != 0)
            {
                temp.Add(tweens[item]);
            }
        }

        return temp;
    }
[Serializable]
public class TweenType
{
    public float donusDerece;
    public float duration;
    public LoopType loopType;
    public Ease ease;
}
}

