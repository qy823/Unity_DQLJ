using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Establish3Dline_Delete_Click : MonoBehaviour
{
  //点击事件 ，挂载底层脚本
  //通过名字传递上去删除
  //作用删除线段


  //public Establish3Dline_Bus Establish3Dline_Bus;
  [Header("中间交互层 点击删除线段")]
  public Establish_3Dline_Interaction Establish_3Dline_Interaction;//交互层

  private DateTime? lastRightClickTime; // 用于存储上一次右键点击的时间 

  public void OnMouseDown()
  {
    //2024.07.29 暂时废弃宋茂林的删除功能，改为点击删除线段
    //点击线将父级名字给总线
    //Establish3Dline_Bus.shanchu(transform.parent.name);//将点击的 线段名字传递上去

    //连续双击线段进入是否删除线段
    if (lastRightClickTime.HasValue && (DateTime.Now - lastRightClickTime.Value).TotalMilliseconds < 1000)
    {
      Establish_3Dline_Interaction.Del_Line3D_Win(transform.parent.gameObject);//将父物体传递上去
      //return;
    }
    else
    {
      Debug.Log("当前没有连续双击，不做任何操作");
    }
    lastRightClickTime = DateTime.Now;//更新最新点击时间


  }
}
