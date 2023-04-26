using UnityEngine;

public static class VectorExtansion
{
    public static Vector3 WithAxis(this Vector3 v, Axis axis, float value)
    {
        return new Vector3(
            axis == Axis.X ? value : v.x,
            axis == Axis.Y ? value : v.y,
            axis == Axis.Z ? value : v.z
            );
    }
}

public enum Axis
{
    X, Y, Z
}