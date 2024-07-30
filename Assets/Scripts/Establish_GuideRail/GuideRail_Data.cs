using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//记录物体的详细信息 
public class Generate_Object3D
{
    [Header("存储的物体名字")]
    public string Name;

    [Header("3D 物体")]
    public GameObject This_Object3D;

    [Header("存储的世界坐标")]
    public Vector3 World_Position;
    [Header("存储的世界旋转")]
    public Quaternion World_Rotation;


    [Header("存储的局部坐标")]
    public Vector3 This_Position;
    [Header("存储的局部旋转")]
    public Quaternion This_Rotation;
}

[System.Serializable]//记录原型物体 Gameobject+物体名称
public class Prototype_Object3D
{
    [Header("获取3D 物体原型")]
    public GameObject Object3D;
    [Header("物体所对应的3D 物体名称")]
    public string Object3D_Name;

    [Header("副本创建的数量 一共创建过的数量而不是当前数量")]
    public int Object3D_Quantity;

    [Header("存储3D 创建的物体详细数据")]
    public List<Generate_Object3D> List_Generate_Object3D;
}

[System.Serializable]//记录一条导轨上的物体
public class GuideRail_Object3D
{
    [Header("物体所对应的3D 物体名称")]
    public List<string> Object3D_Name;
    [Header("源物体ID")]
    public List<int> Generate_Object3D_ID;
    // [Header("创建的副本ID")]
    // public List<int> Prototype_Object3D_ID;
    [Header("创建的数量")]
    public int Quantity;
}

public class GuideRail_Data : MonoBehaviour
{
    /// <summary>
    /// 数据层 记录导轨上的物体数据
    /// 最新修改时间：2024.07.29
    /// </summary>

    [Header("挂 在看这个的相机")]
    public GameObject Main_Camera;

    [Header("3D 物体源对象")]
    public GameObject[] Main_Object3D;

    [Header("3D 物体源对象 副本")]
    public GameObject[] Main_Object3D_FB;

    [Header("记录数据 物体源对象+对应副本列表数据")]
    public Prototype_Object3D[] Prototype_Object3D;
    [Header("记录每一条导轨的数据")]
    public GuideRail_Object3D[] GuideRail_Object3D;

    public void Awake()
    {
        GuideRail_Data_Reset();
    }

    /// <summary>
    /// 数据重置
    /// 2024.07.29
    /// </summary>
    public void GuideRail_Data_Reset()
    {
        //对物体进行销毁

        //进行数据的重置
        Prototype_Object3D = new Prototype_Object3D[Main_Object3D.Length];
        //将物体的名字记录下来 物体也拿到 
        for (int i = 0; i < Main_Object3D.Length; i++)
        {
            Prototype_Object3D[i] = new Prototype_Object3D();//单独实例化
            Prototype_Object3D[i].Object3D = Main_Object3D[i];
            Prototype_Object3D[i].Object3D_Name = Main_Object3D[i].name;//拿到源物体名字

            Prototype_Object3D[i].List_Generate_Object3D = new List<Generate_Object3D>();//单独实例化
        }
    }

    #region  放置物体

    /// <summary>
    /// 确定放置3D物体
    /// 进入连线模式 关闭放置物体模式
    /// 2024.07.26 
    /// </summary>
    public void Confirm_Object3D()
    {
        for (int i = 0; i < Prototype_Object3D.Length; i++)
        {
            for (int j = 0; j < Prototype_Object3D[i].List_Generate_Object3D.Count; j++)
            {
                //   GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().
                if (Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>() != null)
                    Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>().enabled = false;//关闭碰撞体

                //销毁刚体
                if (Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>() != null)
                    Destroy(Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>());//销毁刚体

                //拿到当前物体再当前类型中的标记 顺序
                if (Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>() != null)
                {
                    Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().Object_Type_Order = j;
                    Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().BoxCollider_true(true);//打开端子碰撞体
                }

            }
        }

        GuideRail_Bus.GuideRail_Bool = true;//标识进入连线模式 不可以再放置元器件
    }

    #endregion


    #region  记录数据
    /// <summary>
    /// 记录数据
    /// 输入一个Generate_Object3D对象，一个GameObject
    /// 将GameObject的详细数据记录进 Generate_Object3D对象
    /// </summary>
    /// <param name="Fqy"></param>
    /// <param name="Object_3D"></param>
    public void Record_Generate_Object3D(Generate_Object3D Fqy, GameObject Object_3D)
    {
        Fqy.This_Object3D = Object_3D;
        Fqy.Name = Object_3D.name;//获取该物体名字

        Fqy.World_Position = Object_3D.transform.TransformDirection(transform.position);//局部空间转换为世界空间中的位置
        Fqy.World_Rotation = Object_3D.transform.rotation;//记录世界坐标旋转

        Fqy.This_Position = Object_3D.transform.localPosition;//局部局部转换为世界空间中的位置
        Fqy.This_Rotation = Object_3D.transform.localRotation;//记录局部坐标旋转
    }

    /// <summary>
    /// 添加指定类型的中的 指定物体名字的 物体以及其他记录数据
    /// </summary>
    public void Establish_Object3D_Name(int Fqy, string Name, GameObject T_Object_3D)
    {
        if ((Fqy > Prototype_Object3D.Length || Fqy < 0) && Name == "")
        {
            Debug.Log("参数输入有误，请检测程序");
            return;
        }
        List<Generate_Object3D> T1_Generate_Object3D = new List<Generate_Object3D>();
        T1_Generate_Object3D = Prototype_Object3D[Fqy].List_Generate_Object3D;//拿到ID标记的数组

        //使用查找名字的方法
        int Love = 0;
        for (int i = 0; i < T1_Generate_Object3D.Count; i++)
        {
            if (T1_Generate_Object3D[i].Name != Name)
            {

                Love++;
            }
            else
            {
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Object3D = T_Object_3D;
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].Name = T_Object_3D.name;//获取该物体名字

                Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Position = T_Object_3D.transform.TransformDirection(transform.position);//局部空间转换为世界空间中的位置
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Rotation = T_Object_3D.transform.rotation;//记录世界坐标旋转

                Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Position = T_Object_3D.transform.localPosition;//局部局部转换为世界空间中的位置
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Rotation = T_Object_3D.transform.localRotation;//记录局部坐标旋转
                Debug.Log("当前记录数据的物体名称：" + Name);
                return;
            }
        }

        if (Love >= T1_Generate_Object3D.Count)
        {
            Debug.Log("没有在列表找到这个 物体的名字");
        }
    }
    #endregion

    #region 删除部分

    /// <summary>
    /// 先执行删除物体，再清除指定列表
    /// </summary>
    /// <param name="T_Generate_Object3D"> 列表</param>
    /// <param name="Name">名字</param>
    /// <param name="Fqy">删除的索引</param>
    public void Delete_Generate_Object3D(List<Generate_Object3D> T_Generate_Object3D, string Name = "", int Fqy = -1)
    {
        //使用ID 直接删除
        if (Fqy > -1)//输入ID 值正确直接采用删除 对应索引
        {
            // Destroy(List_Generate_Object3D[Fqy].This_Object3D);//直接删除物体 异步不会影响主线程
            DestroyImmediate(T_Generate_Object3D[Fqy].This_Object3D);//立即删除 代码顺序执行会影响主线程
            T_Generate_Object3D.RemoveAt(Fqy);
            return;
        }

        //使用查找名字的方法
        int Love = 0;
        for (int i = 0; i < T_Generate_Object3D.Count; i++)
        {
            if (T_Generate_Object3D[i].Name != Name)
            {
                Love++;
            }
            else
            {
                //Destroy(List_Generate_Object3D[i].This_Object3D);//直接删除物体 异步不会影响主线程
                DestroyImmediate(T_Generate_Object3D[i].This_Object3D);//立即删除 代码顺序执行会影响主线程
                T_Generate_Object3D.RemoveAt(i); //删除指定索引
                return;
            }
        }

        if (Love >= T_Generate_Object3D.Count)
        {
            Debug.Log("没有在列表找到这个 物体的名字");
        }
    }
    /// <summary>
    /// 删除指定类型的中的指定物体名字的 物体以及其他记录数据
    /// 物体移出删除
    /// </summary>
    public void Delete_Object_Name(int Fqy, string Name = "")
    {

        if ((Fqy > Prototype_Object3D.Length || Fqy < 0) && Name == "")
        {
            Debug.Log("参数输入有误，请检测程序");
            return;
        }
        List<Generate_Object3D> T1_Generate_Object3D = new List<Generate_Object3D>();
        T1_Generate_Object3D = Prototype_Object3D[Fqy].List_Generate_Object3D;//拿到ID标记的数组

        //使用查找名字的方法
        int Love = 0;
        for (int i = 0; i < T1_Generate_Object3D.Count; i++)
        {
            if (T1_Generate_Object3D[i].Name != Name)
            {
                Love++;
            }
            else
            {
                //2024.07.21 屏蔽删除物体数量 让这个值成为当前创建物体的总次数 不减少只加
                //Prototype_Object3D[Fqy].Object3D_Quantity--;//记录创建副本的数量-1
                if (Prototype_Object3D[Fqy].Object3D_Quantity <= 0)//记录当前当前数量
                {
                    Prototype_Object3D[Fqy].Object3D_Quantity = 0;
                }
                //Destroy(List_Generate_Object3D[i].This_Object3D);//直接删除物体 异步不会影响主线程
                DestroyImmediate(T1_Generate_Object3D[i].This_Object3D);//立即删除 代码顺序执行会影响主线程
                //T1_Generate_Object3D.RemoveAt(i); //删除指定索引
                Prototype_Object3D[Fqy].List_Generate_Object3D.RemoveAt(i); //删除原数组指定索引
                //Prototype_Object3D[Fqy].List_Generate_Object3D = T1_Generate_Object3D;//将值返还给原数组

                Debug.Log("删除指定物体名字：" + Name);
                return;
            }
        }

        if (Love >= T1_Generate_Object3D.Count)
        {
            Debug.Log("没有在列表找到这个 物体的名字");
        }
    }

    #endregion
}
