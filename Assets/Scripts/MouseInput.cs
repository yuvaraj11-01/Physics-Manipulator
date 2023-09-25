using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseInput
{
    static Vector3 tempPlayerPos = Vector3.right;

    public static Vector3 GetPlayerMousePos(out bool inBounds)
    {
        var width = Camera.main.scaledPixelWidth;
        inBounds = false;

        if (Input.mousePosition.x < width)
        {
            tempPlayerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inBounds = true;
        }

        return tempPlayerPos;
    }
}
