using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Trigger : MonoBehaviour
{
    /// <summary>
    /// 检测鼠标进入该导轨内 开始进行 物体导轨放置位置计算
    /// 最新修改日期：2024.07.20
    /// </summary>


    [Header("当前导轨标记")]
    public int Trigger_ID;
    [Header("导轨高亮控制触发 ")]

    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;

    // Update is called once per frame  void OnTriggerEnter(Collider other)//接触时触发，无需调用
    // void OnTriggerEnter(Collider other)//接触时触发，无需调用
    // {
    //     Debug.Log(Time.time + ":进入该触发器的对象是：" + other.gameObject.name);
    //     // Show(prompt);
    // }

    /// <summary>
    /// 鼠标进入触发
    /// </summary>
    public void OnMouseEnter()
    {
        GuideRail_Object3D_Highlight.Generate_Ouline_One(Trigger_ID);
        // Debug.Log("进入，导轨：" + Trigger_ID);
    }

    /// <summary>
    /// 鼠标移出
    /// </summary>
    public void OnMouseExit()
    {
        GuideRail_Object3D_Highlight.GuideRail_Object3D_Highlight_Reset();
        //  Debug.Log("移出，导轨：" + Trigger_ID);
    }
}
