using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuideRail_Object3D_FollowMovement : MonoBehaviour
{
    //3D 物体跟随移动 
    //按下什么按键将在指定位置放下  记录该位置的信息（该物体半透明状态————>该状态不跟随鼠标移动 鼠标移出物体物体删除）
    //按下指定按键 将物体放置在该位置  （移出也不会删除物体了） 按下指定按键该脚本禁用＋脚本禁用
    //放置之后高亮全部关闭所有生成物体（空高亮）全部消失 ————>暂时没有处理

    //删除物体+删除数据 部分

    [Header("获取数据库")]
    public GuideRail_Data GuideRail_Data;
    [Header("交互层")]
    public GuideRail_Interaction GuideRail_Interaction;
    // [Header("材质变化")]
    // public GuideRail_Object3D_Texture GuideRail_Object3D_Texture;
    // [Header("创建")]
    // public GuideRail_Object3D_Transcript GuideRail_Object3D_Transcript;


    [Header("当前物体在 Generate_Object3D 位置")]
    public int Generate_Object3D_ID;//实时变化的
    [Header("获取物体名字")]
    public string Object_Name;//实时变化的
    [Header("是否可以进行删除")]
    public bool Delete_Bool = false; // 是否正在拖拽


    //跟随移动 部分
    [Header("物体距离鼠标距离")]
    public float distance = 0.5f;                 //距离

    //[Header("是否跟随鼠标一起移动")]
    // public bool GuideRail_Bus.FollowMovement_Bool = false;        // 是否正在拖拽
    // [Header("挂 在看这个的相机")]
    //   public GameObject GuideRail_Data.Main_Camera;
    //  [Header("拿到相机的 角度  物体跟相机旋转角度相同")]
    private Transform Camera_Rotate;//"拿到相机的 角度  物体跟相机旋转角度相同
    //  [Header("在主相机下跟随")]
    private Camera Camera_Main;//在主相机下跟随"
    //[Header("跟随鼠标移动物体")]
    private GameObject Follow;//控制的物体



    // private float Records = 5f; //物体返回原位置的范围 默认为5
    private Vector3 originalPosition;       // 物体的初始位置

    public void Awake()
    {
        if (GuideRail_Data == null)
        {
            GuideRail_Data = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Data>();
        }
        if (GuideRail_Interaction == null)
        {
            GuideRail_Interaction = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Interaction>();
        }
        // if (GuideRail_Object3D_Texture == null)
        // {
        //     GuideRail_Object3D_Texture = GameObject.FindWithTag("GuideRail_GroundFloor").GetComponent<GuideRail_Object3D_Texture>();
        // }




        Camera_Rotate = GuideRail_Data.Main_Camera.gameObject.transform;//拿到值
        Camera_Main = GuideRail_Data.Main_Camera.GetComponent<Camera>();//拿到值
    }


    /// <summary>
    /// 脚本第一次激活运行
    /// </summary>
    public void Start()
    {



        Object_Name = this.gameObject.name;//获取当前物体名字

        Follow = this.gameObject;//获取本身物体
        originalPosition = Follow.transform.position;//记录初始位置
                                                     // lastMousePosition = Follow.transform.position;//记录上一帧位置

        FollowMovement_Object_True();//创建即跟随移动
        Delete_Object_False();//不可以进行删除

    }

    /// <summary>
    /// 实时进行监听
    /// </summary>
    private void Update()
    {
        FollowMovement_Object();//跟随移动
        Judge_Name();//射线检测 名字判断

        if (Input.GetKeyDown(KeyCode.Z))//停止跟随移动 暂时放置
        {
            Move_TZ();
            //    Debug.Log("Start");
        }
        if (Input.GetKeyDown(KeyCode.X))//放置为最终选定位置
        {
            Move_Delete();
            //   Debug.Log("Start");
        }
    }

    /// <summary>
    /// 暂时停止跟随移动 暂时放置
    /// </summary>
    public void Move_TZ()
    {
        FollowMovement_Object_False();//禁止跟随
        Delete_Object_True();//可以进行删除

        //2024.07.21 更改将其写入到中间过渡层
        // GuideRail_Object3D_Texture.Establish_Textur(this.gameObject, true);//设置为半透明状态 创建新材质球方式
        GuideRail_Interaction.Toggle_Texture(this.gameObject, false);//设置为半透明状态 创建新材质球方式

        //将当前数据 拿到传给Data
        GuideRail_Data.Establish_Object3D_Name(Generate_Object3D_ID, Object_Name, this.gameObject);

    }

    /// <summary>
    /// 确定放置该位置 将多余物体删除
    /// </summary>
    public void Move_Delete()
    {
        this.gameObject.AddComponent<Rigidbody>();//添加刚体属性
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;//配置刚体属性
        this.gameObject.GetComponent<BoxCollider>().enabled = true;//配置碰撞体属性

        //2024.07.21 更改将其写入到中间过渡层
        //GuideRail_Object3D_Texture.Replace_Texture(this.gameObject);//设置为不透明状态 在原有基础上改变
        //  GuideRail_Object3D_Texture.Replace_Texture(this.gameObject);//设置为不透明状态 在原有基础上改变
        GuideRail_Interaction.Toggle_Texture(this.gameObject, true);//设置为不透明状态 创建新材质球方式

        FollowMovement_Object_False();//禁止跟随
        Delete_Object_False();//不可以进行删除

        GuideRail_Interaction.Close_Object3D();//关闭高亮
        this.GetComponent<Entity_Object3D_Click>().enabled = true;
        Destroy(this);//移除脚本自身

    }

    /// <summary>
    /// 点击3D 物体
    /// </summary>
    // public void OnMouseDown()
    // {
    //     //  Debug.Log("OnMouseDown");
    // }

    /// <summary>
    /// 当鼠标悬浮在物体上
    /// </summary>
    // void OnMouseOver()
    // {
    //     //Debug.Log("OnMouseOver");

    // }

    /// <summary>
    /// 当鼠标移开时
    /// </summary>
    void OnMouseExit()
    {
        // Debug.Log("OnMouseExit");
        Delete_Object();//移出删除
                        // Debug.Log("OnMouseExit");
    }


    #region 跟随鼠标移动

    /// <summary>
    /// 物体跟随鼠标移动
    /// </summary>
    public void FollowMovement_Object()
    {
        if (GuideRail_Bus.FollowMovement_Bool == true)//为true 才跟随物体移动
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;//配置碰撞体属性

            Vector3 m_MousePos = new Vector3(Input.mousePosition.x + 50f, Input.mousePosition.y + 50f, distance);
            transform.position = Camera_Main.ScreenToWorldPoint(m_MousePos);

            Follow.transform.rotation = Camera_Rotate.rotation;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;//配置碰撞体属性
        }
    }

    /// <summary>
    /// 启动 可以进行跟随物体移动
    /// </summary>
    public void FollowMovement_Object_True()
    {
        GuideRail_Bus.FollowMovement_Bool = true;
    }

    /// <summary>
    /// 停止 物体不在跟随物体移动
    /// </summary>
    public void FollowMovement_Object_False()
    {
        GuideRail_Bus.FollowMovement_Bool = false;
    }

    #endregion


    #region 移出或者其他操作删除 该物体

    /// <summary>
    ///删除物体以及删除记录数据
    /// </summary>
    public void Delete_Object()
    {
        if (Delete_Bool == true)
        {
            Debug.Log("当前点击的对象名字：" + this.gameObject.name);
            GuideRail_Data.Delete_Object_Name(Generate_Object3D_ID, Object_Name);
        }
    }

    /// <summary>
    /// 可以进行删除
    /// </summary>
    public void Delete_Object_True()
    {
        Delete_Bool = true;
    }

    /// <summary>
    /// 不可以进行删除
    /// </summary>
    public void Delete_Object_False()
    {
        Delete_Bool = false;
    }
    #endregion

    #region 射线检测代码 检测点击物体 是不是高亮的物体

    [Header("记录高亮位置的")]
    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;


    /// <summary>
    /// 将物体定位过去，物体移动到点击的空高亮位置
    /// 2024.06.21
    /// </summary>
    public void Judge_Name()
    {
        //中断
        if (GuideRail_Bus.FollowMovement_Bool == false)//只有 GuideRail_Bus.FollowMovement_Bool为true的时候点击才可以进入
        { return; }

        string Fqy = Ray_Detection(Camera_Main);//拿到值射线返回值
                                                //中断
        if (Fqy == null)//为空直接返回 中断
        { return; }

        //根据名字查找位置 ID 
        int Love = GuideRail_Object3D_Highlight.Inquire_Name(Fqy);//拿到返回ID 
        if (Love < 0)//小于0 未找到直接返回
        { Debug.Log("当前点击位置不正确"); return; }

        //拿到位置移动过去
        this.gameObject.transform.position = GuideRail_Object3D_Highlight.List_Object3D[Love].gameObject.transform.position;
        Move_TZ();//暂时放置

        //拿到导轨标记 
        string qy = Fqy[0].ToString();//先转化为字符串再处理
        //this.GetComponent<Entity_Object3D_Click>().DG_ID = int.Parse(qy);
        this.GetComponent<Object3D_Informatization>().GuideRail_ID = int.Parse(qy);
        Debug.Log("当前是第：" + Fqy[0] + "导轨");

        GuideRail_Interaction.Key_Trigger(false);//触发器打开，移出消失
        GuideRail_Interaction.Close_Object3D();//暂时关闭高亮
    }


    /// <summary>
    /// 射线检测 返回点击到的物体的名字
    /// </summary>
    /// <param name="GuideRail_Data.Main_Camera"></param>
    public string Ray_Detection(Camera Main_Camera)
    {
        string Fqy = null;
        if (Input.GetMouseButtonDown(0))
        {
            GuideRail_Interaction.Key_Trigger(false);
        }
        else
        {
            GuideRail_Interaction.Key_Trigger(true);
        }


        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 发射一条射线从摄像机的位置向鼠标点击位置
            Ray ray = Main_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线击中了物体
            if (Physics.Raycast(ray, out hit))
            {
                // 获取击中的物体
                GameObject hitObject = hit.collider.gameObject;
                // 输出物体名字到控制台
                //     Debug.Log("Clicked object name: " + hitObject.name);
                Fqy = hitObject.name;
                return Fqy;
            }
        }

        return Fqy;
    }
    #endregion
}
