using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Highlight : MonoBehaviour
{
    /// <summary>
    ///生成指定物体的空高亮 创建副本改变材质为透明
    ///记录空间坐标 
    ///记录当前导轨生成物体数量
    /// </summary>

    ///移入指定导轨生成 高亮
    ///统计 该导轨生成数量
    ///计算剩余容纳量

    [Header("数据")]
    public GuideRail_Data GuideRail_Data;
    [Header("物体组")]
    public List<GameObject> List_Object3D;
    [Header("参照复制物体")]
    public GameObject[] Object_FB;

    [Header("起始位置")]
    public GameObject[] Object_KS;

    [Header("最大值 BoxCollider")]
    public GameObject[] Object_Max;

    [Header("导轨打组 一共有几个导轨 挂几个路径下的空物体")]//打组
    public GameObject[] GuideRail_Object;

    public Vector3 _Position;
    public void Start()
    {
        List_Object3D = new List<GameObject>();//实例化

        GuideRail_Data.GuideRail_Object3D = new GuideRail_Object3D[Object_KS.Length];// 申明长度
        for (int i = 0; i < Object_KS.Length; i++)
        {
            GuideRail_Data.GuideRail_Object3D[i] = new GuideRail_Object3D();//实例化
        }
        //    Test(Object_FB[1]);
        //   Row_generate(Object_1, Object_3, Object_4);
    }


    //测试2
    public void Outline_TJ(int ID)
    {
        // Generate_Ouline(Object_FB[ID]);//隐藏这个物体
    }

    /// <summary>
    /// 重置
    /// </summary>
    public void GuideRail_Object3D_Highlight_Reset()
    {
        //销毁之前的
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            //List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//不在作为触发器
            Destroy(List_Object3D[i]);//销毁
        }
        //   List_Object3D = new List<GameObject>();//实例化
        List_Object3D.Clear();//清空

    }

    /// <summary>
    /// 上层直接生成 空高亮
    /// </summary>
    /// <param name="Object_Test"></param>
    public void Generate_Ouline(GameObject Object_Test)
    {
        //销毁之前的
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            //List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//不在作为触发器
            Destroy(List_Object3D[i]);//销毁
        }
        //   List_Object3D = new List<GameObject>();//实例化
        List_Object3D.Clear();//清空
        for (int i = 0; i < GuideRail_Object.Length; i++)
        {
            Row_generate(Object_Test, Object_KS[i], Object_Max[i], GuideRail_Object[i], i);
        }

        //    List_Object3D.Clear();// = new List<GameObject>();//实例化
        StartCoroutine(DelayedOperation_Destroy(2));//延时2s销毁
    }

    /// <summary>
    /// 根据传进来的物体 进行创建  尝试可以在物体上创建多少个 即还可以放置多少物体
    /// </summary>
    /// <param name="Object_ID"></param>
    public void Generate_Ouline_One(int Object_ID)
    {
        //没有移动物体直接返回
        if (GuideRail_Bus.FollowMovement_Bool == false) { return; }

        // 清除之前的
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            //List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//不在作为触发器
            Destroy(List_Object3D[i]);//销毁
        }
        //   List_Object3D = new List<GameObject>();//实例化
        List_Object3D.Clear();//清空

        //Debug.Log("当前导轨是：" + Object_ID);//计算该导轨一共可以生成了多少个高亮物体 
        int Fqy = Row_generate(Object_FB[GuideRail_Bus.Type_ID], Object_KS[Object_ID], Object_Max[Object_ID], GuideRail_Object[Object_ID], Object_ID);

        GuideRail_Data.GuideRail_Object3D[Object_ID].Quantity = Fqy - List_Object3D.Count;//拿到该条导轨上的物体元件数量
        Debug.Log("当前导轨" + Object_ID + ", 物体数量为：" + GuideRail_Data.GuideRail_Object3D[Object_ID].Quantity);
        StartCoroutine(DelayedOperation_Destroy(0.1f));//延时2s销毁
    }

    /// <summary>
    /// 延时销毁
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayedOperation_Destroy(float Time_destroy)
    {
        // Debug.Log("开始延时操作");
        yield return new WaitForSeconds(Time_destroy);
        Debug.Log("延时销毁操作完成");
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//不在作为触发器

            Destroy(List_Object3D[i].GetComponent<GuideRail_Object3D_Highlight>());//销毁脚本
        }
    }


    /// <summary>
    ///  单轴，单一化处理 世界坐标？
    /// 坐标复杂化，使用相对坐标+世界坐标？？
    /// <summary>
    /// <param name="Object3D_FB"></param> //复制源物体
    /// <param name="Object3D"></param>//起始位置
    /// <param name="Initial_Position"></param>//最大位置
    /// <param name="Parent_Object"></param>//打组
    public int Row_generate(GameObject Object3D_FB, GameObject Object3D, GameObject Initial_Position, GameObject Parent_Object, int DG_ID = 0)
    {
        // Debug.Log("起始物体名称为：" + Object3D.name);
        // Debug.Log("开始位置：" + Object3D.transform.localPosition);

        Vector3 Fqy = Object3D_FB.GetComponent<BoxCollider>().size;//拿到大小
        Vector3 Qy = Object3D.transform.localPosition;//拿到开始位置
        float Love = Fqy.x + 0.2f;//拿到每一个的距离

        int like = 0;//次数
        float F_Value = 0;//值

        while (true)
        {
            GameObject Object_test = Instantiate(Object3D_FB);//创建一个副本
            Object_test.transform.parent = Parent_Object.transform;//认 Object_1 做父物体
            Object_test.name = DG_ID + "_" + Object3D.name + "_" + like;//重新命名 导轨ID
            List_Object3D.Add(Object_test);//新增 进去

            Object_test.SetActive(true);//激活物体
                                        // List_Object3D[like] = Object_test;//记录物体

            Object_test.gameObject.transform.localPosition = Qy;//改变位置

            Qy.x += Love;
            like++;
            F_Value = Love * like;
            //  Debug.Log("当前变化值为：" + like);
            if (F_Value > Initial_Position.GetComponent<BoxCollider>().size.x)
            {

                //  Debug.Log("当前达到最大值");
                return like;
            }
        }
        // return like;


    }

    /// <summary>
    /// 删除指定名字的
    /// </summary>
    public void Delete_Name(string Fqy)
    {
        int Like = 0;
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            if (List_Object3D[i].name == Fqy)//找到名字
            {
                // 找到要删除的物体，执行销毁操作
                Destroy(List_Object3D[i]);
                // DestroyImmediate(List_Object3D[i]);//立即删除
                List_Object3D.RemoveAt(i); // 从列表中移除物体
                Debug.Log("删除成功 :" + Fqy);
                break; // 找到并删除了物体，不需要继续循环
            }
            else
            {
                Like++;
            }
        }

        if (Like == List_Object3D.Count)
        {
            Debug.Log("出现错误未，正确找到该物体");
        }
    }

    /// <summary>
    /// 查找指定名字
    /// </summary>
    public int Inquire_Name(string Fqy)
    {
        int Love = 0;
        int Like = 0;
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            if (List_Object3D[i].name == Fqy)//找到名字
            {
                Love = i;
                Debug.Log("在列表中，找到点击物体位置");
            }
            else
            {
                Like++;
            }
        }

        if (Like == List_Object3D.Count)
        {
            Debug.Log("当前点击物体位置不正确, 物体名字为：" + Fqy);
            Love = -1;
            //    return Love;
        }
        return Love;
    }
}
