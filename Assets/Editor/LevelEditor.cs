using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(Map))]
public class LevelEditor : Editor
{
    Map mapGen;
    bool first;
    SerializedProperty gridArray;
    public void OnEnable()
    {
        mapGen = (Map)target;      
    }
    void OnSceneGUI()
    {
        EditorUtility.SetDirty(mapGen);
        Event e = Event.current;
        Ray ray = Camera.current.ScreenPointToRay(new Vector2(e.mousePosition.x, Camera.current.pixelHeight - e.mousePosition.y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && e.type == EventType.keyDown && e.keyCode == KeyCode.L && first)
        {
            first = false;
            Vector3 local_point = new Vector3(Mathf.Floor(hit.point.x + mapGen.width / 2), Mathf.Floor(hit.point.y + mapGen.height / 2));
            mapGen.SetBoolArray((int)local_point.x, (int)local_point.y, !mapGen.m_tileGrid[(int)local_point.x + (int)local_point.y * mapGen.width]);
        }
        else if (e.type == EventType.keyUp && e.keyCode == KeyCode.L)
            first = true;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUILayout.BeginHorizontal();

        GUILayout.Label(new GUIContent("Map height", "The height of the map"));
        mapGen.height = EditorGUILayout.IntSlider(mapGen.height + mapGen.height % 2, 2, 30);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Map width", "The width of the map"));
        mapGen.width = EditorGUILayout.IntSlider(mapGen.width - mapGen.width % 2, 2, 30);
        GUILayout.EndHorizontal();

        if (GUILayout.Button(new GUIContent("Generate Map", "Some background stuff")))
        {
            var children = new List<GameObject>();
            foreach (Transform child in mapGen.transform) children.Add(child.gameObject);
            children.ForEach(child => DestroyImmediate(child));
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/HitBack.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            clone.transform.SetParent(mapGen.transform);
            clone.GetComponent<BoxCollider>().size = new Vector3(mapGen.width, mapGen.height);
            mapGen.SetBoolArray();
        }
        SceneView.RepaintAll();
        
    }
}
