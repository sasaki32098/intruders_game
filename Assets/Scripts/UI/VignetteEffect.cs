using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteEffect : MonoBehaviour
{
    private Animator myAnimator;

    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void StartVignette()
    {
        myAnimator.SetTrigger("TriggerVignette");
    }

    public void EndVignette()
    {
    }
}
