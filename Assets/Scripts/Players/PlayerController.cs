using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    private float speed = 0;
    public float DefaultSpeed;
    private bool flag = true;
    private bool stopFlag = true;
    private bool stayFlag = true;
    private bool startFlag = false;
    private Rigidbody2D myRigidBody;

    public GameObject leftBeamObject;
    public GameObject rightBeamObject;
    private LaserBeam leftBeam;
    private LaserBeam rightBeam;

    public GameObject Beam;
    private LaserBeamVersionColision beam;

    public GameObject GameManagerObject;
    private GameManager GameManager;

    public AudioSource LaserSound;

    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();

        GameManager = GameManagerObject.GetComponent<GameManager>();
        leftBeam = leftBeamObject.GetComponent<LaserBeam>();
        rightBeam = rightBeamObject.GetComponent<LaserBeam>();

        LaserSound = GetComponent<AudioSource>();

        speed = DefaultSpeed;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (startFlag == true)
        {
            if (stopFlag)
            {
                if (flag)
                {
                    myRigidBody.velocity = new Vector2(0.0f, speed * Time.deltaTime);
                }
                else
                {
                    myRigidBody.velocity = new Vector2(0.0f, -speed * Time.deltaTime);
                }
            }
            else
            {
                myRigidBody.velocity = Vector2.zero;
            }
        }
    }

    public void SetStartFlag(bool flag)
    {
        startFlag = flag;
        if (flag == false)
        {
            myRigidBody.velocity = Vector2.zero;
        }
    }

    public void SetPlayerMove()
    {
        if (stayFlag)
        {
            if (GameManager.CheckStatus())
            {
                flag = !flag;

                LaserSound.PlayOneShot(LaserSound.clip);

                GameObject enemyObject = Instantiate(Beam
                            , new Vector3(this.transform.position.x, this.transform.position.y, 0.0f)
                            , Quaternion.identity);

                enemyObject.GetComponent<LaserBeamVersionColision>().LaserUnityAction = SetAttackPoint;
            }
        }
    }

    public void SetAttackPoint()
    {
        GameManager.ChangeAttackPointStatusFalse();
    }

    public void SetSpeed(float speed, bool isReset)
    {
        if (isReset)
        {
            this.speed = DefaultSpeed;
        }
        else
        {
            this.speed += speed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Enemy" && other.tag != "White_Enemy")
        {
            StartCoroutine(KeyDownStop());
            stayFlag = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy" && other.tag != "White_Enemy")
        {
            flag = !flag;
            stayFlag = false;
            StartCoroutine(Stop());
        }
    }

    private IEnumerator Stop()
    {
        stopFlag = false;
        yield return new WaitForSecondsRealtime(1.0f);
        stopFlag = true;
    }

    private IEnumerator KeyDownStop()
    {
        yield return new WaitForSecondsRealtime(0.1f);
    }
}

