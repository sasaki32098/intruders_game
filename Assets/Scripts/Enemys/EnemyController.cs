using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float TargetPosition = 0f;
    private float TweenDuration = 3.0f;
    public int ExitSpan = 1;

    private bool isLaserHit = false;
    public bool IsLaserHit { get => isLaserHit; set => isLaserHit = value; }

    private Tween tween;
    private EnemyManager enemyManager;

    public GameObject Explotion;

    private string myTag = string.Empty;

    private float timeScale;

    public void Init(EnemyManager _enemyManager, float _targetPosition, float _duration, float _speed)
    {
        myTag = gameObject.tag;

        enemyManager = _enemyManager;

        TweenDuration = _duration;
        timeScale = _speed;
        this.TargetPosition = _targetPosition;
    }

    void Start()
    {
        // float startDuration = TweenDuration / 2;

        tween = transform.DOMoveY(TargetPosition, TweenDuration).SetEase(Ease.InOutQuad).OnComplete(()
        => Started());
        tween.timeScale = timeScale;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if ((this.tween != null) && this.tween.active && this.tween.IsPlaying())
        {
            this.tween.timeScale = timeScale;
        }
    }

    public void Started()
    {
        tween = transform.DOMoveY(-TargetPosition, TweenDuration).SetLoops(this.ExitSpan, LoopType.Yoyo).SetEase(Ease.InOutQuad).OnComplete(
                () => LoopEnd(transform.position.y));
    }

    public void SetSpeed(float _timeScale)
    {
        timeScale = _timeScale;
    }

    private void LoopEnd(float positionY)
    {
        float moveYposition;
        if (positionY <= 0)
        {
            moveYposition = -10.0f;
        }
        else
        {
            moveYposition = 10.0f;
        }
        transform.DOMoveY(moveYposition, 1f).SetEase(Ease.InOutQuad).OnComplete(() => Hit(false));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "White_Enemy")
        {
            return;
        }
    }

    public void Hit(bool isHit = true)
    {
        enemyManager.Dead(this, isHit, myTag);
    }

    private void OnDestroy()
    {
        GameObject.Instantiate(this.Explotion, this.transform.position, Quaternion.identity);
    }
}
