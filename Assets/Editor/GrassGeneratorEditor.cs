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
        grassGenerator.activate = EditorGUILayout.Toggle("Скрипт работает", grassGenerator.activate);

        EditorGUILayout.SelectableLabel("Исключения", EditorStyles.boldLabel);

        //EditorGUILayout.PropertyField(filter);
        EditorGUILayout.PropertyField(exceptionsObject);

        EditorGUILayout.SelectableLabel("Настройки травы", EditorStyles.boldLabel);

        grassGenerator.densityGrass = EditorGUILayout.Slider("Плотность травы", grassGenerator.densityGrass, 0.01f, 1);
        grassGenerator.scale = EditorGUILayout.Slider("Размер травы", grassGenerator.scale, 0.01f, 2);
        grassGenerator.widths = EditorGUILayout.Slider("Ширина травы", grassGenerator.widths, 0.01f, 2);

        grassGenerator.destoyGrass = EditorGUILayout.Toggle("Уничтожение травы", grassGenerator.destoyGrass);

        grassGenerator.allowRandomRotate = EditorGUILayout.Toggle("Рандомный поворот", grassGenerator.allowRandomRotate);
        if(!grassGenerator.allowRandomRotate) grassGenerator.angle = EditorGUILayout.FloatField("Поворот травы", grassGenerator.angle);

        EditorGUILayout.PropertyField(grassMat);

        EditorGUILayout.PropertyField(grassType);

        EditorGUILayout.SelectableLabel("Настройка генерации", EditorStyles.boldLabel);

        grassGenerator.width = EditorGUILayout.FloatField("Длина Участка", grassGenerator.width);
        grassGenerator.height = EditorGUILayout.FloatField("Ширина Участка", grassGenerator.height);
        grassGenerator.chunkCountEditor = EditorGUILayout.IntSlider("Количество чанков", grassGenerator.chunkCountEditor, 1, 3);
        grassGenerator.offShadow = EditorGUILayout.Toggle("Не отображать тени", grassGenerator.offShadow);

        EditorGUILayout.SelectableLabel("Настройка оптимизации", EditorStyles.boldLabel);

        grassGenerator.optimiz = EditorGUILayout.Toggle("Оптимизировать", grassGenerator.optimiz);
        if (grassGenerator.optimiz)
        {
            grassGenerator.maxDistOptimiz = EditorGUILayout.FloatField("Дистанция отключения", grassGenerator.maxDistOptimiz);
            grassGenerator.playerMaxSpeed = EditorGUILayout.FloatField("Макс скорость игрока", grassGenerator.playerMaxSpeed);
        }



        //grassGenerator.chunkCount = grassGenerator.chunkCount - grassGenerator.chunkCount % 2;
        //if (grassGenerator.chunkCount <= 0) grassGenerator.chunkCount = 1;

        if (GUILayout.Button("Сгенерировать"))
        {
            GenerateStart();
            grassGenerator.FindSizeGener();
            grassGenerator.StartGenerate();
            EditorUtility.SetDirty(grassGenerator);
        }

        /*if (GUILayout.Button("Найти SiGener"))
        {
            grassGenerator.FindSizeGener();
        }*/

        serializedObject.ApplyModifiedProperties();

        //EditorUtility.SetDirty(grassGenerator);
    }
}
