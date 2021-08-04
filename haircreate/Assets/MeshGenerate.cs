using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGenerate : MonoBehaviour
{
    private Mesh mesh;
    private Material render;

    Vector3[] ArrayPos;
    Vector2[] uv;
    Vector4[] tangent;
    int[] triangle;

    public void GenerateMesh(List<Vector3> Pointpos, int hairwidth)
    {
        ArrayPos = new Vector3[Pointpos.Count];
        uv = new Vector2[Pointpos.Count];
        tangent = new Vector4[Pointpos.Count];

        render = GetComponent<Renderer>().material;
        render.color = Color.red;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        GetComponent<MeshRenderer>().material = render;
        mesh.name = "Hair Grid";

        for (int i = 0; i < Pointpos.Count; i++)
        {
            tangent[i] = new Vector4(1f, 0f, 0f, -1f);
            ArrayPos[i] = Pointpos[i];
            uv[i].x = Pointpos[i].x;
            uv[i].y = Pointpos[i].y;
        }

        mesh.vertices = ArrayPos;
        mesh.uv = uv;
        mesh.tangents = tangent;

        int point = ((Pointpos.Count / (3 + (hairwidth - 1) * 2) - 1)) * 2 * hairwidth;
        triangle = new int[point*6];
        int t = 0;//初始三角形
        int k = 0;//累加
        for (int vi = 0, x = 1; x <= point; x++, vi += k)
        {
            t = SetQuad(triangle, t, vi, vi + 1, vi + 3 + (2 * (hairwidth - 1)), vi + 4 + (2 * (hairwidth - 1)));
            if (x % (hairwidth * 2) != point % (hairwidth * 2)) k = 1;  //在同一行
            else k = 2;  //對vi的累加  (需換行時)
        }
        mesh.triangles = triangle;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
    private static int SetQuad(int[] triangles, int i, int v0, int v1, int v2, int v3)
    {
        triangles[i] = v0;
        triangles[i + 1] = v1;
        triangles[i + 2] = v2;
        triangles[i + 3] = v2;
        triangles[i + 4] = v1;
        triangles[i + 5] = v3;
        return i + 6;
    }
}
