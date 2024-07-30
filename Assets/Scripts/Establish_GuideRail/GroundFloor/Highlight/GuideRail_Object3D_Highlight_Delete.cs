using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Highlight_Delete : MonoBehaviour
{
    /// <summary>
    /// 当已经有创建的物体空高亮 自我删除
    /// </summary>
    // Start is called before the first frame update
    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;
    //检测鼠标移出
    void OnTriggerEnter(Collider other)
    {
        GuideRail_Object3D_Highlight.Delete_Name(this.gameObject.name);//触发删除
        //  Destroy(this.gameObject);
        Debug.Log("进入该触发器的对象是：" + gameObject.name);
    }
}
