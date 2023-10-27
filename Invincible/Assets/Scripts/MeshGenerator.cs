using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] Vector2Int size;
    Vector3[] vertices;
    int[] triangles;

    int[] CreateTraingles()
    {
        
        triangles = new int[size.x * size.y * 6];

        for (int z = 0, vert = 0, tris = 0; z < size.y; z++)
        {
            for (int x = 0; x < size.x; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size.x + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size.x + 1;
                triangles[tris + 5] = vert + size.x + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        return triangles;
    }

    private Vector3[] CreateVertices()
    {
        vertices = new Vector3[(size.x + 1) * (size.y + 1)];

        for (int i = 0, z = 0; z <= size.y; z++)
        {
            for (int x = 0; x <= size.x; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        return vertices;
    }

    void Generate()
    {
        mesh = new Mesh();
       // mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        meshFilter.sharedMesh = mesh;
    }

    private void Start()
    {
        Generate();
        CreateTraingles();
        CreateVertices();
    }
}
