using System;
using UnityEngine;

public interface IMapper
{
    int scaleFactor
    {
        //square size
        get;
    }

    Vector3 worldPos
    {
        //center
        get;
    }

    bool[,] tileGrid
    {
        //tilegrid
        get;
    }
}


