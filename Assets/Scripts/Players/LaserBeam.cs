using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public int xDirection;
    public bool isRayCast = false;
    public LineRenderer lineRenderer;
    public float lineWidth = 0;
    public float lineStartPosition = 0;

    public int deleteCount = 1;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    void Update()
    {

    }

    [System.Obsolete]
    public bool Shot()
    {
        bool hit = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRayCast)
            {
                lineRenderer.enabled = false;
                hit = OnRay();
            }
        }

        return hit;
    }

    [System.Obsolete]
    bool OnRay()
    {
        bool enemyHit = false;

        Vector2 origin = new Vector3(this.gameObject.transform.position.x - (lineStartPosition),
                                     this.gameObject.transform.position.y, 0);
        Vector2 direction = new Vector3(xDirection, 0, 0);

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, origin);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 10f, 1 << 6);
        if (hit)
        {
            // Debug.Log($"collider:{hit.collider} point:{hit.point} name:{hit.transform.name} layer:{hit.transform.gameObject.layer}");
            enemyHit = true;
            EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController>();
            enemy.Hit(true);
        }

        lineRenderer.startWidth = lineWidth;
        lineRenderer.SetPosition(1, new Vector3(direction.x * 2.0f, origin.y, 0));
        StartCoroutine(FadeOut());

        return enemyHit;
    }

    [System.Obsolete]
    public IEnumerator FadeOut()
    {
        int a = deleteCount;
        while (a > 0)
        {
            yield return new WaitForSeconds(0.01f);
            a -= 1;
        }
        lineRenderer.enabled = false;
        yield break;
    }
}
