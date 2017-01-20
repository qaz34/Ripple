using UnityEngine;
using System.Collections;
using UnityEditor;
public class Map : MonoBehaviour, IMapper
{
    public int width;
    public int height;
    int scale = 1;
    private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;

    public bool[,] m_tileGrid;
    public int scaleFactor
    {
        get
        {
            return scale;
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
            return m_tileGrid;
        }
    }
    public static void GUIDrawRect(Rect position, Color color)
    {
        if (_staticRectTexture == null)
        {
            _staticRectTexture = new Texture2D(1, 1);
        }

        if (_staticRectStyle == null)
        {
            _staticRectStyle = new GUIStyle();
        }

        _staticRectTexture.SetPixel(0, 0, color);
        _staticRectTexture.Apply();

        _staticRectStyle.normal.background = _staticRectTexture;

        GUI.Box(position, GUIContent.none, _staticRectStyle);
    }

    void OnGUI()
    {
        GUIDrawRect(new Rect(1, 1, 1, 1), Color.red);
    }

    public void OnDrawGizmos()
    {
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
