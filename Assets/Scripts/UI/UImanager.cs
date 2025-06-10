using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    public GameObject TitleObject;
    public GameObject StartUIObject;
    public GameObject Description;
    public GameObject EndUIObject;
    public GameObject TimerTextObject;
    public GameObject EnemyCountObject;
    public GameObject SpaceKeyImageObject;
    public GameObject SpeedUpObject;
    public GameObject ResultCountTextObject;
    public GameObject ResultTextObject;
    public GameObject ResultReturnToTitleObject;

    public GameManager gameManager;
    public float Timer = 60.0f;

    public GameObject TextSound;

    private TMP_Text description;
    private TMP_Text result;
    private TMP_Text returnToTitle;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI enemyCountText;
    private Image spaceKeyImage;
    private TextMeshProUGUI speedUpText;
    private TextMeshProUGUI resultCountText;
    private Animator textPingPong;
    private AudioSource textSound;

    private int enemyCount = 0;
    public bool startFlag = false;

    void Start()
    {
        description = Description.GetComponent<TMP_Text>();
        description.maxVisibleCharacters = 0;

        result = ResultTextObject.GetComponent<TMP_Text>();
        result.maxVisibleCharacters = 0;

        returnToTitle = ResultReturnToTitleObject.GetComponent<TMP_Text>();
        returnToTitle.maxVisibleCharacters = 0;

        timerText = TimerTextObject.GetComponent<TextMeshProUGUI>();
        textPingPong = TimerTextObject.GetComponent<Animator>();
        timerText.text = string.Empty;

        enemyCountText = EnemyCountObject.GetComponent<TextMeshProUGUI>();
        enemyCountText.text = string.Empty;

        spaceKeyImage = SpaceKeyImageObject.GetComponent<Image>();
        spaceKeyImage.sprite = Resources.Load<Sprite>("UI/SpaceKey_01");

        speedUpText = SpeedUpObject.GetComponent<TextMeshProUGUI>();
        speedUpText.text = string.Empty;

        resultCountText = ResultCountTextObject.GetComponent<TextMeshProUGUI>();
        resultCountText.text = string.Empty;

        textSound = TextSound.GetComponent<AudioSource>();

        StartUIObject.SetActive(false);
        EndUIObject.SetActive(false);
    }

    void Update()
    {
        if (startFlag)
        {
            TimerControl();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceKeyImage.sprite = Resources.Load<Sprite>("UI/SpaceKey_02");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceKeyImage.sprite = Resources.Load<Sprite>("UI/SpaceKey_01");
        }
    }

    private IEnumerator ShowDescriptionText(TMP_Text showText, bool OnGameFlag = true)
    {
        // 文字の表示数を0に(テキストが表示されなくなる)
        showText.maxVisibleCharacters = 0;

        // テキストの文字数分ループ
        for (var i = 0; i < showText.text.Length; i++)
        {
            // 一文字ごとに0.2秒待機
            yield return new WaitForSeconds(0.1f);

            // 文字の表示数を増やしていく
            showText.maxVisibleCharacters = i + 1;

            textSound.PlayOneShot(textSound.clip);
        }

        if (OnGameFlag)
        {
            gameManager.MyGameState = GameStateModel.GameState.ReadyOnGame;
        }
    }

    public void StartDescription()
    {
        this.gameManager.MyGameState = GameStateModel.GameState.displayingText;
        TitleObject.SetActive(false);

        StartUIObject.SetActive(true);
        StartCoroutine(ShowDescriptionText(description));
    }

    public void SetUIEnable()
    {
        StartUIObject.SetActive(false);

        enemyCountText.text = "0 体ゲキハ";
        startFlag = true;
    }

    public void SetResultUIEnable()
    {
        StartCoroutine(StartResultShow());
    }

    private IEnumerator StartResultShow()
    {
        EndUIObject.SetActive(true);
        yield return StartCoroutine(ShowDescriptionText(result, false));

        resultCountText.text = enemyCount.ToString();

        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(ShowDescriptionText(returnToTitle, false));

        this.gameManager.MyGameState = GameStateModel.GameState.Result;
    }

    private void TimerControl()
    {
        string timerSecond = "##";

        Timer -= Time.deltaTime;
        if (Timer <= 1)
        {
            timerSecond = "0";
        }
        timerText.text = Timer.ToString($"{timerSecond}.##");

        if (0.0f >= Timer)
        {
            // タイマーを消す
            startFlag = false;
            timerText.text = "";
            gameManager.SetResult();
        }
    }

    public void EnemyHitCountAdd(float addTime)
    {
        enemyCount++;
        enemyCountText.text = $"{enemyCount} 体ゲキハ";
        Timer += addTime;
        textPingPong.SetTrigger("PingPongTrigger");
    }

    public void EnemyHitCountSub(float subTime)
    {
        Timer -= subTime;
    }

    public void SetSpeedUpText()
    {
        speedUpText.text = "SPEED UP";
        StartCoroutine(SetSpeedUpTextCorutine());
    }

    private IEnumerator SetSpeedUpTextCorutine()
    {
        yield return new WaitForSeconds(1.0f);
        speedUpText.text = string.Empty;
    }
}
