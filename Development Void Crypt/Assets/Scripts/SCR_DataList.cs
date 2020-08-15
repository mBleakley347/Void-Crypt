using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAreas
{
    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;

    public bool seen = false;

    public void Set(bool temp)
    {
        left = temp;
        right = temp;
        up = temp;
        down = temp;
        seen = temp;
    }
}

public class Enums
{
    public enum directions
    {
        right,
        left,
        up,
        down
    };
}