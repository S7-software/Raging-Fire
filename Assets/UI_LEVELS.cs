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

    private void Awake()
    {
        STUIAnim.SetAwake(_panel, _canvasGroup);
    }
    private void Start()
    {
        STUIAnim.In(_panel, _canvasGroup, _trasformTable, _duration);

        for (int i = 1; i < 25; i++)
        {
            GameObject gameObject = Instantiate(_level,_transformContent);
          
            gameObject.GetComponent<LevelHandle>().SetButton(i, Random.Range(1, 4), true);
        }
        for (int i = 25; i < 30; i++)
        {
            GameObject gameObject = Instantiate(_level, _transformContent);

            gameObject.GetComponent<LevelHandle>().SetButton(i, 0, false);
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
        SceneManager.LoadScene(hangi);
    }
}
