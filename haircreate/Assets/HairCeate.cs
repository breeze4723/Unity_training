using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairCeate : MonoBehaviour
{
    // Start is called before the first frame update
    int putdown= 0,meshCount=0;
    public static List<Vector3> Pointpos =new List<Vector3>();
    public List<GameObject> Hair = new List<GameObject>();
    Vector3 newPos, oldPos;
    public MeshGenerate meshCreate;
    public PositionGenerate positionCreate;
    float length = 0.5f;
    public static int hairwidth=3;
    public int InputRange=1;//(1~10)
    void Start()
    {
        positionCreate = gameObject.AddComponent<PositionGenerate>();
        
    }

    // Update is called once per frame
    void Update()
    {
        inputRG();
        if (Input.GetMouseButtonDown(0))
        {
            
            GameObject model = new GameObject();
            Hair.Add(model);
            Hair[meshCount].name = "hairmodel" + meshCount;
            

            oldPos = newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            

            putdown = 1;
        }
        if (putdown==1)
        {
            newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            float dist = Vector3.Distance(oldPos, newPos);
            if (dist > length)
            {
                positionCreate = gameObject.GetComponent<PositionGenerate>();
                positionCreate.GetPosition(oldPos,newPos,InputRange);
                oldPos = newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            }
            if (Pointpos.Count >= (3 + (hairwidth - 1)*2)*2)
            {
                
                if (Hair[meshCount].GetComponent<MeshGenerate>() == null)
                {
                    meshCreate= Hair[meshCount].AddComponent<MeshGenerate>();
                }
                else meshCreate = Hair[meshCount].GetComponent<MeshGenerate>();
                meshCreate.GenerateMesh(Pointpos, hairwidth);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(Vector3.Distance(Pointpos[0],Pointpos[6]));
                if (Pointpos.Count >= (3 + (hairwidth - 1) * 2) * 2) meshCount++;
                else
                {//清除建立失敗的髮片GameObject
                    int least = Hair.Count - 1;
                    Destroy(Hair[least]);
                    Hair.RemoveAt(least);
                }
                
                Pointpos.Clear();
                putdown = 0;
            }
            
        }
        Debug.Log(InputRange);
    }
    void inputRG()
    {
        if (Input.GetKeyDown("down")&&InputRange<10)
        {
            InputRange++;
        }
        if (Input.GetKeyDown("up") && InputRange >1)
        {
            InputRange--;
        }
    }
}
