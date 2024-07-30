using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Establish_3Dline_Data : MonoBehaviour
{
    /// <summary>
    /// 记录连线产生的一系列数据信息
    /// 记录线段数量，记录具体连接线段
    /// 2024.07.23 新增；便于提取；
    /// 2024.07. 最新修改
    /// </summary>

    public int css = 0; //记录有多少线段了
    // public MeshRenderer[] meshRenderers = new MeshRenderer[2];
    public List<GameObject> Line3D_GameObjects = new List<GameObject>(); // 存储所有的线段对象
    public List<string> Line3D_GameObject_Str = new List<string>(); // 存储所有的线段对象名字

    public void Establish_3Dline_Data_Reset()
    {
        //销毁所有连接的线段物体
        for (int i = 0; i < Line3D_GameObjects.Count; i++)
        {
            Destroy(Line3D_GameObjects[i]);//销毁当前线段物体  
        }


        //  销毁完毕 清空列表
        Line3D_GameObjects.Clear(); // 存储所有的线段对象
        Line3D_GameObject_Str.Clear(); // 存储所有的线段对象名字
        //防止没有清空  重新new 一下
        Line3D_GameObjects = new List<GameObject>();
        Line3D_GameObject_Str = new List<string>();
        //   css = 0; //记录有多少线段了
    }


    #region 判断、添加、删除 3D 线段
    /// <summary>
    ///  判断当前数据库中是否存在相同的名字 即存在相同的线段
    /// true 成功添加 false 失败 ==>当前数组中以及存在相同的名字
    /// 最新修改时间：2024.07.23 15：50
    /// </summary>
    public bool Judge_Line3D_GameObject(string Line3D_GameObject_Name)
    {
        if (Line3D_GameObject_Str.Count == 0) { return true; }//当前还没有开始添加直接退出

        bool Fqy = !Line3D_GameObject_Str.Contains(Line3D_GameObject_Name);//因为找到了为true; 所以取反 false为已经找到了 true为找到
        //int index = Array.IndexOf(Line3D_GameObject_Str, "orange");

        if (Fqy == false)
        {
            //    Debug.Log("当前数组中存在相同的名字：" + Line3D_GameObject_Name + "，即当前线段已存在，添加失败！");
            return Fqy;
        }
        // Line3D_GameObjects.Add(Line3D_GameObject);//添加新的物体
        // Line3D_GameObject_Str.Add(Line3D_GameObject.name);//添加新的线段名字 
        //  Debug.Log("当前数组中不存在相同的名字，即当前线段不存在，可添加成功！");
        return Fqy;
    }

    /// <summary>
    /// 将创建的线段加入进来
    /// 最新修改时间：2024.07.23 16：20
    /// </summary>
    /// <param name="Line3D_GameObject"></param> 新增线段
    public void Addition_Line3D_GameObject(GameObject Line3D_GameObject)
    {
        Line3D_GameObjects.Add(Line3D_GameObject);//添加新的物体
        Line3D_GameObject_Str.Add(Line3D_GameObject.name);//添加新的线段名字
    }


    /// <summary>
    /// 删除指定线段
    /// 最新修改时间：2024.07.23 16：50
    /// </summary>
    /// <param name="Line3D_GameObject"></param>
    public void Delete_Line3D_GameObject(GameObject Line3D_GameObject)
    {
        //删除存储类容
        Line3D_GameObject_Str.Remove(Line3D_GameObject.name);//删除线段名字
        Line3D_GameObjects.Remove(Line3D_GameObject);//删除物体

        //对物体进行销毁
        Destroy(Line3D_GameObject);//销毁当前线段物体  
    }
    #endregion
}
