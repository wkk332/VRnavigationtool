using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class DituCtrl : MonoBehaviour
{

    public GameObject ceng2;
    public RectTransform point; //2d 坐标
    public Transform point3D; //3d坐标
    public Nav_pro Nav_pro;

    public void Show2ceng(bool b)
    {
        int i = b ? 0 : 6;
        ChangeLayer(ceng2.transform, i);
    }

    private void ChangeLayer(Transform transform, int layer)
    {
        if (transform.childCount > 0)//如果子物体存在
        {
            for (int i = 0; i < transform.childCount; i++)//遍历子物体是否还有子物体
            {
                ChangeLayer(transform.GetChild(i), layer);//这里是只将最后一个无子物体的对象设置层级
            }
            transform.gameObject.layer = layer;//将存在的子物体遍历结束后需要把当前子物体节点进行层级设置
        }
        else					//无子物体
        {
            transform.gameObject.layer = layer;
        }
    }

    //设置目标点
    public void Click(ActivateEventArgs c)
    {
        XRRayInteractor cc = (XRRayInteractor)c.interactorObject;

        if (cc.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            point.position = hit.point;
            point.localPosition = new Vector3(point.localPosition.x, point.localPosition.y, 0);
            point3D.position = new Vector3(point.anchoredPosition.x, 20, point.anchoredPosition.y);
            RayPoint();
        }

    }

    public void RayPoint()
    {
        Ray ray = new Ray(point3D.position, point3D.forward);
        if (Physics.Raycast(ray, out RaycastHit shootHit, 1000, 1 << 0)) 
        {
            Nav_pro.MovePoint = shootHit.point;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
