﻿using UnityEngine;
using System.Collections;
using UnityEditor;
[System.Serializable]
public class Map : MonoBehaviour, IMapper
{
    public int width;
    public int height;
    [SerializeField]
    public bool[] m_tileGrid;
    void Start()
    {
    }

    public int scaleFactor
    {
        get
        {
            return 1;
        }
    }
    public Vector3 worldPos
    {
        get
        {
            return transform.position;
        }
    }
    public bool[,] tileGrid
    {
        get
        {
            bool[,] b = new bool[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    b[x, y] = m_tileGrid[x + y * width];
                }
            }
            return b;
        }
    }
    public void SetBoolArray()
    {
        m_tileGrid = new bool[width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                m_tileGrid[x + y * width] = true;
            }
        }
    }
    public void SetBoolArray(int x, int y, bool value)
    {
        m_tileGrid[x + y * width] = value;
    }
    public void OnDrawGizmos()
    {
        if (m_tileGrid != null)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!m_tileGrid[x + y * width])
                        Gizmos.DrawCube(new Vector3(x - (width / 2) + .5f, y - (height / 2) + .5f), new Vector3(1, 1));
                }
            }
        float _x = transform.position.x;
        float _y = transform.position.y;
        //outerLines
        Gizmos.DrawLine(new Vector3(width / 2 + _y, height / 2 + _x), new Vector3(-width / 2 + _y, height / 2 + _x));
        Gizmos.DrawLine(new Vector3(width / 2 + _y, height / 2 + _x), new Vector3(width / 2 + _y, -height / 2 + _x));
        Gizmos.DrawLine(new Vector3(-width / 2 + _y, -height / 2 + _x), new Vector3(-width / 2 + _y, height / 2 + _x));
        Gizmos.DrawLine(new Vector3(-width / 2 + _y, -height / 2 + _x), new Vector3(width / 2 + _y, -height / 2 + _x));

        //grid
        for (int y = -height / 2; y < height / 2; y++)
        {
            Gizmos.DrawLine(new Vector3(-width / 2, y), new Vector3(width / 2, y));
        }
        for (int x = -width / 2; x < width / 2; x++)
        {
            Gizmos.DrawLine(new Vector3(x, -height / 2), new Vector3(x, height / 2));
        }
    }
}
