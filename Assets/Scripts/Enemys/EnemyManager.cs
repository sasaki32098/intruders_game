using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> FirstLevelEnemys;
    public List<GameObject> SecondLevelEnemys;

    public float TimeSpan;
    public float SubTimeSpan;
    public float TargetPosition = 0;

    private float timeElapsed;
    private bool startFlag = false;
    private List<EnemyController> instantiateEnemys;
    private GameManager gameManager;

    public float enemySpeed = 1.0f;
    public float EnemyAddSpeed;
    private int level = 0;

    public AudioSource ExplosionSound;

    private int spawnCount = 0;

    public void Init(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    void Start()
    {
        instantiateEnemys = new List<EnemyController>();
        ExplosionSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (startFlag == true)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= TimeSpan)
            {
                Spawn();
                timeElapsed = 0.0f;
            }
        }
    }

    public void Spawn()
    {
        if (level == 0)
        {
            InstantiateEnemy(FirstLevelEnemys);
        }
        else if (level == 1)
        {
            InstantiateEnemy(SecondLevelEnemys);
        }
        else if (level == 2)
        {
            InstantiateEnemy(SecondLevelEnemys);
        }
        else
        {
            InstantiateEnemy(SecondLevelEnemys);
        }
    }

    public void InstantiateEnemy(List<GameObject> enemyList)
    {
        GameObject enemy = enemyList[Random.Range(0, enemyList.Count)];
        float yTargetPosition;
        float ySpawnPosition;

        if (spawnCount % 2 == 0)
        {
            yTargetPosition = -TargetPosition;
            ySpawnPosition = 6.0f;
        }
        else
        {
            yTargetPosition = TargetPosition;
            ySpawnPosition = -6.0f;
        }

        Vector3 spawnPos = new(Random.Range(-1, 3), ySpawnPosition, 0.0f);

        if (enemy.name == "Enemy_Big")
        {
            spawnPos = new(0.0f, ySpawnPosition, 0.0f);
        }

        GameObject obj = Instantiate(enemy,
                                    spawnPos,
                                    Quaternion.identity,
                                    this.transform);

        EnemyController enemyController = obj.GetComponent<EnemyController>();

        enemyController.Init(this, yTargetPosition, 3.0f, enemySpeed);
        instantiateEnemys.Add(enemyController);

        spawnCount++;
    }

    public void SetStartFlag(bool flag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in objects)
        {
            Destroy(item);
        }
        startFlag = flag;
    }

    public void SetSpeed()
    {
        TimeSpan -= SubTimeSpan;
        foreach (EnemyController e in instantiateEnemys)
        {
            enemySpeed += EnemyAddSpeed;
            e.SetSpeed(enemySpeed);
        }

        level++;
    }

    public void Dead(EnemyController _enemy, bool isHit, string tag)
    {
        instantiateEnemys.Remove(_enemy);
        Destroy(_enemy.gameObject);

        // 攻撃ポイントを更新
        if (isHit)
        {
            ExplosionSound.PlayOneShot(ExplosionSound.clip);
            if (tag == "Enemy")
            {
                gameManager.ChangeAttackPointStatusTrue();
            }
            else if (tag == "White_Enemy")
            {
                gameManager.ChangeAttackPointStatusFalse(tag);
            }
        }
    }
}
