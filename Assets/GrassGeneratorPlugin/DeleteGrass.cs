using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGrass : MonoBehaviour
{
    MeshFilter filter;

    private void Start()
    {
        filter = GetComponent<MeshFilter>();
    }

    public void DeleteGrassRad(Vector3 point, float radius)
    {
        /*List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();*/

        Vector3[] vert = filter.sharedMesh.vertices;

        //int[] tria = filter.sharedMesh.triangles;

        for(int i = 0; i < vert.Length; i++)
        {
            float dist = Vector3.Distance(point, transform.position + vert[i]);
            if(dist < radius)
            {
                Vector3 p = vert[i];
                p.y -= 10;
                vert[i] = p;
            }
        }

        filter.mesh.vertices = vert;

        filter.mesh.RecalculateBounds();
        filter.mesh.RecalculateTangents();
    }
}
