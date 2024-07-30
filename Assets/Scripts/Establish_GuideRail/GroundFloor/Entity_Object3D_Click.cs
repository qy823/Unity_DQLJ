using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;
using System;

public class Entity_Object3D_Click : MonoBehaviour
{
    /// <summary>
    /// 检测双击重新跟随移动
    /// 实体点击3D 物体 
    /// 效果删除该物体 这类型的物体重新跟随一起移动
    /// 在移动其他物体的时候 不能删除物体再次跟随移动
    /// </summary>

    [Header("物体类型标记 属于源数组中的哪一个位置")]
    public int List_ID;//属于哪一种类型物体 方便删除后重新创建

    // [Header("当前物体属于 哪一个导轨（ID）")]
    // public int DG_ID;

    // [Header("数据")]
    // public GuideRail_Data GuideRail_Data;
    [Header("交互层")]
    public GuideRail_Interaction GuideRail_Interaction;
    // [Header("创建物体")]
    // public GuideRail_Object3D_Transcript GuideRail_Object3D_Transcript;

    private DateTime? lastRightClickTime; // 用于存储上一次右键点击的时间  

    private void Start()
    {
        if (GuideRail_Interaction == null)
        {
            GuideRail_Interaction = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Interaction>();
        }

        lastRightClickTime = DateTime.Now;
    }

    //放置后点击 物体 ,没有跟随移动才可以进行点击
    public void OnMouseDown()
    {
        //放置没有挂载到
        if (GuideRail_Interaction == null)
        {
            GuideRail_Interaction = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Interaction>();
        }

        if (GuideRail_Bus.FollowMovement_Bool == false)
        {
            // 如果LastRightClickTime是null，或者与当前时间间隔小于1秒  
            if (lastRightClickTime.HasValue && (DateTime.Now - lastRightClickTime.Value).TotalMilliseconds < 1000)
            {
                GuideRail_Interaction.Click_Recreating(List_ID, this.name);
                // GuideRail_Data.Delete_Object_Name(List_ID, this.name);//先将物体删除了
                // GuideRail_Object3D_Transcript.Establish_Object3D_ListID();
                // GuideRail_Interaction.Toggle_Object3D(List_ID);//创建新的物体
                // Debug.Log("规定时间内双击");
            }

            // 更新最后一次右键点击的时间  
            lastRightClickTime = DateTime.Now;
        }
    }


}
