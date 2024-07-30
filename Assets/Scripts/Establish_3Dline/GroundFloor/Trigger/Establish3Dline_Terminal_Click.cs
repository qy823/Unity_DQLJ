using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;
using System;

public class Establish3Dline_Terminal_Click : MonoBehaviour
{

      /// <summary>
      /// 创建3D连线的触发
      /// 记录端子的位置数据等
      ///检测点击端子 用于交互3D连线 和元器件连接
      ///将 Object3D_Informatization 信息脚本数据传递上去
      ///最新修改时间：2024.07.25 
      /// </summary>

      //自动挂载和部分需要手动挂载的脚本
      private Establish3Dline_Bus Establish3Dline_Bus;//总线 宋茂林
      private Establish_3Dline_Interaction Establish_3Dline_Interaction;//3D连线创建 中间交互层
      [Header("端子详细信息")]
      public Element_Terminal_Data Element_Terminal_Data;

      [Header("请给端子挂载元器件对应的 信息脚本（Object3D_Informatization）")]
      public GameObject Object3D_Informatization;//可以通过这个获取原件整体信息


      //需要配置的端子数据  
      [Header("记录是走上端子还是走下端子;默认0走下,1走上;走下会自动在导轨id加1")]
      public int UpDown;// [Header("默认0走下,1走上")]// [Header("走下会自动在导轨id加1")]


      //根据元器件信息自动获取的
      [Header("记录他是第几个 端子唯一标识")]
      public int IndexID;//1、自行手动分配（暂停使用）/ 2、根据上层管理的排序进行自动分配（暂停使用已经被注释）/3、采用上层分配的序号（正在使用 优先级别最高）

      [Header("根据上层信息拿到记录导轨序号 ")]//会根据最新点击触发更新为上层最新导轨信息
      public int GuideRail_ID;

      [Header("根据上层信息+本端子信息 拼接 命名字符串")]
      public string GuideRail_String;

      // [Header("当前端子的 连接数量")]
      // public int Establish3Dline_Quantity;
      // [Header("当前端子的 连接物体 具体到物体")]
      // public List<GameObject> Establish3Dline_Object3D;

      //内部变量
      private DateTime? lastRightClickTime; // 用于存储上一次右键点击的时间 

      [Header("存储当前 与这个端子相连 的端子物体")]
      public List<GameObject> Terminal_Object3D;

      /// <summary>
      /// 初始化 根据上层管理自动给端子 给标识位
      /// </summary>
      // public void Awake()
      // {
      //       IndexID = -1;//给一个初始值
      //       for (int i = 0; i < Object3D_Informatization.GetComponent<Object3D_Informatization>().Object3D_Terminal.Length; i++)
      //       {
      //             //表明上层存储管理本物体
      //             if (this.gameObject == Object3D_Informatization.GetComponent<Object3D_Informatization>().Object3D_Terminal[i].gameObject)
      //             {
      //                   IndexID = i;
      //             }
      //       }
      //       if (IndexID < 0)
      //       {
      //             Debug.LogAssertion("没有在上层，找到对应的端子信息(请排查错误)；当前物体名字为：" + this.gameObject.name);
      //       }
      // }

      // Start函数在场景加载时被调用，只执行一次
      public void Start()
      {
            Establish_3Dline_Interaction = GameObject.FindWithTag("Establish_3Dline").GetComponent<Establish_3Dline_Interaction>();//获取3D连线创建 中间交互层
            Establish3Dline_Bus = GameObject.FindWithTag("Establish_3Dline").GetComponent<Establish3Dline_Bus>();//获取bus脚本

            GuideRail_ID = Object3D_Informatization.GetComponent<Object3D_Informatization>().GuideRail_ID;//更新导轨序号
                                                                                                          //字符串拼接：元器件物体类型+物体序号+端子序号                                                                                         
            GuideRail_String = Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type + Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type_Order + IndexID + "_";
      }


      /// <summary>
      /// 内部点击事件 触发函数
      /// </summary>
      private void OnMouseDown()
      {
            // 如果LastRightClickTime是null，或者与当前时间间隔小于1秒  
            if (lastRightClickTime.HasValue && (DateTime.Now - lastRightClickTime.Value).TotalMilliseconds < 1000)
            {
                  Debug.Log("当前连续双击，不做任何操作");
                  return;
            }
            lastRightClickTime = DateTime.Now;//更新最新点击时间

            if (Establish3Dline_Bus == null)
            {
                  Establish3Dline_Bus = GameObject.FindWithTag("Establish_3Dline").GetComponent<Establish3Dline_Bus>();
            }

            GuideRail_ID = Object3D_Informatization.GetComponent<Object3D_Informatization>().GuideRail_ID;//更新导轨序号
            //更新一些避免出现问题
            //字符串拼接：元器件物体类型+物体序号+端子序号
            GuideRail_String = Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type + Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type_Order + IndexID + "_";
            Establish_3Dline_Interaction.Line3D(this.gameObject);

            //2024.07.23 注释 取消使用宋茂林一套脚本
            //组合字符串 用于命名（物体类型+物体序号）
            //             string text = Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type
            //             + Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type_Order;

            //             Establish3Dline_Bus.OnwutiDown(this.transform.position, GuideRail_ID,
            //  text, IndexID, this.transform.GetComponent<MeshRenderer>(), UpDown);
      }


      /// <summary>
      /// 列表添加物体/列表删除物体
      /// 2024.07.25 新增储存 线路连接的端子 物体列表
      /// 2024.07.26 新增 更新一下上层存储的连线情况
      /// </summary>
      /// <param name="obj"></param> 传进来的物体
      /// <param name="Fqy"></param> 默认true代表添加 false代表删除
      public void Terminal_Object3D_AddDel(GameObject obj, bool Fqy = true)
      {
            if (Fqy)//true 代表添加 false代表删除
            {
                  Terminal_Object3D.Add(obj);
            }
            else
            {
                  Terminal_Object3D.Remove(obj);//删除指定值
                  // Terminal_Object3D.RemoveAt(0);//删除下标为index的元素
                  // Terminal_Object3D.RemoveRange(3, 2);//从下标index开始，删除count个元素
            }

            //2024.07.26 新增 更新一下上层存储的连线情况
            Object3D_Informatization.GetComponent<Object3D_Informatization>().Update_GuideRail_Establish3Dline_Bool();//更新元器件的最 端子连接情况
      }


}
