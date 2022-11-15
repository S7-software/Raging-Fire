using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CUSTOMIZE : MonoBehaviour
{
    [SerializeField] Image _panel;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] GameObject _level;
    [SerializeField] Transform _trasformTable, _transformContent;
    [SerializeField] float _duration = 0.4f;
    [SerializeField] Button _btnExit;
}
