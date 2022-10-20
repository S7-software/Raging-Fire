using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class PlayerTweens : MonoBehaviour
{

    [SerializeField] TweenTypeValues[] tweens;

    public List<TweenTypeValues> GetChosenTweens( params int[] indexs)
    {
        List<TweenTypeValues> temp = new List<TweenTypeValues>();
        int errorIndex = 0;
        foreach (var item in indexs)
        {
            if (item > -1 && item < tweens.Length && tweens.Length != 0)
            {
                temp.Add(tweens[item]);
            }
            else
            {
                Debug.Log("PlayerTweens--GetChosenTweens error--" + errorIndex);
            }
            errorIndex++;
        }

        return temp;
    }
[Serializable]
public class TweenTypeValues
{
    public float donusDerece;
    public float duration;
    public LoopType loopType;
    public Ease ease;
}
}

