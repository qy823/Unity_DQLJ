using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3D_Informatization : MonoBehaviour
{
    /// <summary>
    /// 存储当前元件信息
    /// 存储传给当前物体的信息
    /// 最新修改时间：2024.07.25 
    /// </summary>

    //需要手动配置的值
    [Header("元器件详细信息配置 以及后续记录")]
    public Element_Message_Data Element_Message_Data;

    [Header("物体类型")]
    public string Object_Type;
    [Header("导轨标记 （位置）")]
    public int GuideRail_ID = -1;
    [Header("管理 物体下面的接口端子 物体")]
    public GameObject[] Object3D_Terminal;


    //根据运行数据自动赋值

    [Header("物体 类型顺序标记")]
    public int Object_Type_Order;//第几个

    [Header("物体的端子连线信息 Bool")]//跟 物体下面的接口端子物体 一一对应
    public bool[] GuideRail_Establish3Dline_Bool;//true 当前端子有连接;false 当前端子没有连接;

    public void Start()
    {
        GuideRail_Establish3Dline_Bool = new bool[Object3D_Terminal.Length];//长度跟管理的端子数量一致
    }


    /// <summary>
    /// 打开端子碰撞体表示着开始连线
    /// 物体下所有的接口端子
    /// 打开/关闭 端子的碰撞体
    /// </summary>
    /// <param name="Fqy"></param>
    public void BoxCollider_true(bool Fqy)
    {
        for (int i = 0; i < Object3D_Terminal.Length; i++)
        {
            if (Object3D_Terminal[i].GetComponent<BoxCollider>() != null)
            {
                Object3D_Terminal[i].GetComponent<BoxCollider>().enabled = Fqy;
            }
            // else if (Object3D_Terminal[i].GetComponent<MeshCollider>() != null)
            // {
            //     Object3D_Terminal[i].GetComponent<MeshCollider>().enabled = Fqy;
            // }
            else if (Object3D_Terminal[i].GetComponent<SphereCollider>() != null)
            {
                Object3D_Terminal[i].GetComponent<SphereCollider>().enabled = Fqy;
            }
        }


        //2024.07.25 更改位上层分配唯一标识 不使用手动分配
        //当前 分配底层端子排序？？ 暂时使用上层分配的方法 
        for (int i = 0; i < Object3D_Terminal.Length; i++)
        {
            Object3D_Terminal[i].GetComponent<Establish3Dline_Terminal_Click>().IndexID = i;
        }
    }


    /// <summary>
    /// 更新下层最新端子连接情况
    /// 2024.07.26 
    /// </summary>
    public void Update_GuideRail_Establish3Dline_Bool()
    {
        GuideRail_Establish3Dline_Bool = new bool[Object3D_Terminal.Length];
        for (int i = 0; i < Object3D_Terminal.Length; i++)
        {
            //大于零标识当前是有连接线路的
            if (Object3D_Terminal[i].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D.Count > 0)
            {
                GuideRail_Establish3Dline_Bool[i] = true;
            }
        }
    }


    /// <summary>
    /// 返回当前元器件连接的端子数量
    /// </summary>
    /// <returns></returns>
    public int Return_3Dline()
    {
        int fqy = 0;

        foreach (bool i in GuideRail_Establish3Dline_Bool)
        {
            if (i)
            {
                fqy++;
            }
        }

        return fqy;

        // for (int i = 0; i < GuideRail_Establish3Dline_Bool.Length; i++)
        // {
        //     if (GuideRail_Establish3Dline_Bool[i])
        //     {
        //         fqy++;
        //     }
        // }
    }

}
