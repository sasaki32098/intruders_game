using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing2D
{
    public static Vector2 SineInOut(float t, float totaltime, Vector2 min, Vector2 max)
    {
        max -= min;
        return -max / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }
}
