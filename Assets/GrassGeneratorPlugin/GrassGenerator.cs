using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static GrassGenerator;


//using UnityEditor;

public class GrassGenerator : MonoBehaviour
{

    public bool activate;

    List<Vector3> vertices = new List<Vector3>();
    List<Vector3> normals = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    public enum GrassType { Quad = 1, Triangles = 2 };
    public GrassType grassType;

    int ids = 0;

    [Range(0.01f, 1)] public float densityGrass = 0.5f;

    public float scale = 0.5f;
    public float widths = 0.5f;

    //float shadowWidthUv = 1f;

    public SizeGener[] exceptionsObject;

    public float width = 110;
    public float height = 110;

    public int chunkCount = 1;
    public int chunkCountEditor = 1;

    public bool allowRandomRotate;
    public float angle;

    public Material grassMat;

    public bool optimiz;
    public float maxDistOptimiz;
    public float playerMaxSpeed = 1;

    public bool destoyGrass = false;

    public bool offShadow = false;

    //[SerializeField] float maxDist;

    public void FindSizeGener()
    {
        exceptionsObject = FindObjectsOfType<SizeGener>();
    }

    void PreGener(int chunkX, int chunkY)
    {
        int ch = (int) Mathf.Sqrt(chunkCount);

        for (float x = 0; x < width / ch; x += scale * widths)
        {
            for (float z = 0; z < height / ch; z += scale * widths)
            {
                int rnd = Random.Range(0, 100);
                if (rnd < densityGrass * 100)
                {
                    Vector3 pos = new Vector3(x, 0, z);

                    if (exceptionsObject.Length == 0)
                    {
                        GenerateGrass(pos);
                        continue;
                    }

                    float minDist = 999;
                    SizeGener minGener = exceptionsObject[0];

                    for (int dom = 0; dom < exceptionsObject.Length; dom++)
                    {
                        if (exceptionsObject[dom] == null) continue;

                        Vector3 grassPosition = pos + transform.position + new Vector3(width / ch * chunkX, 0, height / ch * chunkY);

                        float dist = Vector3.Distance(grassPosition, exceptionsObject[dom].transform.position);
                        if (minDist > dist)
                        {
                            minGener = exceptionsObject[dom];
                            minDist = dist;
                        }
                    }
                    if (minDist > minGener.sizeGener) GenerateGrass(pos);
                }
            }
        }
    }

    public void StartGenerate()
    {
        angle = Mathf.Deg2Rad * 1f * angle;

        if (!activate) return;

        int chunkX = (int) Mathf.Sqrt(chunkCount);

        int alls = 0;

        for(int y = 0; y < chunkX; y++)
        {
            for (int x = 0; x < chunkX; x++)
            {
                PreGener(x, y);
                MeshFilter filter = transform.GetChild(alls).GetComponent<MeshFilter>();
                MeshRenderer rend = transform.GetChild(alls).GetComponent<MeshRenderer>();

                if (offShadow) rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                EndGenerate(filter, alls.ToString());
                alls++;
            }
        }
    }

    void AddUvs(bool shadowed)
    {
        float addUvs = 0;

        //if (shadowed) addUvs = shadowWidthUv;

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));
    }

    Vector3[] CalculateNormalsManaged(Vector3[] verts, Vector3[] normals, int[] tris)
    {
        for (int i = 0; i < tris.Length; i += 3)
        {
            int tri0 = tris[i];
            int tri1 = tris[i + 1];
            int tri2 = tris[i + 2];
            Vector3 vert0 = verts[tri0];
            Vector3 vert1 = verts[tri1];
            Vector3 vert2 = verts[tri2];
            // Vector3 normal = Vector3.Cross(vert1 - vert0, vert2 - vert0);
            Vector3 normal = new Vector3()
            {
                x = vert0.y * vert1.z - vert0.y * vert2.z - vert1.y * vert0.z + vert1.y * vert2.z + vert2.y * vert0.z - vert2.y * vert1.z,
                y = -vert0.x * vert1.z + vert0.x * vert2.z + vert1.x * vert0.z - vert1.x * vert2.z - vert2.x * vert0.z + vert2.x * vert1.z,
                z = vert0.x * vert1.y - vert0.x * vert2.y - vert1.x * vert0.y + vert1.x * vert2.y + vert2.x * vert0.y - vert2.x * vert1.y
            };
            normals[tri0] += normal;
            normals[tri1] += normal;
            normals[tri2] += normal;
        }

        for (int i = 0; i < normals.Length; i++)
        {
            // normals [i] = Vector3.Normalize (normals [i]);
            Vector3 norm = normals[i];
            float invlength = 1.0f / (float)System.Math.Sqrt(norm.x * norm.x + norm.y * norm.y + norm.z * norm.z);
            normals[i].x = norm.x * invlength;
            normals[i].y = norm.y * invlength;
            normals[i].z = norm.z * invlength;
        }

        return normals;
    }

    void GenerateGrassTriangles(Vector3 pos, bool shadowed = false)
    {
        pos += new Vector3(Random.Range(-10, 10) / 10.0f, 0, Random.Range(-10, 10) / 10.0f);
        List<Vector3> verticesCh = new List<Vector3>();

        verticesCh.Add(new Vector3(0, 0, 0));
        verticesCh.Add(new Vector3(1 * scale * widths, 0, 0));
        verticesCh.Add(new Vector3(1 * scale * widths / 2, 1 * scale, 0));

        if (allowRandomRotate)
        {
            angle = Random.Range(0, 90) * 1f;
            angle = Mathf.Deg2Rad * angle;
        }

        for (int i = 0; i < verticesCh.Count; i++)
        {
            float x = verticesCh[i].x;
            float z = verticesCh[i].z;

            float xx = x * Mathf.Cos(angle) - z * Mathf.Sin(angle);
            float zz = x * Mathf.Sin(angle) + z * Mathf.Cos(angle);

            verticesCh[i] = new Vector3(xx, verticesCh[i].y, zz);

            normals.Add(new Vector3(xx, 1, zz));

            vertices.Add(pos + verticesCh[i]);
        }

        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(0.5f, 1));

        triangles.Add(ids);
        triangles.Add(ids + 1);
        triangles.Add(ids + 2);

        triangles.Add(ids + 2);
        triangles.Add(ids + 1);
        triangles.Add(ids);

        ids += 3;
    }

    public void GenerateGrass(Vector3 pos, bool shadowed = false)
    {
        
        if(grassType == GrassType.Triangles)
        {
            GenerateGrassTriangles(pos, shadowed);
            return;
        }
        //int rnds = Mathf.FloorToInt(Random.RandomRange(0, 10));

        /*if(rnds == 0) rnd = new Vector3(0, 0, 1 * scale * widths);
        else if(rnds == 1) rnd = new Vector3(0, 0, -1 * scale * widths);*/

        pos += new Vector3(Random.Range(-10,10) / 10.0f, 0, Random.Range(-10, 10) / 10.0f);

        List<Vector3> verticesCh = new List<Vector3>();

        verticesCh.Add(new Vector3(0, 0, 0));
        verticesCh.Add(new Vector3(1 * scale * widths, 0, 0));
        verticesCh.Add(new Vector3(0, 1 * scale, 0));
        verticesCh.Add(new Vector3(1 * scale * widths, 1 * scale, 0));

        //Перевернутый

        verticesCh.Add(new Vector3(1 * scale * widths / 2, 0, -1 * scale * widths / 2));
        verticesCh.Add( new Vector3(1 * scale * widths / 2, 0, 1 * scale * widths / 2));
        verticesCh.Add(new Vector3(1 * scale * widths / 2, 1 * scale, -1 * scale * widths / 2));
        verticesCh.Add(new Vector3(1 * scale * widths / 2, 1 * scale, 1 * scale * widths / 2));

        //float angle = 45;

        if (allowRandomRotate)
        {
            angle = Random.Range(0, 90) * 1f;
            angle = Mathf.Deg2Rad * angle;
        }

        for (int i = 0; i < verticesCh.Count; i++)
        {
            float x = verticesCh[i].x;
            float z = verticesCh[i].z;

            float xx = x * Mathf.Cos(angle) - z * Mathf.Sin(angle);
            float zz = x * Mathf.Sin(angle) + z * Mathf.Cos(angle);

            verticesCh[i] = new Vector3(xx, verticesCh[i].y, zz);

            normals.Add(new Vector3(xx, 1, zz));

            vertices.Add(pos + verticesCh[i]);
        }

        /*normals.Add(-Vector3.forward);
        normals.Add(-Vector3.forward);
        normals.Add(-Vector3.forward);
        normals.Add(-Vector3.forward);

        normals.Add(-Vector3.forward);
        normals.Add(-Vector3.forward);
        normals.Add(-Vector3.forward);
        normals.Add(-Vector3.forward);*/

        

        //vertices.Add(pos + new Vector3(0.5f * scale * widths, 2 * scale, 0) + rnd * 0.5f);

        /*uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(1 * shadowWidthUv, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1 * shadowWidthUv, 1));
        uvs.Add(new Vector2(0.5f * shadowWidthUv, 1));*/

        AddUvs(shadowed);

        triangles.Add(ids);
        triangles.Add(ids+1);
        triangles.Add(ids+2);

        triangles.Add(ids + 2);
        triangles.Add(ids + 1);
        triangles.Add(ids + 3);

        triangles.Add(ids + 2);
        triangles.Add(ids + 1);
        triangles.Add(ids);

        triangles.Add(ids + 3);
        triangles.Add(ids + 1);
        triangles.Add(ids + 2);

        ids += 4;

        //rnd = new Vector3(rnd.z, rnd.y, rnd.x);

        //Перевернутый

        AddUvs(shadowed); // вот это

        triangles.Add(ids);
        triangles.Add(ids + 1);
        triangles.Add(ids + 2);

        triangles.Add(ids + 2);
        triangles.Add(ids + 1);
        triangles.Add(ids + 3);

        triangles.Add(ids + 2);
        triangles.Add(ids + 1);
        triangles.Add(ids);

        triangles.Add(ids + 3);
        triangles.Add(ids + 1);
        triangles.Add(ids + 2);

        ids += 4;
    }

    public void EndGenerate(MeshFilter filter, string names)
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.normals = normals.ToArray();

        mesh.Optimize();

        //mesh.RecalculateNormals();

        mesh.RecalculateBounds();
        mesh.RecalculateTangents();

        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        normals.Clear();

        ids = 0;

#if UNITY_EDITOR
        AssetDatabase.CreateAsset(mesh, "Assets/GrassData/modelGrass" + names + ".asset");
        AssetDatabase.SaveAssets();
#endif
        if (optimiz)
        {
            OffOnDistance off = filter.gameObject.AddComponent<OffOnDistance>();
            off.maxDist = maxDistOptimiz;
            off.playerMaxSpeed = playerMaxSpeed;
            off.size = Mathf.Sqrt(width * height) / 4;

            off.gameObject.layer = 6;

            /*BoxCollider box = filter.gameObject.AddComponent<BoxCollider>();
            box.center = filter.gameObject.transform.position + new Vector3(width / ch, 0, height / ch);
            box.size = new Vector3(width / ch, 1, height / ch);
            box.isTrigger = true;*/
        }

        if (destoyGrass)
        {
            int ch = (int)Mathf.Sqrt(chunkCount);

            filter.gameObject.AddComponent<DeleteGrass>();
            BoxCollider box = filter.gameObject.AddComponent<BoxCollider>();
            box.center = new Vector3(width / ch / 2, 0, height / ch / 2);
            box.size = new Vector3(width / ch, 1, height / ch);
            box.isTrigger = true;

            
        }

        filter.mesh = mesh;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(0, 1, 0, 0.2f);

        Vector3 size = new Vector3(width, 1, height);

        Gizmos.DrawCube(transform.position + new Vector3(width / 2, 1, height / 2), size);
    }

    /*public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Test"))
            Debug.Log("It's alive: " + target.name);
    }*/
}
