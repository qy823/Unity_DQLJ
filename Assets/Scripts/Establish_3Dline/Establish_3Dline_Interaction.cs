using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Establish_3Dline_Interaction : MonoBehaviour
{
    /// <summary>
    /// 3D连接 交互层
    /// 2024.07.25
    /// </summary>

    [Header("获取创建层")]
    public Establish3Dline_GameObject Establish3Dline_GameObject;
    [Header("拿取数据层")]
    public Establish_3Dline_Data Establish_3Dline_Data;




    /// <summary>
    /// 重置
    /// </summary>
    public void Establish_3Dline_Interaction_Reset()
    {
        //交互层中需要重置的数据
        Del_Line3D_GameObject = null;//清空临时存储的线段对象
        Execute_Bool = false;//当前创建操作是否执行完毕 初始化

        //数据层的重置
        Establish_3Dline_Data.Establish_3Dline_Data_Reset();
    }




    #region 删除3D 线段
    [Header("删除3D 线段的窗口")]
    public GameObject Del_Line3D_Window;
    private GameObject Del_Line3D_GameObject;//临时存储需要删除的线段对象

    /// <summary>
    /// 删除指定的3D 线段 赋值进来
    /// 出现弹窗
    /// </summary>
    public void Del_Line3D_Win(GameObject Line3DObject)
    {
        Del_Line3D_GameObject = Line3DObject;//临时储存在这里
                                             //出现删除提示弹窗
        Del_Line3D_Window.SetActive(true);//激活弹窗
    }

    public void Del_Line3D()
    {
        //删除指定线段
        if (Del_Line3D_GameObject != null)
        {
            Establish_3Dline_Data.Delete_Line3D_GameObject(Del_Line3D_GameObject);//删除指定线段
            Del_Line3D_GameObject = null;//清空临时存储的线段对象
        }

    }
    #endregion


    #region 创建连线3D线段 
    //当前还没有删除线段的功能 
    //可以设置 1、线段大小（粗细） 2、线段颜色  3、线段标号管标号 【以上都没有写完】

    //测试使用
    [Header("配置3D 连线的属性面板")]
    public Line3D_Property_Panel Line3D_Property_Panel;
    [Header("激活控制面板")]
    public GameObject Line3D_Property_Panel_GameObject;

    // private Line3D_Property LS_Line3D_Property;//接收传递过来的属性值
    // private Color Line3D_Color;// [Header("配置3D 连线的颜色")]
    // private float Line3D_Size;//  [Header("配置3D 连线的大小")]
    // private int Line3D_BHG;// [Header("配置3D 连线的标号管标号")]
    private GameObject[] Establish3Dline = new GameObject[2];//临时存点击的两个端子
    private Color CurrentColor_Click = Color.black;//设置端子的点击颜色
    private Color CurrentColor_Start = Color.white;//设置端子的点击颜色
    private bool Execute_Bool = false;//当前创建操作 是否执行完毕

    /// <summary>
    /// 重置 当前执行完毕
    /// </summary>
    public void Line3D_Execute_Reset()
    {
        Establish3Dline[0].GetComponent<Renderer>().material.color = CurrentColor_Start;// new Color();//恢复端子1初始颜色
        Establish3Dline[1].GetComponent<Renderer>().material.color = CurrentColor_Start;//new Color();//恢复端子2初始颜色
        Establish3Dline = new GameObject[2];
        Execute_Bool = false;//当前执行完毕
    }


    /// <summary>
    /// 创建3D线段 点击端子创建
    /// 2024.07.26
    /// </summary>
    /// <param name="Fqy"></param>
    public void Line3D(GameObject Fqy)
    {
        if (Execute_Bool) { return; }//避免没执行完毕被重新点击执行
        Execute_Bool = true;//当前正在执行

        //当前部分主要是判断端子是否被重复点击
        //点击第一个端子
        if (Establish3Dline[0] == null && Establish3Dline[1] == null)//没有任何存储端子
        {
            Establish3Dline[0] = Fqy;//将值传进来
            Establish3Dline[0].GetComponent<Renderer>().material.color = CurrentColor_Click;//设置颜色 点击颜色
            Execute_Bool = false;//当前执行完毕
            return;
        }
        //点击了第二个端子
        else if (Establish3Dline[0] != null && Establish3Dline[1] == null)//只有一个存储端子 
        {
            Establish3Dline[1] = Fqy;//将值传进来
            Establish3Dline[1].GetComponent<Renderer>().material.color = CurrentColor_Click;//设置颜色 点击颜色
            //两次点击的是同一个端子
            if (Establish3Dline[1] == Establish3Dline[0])
            {
                Debug.Log("两次点击的是同一个端子,重新选择");
                Line3D_Execute_Reset();//当前错误  执行完毕
                return;
            }
        }


        //2024.07.27 直接跳出 采用点击确认按键才可以继续下面的判定
        Line3D_Property_Panel_GameObject.SetActive(true);//激活控制面板
        return; //直接跳出 采用点击确认按键才可以继续下面的判定



        //判断当前是否点击的是一个元器件上的端子 如果是那就有问题
        // if (Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name ==
        // Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name)
        // {
        //     Debug.Log("当前选中的是一个元器件上的端子,重新选择");
        //     Line3D_Execute_Reset();//当前错误  执行完毕
        //     return;
        // }

        // //对线路进行唯一命名 保证物体线路的唯一性  排个序以免改变点击顺序连接后名字发生变化就可以进行连接
        // string Line3D_name = null;  //Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_【Line3D】";
        // if (string.Compare(Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String, Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String) > 0)
        // {
        //     Line3D_name = Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
        //     Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_【Line3D】";
        // }
        // else
        // {
        //     Line3D_name = Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
        //     Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_【Line3D】";

        //     // //交换一下顺序
        //     // GameObject[] test = Establish3Dline;
        //     // Establish3Dline[0] = test[1];
        //     // Establish3Dline[1] = test[0];
        // }

        // //没有存在相同的线段可以进行连接
        // if (Establish_3Dline_Data.Judge_Line3D_GameObject(Line3D_name))//对准备要创建的线路放在数据库进行判断
        // {
        //     Establish3Dline_GameObject.Line3D_Property(Line3D_Color, Line3D_Size, Line3D_BHG);//配置线段颜色 大小 标号管标号
        //     GameObject Fqy_gameobject = Establish3Dline_GameObject.Establish_Line3D(Establish3Dline[0], Establish3Dline[1]);//调用底层创建完整的连线
        //     Fqy_gameobject.name = Line3D_name;//给创建的物体重新命名

        //     Establish_3Dline_Data.Addition_Line3D_GameObject(Fqy_gameobject);//将数据新增到 data

        //     //为线段添加信息 脚本
        //     Line3D_Informatization LS_Line3D_Informatization = Fqy_gameobject.AddComponent<Line3D_Informatization>();//为物体添加脚本
        //     // LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = new GameObject[2];//将端子信息传进来 将连接的端子传给 Line3D_Informatization 后面做信息验证可以使用；
        //     LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = Establish3Dline;//将连线的两个端子存储在线段上

        //     //发送到端子上存储  由一个端子就可以拿到当前的线段连接
        //     Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[1]);
        //     Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[0]);


        // }
        // //存在相同的线段不可以进行连接
        // else
        // {

        // }

        // Line3D_Execute_Reset();//当前错误  执行完毕//将数据重置
    }


    /// <summary>
    /// 确定按键按下
    /// </summary>
    public void Yes_Click()
    {
        Line3D_Property LS_Line3D_Property = Line3D_Property_Panel.Toggle_SHUJU();//将面板数据拿到转换  暂时存储

        // Line3D_Color = Fqy.Line3D_Color_Data;//获取颜色
        // Line3D_Size = Fqy.Line3D_Size_Data;//获取线段大小
        // Line3D_BHG = Fqy.Line3D_BHG_Data;//获取标号管标号


        //判断当前是否点击的是一个元器件上的端子 如果是那就有问题
        if (Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name ==
        Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name)
        {
            Debug.Log("当前选中的是一个元器件上的端子,重新选择");
            Line3D_Execute_Reset();//当前错误  执行完毕
            return;
        }

        //对线路进行唯一命名 保证物体线路的唯一性  排个序以免改变点击顺序连接后名字发生变化就可以进行连接
        string Line3D_name = null;  //Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_【Line3D】";
        if (string.Compare(Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String, Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String) > 0)
        {
            Line3D_name = Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
            Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_【Line3D】";
        }
        else
        {
            Line3D_name = Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
            Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_【Line3D】";

            // //交换一下顺序
            // GameObject[] test = Establish3Dline;
            // Establish3Dline[0] = test[1];
            // Establish3Dline[1] = test[0];
        }

        //没有存在相同的线段可以进行连接
        if (Establish_3Dline_Data.Judge_Line3D_GameObject(Line3D_name))//对准备要创建的线路放在数据库进行判断
        {
            Establish3Dline_GameObject.Line3D_Property(LS_Line3D_Property.Line3D_Color_Data, LS_Line3D_Property.Line3D_Size_Data, LS_Line3D_Property.Line3D_BHG_Data);//配置线段颜色 大小 标号管标号
            GameObject Fqy_gameobject = Establish3Dline_GameObject.Establish_Line3D(Establish3Dline[0], Establish3Dline[1]);//调用底层创建完整的连线
            Fqy_gameobject.name = Line3D_name;//给创建的物体重新命名

            Establish_3Dline_Data.Addition_Line3D_GameObject(Fqy_gameobject);//将数据新增到 data

            //为线段添加信息 脚本
            Line3D_Informatization LS_Line3D_Informatization = Fqy_gameobject.AddComponent<Line3D_Informatization>();//为物体添加脚本
            // LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = new GameObject[2];//将端子信息传进来 将连接的端子传给 Line3D_Informatization 后面做信息验证可以使用；
            LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = Establish3Dline;//将连线的两个端子存储在线段上

            //发送到端子上存储  由一个端子就可以拿到当前的线段连接
            Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[1]);
            Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[0]);


        }
        //存在相同的线段不可以进行连接
        else
        {

        }

        Line3D_Execute_Reset();//当前错误  执行完毕//将数据重置
    }
    #endregion
}
