using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MAIN_MENU : MonoBehaviour
{
    [SerializeField] Button _btnPlay, _btnSettings,_btnLevels, _btnExit;
    [SerializeField] Image _panel;
    [SerializeField] Transform _transformTable;
    [SerializeField] float _durationAnim=.4f;
  
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        STUIAnim.SetAwake(_panel, _canvasGroup);

    }

    private void Start()
    {
        STUIAnim.In(_panel, _canvasGroup, _transformTable, _durationAnim);
        _btnPlay.onClick.AddListener(() =>
        {
            STUIAnim.Out(_panel, _canvasGroup, _transformTable, _durationAnim, gameObject);
            Invoke("ShowPauseBtn", _durationAnim - 0.1f);
        });
        _btnSettings.onClick.AddListener(() =>
        {
            STUIAnim.Out(_panel, _canvasGroup, _transformTable, _durationAnim, gameObject);
            Invoke("Settings", _durationAnim - 0.1f);
        });
        _btnLevels.onClick.AddListener(() =>
        {
            STUIAnim.Out(_panel, _canvasGroup, _transformTable, _durationAnim, gameObject);
            Invoke("Levels", _durationAnim - 0.1f);
        });
        _btnSettings.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    void Settings() { GameManager.instantiate.CloneUI_SETTINGS(); }
    void Levels() { GameManager.instantiate.CloneUI_LEVELS(); }
    void ShowPauseBtn() { GameManager.instantiate.SetBtnActivePause(true); }

}
