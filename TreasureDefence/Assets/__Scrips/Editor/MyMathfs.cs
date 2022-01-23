using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMathfs
{
    public const float TAU = 6.28318530718f; // full turn in radiants (2 * Pi)

    public static Vector2 GetUnitVectorByAngle(float angRad)
    {
        return new Vector2(
            Mathf.Cos(angRad),
            Mathf.Sin(angRad)
        );
    }
}