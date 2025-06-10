using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AttackPointController : MonoBehaviour
{
    private Image image;
    private Sprite sprite;

    private float reloadSpeed = 1.0f;

    private Coroutine _changeStatusCoroutine;

    public int Status = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeStatusEmpty()
    {
        sprite = Resources.Load<Sprite>("AttackPoint_Empty");
        image = this.GetComponent<Image>();
        image.sprite = sprite;

        Status = 2;
    }

    public void StartChangeStatusReload()
    {
        Status = 3;
        _changeStatusCoroutine = StartCoroutine(ChangeStatusReloadCoroutine());
    }

    public void StopChangeStatusReload()
    {
        Status = 2;
        StopCoroutine(_changeStatusCoroutine);
        _changeStatusCoroutine = null;
    }

    public void ChangeStatusReload()
    {
        sprite = Resources.Load<Sprite>("AttackPoint_Max_Gray");
        image = this.GetComponent<Image>();
        image.sprite = sprite;

        Status = 1;
    }

    private IEnumerator ChangeStatusReloadCoroutine()
    {
        yield return new WaitForSeconds(this.reloadSpeed);

        ChangeStatusReload();
    }

    public void SetSpeed(float reloadSpeed)
    {
        this.reloadSpeed -= reloadSpeed;
    }
}
