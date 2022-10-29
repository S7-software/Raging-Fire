using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class STUIAnim : MonoBehaviour
{
    public static void SetAwake(Image panel, CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        panel.DOFade(0, 0);
    }

   public static void In(Image panel,CanvasGroup canvasGroup,Transform transformTable,float duration)
    {
        panel.DOFade(0.1f, duration).SetEase(Ease.OutBack);
        canvasGroup.DOFade(1, duration).SetEase(Ease.OutBack);
        transformTable.DOMoveY(-200, duration).From().SetEase(Ease.OutBack);
        transformTable.DOScale(Vector3.zero, duration).From().SetEase(Ease.OutBack);

    }
    public static void Out(Image panel, CanvasGroup canvasGroup, Transform transformTable, float duration,GameObject gameObjectDestroy)
    {
        panel.DOFade(0, duration).SetEase(Ease.OutBack);
        canvasGroup.DOFade(0, duration).SetEase(Ease.OutBack);
        transformTable.DOMoveY(-200, duration).SetEase(Ease.OutBack);
        transformTable.DOScale(Vector3.zero, duration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            Destroy(gameObjectDestroy, 0.2f);
        });
    }
}
