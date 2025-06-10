using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject AttackPointManagerObject;
    public GameObject EnemyManagerObject;
    public GameObject PlayerControllerObject;
    public CameraShaker cameraShaker;
    public GameObject VignetteEffectObject;
    public IrisShot IrisShot;

    public bool StartFlag = false;
    public bool ResultFlag = false;

    private AttackPointManager attackPointManager;
    private EnemyManager enemyManager;
    public UImanager uiManager;
    private PlayerController playerController;
    private VignetteEffect vignetteEffect;

    public int SpeedUpCount = 0;
    public float AddSpeed = 0;
    public float AddTime = 0;
    private int enemyDestroyCount = 0;

    public float SubTime = 0;

    public GameStateModel.GameState MyGameState = GameStateModel.GameState.Title;

    public AudioSource BGMSound;

    private void Awake()
    {
        IrisShot.IrisIn();
    }

    void Start()
    {
        Application.targetFrameRate = 30;

        attackPointManager = AttackPointManagerObject.GetComponent<AttackPointManager>();

        enemyManager = EnemyManagerObject.GetComponent<EnemyManager>();
        enemyManager.Init(this);

        playerController = PlayerControllerObject.GetComponent<PlayerController>();

        vignetteEffect = VignetteEffectObject.GetComponent<VignetteEffect>();

        BGMSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetInputValue();
        }
    }

    public void SetInputValue()
    {

        switch (MyGameState)
        {
            case GameStateModel.GameState.Title:
                uiManager.StartDescription();
                break;
            case GameStateModel.GameState.displayingText:
                break;
            case GameStateModel.GameState.ReadyOnGame:
                GameStart();
                BGMSound.Play();
                MyGameState = GameStateModel.GameState.OnGame;
                break;
            case GameStateModel.GameState.OnGame:
                playerController.SetPlayerMove();
                break;
            case GameStateModel.GameState.Result:
                IrisShot.IrisOut();
                break;

        }
    }

    public void GameStart()
    {
        playerController.SetStartFlag(true);
        uiManager.SetUIEnable();
        enemyManager.SetStartFlag(true);
    }

    public void SetSceneChange()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void SetResult()
    {
        MyGameState = GameStateModel.GameState.displayingText;
        enemyManager.SetStartFlag(false);
        playerController.SetStartFlag(false);
        uiManager.SetResultUIEnable();
    }

    public void SetSpeedUp()
    {
        uiManager.SetSpeedUpText();
        playerController.SetSpeed(AddSpeed, false);
        enemyManager.SetSpeed();
        attackPointManager.SetReloadSpeed();
    }

    public bool CheckStatus()
    {
        return attackPointManager.CheckStatus();
    }

    public void ChangeAttackPointStatusTrue()
    {
        uiManager.EnemyHitCountAdd(AddTime);
        enemyDestroyCount++;
        cameraShaker.CameraShake();
        if (enemyDestroyCount >= SpeedUpCount)
        {
            SetSpeedUp();
            enemyDestroyCount = 0;
        }
    }

    public void ChangeAttackPointStatusFalse(string tag = "")
    {
        attackPointManager.ChangeStatus();

        if (tag.Equals("White_Enemy"))
        {
            vignetteEffect.StartVignette();
            cameraShaker.CameraShake();
            uiManager.EnemyHitCountSub(SubTime);
        }
    }
}
