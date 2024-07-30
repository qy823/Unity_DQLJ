using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Line3D_GameObject
{
    [Header("球预制体")]
    public GameObject ballPrefab; // 小球的预制体 拐角部分的球
    [Header("线预制体")]
    public GameObject linePrefab; // 圆柱体的线段 线的大小改哪部分缩放
    [Header("标号管")]
    public GameObject GameObject_BHG;//拿到可以更改标号管的预制体 开头连接部分
    // [Header("标号管移动方向")]
    // public Vector3 BHG_Move_Direction;

    [Header("圆柱体的缩放")]
    public Vector3 YZT_OriginalScale; // 原始圆柱体的缩放
    [Header("小球的缩放")]
    public Vector3 XQ_OriginalScale; // 原始小球的缩放
    [Header("标号管缩放")]
    public Vector3 BHG_OriginalScale;



}

public class Establish3Dline_GameObject : MonoBehaviour
{
    /// <summary>
    /// 根据输入起点、终点、线路粗细度（缩放）、线路颜色  创建线段
    /// 最新修改 
    /// </summary>

    [Header("需要去拿取的源预制体/信息")]
    public Line3D_GameObject Line3D_GameObject;//基于预制体的Line3D_GameObject 进行创建复制

    //需要用到的数据
    //起点 (GameObject)、 终点 (GameObject) 、线路细度（缩放 挡位）、线路颜色 （可选）
    //详细定位信息导轨走上/下  当前处于哪一条导轨 \
    // [Header("记录点位位置 定位导轨位置信息 ")][SerializeField]//定位导轨位置信息 可变
    private GameObject[] GuideRail_WZ_List = new GameObject[5];

    [Header("记录点位位置 定位导轨位置信息 右侧")]//定位导轨位置信息
    [SerializeField] private GameObject[] GuideRail_WZ_List_R = new GameObject[5];

    [Header("记录点位位置 定位导轨位置信息 左侧")]//定位导轨位置信息
    [SerializeField] private GameObject[] GuideRail_WZ_List_L = new GameObject[5];


    [SerializeField]//序列化可见
    [Header("用于创建的初始线段 打组物体")] private GameObject Line3D_XD;//用于创建的初始线段 
    [SerializeField]//序列化可见
    [Header("将创建好的线段（完整）存储到父物体")] private GameObject Line3D_XD_Main;//存储完整的连接线段父物体
    private float Line3D_BHG_Position = 0.2f;//标号管的位置偏移量

    private Color LS_Color;//存储临时颜色变化
    private int BHG_ID;//临时存储标号管的ID
    private float Line3D_localScale = 0;//调节线路粗细度大小 



    private Vector3 YZT_OriginalScale_Start; // 原始圆柱体的初始缩放  // [Header("圆柱体的缩放")]
    private Vector3 XQ_OriginalScale_Start; // 原始小球的初始缩放    //[Header("小球的缩放")]
    private Vector3 BHG_OriginalScale_Start; // 原始标号管的初始缩放   //[Header("标号管缩放")]

    public void Start()
    {
        //记录初始值
        YZT_OriginalScale_Start = Line3D_GameObject.YZT_OriginalScale;
        XQ_OriginalScale_Start = Line3D_GameObject.XQ_OriginalScale;
        BHG_OriginalScale_Start = Line3D_GameObject.BHG_OriginalScale;
        //初始化数据
    }


    /// <summary>
    /// 3D 连线的相应属性设置
    /// </summary>
    public void Line3D_Property(Color Line3D_Color, float Line3D_CX = 1, int _BHG_ID = 0)
    {
        // YZT_OriginalScale_Start = Line3D_GameObject.YZT_OriginalScale;
        // XQ_OriginalScale_Start = Line3D_GameObject.XQ_OriginalScale;
        // BHG_OriginalScale_Start = Line3D_GameObject.BHG_OriginalScale;
        LS_Color = Line3D_Color;//线段颜色

        Line3D_GameObject.YZT_OriginalScale = new Vector3(YZT_OriginalScale_Start.x * Line3D_CX, YZT_OriginalScale_Start.y, YZT_OriginalScale_Start.z * Line3D_CX);//更新圆柱体的缩放
        Line3D_GameObject.XQ_OriginalScale = new Vector3(XQ_OriginalScale_Start.x * Line3D_CX, XQ_OriginalScale_Start.y * Line3D_CX, XQ_OriginalScale_Start.z * Line3D_CX);//更新小球的缩放
        Line3D_GameObject.BHG_OriginalScale = new Vector3(BHG_OriginalScale_Start.x * Line3D_CX, BHG_OriginalScale_Start.y * Line3D_CX, BHG_OriginalScale_Start.z * Line3D_CX);//更新标号管的缩放

        BHG_ID = _BHG_ID; //更新标号管的ID
    }



    /// <summary>
    /// 输入一个端子物体 判断是离左侧更近还是右侧更近
    /// 使用更近的方向的导轨定位点位
    /// 2024.07.25 新增左右方向点位数组判断 优先走更近的一边
    /// </summary>
    /// <param name="DZ_GameObject"></param>
    private void Judge_Distance(GameObject DZ_GameObject)
    {
        float distance_R = Vector3.Distance(DZ_GameObject.transform.position, GuideRail_WZ_List_R[0].transform.position);//计算出到右侧第一个点位的距离
        float distance_L = Vector3.Distance(DZ_GameObject.transform.position, GuideRail_WZ_List_L[0].transform.position);//计算出到坐标第一个点位的距离

        //判断使用哪一边的点位 谁近用谁的
        if (distance_R > distance_L)
        {
            GuideRail_WZ_List = GuideRail_WZ_List_L;//使用左侧点位
        }
        else
        {
            GuideRail_WZ_List = GuideRail_WZ_List_R;//使用右侧点位
        }
    }


    /// <summary>
    /// 根据输入的数据完整的创建出线段,并返回给上层使用（GameObject）
    /// 根据拿到的两个端子物体，so 就可以拿到端子信息 ，根据端子信息，可以创建出线段（导轨位置序号）
    /// 2024.07.23 重构返回创建好的Line3D
    /// </summary>
    /// <returns></returns>
    public GameObject Establish_Line3D(GameObject DZ_Object1, GameObject DZ_Object2)
    {
        Judge_Distance(DZ_Object1);//根据第一个端子位置计算线路走左边还是右边

        //GameObject Line3D = null;
        GameObject XD_Object = Instantiate(Line3D_XD, new Vector3(0, 0, 0), Quaternion.identity);//创建连线父物体
        XD_Object.SetActive(true);//把东西显示出来 激活

        //XD_Object.transform.localScale = new Vector3(Line3D_localScale, Line3D_localScale, Line3D_localScale);//调节整体大小

        int[] LS_GuideRail_ID = new int[2];//创建导轨位置序号数组 用于接收dZ端子的导轨位置序号
        //根据端子物体，获取导轨位置序号
        LS_GuideRail_ID[0] = DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID;//物体1 导轨序号
        LS_GuideRail_ID[1] = DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID;//物体2 导轨序号

        //记录是走上端子还是走下端子;默认0走下,1走上;走下会自动在导轨id加1  UpDown会影响连接方式
        if (DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().UpDown == 0) { LS_GuideRail_ID[0]++; }
        if (DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().UpDown == 0) { LS_GuideRail_ID[1]++; }

        MeshRenderer[] DZ_MeshRenderer = new MeshRenderer[2];//创建MeshRenderer数组 用于接收dZ端子的MeshRenderer
                                                             //根据端子物体，获取MeshRenderer
                                                             // DZ_MeshRenderer[0] = DZ_Object1.GetComponent<MeshRenderer>();//物体1 端子网格渲染器
                                                             // DZ_MeshRenderer[1] = DZ_Object2.GetComponent<MeshRenderer>();//物体2 端子网格渲染器

        //2024.07.23 16：30 更改将命名放在上层去
        //根据信息改名物体名字 保证物体名字的唯一性  
        //根据 端子1【端子物体名字（底层组合命名字符串）和端子ID进行组合命名】+端子2【端子物体名字（底层组合命名字符串）和端子ID进行组合命名】
        // XD_Object.transform.name = DZ_Object1.name + DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID + "_"
        // + DZ_Object2.name + DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID + "_wire";

        //当前在同一个导轨下，则创建导轨连接线段
        if (LS_GuideRail_ID[0] == LS_GuideRail_ID[1])
        {
            Line_Object3D_Case2(DZ_Object1, DZ_Object2, GuideRail_WZ_List[LS_GuideRail_ID[0]].transform.position, XD_Object);
            Debug.Log("当前在同一个导轨下");
        }
        //当前为不在同一个导轨下的连线 
        else
        {
            Line_Object3D_Case1(DZ_Object1, GuideRail_WZ_List[LS_GuideRail_ID[0]].transform.position, GuideRail_WZ_List[LS_GuideRail_ID[1]].transform.position, DZ_Object2, XD_Object);
            Debug.Log("不在同一个导轨下");
        }

        #region 数据重置 看看是否需要数据重置?

        #endregion

        //拿取物体的网格信息进行操作

        XD_Object.transform.parent = Line3D_XD_Main.transform;//认父物体
        return XD_Object;
    }
    #region 3D连线底层代码
    // 2024.07.23 重构底层代码



    /// <summary>
    /// 3D连线 情况1：当前端子不在同一个导轨下
    /// 根据传入信息进行3D连线 函数内部进行计算连线
    /// 传入起点、终点、经过点位、线路颜色 、打组在哪个父物体下
    /// 最新修改日期：2024.07.23 11：30
    /// </summary>
    /// <param name="DZ_Object1"></param> //端子1
    /// <param name="DZ_Object2"></param> //端子2
    /// <param name="PT1"></param> 途径点1
    /// <param name="PT2"></param> 途径点2
    /// <param name="Fqy_Object"></param> 打组父物体
    private void Line_Object3D_Case1(GameObject DZ_Object1, Vector3 PT1, Vector3 PT2, GameObject DZ_Object2, GameObject Fqy_Object)
    {
        //计算标号管位置
        Vector3 BHG_Object3D_1;
        Vector3 BHG_Object3D_2;

        //临时接收当前端子走线的方向
        int[] UpDowns = new int[2];
        //根据端子物体，获取端子走线的方向
        UpDowns[0] = DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//拿取该端子1 走线的方向
        UpDowns[1] = DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//拿取该端子2 走线的方向


        if (UpDowns[0] == 0)//下
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y - Line3D_BHG_Position, DZ_Object1.transform.position.z);
        }
        else//上
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y + Line3D_BHG_Position, DZ_Object1.transform.position.z);
        }

        if (UpDowns[1] == 0)
        {
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y - Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }
        else
        {
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y + Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }

        //生成标号管1
        // GameObject ball1 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_1, Quaternion.identity, Fqy_Object.transform);
        //Grade_Toggle_position(ball1, );//修改标号管位置 和标号管的标号（Text）
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_1, (BHG_ID + 0) + "");

        //生成标号管2
        // GameObject ball2 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_2, Quaternion.identity, Fqy_Object.transform);
        // Grade_Toggle_position(ball2, "2");//修改标号管位置 和标号管的标号（Text）
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_2, (BHG_ID + 1) + "");



        float zz = DZ_Object1.transform.position.z;   //记录高度
        Vector3 x1 = new Vector3(DZ_Object1.transform.position.x, PT1.y, zz);//记录坐标点位

        //创建线路走线
        Line3D_Establish3Dline(DZ_Object1.transform.position, x1, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x1, PT1, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(PT1, PT2, LS_Color, Fqy_Object);

        Vector3 x2 = new Vector3(DZ_Object2.transform.position.x, PT2.y, zz);//记录坐标点位

        //创建走线
        Line3D_Establish3Dline(PT2, x2, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x2, DZ_Object2.transform.position, LS_Color, Fqy_Object);
    }

    /// <summary>
    /// 3D连线 情况2：当前端子在同一个导轨下
    /// 根据传入信息进行3D连线 函数内部进行计算连线
    /// 传入起点、终点、经过点位、线路颜色 、打组在哪个父物体下
    /// 最新修改日期：2024.07.23 14：30
    /// </summary>
    /// <param name="DZ_Object1"></param> //端子1
    /// <param name="DZ_Object2"></param> //端子2
    /// <param name="PT"></param> //途径点
    /// <param name="Fqy_Object"></param> //打组父物体
    private void Line_Object3D_Case2(GameObject DZ_Object1, GameObject DZ_Object2, Vector3 PT, GameObject Fqy_Object)
    {
        //计算标号管位置
        Vector3 BHG_Object3D_1;
        Vector3 BHG_Object3D_2;

        //临时接收当前端子走线的方向
        int[] UpDowns = new int[2];
        //根据端子物体，获取端子走线的方向
        UpDowns[0] = DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//拿取该端子1 走线的方向
        UpDowns[1] = DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//拿取该端子2 走线的方向


        if (UpDowns[0] == 0)
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y - Line3D_BHG_Position, DZ_Object1.transform.position.z);
            // BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y - Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }
        else
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y + Line3D_BHG_Position, DZ_Object1.transform.position.z);
            //  BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y + Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }

        if (UpDowns[1] == 0)
        {
            // BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y - Line3D_BHG_Position, DZ_Object1.transform.position.z);
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y - Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }
        else
        {
            // BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y + Line3D_BHG_Position, DZ_Object1.transform.position.z);
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y + Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }

        //创建修改编号管1
        //GameObject ball1 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_1, Quaternion.identity, Fqy_Object.transform);
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_1, (BHG_ID + 0) + "");

        //创建修改编号管2
        //GameObject ball2 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_2, Quaternion.identity, Fqy_Object.transform);
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_2, (BHG_ID + 1) + "");



        float zz = DZ_Object1.transform.position.z;        //记录高度
        Vector3 x1 = new Vector3(DZ_Object1.transform.position.x, PT.y, zz);//记录坐标
        Vector3 x2 = new Vector3(DZ_Object2.transform.position.x, PT.y, zz);//记录坐标

        //创建线路走线
        Line3D_Establish3Dline(DZ_Object1.transform.position, x1, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x1, x2, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x2, DZ_Object2.transform.position, LS_Color, Fqy_Object);
    }



    ///下面是底层的底层了   



    /// <summary>
    /// 根据传入信息进行3D连线 函数内部进行计算连线
    /// 传入起点、终点、线路颜色 、打组在哪个父物体下
    /// 最新修改日期：2024.07.23
    /// </summary>
    /// <param name="initialPosition"></param>
    /// <param name="currentPosition"></param>
    /// <param name="currentColor"></param>
    /// <param name="Fqy_Object"></param>
    private void Line3D_Establish3Dline(Vector3 initialPosition, Vector3 currentPosition, Color currentColor, GameObject Fqy_Object)
    {
        //currentLine = null;


        // 创建小球并设置位置
        GameObject ball = Instantiate(Line3D_GameObject.ballPrefab, initialPosition, Quaternion.identity, Fqy_Object.transform);
        //切换材质 设置线段的颜色
        ball.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
        //修改小球大小
        ball.transform.localScale = new Vector3(Line3D_GameObject.XQ_OriginalScale.x, Line3D_GameObject.XQ_OriginalScale.y, Line3D_GameObject.XQ_OriginalScale.z);


        // 创建线段并设置初始位置
        GameObject currentLine = Instantiate(Line3D_GameObject.linePrefab, initialPosition, Quaternion.identity, Fqy_Object.transform);
        // 设置线段的颜色
        currentLine.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
        // 计算伸缩长度并更新线段的缩放和位置
        float distance = Vector3.Distance(initialPosition, currentPosition);
        //线大小
        currentLine.transform.localScale = new Vector3(Line3D_GameObject.YZT_OriginalScale.x, distance / 2f * Line3D_GameObject.YZT_OriginalScale.y, Line3D_GameObject.YZT_OriginalScale.z);//修改线段长度
                                                                                                                                                                                            //重新计算位置
        currentLine.transform.position = (initialPosition + currentPosition) / 2f;

        // 计算旋转方向
        Vector3 direction = currentPosition - initialPosition;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        currentLine.transform.rotation = rotation;
    }


    /// <summary>
    /// 切换标号管位置，以及标号管的标号（Text）
    /// 传值 改变标号管位置
    /// 最新修改日期：2024.07.23
    /// </summary>
    private void Grade_Toggle_position(GameObject Fqy_Object, Vector3 BHG_Object3D_Vector3, string BHG_Str)
    {
        GameObject BHG = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_Vector3, Quaternion.identity, Fqy_Object.transform);//生成标号管
        BHG.transform.localScale = Line3D_GameObject.BHG_OriginalScale;//修改长度
        BHG.GetComponent<Establish3Dline_Tag_Alter>().ste_Text(BHG_Str + "");//修改标号管文本
    }
    #endregion
}
