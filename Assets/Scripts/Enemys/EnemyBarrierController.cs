using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrierController : MonoBehaviour
{
    public GameObject Target;
    public float rotateAngle;
    public float rotateSpeed;
    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, Target.transform.position.y - 1.5f, transform.position.z);
        transform.Rotate(0, 0, rotateAngle * Time.deltaTime * rotateSpeed);
    }
}
