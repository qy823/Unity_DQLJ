using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Transcript : MonoBehaviour
{
    /// <summary>
    /// 创建副本物体
    ///删除创建 在Data层中记录删除的物体
    /// </summary>

    [Header("数据层")]
    public GuideRail_Data GuideRail_Data;

    [Header("测试")]
    public int ID;

    public void Start()
    {
        if (GuideRail_Data == null)
        {
            GuideRail_Data = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Data>();
        }
    }
    #region 测试使用
    // public void Update()
    // {
    //     // if (Input.GetKeyDown(KeyCode.Alpha9))
    //     // {
    //     //     Establish_Textur(Tset_Object, false);
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.Alpha8))
    //     // {
    //     //     Establish_Textur(Tset_Object, true);
    //     // }
    //     //  Test();//测试
    // }

    /// <summary>
    /// 测试使用
    /// </summary>
    // public void Test()
    // {
    //     //切换创建物体
    //     if (Input.GetKeyDown(KeyCode.Alpha0))
    //     {
    //         ID = 0;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         ID = 1;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         ID = 2;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha3))
    //     {
    //         ID = 3;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha4))
    //     {
    //         ID = 4;
    //     }


    //     if (Input.GetKey(KeyCode.Q))//添加
    //     {
    //         GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Add(new Generate_Object3D());//新增一条
    //         GuideRail_Data.Record_Generate_Object3D(GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D[GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count - 1], Establish_Object3D(ID, GuideRail_Data.Main_Object3D_FB[ID]));
    //     }
    //     if (Input.GetKey(KeyCode.A))//删除
    //     {
    //         //删除最后一条数据
    //         if (GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count > ID)
    //         {
    //             Debug.Log("删除数据第：" + (GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count - 1) + "条");
    //             GuideRail_Data.Delete_Generate_Object3D(GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D, "", GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count - 1);

    //             GuideRail_Data.Prototype_Object3D[ID].Object3D_Quantity--;//记录创建副本的数量-1

    //             if (GuideRail_Data.Prototype_Object3D[ID].Object3D_Quantity <= 0)
    //             {
    //                 GuideRail_Data.Prototype_Object3D[ID].Object3D_Quantity = 0;
    //             }
    //         }
    //     }
    // }


    /// <summary>
    /// 创建生成 （根据原列表生成）
    /// FollowMovement_Bool为true 应该是清除上一个正在跟随的物体再继续
    /// </summary>
    /// <param name="index"></param>
    public void TJ(int index)
    {
        if (GuideRail_Bus.FollowMovement_Bool)
        {
            return;
        }

        GuideRail_Data.Prototype_Object3D[index].List_Generate_Object3D.Add(new Generate_Object3D());//新增一条
        GuideRail_Data.Record_Generate_Object3D(GuideRail_Data.Prototype_Object3D[index].
        List_Generate_Object3D[GuideRail_Data.Prototype_Object3D[index].List_Generate_Object3D.Count - 1], Establish_Object3D(index, GuideRail_Data.Main_Object3D_FB[index]));
    }
    #endregion

    #region 创建物体和添加数据

    /// <summary>
    ///指定一个物体 创建一个副本
    ///将创建副本放入指定 的父物体
    ///更改创建副本的数据
    ///将创建的副本数据记录 （第一次记录一次数据）
    /// </summary>
    public GameObject Establish_Object3D(int Fqy, GameObject Object_1)
    {
        GuideRail_Data.Prototype_Object3D[Fqy].Object3D_Quantity++;//记录创建副本的数量+1
        GameObject Object_test = Instantiate(GuideRail_Data.Prototype_Object3D[Fqy].Object3D);//创建一个副本
        Object_test.name = GuideRail_Data.Prototype_Object3D[Fqy].Object3D_Name + "_" + GuideRail_Data.Prototype_Object3D[Fqy].Object3D_Quantity;//更改副本名称 原物体名字+_+"第几个"
        Object_test.transform.parent = Object_1.transform;//认 Object_1 做父物体

        Object_test.SetActive(true);//激活物体
        return Object_test;
    }

    /// <summary>
    /// 根据类型创建一个
    /// </summary>
    public void Establish_Object3D_ListID()
    {
        Debug.Log("当前已经成功创建");
    }

    #endregion


    // #region  记录数据
    // /// <summary>
    // /// 记录数据
    // /// 输入一个Generate_Object3D对象，一个GameObject
    // /// 将GameObject的详细数据记录进 Generate_Object3D对象
    // /// </summary>
    // /// <param name="Fqy"></param>
    // /// <param name="Object_3D"></param>
    // public void Record_Generate_Object3D(Generate_Object3D Fqy, GameObject Object_3D)
    // {
    //     Fqy.This_Object3D = Object_3D;
    //     Fqy.Name = Object_3D.name;//获取该物体名字

    //     Fqy.World_Position = Object_3D.transform.TransformDirection(transform.position);//局部空间转换为世界空间中的位置
    //     Fqy.World_Rotation = Object_3D.transform.rotation;//记录世界坐标旋转

    //     Fqy.This_Position = Object_3D.transform.localPosition;//局部局部转换为世界空间中的位置
    //     Fqy.This_Rotation = Object_3D.transform.localRotation;//记录局部坐标旋转
    // }

    // /// <summary>
    // /// 添加指定类型的中的 指定物体名字的 物体以及其他记录数据
    // /// </summary>
    // public void Establish_Object3D_Name(int Fqy, string Name, GameObject T_Object_3D)
    // {
    //     if ((Fqy > GuideRail_Data.Prototype_Object3D.Length || Fqy < 0) && Name == "")
    //     {
    //         Debug.Log("参数输入有误，请检测程序");
    //         return;
    //     }
    //     List<Generate_Object3D> T1_Generate_Object3D = new List<Generate_Object3D>();
    //     T1_Generate_Object3D = GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D;//拿到ID标记的数组

    //     //使用查找名字的方法
    //     int Love = 0;
    //     for (int i = 0; i < T1_Generate_Object3D.Count; i++)
    //     {
    //         if (T1_Generate_Object3D[i].Name != Name)
    //         {

    //             Love++;
    //         }
    //         else
    //         {
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Object3D = T_Object_3D;
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].Name = T_Object_3D.name;//获取该物体名字

    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Position = T_Object_3D.transform.TransformDirection(transform.position);//局部空间转换为世界空间中的位置
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Rotation = T_Object_3D.transform.rotation;//记录世界坐标旋转

    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Position = T_Object_3D.transform.localPosition;//局部局部转换为世界空间中的位置
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Rotation = T_Object_3D.transform.localRotation;//记录局部坐标旋转
    //             Debug.Log("当前记录数据的物体名称：" + Name);
    //             return;
    //         }
    //     }

    //     if (Love >= T1_Generate_Object3D.Count)
    //     {
    //         Debug.Log("没有在列表找到这个 物体的名字");
    //     }
    // }
    // #endregion
}
