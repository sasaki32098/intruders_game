using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackPointManager : MonoBehaviour
{
    public List<GameObject> AttackPoints;

    public float GaugeReloadSpeed = 0f;

    private List<AttackPointController> AttackPointsControllers = new List<AttackPointController>();

    private AttackPointController attackPoint;
    private int reloadStatus = 0;
    void Start()
    {
        foreach (var item in AttackPoints)
        {
            AttackPointsControllers.Add(item.GetComponent<AttackPointController>());
        }

        attackPoint = AttackPointsControllers[0];
    }

    void Update()
    {
        Reload();
    }

    private void Reload()
    {
        if (attackPoint == null)
        {
            attackPoint = AttackPointsControllers[0];
        }

        if (attackPoint.Status != 3)
        {
            if (attackPoint = AttackPointsControllers.LastOrDefault(n => n.Status == 2))
            {
                attackPoint.StartChangeStatusReload();
            }
        }
    }

    public bool CheckStatus()
    {
        bool status = true;

        int count = 1;

        foreach (var attackPoint in AttackPointsControllers)
        {
            if (attackPoint.Status == 1)
            {
                // 一個でもtrueならbreakする。
                break;
            }

            if (count >= AttackPointsControllers.Count)
            {
                status = false;
            }

            count++;
        }

        return status;
    }

    public void ChangeStatus()
    {
        foreach (var attackPoint in AttackPointsControllers)
        {
            if (attackPoint.Status == 1)
            {
                attackPoint.ChangeStatusEmpty();
                break;
            }
            else if (attackPoint.Status == 3)
            {
                attackPoint.StopChangeStatusReload();
            }
        }
    }

    public void SetReloadSpeed()
    {
        foreach (var attackPoint in AttackPointsControllers)
        {
            attackPoint.SetSpeed(this.GaugeReloadSpeed);
        }
    }
}
