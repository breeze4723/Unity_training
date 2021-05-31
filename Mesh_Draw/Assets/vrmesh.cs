using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class TYT : MonoBehaviour
{
    //VR
    public SteamVR_Action_Boolean GrabPinch;
    public SteamVR_Action_Boolean Push;
    private SteamVR_Behaviour_Pose Pose;
    //
    public List<Vector3> pointer = new List<Vector3>();//存座標點
    public Vector3 Newpoint;
    public Vector3 Oldpoint;
    public int t = 0;//按鍵辦定 
    private Mesh meshpuduce;
    // Start is called before the first frame update

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Push.GetStateDown(Pose.inputSource))n++;
        if (GrabPinch.GetStateDown(Pose.inputSource))
        {
            Newpoint = Pose.transform.position;//new position
            Oldpoint = Pose.transform.position; ;//last position
            t = 1;

        }
        if (t == 1)
        {
            Newpoint = Pose.transform.position; 
            float lengh = Vector3.Distance(Oldpoint, Newpoint);
            if (lengh > 0.5)
            {
                Produce(Newpoint, Oldpoint);
                Newpoint = Pose.transform.position;//new positio
                Oldpoint = Pose.transform.position;//last position
            }
            if (pointer.Count >= (n * 2 + 1) * 2 && pointer != null)
            {
                CreateMesh();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {

            t = 0;
        }


    }
    public Vector3[] widthlist1;
    public Vector3[] widthlist2;
    int n = 1;//計算寬度
    void Produce(Vector3 a, Vector3 b)
    {
        widthlist1 = new Vector3[n];
        widthlist2 = new Vector3[n];
        Vector3 V_substract = a - b;
        for (int i = 0, j = widthlist1.Length; i < widthlist1.Length; i++, j--)
        {
            Vector3 stamdardVec = new Vector3((V_substract.y) * j, (-V_substract.x) * j, (V_substract.y) * j);
            widthlist1[i] = new Vector3(a.x + stamdardVec.x, a.y + stamdardVec.y, a.z + stamdardVec.z);
            pointer.Add(widthlist1[i]);
        }
        pointer.Add(Newpoint);
        for (int i = 0, j = 1; i < widthlist2.Length; i++, j++)
        {
            Vector3 stamdardVec = new Vector3((-V_substract.y) * j, (V_substract.x) * j, (-V_substract.y) * j);
            widthlist2[i] = new Vector3(a.x + stamdardVec.x, a.y + stamdardVec.y, a.z + stamdardVec.z);
            pointer.Add(widthlist2[i]);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < pointer.Count; i++)
        {
            Gizmos.DrawSphere(pointer[i], 0.1f);
        }
    }
    void CreateMesh()
    {
        Vector3[] vertex_pose = new Vector3[pointer.Count];
        Vector2[] uv_pose = new Vector2[pointer.Count];
        GetComponent<MeshFilter>().mesh = meshpuduce = new Mesh();
        meshpuduce.name = "wendy";
        for (int i = 0; i < pointer.Count; i++)//Vector3轉Vector2
        {

            uv_pose[i].x = pointer[i].x;
            uv_pose[i].y = pointer[i].y;
            vertex_pose[i] = pointer[i];

        }
        meshpuduce.vertices = vertex_pose;
        meshpuduce.uv = uv_pose;
        int face = (pointer.Count / (1 + 2 * n) - 1) * 2 * n;
        int[] triangles = new int[face * 6];
        int t = 0;//初始三角形
        int k = 0;//累加
        for (int vi = 0, x = 1; x <= face; x++, vi += k)
        {
            t = SetQuad(triangles, t, vi, vi + 1, vi + 3 + (2 * (n - 1)), vi + 4 + (2 * (n - 1)));
            if (x % (n * 2) != face % (n * 2)) k = 1;
            else k = 2;
        }
        meshpuduce.triangles = triangles;
        Debug.Log(face);

    }
    private static int SetQuad(int[] triangles, int i, int v00, int v10, int v01, int v11)
    {
        triangles[i] = v00;
        triangles[i + 1] = v10;
        triangles[i + 2] = v01;
        triangles[i + 3] = v01;
        triangles[i + 4] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }
}
