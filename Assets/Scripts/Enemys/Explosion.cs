using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float mLength;
    private float mCur;

    void Start()
    {
        Animator animOne = GetComponent<Animator>();
        AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
        mLength = infAnim.length;
        mCur = 0;
    }

    void Update()
    {
        mCur += Time.deltaTime;
        if (mCur > mLength)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
