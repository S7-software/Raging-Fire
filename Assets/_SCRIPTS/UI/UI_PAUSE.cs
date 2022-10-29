using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_PAUSE : MonoBehaviour
{

    [Header("Set")]
    [SerializeField] float _duration = 0.4f;
    [Header("Variables")]
    [SerializeField] TMP_Text _txtPause;
    [SerializeField] Button _btnMainMenu, _btnRestart, _btnContuine;
    [SerializeField] Transform _trnsTable;
    [SerializeField] Image _panel;
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        STUIAnim.SetAwake(_panel, _canvasGroup);

        _txtPause.text = "PAUSE";
    }
    void Start()
    {
        SetButtons();
        STUIAnim.In(_panel, _canvasGroup, _trnsTable, _duration);


    }

    private void SetButtons()
    {

        _btnContuine.onClick.AddListener(() =>
        {
            STUIAnim.Out(_panel, _canvasGroup, _trnsTable, _duration, gameObject);
            Invoke("ShowPauseBtn", _duration - 0.1f);
        });
        _btnRestart.onClick.AddListener(() =>
        {
            DOTween.KillAll(); STScene.Restart();
        });
        _btnMainMenu.onClick.AddListener(() =>
        {
            STUIAnim.Out(_panel, _canvasGroup, _trnsTable, _duration, gameObject);
            Invoke("Menu", _duration - 0.1f);

        });


    }

    void Menu() { GameManager.instantiate.CloneUI_MAIN_MENU(); }
       
    void ShowPauseBtn() { GameManager.instantiate.SetBtnActivePause(true);}


}
