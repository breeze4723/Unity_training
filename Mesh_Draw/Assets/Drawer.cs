using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Drawer : MonoBehaviour
{
    //VR
    public SteamVR_Action_Boolean GrabPinch;
    private SteamVR_Behaviour_Pose Pose;

    //生成
    private LineRenderer player;
    public List<Vector3> MousePointPos = new List<Vector3>();
    public List<Vector3> SpherePos = new List<Vector3>();
    public List<GameObject> SphereGroup = new List<GameObject>();
    private Vector3 LastPos;
    private bool Down;
    Vector3 MousePos;
    int number0 = 1, number1 = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    void Start()
    {
        player = gameObject.AddComponent<LineRenderer>();
        player.material = new Material(Shader.Find("Sprites/Default"));
        player.SetColors(Color.green, Color.gray);
        player.SetWidth(0.01f, 0.01f);

    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponent<LineRenderer>();
        if (GrabPinch.GetStateDown(Pose.inputSource))
        {

            MousePos = Pose.transform.position;

            player.numCapVertices = 2;//端點圓度
            player.numCornerVertices = 2;//拐彎圓滑度

            player.positionCount = MousePointPos.Count;
            player.SetPositions(MousePointPos.ToArray());
            LastPos = Pose.transform.position;
            MousePointPos.Add(LastPos);
            Down = true;
            if (number1 > number0)
            {
                number0++;
            }
            number1++;
        }
        if (Down == true)
        {

            MousePos = Pose.transform.position;
            float dist = Vector3.Distance(LastPos, MousePos);
            if (dist > 0.05f)
            {//更新座標
                MousePos = Pose.transform.position;
                MousePointPos.Add(MousePos);
                LastPos = Pose.transform.position;
                player.positionCount = MousePointPos.Count;
                player.SetPositions(MousePointPos.ToArray());
                GameObject sphere;
                SphereGroup.Add(sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere));
                sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                sphere.transform.position = MousePos;
            }

            Debug.Log(dist);

        }
        if (GrabPinch.GetStateUp(Pose.inputSource))
        {
            Down = false;
            MousePointPos.Clear();
            player = null;

        }
        if (number1 > number0)
        {
            for (int i = 0; i <= SphereGroup.Count - 1; i++)
            {
                Destroy(SphereGroup[i]);
            }
            number0++;
        }

    }
}
