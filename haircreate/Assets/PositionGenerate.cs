using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGenerate : MonoBehaviour
{
    public List<Vector3> GetPointPos = HairCeate.Pointpos;
    int width=HairCeate.hairwidth;
    public void GetPosition(Vector3 OldPos, Vector3 NewPos, int range)
    {
        Vector3 Vec = NewPos - OldPos;
        for (int i = 0, j = width; i < width; i++, j--)
        {
            float n = (j /3.0f) * 0.7f * 0.1f * range;
            Vector3 Vec1 = new Vector3((Vec.y) * n, (-Vec.x) * n, (Vec.z) *n);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        GetPointPos.Add(OldPos);
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            float n = (j / 3.0f) * 0.7f *0.1f* range;
            Vector3 Vec1 = new Vector3((-Vec.y) * n, (Vec.x) * n, (-Vec.z)*n);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        /*for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3((Vec.z) * j, (-Vec.y) * j, Vec.x * j);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }
        for (int i = 0, j = 1; i < width; i++, j++)
        {
            Vector3 Vec1 = new Vector3((-Vec.z) * j, (Vec.y) * j, Vec.x * j);
            Vector3 temp = new Vector3(OldPos.x + Vec1.x, OldPos.y + Vec1.y, OldPos.z + Vec1.z);
            GetPointPos.Add(temp);
        }*/
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < GetPointPos.Count; i++)
        {
            Gizmos.DrawSphere(GetPointPos[i], 0.1f);
        }
    }
}
