using UnityEngine;

public static class Ease
{
    public static float OutQuint(float x)
    {
        //float x = (n1 - n0) * per;
        return 1 - Mathf.Pow(1 - x, 5);
    }
}
