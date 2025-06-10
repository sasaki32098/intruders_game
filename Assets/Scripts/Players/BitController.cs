using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BitController : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {

    }

    void Update()
    {
        gameObject.transform.DOLocalMoveY(Player.transform.position.y, 0.1f);
    }
}
