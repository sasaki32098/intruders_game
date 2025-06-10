using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IrisShot : MonoBehaviour
{
    [SerializeField] RectTransform unmask;
    readonly Vector2 IRIS_IN_SCALE = new Vector2(20000, 8000);
    readonly float SCALE_DURATION = 1;
    public GameManager GameManager;

    public void IrisIn()
    {
        Debug.Log("IrisIn");
        unmask.DOScale(IRIS_IN_SCALE, SCALE_DURATION).SetEase(Ease.InCubic);
    }

    public void IrisOut()
    {
        Debug.Log("IrisOut");
        unmask.DOScale(new Vector3(0, 0, 0), SCALE_DURATION).SetEase(Ease.OutCubic)
                                                            .OnComplete(() => GameManager.SetSceneChange());
    }
}
