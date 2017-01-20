using UnityEngine;
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
            return new bool[1, 1];
        }
    }
    public void SetBoolArray()
    {
        m_tileGrid = new bool[width * height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                m_tileGrid[i + j * width] = true;
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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!m_tileGrid[i + j * width])
                        Gizmos.DrawCube(new Vector3(j - (height / 2) + .5f, i - (width / 2) + .5f), new Vector3(1, 1));
                }
            }
        float _x = transform.position.x;
        float _y = transform.position.y;
        //outerLines
        Gizmos.DrawLine(new Vector3(height / 2 + _x, width / 2 + _y), new Vector3(height / 2 + _x, -width / 2 + _y));
        Gizmos.DrawLine(new Vector3(height / 2 + _x, width / 2 + _y), new Vector3(-height / 2 + _x, width / 2 + _y));
        Gizmos.DrawLine(new Vector3(-height / 2 + _x, -width / 2 + _y), new Vector3(height / 2 + _x, -width / 2 + _y));
        Gizmos.DrawLine(new Vector3(-height / 2 + _x, -width / 2 + _y), new Vector3(-height / 2 + _x, width / 2 + _y));
        //grid
        for (int y = -width / 2; y < width / 2; y++)
        {
            Gizmos.DrawLine(new Vector3(-height / 2, y), new Vector3(height / 2, y));
        }
        for (int x = -height / 2; x < height / 2; x++)
        {
            Gizmos.DrawLine(new Vector3(x, -width / 2), new Vector3(x, width / 2));
        }
    }
}
