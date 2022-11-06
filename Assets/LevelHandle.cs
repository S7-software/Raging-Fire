using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LevelHandle : MonoBehaviour
{
    [SerializeField] Color _on, _off;
    [SerializeField] TMP_Text _txtLevelNumber;
    [SerializeField] Image[] _stars,_starsSocked;
    [SerializeField] Button _myBtn;
    int _index;

    public void SetButton(int level,int stars,bool aktif)
    {
        _index = level;
        _txtLevelNumber.text = "" + _index;
        _txtLevelNumber.color= aktif ? _on : _off;
        _stars[0].enabled = stars > 0;
        _stars[1].enabled = stars > 1;
        _stars[2].enabled = stars > 2;
        foreach (var item in _starsSocked)
        {
            item.color = aktif ? _on : _off;
        }
        _myBtn.interactable = aktif;
        _myBtn.image.color = aktif ? _on : _off;
        _myBtn.onClick.AddListener(() => { DOTween.KillAll();  FindObjectOfType<UI_LEVELS>().GoToLevel(_index); });
    }
   
}
