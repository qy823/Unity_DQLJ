using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Establish3Dline_Bus : MonoBehaviour
{
    // /// <summary>
    // /// 创建线段 总线作用
    // /// 可能webgl 有bug
    // /// </summary>

    public static bool Establish3Dline_Bool = false;//3D连线模式是否结束
    [Header("3D连线模式交互层")]
    public Establish_3Dline_Interaction Establish_3Dline_Interaction;

    /// <summary>
    /// 3D连线重置
    /// 数据重置 
    /// </summary>
    public void Establish3Dline_Bus_Reset()
    {
        Establish3Dline_Bool = false;
        Establish_3Dline_Interaction.Establish_3Dline_Interaction_Reset();//重置交互层数据 + data数据重置

    }

    //下面代码暂时都不需要
    // [Header("线颜色")]
    // public Color currentColor;//线颜色
    // [Header("原来颜色")]
    // public Color ysColor;//端子原来颜色
    // [Header("球预制体")]
    // public GameObject ballPrefab; // 小球的预制体
    // [Header("线预制体")]
    // public GameObject linePrefab; // 圆柱体的线段

    // private List<GameObject> linesList = new List<GameObject>(); // 存储所有的线段对象
    // [Header("圆柱体的缩放")]
    // public Vector3 originalScale; // 原始圆柱体的缩放
    // [Header("小球的缩放")]
    // public Vector3 originalScale_; // 原始小球的缩放
    // [Header("世界相机")]
    // public Camera Camera_3D;
    // public GameObject kwt;
    // public int kwtNameIndex;
    // //记录现在点击的线段，点s，查找对应名称的线段并删除

    // // public GameObject EmptyObject;
    // [Header("标号管")]
    // public GameObject GameObject_BHG;
    // [Header("标号管缩放")]
    // public Vector3 BHG_SF; // 原始圆柱体的缩放
    // [Header("标号管下移位置")]

    // public float BHG_WZ;
    // //public float desiredZ = 1f; // 预制体所在平面的Z坐标（相对于相机）
    // [Header("记录点位位置 定位导轨位置信息")]//定位导轨位置信息
    // public GameObject[] gameObject_WZ_list = new GameObject[2];



    // [Header("记录两个端子的世界位置")]
    // public Vector3[] cc = new Vector3[2];
    // public GameObject kwt_;//父物体
    // [Header("记录连接的两个元件名字")]
    // public string[] parentNames_;//记录连接的两个元件名字
    // public ArrayList parentNames = new ArrayList();//记录已经连接的线段的名称
    // public int[] terminal_ints_list = new int[2];//记录两个端子再元件上是几号

    // public int[] list_Datas = new int[2];//记录两个元件分别是哪一条的
    // public int css = 0; //记录有多少线段了
    // private MeshRenderer[] meshRenderers = new MeshRenderer[2];
    // private int[] UpDowns = new int[2];


    // /// <summary>
    // /// 端子世界位置，第几条导轨，元件名称，几号端子
    // /// </summary>
    // /// <param name="wzlist">端子世界位置</param>
    // /// <param name="listID">第几条导轨</param>
    // /// <param name="parentName">元件名称</param>
    // /// <param name="int_list">几号端子</param>
    // public void OnwutiDown(Vector3 wzlist, int listID, string parentName, int int_list, MeshRenderer LS_MeshRenderer, int UpDown)
    // {
    //     //如果是下走向端子，点位向下移动一个
    //     if (UpDown == 0) { listID++; };


    //     // listID++;
    //     //  int_list++;
    //     if (parentNames_[0] == "")//选第一次
    //     {
    //         LS_MeshRenderer.material.color = currentColor;
    //         list_Datas[0] = listID;
    //         //上下
    //         UpDowns[0] = UpDown;
    //         //端子材质组件
    //         meshRenderers[0] = LS_MeshRenderer;
    //         //元件上的几号端子
    //         terminal_ints_list[0] = int_list;
    //         //  Debug.Log("元件名称" + parentName);
    //         //元件名称
    //         parentNames_[0] = parentName;
    //         //端子位置
    //         cc[0] = wzlist;
    //     }
    //     //当第一个端子和第二个端子不在同一个元件上时才能连线
    //     else if (parentName != parentNames_[0])//第二次进行连线
    //     {
    //         list_Datas[1] = listID;
    //         UpDowns[1] = UpDown;
    //         meshRenderers[1] = LS_MeshRenderer;
    //         parentNames_[1] = parentName;
    //         terminal_ints_list[1] = int_list;

    //         //  kwtNameIndex++;
    //         // Debug.Log(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire");
    //         //Debug.Log(parentNames.Contains(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire"));
    //         //Debug.Log(parentNames.Contains(parentNames_[1] + terminal_ints_list[1] + "_" + parentNames_[0] + terminal_ints_list[0] + "_wire"));
    //         for (int i = 0; i < parentNames.Count; i++)
    //         {
    //             Debug.Log(parentNames[i]);
    //         }
    //         //如果没有连线，就创建连线，两个顺序都判断一遍
    //         if (!parentNames.Contains(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire") && !parentNames.Contains(parentNames_[1] + terminal_ints_list[1] + "_" + parentNames_[0] + terminal_ints_list[0] + "_wire"))
    //         {
    //             //线段数加1
    //             css++;
    //             //创建线段的父物体
    //             GameObject kwt_mb = Instantiate(kwt, new Vector3(0, 0, 0), Quaternion.identity);
    //             kwt_ = kwt_mb;
    //             Debug.Log("连线成功");
    //             //将文字记录到数组里面
    //             parentNames.Add(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire");
    //             kwt_.transform.name = parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire";
    //             // terminal_ints_list[1] = listID;
    //             cc[1] = wzlist;
    //             //  locationCalculate(cc[0], gameObject_WZ_list[terminal_ints_list[0]].transform.position, gameObject_WZ_list[terminal_ints_list[1]].transform.position, cc[1]);
    //             //判断是同一排还是不同进行两种逻辑的连线
    //             if (list_Datas[0] != list_Datas[1])
    //             {
    //                 locationCalculate(cc[0], gameObject_WZ_list[list_Datas[0]].transform.position, gameObject_WZ_list[list_Datas[1]].transform.position, cc[1]);
    //             }
    //             else
    //             {
    //                 typ(cc[0], cc[1], gameObject_WZ_list[list_Datas[0]].transform.position);
    //             }

    //             //重置数据，用于下一次创建

    //             parentNames_[0] = "";
    //             parentNames_[1] = "";
    //             //恢复端子颜色
    //             meshRenderers[0].material.color = ysColor;
    //             meshRenderers[1].material.color = ysColor;
    //             //  return;
    //         }
    //         else
    //         {
    //             meshRenderers[0].material.color = ysColor;
    //             meshRenderers[1].material.color = ysColor;
    //             parentNames_[0] = "";
    //             parentNames_[1] = "";
    //             Debug.Log("有线了");
    //         }

    //         //xian(gameObjectq.transform.position, gameObjecth.transform.position);
    //     }
    //     else//同一个元件上
    //     {

    //         //重置数据，用于下一次创建
    //         parentNames_[0] = "";
    //         parentNames_[1] = "";
    //         meshRenderers[0].material.color = ysColor;
    //         // meshRenderers[1].material.color = ysColor;
    //         //  meshRenderers[1].material.color = ysColor;
    //         // Debug.Log(parentNames_[0]);
    //         // Debug.Log(parentNames_[1]);
    //         Debug.Log("两次同一个元件");
    //         //xian(gameObjectq.transform.position, gameObjecth.transform.position);
    //     }
    // }
    // /// <summary>
    // /// 端子1，端子2，点位
    // /// </summary>
    // /// <param name="q">端子1</param>
    // /// <param name="q1">端子2</param>
    // /// <param name="index">点位</param>
    // public void typ(Vector3 q, Vector3 q1, Vector3 index)
    // {
    //     //计算标号管位置
    //     Vector3 bhg_1;
    //     Vector3 bhg_2;
    //     if (UpDowns[0] == 0)
    //     {
    //         bhg_1 = new Vector3(q.x, q.y - BHG_WZ, q.z);
    //     }
    //     else
    //     {
    //         bhg_1 = new Vector3(q.x, q.y + BHG_WZ, q.z);
    //     }
    //     GameObject ball1 = Instantiate(GameObject_BHG, bhg_1, Quaternion.identity, kwt_.transform);
    //     ball1.transform.localScale = BHG_SF;
    //     if (UpDowns[1] == 0)
    //     {
    //         bhg_2 = new Vector3(q1.x, q1.y - BHG_WZ, q1.z);
    //     }
    //     else
    //     {
    //         bhg_2 = new Vector3(q1.x, q1.y + BHG_WZ, q1.z);
    //     }

    //     GameObject ball2 = Instantiate(GameObject_BHG, bhg_2, Quaternion.identity, kwt_.transform);
    //     ball2.transform.localScale = BHG_SF;//修改长度

    //     //生成标号管


    //     //修改标号管
    //     ball1.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("1");
    //     //修改标号管
    //     ball2.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("2");

    //     // Debug.Log("q" + q);
    //     // Debug.Log("q1" + q1);
    //     //  Debug.Log("index" + index);
    //     float zz = q.z;
    //     Vector3 x1 = new Vector3(q.x, index.y, zz);
    //     Vector3 x2 = new Vector3(q1.x, index.y, zz);
    //     xian(q, x1);
    //     xian(x1, x2);
    //     xian(x2, q1);
    // }

    // /// <summary>
    // /// 端子位置1，点位1，点位2，端子位置2，
    // /// </summary>
    // /// <param name="q">端子位置1</param>
    // /// <param name="q1">点位1</param>
    // /// <param name="h1">点位2</param>
    // /// <param name="h">端子位置2</param>
    // public void locationCalculate(Vector3 DZ1, Vector3 DW1, Vector3 DW2, Vector3 DZ2)
    // {
    //     //计算标号管位置
    //     Vector3 bhg_1;
    //     Vector3 bhg_2;

    //     if (UpDowns[0] == 0)//下
    //     {
    //         bhg_1 = new Vector3(DZ1.x, DZ1.y - BHG_WZ, DZ1.z);
    //     }
    //     else//上
    //     {
    //         bhg_1 = new Vector3(DZ1.x, DZ1.y + BHG_WZ, DZ1.z);
    //     }

    //     if (UpDowns[1] == 0)
    //     {
    //         bhg_2 = new Vector3(DZ2.x, DZ2.y - BHG_WZ, DZ2.z);
    //     }
    //     else
    //     {
    //         bhg_2 = new Vector3(DZ2.x, DZ2.y + BHG_WZ, DZ2.z);
    //     }
    //     //生成标号管
    //     GameObject ball1 = Instantiate(GameObject_BHG, bhg_1, Quaternion.identity, kwt_.transform);
    //     ball1.transform.localScale = BHG_SF;//修改长度
    //     //修改标号管
    //     ball1.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("1");
    //     GameObject ball2 = Instantiate(GameObject_BHG, bhg_2, Quaternion.identity, kwt_.transform);
    //     ball2.transform.localScale = BHG_SF;//修改长度
    //     //修改标号管
    //     ball2.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("2");
    //     //  Debug.Log("q" + q);
    //     // Debug.Log("q1" + q1);
    //     // Debug.Log("h1" + h1);
    //     // Debug.Log("h" + h);
    //     //记录高度
    //     float zz = DZ1.z;
    //     Vector3 x1 = new Vector3(DZ1.x, DW1.y, zz);
    //     xian(DZ1, x1);
    //     xian(x1, DW1);
    //     xian(DW1, DW2);
    //     Vector3 x2 = new Vector3(DZ2.x, DW2.y, zz);
    //     xian(DW2, x2);
    //     xian(x2, DZ2);

    // }

    // private GameObject currentLine; // 当前的线段对象

    // //传入两个位置创造线段
    // public void xian(Vector3 initialPosition, Vector3 currentPosition)
    // {
    //     //  Debug.Log(initialPosition);
    //     //  Debug.Log(currentPosition);
    //     // 创建小球并设置位置
    //     GameObject ball = Instantiate(ballPrefab, initialPosition, Quaternion.identity, kwt_.transform);
    //     //切换材质 设置线段的颜色
    //     ball.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
    //     //修改小球大小
    //     ball.transform.localScale = new Vector3(originalScale_.x, originalScale_.y, originalScale_.z);
    //     // 创建线段并设置初始位置
    //     currentLine = Instantiate(linePrefab, initialPosition, Quaternion.identity, kwt_.transform);
    //     // 设置线段的颜色
    //     currentLine.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
    //     // 计算伸缩长度并更新线段的缩放和位置
    //     float distance = Vector3.Distance(initialPosition, currentPosition);
    //     //线大小
    //     currentLine.transform.localScale = new Vector3(originalScale.x, distance / 2f * originalScale.y, originalScale.z);//修改线段长度
    //     //重新计算位置
    //     currentLine.transform.position = (initialPosition + currentPosition) / 2f;

    //     // 计算旋转方向
    //     Vector3 direction = currentPosition - initialPosition;
    //     Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
    //     currentLine.transform.rotation = rotation;




    // }
    // private string fjName; // 记录删除的元件名称

    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.S) && parentNames.Contains(fjName))
    //     {
    //         //删除元件
    //         Destroy(GameObject.Find(fjName));
    //         //从数组终删除标记
    //         parentNames.Remove(fjName);
    //         css--;
    //     }
    // }

    // //用于删除？？
    // //点击线段记录点击的名称 
    // public void shanchu(string a)
    // {
    //     Debug.Log(a);
    //     fjName = a;
    //     //css--;
    //     //  Destroy(GameObject.Find(a));
    // }

}
