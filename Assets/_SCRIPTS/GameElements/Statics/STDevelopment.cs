using System;
using UnityEngine;
public class STDevelopment:MonoBehaviour
{

  static  bool isTesting = true;
    public static void Testing(GameObject gameObject)
    {
        if (!isTesting) { Destroy(gameObject); }
    }
}
