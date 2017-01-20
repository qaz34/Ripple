using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(Map))]
public class LevelEditor : Editor
{
    Map mapGen;
    public void OnEnable()
    {
        mapGen = (Map)target;
        SceneView.onSceneGUIDelegate += MapUpdate;
    }
    void MapUpdate(SceneView sceneView)
    {
        
        Event e = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector2(e.mousePosition.x, Camera.current.pixelHeight - e.mousePosition.y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && e.keyCode == KeyCode.L)
        {
            Vector3 local_point = new Vector3(Mathf.Floor(hit.point.x + mapGen.height / 2), Mathf.Floor(hit.point.y + mapGen.width / 2));
            
        }
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        GUILayout.Label(new GUIContent("Map height", "The height of the map"));
        mapGen.width = EditorGUILayout.IntSlider(mapGen.width + mapGen.width % 2, 2, 100);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Map width", "The width of the map"));
        mapGen.height = EditorGUILayout.IntSlider(mapGen.height - mapGen.height % 2, 2, 100);
        GUILayout.EndHorizontal();

        if (GUILayout.Button(new GUIContent("Generate Map", "Some background stuff")))
        {
            var children = new List<GameObject>();
            foreach (Transform child in mapGen.transform) children.Add(child.gameObject);
            children.ForEach(child => DestroyImmediate(child));
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/HitBack.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            clone.transform.SetParent(mapGen.transform);
            clone.GetComponent<BoxCollider>().size = new Vector3(mapGen.height, mapGen.width);
            mapGen.m_tileGrid = new bool[mapGen.width, mapGen.height];
        }
        SceneView.RepaintAll();
    }
}
