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

        GUILayout.Label(new GUIContent("Map height and width", "The height of the map"));
        mapGen.height = EditorGUILayout.IntSlider(mapGen.height + mapGen.height % 2, 2, 100);
        mapGen.width = mapGen.height;
        GUILayout.EndHorizontal();


        if (GUILayout.Button(new GUIContent("Generate Map", "Some background stuff")))
        {
            var children = new List<GameObject>();
            foreach (Transform child in mapGen.transform) children.Add(child.gameObject);
            children.ForEach(child => DestroyImmediate(child));
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/HitBack.prefab", typeof(GameObject));
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            clone.transform.SetParent(mapGen.transform);
            clone.layer = 8;
            clone.GetComponent<BoxCollider>().size = new Vector3(mapGen.width, mapGen.height);
            mapGen.SetBoolArray();
            Transform wave = GameObject.FindGameObjectWithTag("Wave").transform;
            wave.localScale = new Vector3((float)mapGen.width / 10, 0, (float)mapGen.height / 10);
        }
        SceneView.RepaintAll();

    }
}
