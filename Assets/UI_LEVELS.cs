using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LEVELS : MonoBehaviour
{
    [SerializeField] Image _panel;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] GameObject _level;
    [SerializeField] Transform _trasformTable,_transformContent;
    [SerializeField] float _duration = 0.4f;
    [SerializeField] Button _btnExit;

    int _tempLevel;
    private void Awake()
    {
        STUIAnim.SetAwake(_panel, _canvasGroup);
    }
    private void Start()
    {
        
        STUIAnim.In(_panel, _canvasGroup, _trasformTable, _duration);
        int maxLevel = SaveSystem.GetMaxLevel();
        for (int i = 1; i < 21; i++)
        {
            GameObject gameObject = Instantiate(_level,_transformContent);
          
            gameObject.GetComponent<LevelHandle>().SetButton(
                i,
                SaveSystem.GetStarsOfLevel(i),
                i<=maxLevel);
        }
    
        

        _btnExit.onClick.AddListener(() =>
        {
            PlayerPrefs.SetFloat("lastPositionY", _transformContent.localPosition.y);
            STUIAnim.Out(_panel, _canvasGroup, _trasformTable, _duration, gameObject); Invoke("Menu", _duration - 0.1f);
        });

        _transformContent.localPosition = new Vector3(
            _transformContent.localPosition.x,
            PlayerPrefs.GetFloat("lastPositionY", -4000f),
            _transformContent.localPosition.z);
    }


    void Menu() { GameManager.instantiate.CloneUI_MAIN_MENU(); }
    public void GoToLevel(int hangi)
    {
        _tempLevel = hangi;
        STUIAnim.SetAwake(_panel, _canvasGroup);
        GECIS.ST.In();
        Invoke("GoToLevel", 1);
        
    }
    void GoToLevel()
    {
        SceneManager.LoadScene(_tempLevel);
    }
}
