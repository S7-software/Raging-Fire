using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HAVAI_FISEK : MonoBehaviour
{
    [SerializeField] GameObject[] _trails;
    [SerializeField] float _yukseklik=30f, _sure=1f,_delay=0.2f;
    [SerializeField] GameObject _patlama,_yilzdiz;


    public void SetStartFinish(int kacYildiz)
    {
        switch (kacYildiz)
        {
            case 2:
                Run(_trails[0], 0);
                Run(_trails[3], _delay);
                break;
                

            case 3:
                Run(_trails[0], 0);
                Run(_trails[1], _delay);
                Run(_trails[2], _delay+_delay);
                break;
            case 1:
            default:
                Run(_trails[1], 0);
                break;
        }
    }

    void Run(GameObject obj,float delay)
    {
        obj.SetActive(true);
        obj.transform.DOLocalMoveY(_yukseklik, _sure).SetDelay(delay).OnComplete(() =>
        {
            GameObject patlama = Instantiate(_patlama,obj.transform);
            patlama.transform.SetParent(transform);
            GameObject yildiz = Instantiate(_yilzdiz);
            yildiz.transform.position = patlama.transform.position;
            yildiz.transform.position += new Vector3(0, 0, 2);
            obj.SetActive(false);

        });
    }

    
}
