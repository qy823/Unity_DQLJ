using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Interaction : MonoBehaviour
{
    /// <summary>
    /// 中间过渡层
    /// 最新修改日期：2024.07.29 加上重置
    /// </summary>


    [Header("数据")]
    public GuideRail_Data GuideRail_Data;
    [Header("创建 底层")]
    public GuideRail_Object3D_Transcript GuideRail_Object3D_Transcript;
    [Header("高亮 底层")]
    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;

    [Header("材质变化")]
    public GuideRail_Object3D_Texture GuideRail_Object3D_Texture;

    public void Start()
    {
        if (GuideRail_Data == null)
        {
            GuideRail_Data = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Data>();
        }
        if (GuideRail_Object3D_Texture == null)
        {
            GuideRail_Object3D_Texture = GameObject.FindWithTag("GuideRail_GroundFloor").GetComponent<GuideRail_Object3D_Texture>();
        }
    }

    public void GuideRail_Interaction_Reset()
    {

        //数据层需要重置的
        GuideRail_Data.GuideRail_Data_Reset();
    }

    #region 材质变化
    public void Key_Trigger(bool Fqy)
    {
        //   Debug.Log("当前状态为：" + Fqy);
        if (Fqy)
        {
            Trigger_Object3D.SetActive(true);
        }
        else
        {
            Trigger_Object3D.SetActive(false);
        }
        Debug.Log("材质变化 当前状态为：" + Fqy);
    }

    /// <summary>
    /// 物体材质替换 透明和不透明之间切换
    /// </summary>
    public void Toggle_Texture(GameObject target_Object, bool Fqy = true)
    {
        if (Fqy)
        {
            GuideRail_Object3D_Texture.Replace_Texture(target_Object);//设置为不透明状态 在原有基础上改变
        }
        else
        {
            GuideRail_Object3D_Texture.Establish_Textur(this.gameObject, true);//设置为半透明状态 创建新材质球方式
        }
    }

    /// <summary>
    /// 确定位置关闭高亮
    /// </summary>
    public void Close_Object3D()
    {
        GuideRail_Object3D_Highlight.GuideRail_Object3D_Highlight_Reset();
    }

    #endregion



    /// <summary>
    /// 创建一套
    /// </summary>
    /// <param name="ID"></param>
    public void Toggle_Object3D(int ID)
    {
        //加上如果进入了连线模式就不可以 再放置物体 2024.07.26 00：00
        if (GuideRail_Bus.GuideRail_Bool) { return; }//如果处于连线模式就不可以创建新的物体 强制退出

        if (GuideRail_Bus.FollowMovement_Bool) { return; }//如果当前有物体正在跟随那就不可以创建新的物体 强制退出

        GuideRail_Object3D_Transcript.TJ(ID);//创建
        GuideRail_Object3D_Highlight.Outline_TJ(ID);//开高亮
        GuideRail_Bus.Type_ID = ID;
    }


    [Header("高亮触发物体 触发器")]//高亮计算
    public GameObject Trigger_Object3D;



    /// <summary>
    /// 双击后删除物体重新再次创建
    /// </summary>
    /// <param name="List_ID"></param>
    /// <param name="List_Name"></param>
    public void Click_Recreating(int List_ID, string List_Name)
    {
        GuideRail_Data.Delete_Object_Name(List_ID, List_Name);//先将物体删除了
        GuideRail_Object3D_Transcript.Establish_Object3D_ListID();
        Toggle_Object3D(List_ID);//创建新的物体
        Debug.Log("规定时间内双击");
    }
}
