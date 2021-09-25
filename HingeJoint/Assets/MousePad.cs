using UnityEngine;

public class MousePad : MonoBehaviour
{
    #region 公有變數
    //------------------------------------------------------------------------------------

    //------------------------------------------------------------------------------------
    #endregion

    #region 私有變數
    //------------------------------------------------------------------------------------
    private Transform dragGameObject;
    private Vector3 offset;
    private bool isPicking;
    private Vector3 targetScreenPoint;
    //------------------------------------------------------------------------------------
    #endregion

    #region 公有方法
    #endregion

    #region 私有方法
    //------------------------------------------------------------------------------------
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckGameObject())
            {
                offset = dragGameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z));
            }
        }

        if (isPicking)
        {
            //當前滑鼠所在的螢幕座標
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPoint.z);
            //把當前滑鼠的螢幕座標轉換成世界座標
            Vector3 curWorldPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);
            Vector3 targetPos = curWorldPoint + offset;

            dragGameObject.position = targetPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPicking = false;
            if (dragGameObject != null)
            {
                dragGameObject = null;
            }
        }

    }
    //------------------------------------------------------------------------------------
    /// <summary>
    /// 檢查是否點選到cbue
    /// </summary>
    /// <returns></returns>
    bool CheckGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {
            isPicking = true;
            //得到射線碰撞到的物體
            dragGameObject = hitInfo.collider.gameObject.transform;

            targetScreenPoint = Camera.main.WorldToScreenPoint(dragGameObject.position);
            return true;
        }
        return false;
    }
    //------------------------------------------------------------------------------------
    #endregion
}
