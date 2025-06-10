using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserBeamVersionColision : MonoBehaviour
{
    private bool isHit = false;
    public UnityAction LaserUnityAction;
    public float LifeTime = 0;

    void Start()
    {

    }

    void Update()
    {
        Destroy(this.gameObject, LifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "White_Enemy")
        {
            Hit(other.gameObject);
            isHit = true;
        }
    }

    private bool Hit(GameObject hitEnemy = null)
    {
        bool enemyHit = false;

        if (hitEnemy != null)
        {
            enemyHit = true;

            EnemyController enemy = hitEnemy.GetComponent<EnemyController>();
            enemy.Hit(true);
        }

        return enemyHit;
    }

    private void OnDestroy()
    {
        if (!isHit)
        {
            LaserUnityAction?.Invoke();
        }
    }
}
