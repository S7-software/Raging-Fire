using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SETTINGS : MonoBehaviour
{
    [Header("Set")]
    [SerializeField] float _duration = 0.4f;
    [Header("Variables")]
    [SerializeField] TMP_Text _txtHeader;
    [SerializeField] Button _btnMainMenu, _btnSoundOn, _btnSoundOff,_btnReset;
    [SerializeField] Transform _transformTable;
    [SerializeField] Image _panel;
    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        STUIAnim.SetAwake(_panel, _canvasGroup);

    }
    private void Start()
    {
        STUIAnim.In(_panel, _canvasGroup, _transformTable, _duration);
        SetButtonHandlers();
        _btnSoundOff.interactable = SoundBox.instance.GetVolume() > 0;
        _btnSoundOn.interactable = SoundBox.instance.GetVolume() <1;
    }

    private void SetButtonHandlers()
    {
        _btnMainMenu.onClick.AddListener(() =>
        {
            SoundBox.instance.PlayOneShot(NamesOfSound.clickGiris);
            STUIAnim.Out(_panel, _canvasGroup, _transformTable, _duration, gameObject);
            Invoke("Menu", _duration - .1f);
        });
        _btnSoundOff.onClick.AddListener(() =>
        {
            SoundBox.instance.SetVolume(0);
            _btnSoundOff.interactable = false;
            _btnSoundOn.interactable = true;

        });
        _btnSoundOn.onClick.AddListener(() =>
        {
            SoundBox.instance.SetVolume(1);
            _btnSoundOff.interactable = true;
            _btnSoundOn.interactable = false;
            SoundBox.instance.PlayOneShot(NamesOfSound.clickGiris);

        });
        _btnReset.onClick.AddListener(() =>
        {
            SoundBox.instance.PlayOneShot(NamesOfSound.clickGiris);
        });
    }

    void Menu() { GameManager.instantiate.CloneUI_MAIN_MENU(); }
}
