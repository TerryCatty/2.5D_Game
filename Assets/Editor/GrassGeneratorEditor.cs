using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Drawing.Printing;
using UnityEngine.Rendering;

[CustomEditor(typeof(GrassGenerator))]
public class GrassGeneratorEditor : Editor
{
    SerializedProperty filter;
    SerializedProperty exceptionsObject;
    SerializedProperty grassMat;
    SerializedProperty grassType;

    private void OnEnable()
    {
        filter = serializedObject.FindProperty("filter");
        exceptionsObject = serializedObject.FindProperty("exceptionsObject");
        grassMat = serializedObject.FindProperty("grassMat");
        grassType = serializedObject.FindProperty("grassType");
    }

    public void GenerateStart()
    {
        string[] unusedFolder = { "Assets/GrassData" };

        foreach (var asset in AssetDatabase.FindAssets("", unusedFolder))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }

        GrassGenerator grassGenerator = (GrassGenerator)target;

        GameObject[] tempArray = new GameObject[grassGenerator.transform.childCount];

        for (int i = 0; i < grassGenerator.transform.childCount; i++)
        {
            tempArray[i] = grassGenerator.transform.GetChild(i).gameObject;
        }

        foreach (var child in tempArray)
        {
            DestroyImmediate(child);
        }

        grassGenerator.chunkCount = (int) Mathf.Pow(4, grassGenerator.chunkCountEditor);

        int chunkX = (int)Mathf.Sqrt(grassGenerator.chunkCount);

        float offsetX = grassGenerator.width / (chunkX * 1.0f);
        float offsetZ = grassGenerator.height / (chunkX * 1.0f);

        for (int i = 0; i < grassGenerator.chunkCount; i++)
        {
            GameObject newObj = new GameObject("Chunk" + i.ToString());

            newObj.AddComponent<MeshFilter>();
            MeshRenderer rend = newObj.AddComponent<MeshRenderer>();

            rend.sharedMaterial = grassGenerator.grassMat;

            newObj.transform.parent = grassGenerator.transform;
            newObj.transform.localScale = new Vector3(1, 1, 1);

            float z = (i - i % chunkX) / chunkX;
            float x = i % chunkX;

            newObj.transform.localPosition = new Vector3(x * offsetX, 0, z * offsetZ);

        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GrassGenerator grassGenerator = (GrassGenerator)target;
        grassGenerator.activate = EditorGUILayout.Toggle("������ ��������", grassGenerator.activate);

        EditorGUILayout.SelectableLabel("����������", EditorStyles.boldLabel);

        //EditorGUILayout.PropertyField(filter);
        EditorGUILayout.PropertyField(exceptionsObject);

        EditorGUILayout.SelectableLabel("��������� �����", EditorStyles.boldLabel);

        grassGenerator.densityGrass = EditorGUILayout.Slider("��������� �����", grassGenerator.densityGrass, 0.01f, 1);
        grassGenerator.scale = EditorGUILayout.Slider("������ �����", grassGenerator.scale, 0.01f, 2);
        grassGenerator.widths = EditorGUILayout.Slider("������ �����", grassGenerator.widths, 0.01f, 2);

        grassGenerator.destoyGrass = EditorGUILayout.Toggle("����������� �����", grassGenerator.destoyGrass);

        grassGenerator.allowRandomRotate = EditorGUILayout.Toggle("��������� �������", grassGenerator.allowRandomRotate);
        if(!grassGenerator.allowRandomRotate) grassGenerator.angle = EditorGUILayout.FloatField("������� �����", grassGenerator.angle);

        EditorGUILayout.PropertyField(grassMat);

        EditorGUILayout.PropertyField(grassType);

        EditorGUILayout.SelectableLabel("��������� ���������", EditorStyles.boldLabel);

        grassGenerator.width = EditorGUILayout.FloatField("����� �������", grassGenerator.width);
        grassGenerator.height = EditorGUILayout.FloatField("������ �������", grassGenerator.height);
        grassGenerator.chunkCountEditor = EditorGUILayout.IntSlider("���������� ������", grassGenerator.chunkCountEditor, 1, 3);
        grassGenerator.offShadow = EditorGUILayout.Toggle("�� ���������� ����", grassGenerator.offShadow);

        EditorGUILayout.SelectableLabel("��������� �����������", EditorStyles.boldLabel);

        grassGenerator.optimiz = EditorGUILayout.Toggle("��������������", grassGenerator.optimiz);
        if (grassGenerator.optimiz)
        {
            grassGenerator.maxDistOptimiz = EditorGUILayout.FloatField("��������� ����������", grassGenerator.maxDistOptimiz);
            grassGenerator.playerMaxSpeed = EditorGUILayout.FloatField("���� �������� ������", grassGenerator.playerMaxSpeed);
        }



        //grassGenerator.chunkCount = grassGenerator.chunkCount - grassGenerator.chunkCount % 2;
        //if (grassGenerator.chunkCount <= 0) grassGenerator.chunkCount = 1;

        if (GUILayout.Button("�������������"))
        {
            GenerateStart();
            grassGenerator.FindSizeGener();
            grassGenerator.StartGenerate();
            EditorUtility.SetDirty(grassGenerator);
        }

        /*if (GUILayout.Button("����� SiGener"))
        {
            grassGenerator.FindSizeGener();
        }*/

        serializedObject.ApplyModifiedProperties();

        //EditorUtility.SetDirty(grassGenerator);
    }
}
