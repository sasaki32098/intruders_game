using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private List<Transform> shakeObject;
    [SerializeField] private Vector3 positionStrength;

    private float shakeDuration = 0.3f;

    void Start()
    {

    }

    void Update()
    {

    }

    public void CameraShake()
    {
        foreach (Transform item in shakeObject)
        {
            item.DOComplete();
            item.DOShakePosition(shakeDuration, positionStrength);
        }
    }
}
