using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CANVAS_DEBUG : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] Button btnVolume,btnGameObjTest;


    void Awake()
    {
        STDevelopment.Testing(gameObject);
        btnVolume.onClick.AddListener(() =>
        {
            bool tempDurum = !volume.enabled;
            string temp = "Volume " + (tempDurum ? "ON" : "FALSE");
            volume.enabled = tempDurum;
            btnVolume.gameObject.GetComponentInChildren<Text>().text = temp;

        });

        btnGameObjTest.onClick.AddListener(() =>
        {
            bool temp = !GameManager.instantiate._testOn;
            string tempSt = "Test " + (temp ? "ON" : "OFF");
            GameManager.instantiate._testOn = temp;
            btnGameObjTest.gameObject.GetComponentInChildren<Text>().text = tempSt;

        });
    
    }
}
