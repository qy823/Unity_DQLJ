using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System.IO;
//using UnityEngine.UIElements;
//using UnityEditor;

public class GuideRail_Object3D_Texture : MonoBehaviour
{
    /// <summary>
    /// 改变导轨上物体材质 半确定状态和确定状态（透明状态和半透明状态）
    /// 确定状态和半确定状态
    /// </summary>

    // Start is called before the first frame update

    [Header("找到所有的子物体")]
    public List<GameObject> Son_Object = new List<GameObject>();//储存物体的列表

    //在原有基础上进行改变 方案1
    [Header("颜色")]
    public Color Object_color;
    [Header("材质球")]
    public Material Object_Material;


    //创建材质球 方案2
    [Header("创建材质球")]
    public Material Object_Material_Test;
    [Header("路径")]
    public string Material_Path = "Assets/Model/NewlyIncreased/";
    [Header("类型名")]
    public string Material_Type = ".mat";


    public void Start()
    {

#if UNITY_EDITOR
        ClearDirectory_File("Assets/Model/NewlyIncreased");
        Material_Path += "/";
        Directory.CreateDirectory(Material_Path);//创建
        //刷新
        AssetDatabase.Refresh();
        //  ClearDirectory_File(Material_Path);
        // Directory.Delete(Material_Path, true); //第二个参数代表如果内容不为空是否也要删除，这样就不会报错了
#endif
    }

    /// <summary>
    /// 重置数据：
    /// Son_Object 
    /// </summary>
    public void Reset()
    {

    }

    /// <summary>
    /// 获取材质
    /// </summary>
    public void Gain_Texture(GameObject Fqy)
    {
        // List<GameObject> CH = new List<GameObject>();//储存物体的列表

        //利用for循环 获取物体下的全部子物体
        for (int c = 0; c < Fqy.transform.childCount; c++)
        {
            //如果子物体下还有子物体 就将子物体传入进行回调查找 直到物体没有子物体为止
            if (Fqy.transform.GetChild(c).childCount > 0)
            {
                Gain_Texture(Fqy.transform.GetChild(c).gameObject);

            }
            Son_Object.Add(Fqy.transform.GetChild(c).gameObject);
        }
    }

    /// <summary>
    /// 方案1 在原有材质球基础上进行改变 如果一个物体需要放置多份的话 方案一会有Bug
    /// 材质更改为半透明材质 / 不透明材质之间切换
    /// </summary>
    public void Replace_Texture(GameObject Object_Test, bool Fqy = false)
    {
        Son_Object.Clear();//清空列表
        Gain_Texture(Object_Test);//遍历下面记录所有的子物体

        float Object_color_A = 1;//默认状态 不透明
        if (Fqy)//为True 改变透明度半透明
        {
            Object_color_A = 0.5f;
        }


        // 检查游戏对象上是否有材质组件 父物体
        if (Object_Test.GetComponent<Renderer>() != null && Object_Test.GetComponent<Renderer>().material != null)
        {
            //拿到材质球
            Object_Material = Object_Test.GetComponent<Renderer>().material;
            //拿到材质球颜色
            Object_color = Object_Test.GetComponent<MeshRenderer>().material.color;
            // 根据当前颜色，设置新的颜色并调整透明度
            Object_color.a = Object_color_A;
            //  Debug.Log("已经成功改变颜色");

            //改变原物体颜色
            Object_Test.GetComponent<MeshRenderer>().material.color = Object_color;
        }

        try
        {
            for (int i = 0; i < Son_Object.Count; i++)
            {
                // Debug.Log(i);
                if (Son_Object[i].GetComponent<Renderer>() != null && Son_Object[i].GetComponent<Renderer>().material != null)
                {
                    // 拿到子物体的材质球
                    Object_Material = Son_Object[i].GetComponent<Renderer>().material;
                    // 拿到子物体的材质球颜色
                    Object_color = Son_Object[i].GetComponent<MeshRenderer>().material.color;
                    // 根据当前颜色，设置新的颜色并调整透明度
                    Object_color.a = Object_color_A;
                    // 改变子物体的颜色
                    Son_Object[i].GetComponent<MeshRenderer>().material.color = Object_color;
                }
            }
        }
        catch
        {

            Debug.Log(Object_Test.name + "异常返回");
            //    throw;
        }
    }


    /// <summary>
    /// 方案2 在原有材质球基础上进行复制 创建新的材质球 来改变颜色就不会影响其他的材质球
    /// 材质更改为半透明材质 / 不透明材质之间切换
    /// 后面还需要优化材质球管理部分 是否需要删除 遍历删除路径下的？？
    /// 回复初始不透明状态？ 直接找到之前的材质球？？
    /// </summary>
    public void Establish_Textur(GameObject Object_Test, bool Fqy = false)
    {
        return;//2024.5.13 暂时关闭

        Son_Object.Clear();//清空列表
        Gain_Texture(Object_Test);//遍历下面记录所有的子物体

        string Material_Name = null;
        float Object_color_A = 1;//默认状态 不透明
        if (Fqy)//为True 改变透明度半透明
        {
            Object_color_A = 0.5f;
        }
        //实例化创建一个材质球
        Object_Material_Test = new Material(Shader.Find("Standard"));

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("当前为WebGL IF判断");
            try
            {
                if (Object_Test.GetComponent<Renderer>() != null && Object_Test.GetComponent<Renderer>().material != null)
                {
                    //材质名称
                    Material_Name = Material_Path + Object_Test.gameObject.name + Material_Type;
                    //拿到材质球 源属性
                    Object_Material_Test = Object_Test.GetComponent<Renderer>().material;

                    //替换为新的材质球 这里应该会有Bug 部分模型会挂载多个材质球 2024.4.28 
                    //可以检测 检测数material组长度来做 暂时不更改
                    Object_Test.GetComponent<MeshRenderer>().material = Object_Material_Test;// AssetDatabase.LoadAssetAtPath<Material>(Material_Name)

                    // 拿到子物体的材质球颜色
                    Object_color = Object_Test.GetComponent<MeshRenderer>().material.color;
                    // 根据当前颜色，设置新的颜色并调整透明度
                    Object_color.a = Object_color_A;
                    // 改变子物体的颜色
                    Object_Test.GetComponent<MeshRenderer>().material.color = Object_color;
                }

                ///遍历 将所有子物体都拿到
                for (int i = 0; i < Son_Object.Count; i++)
                {
                    // Debug.Log(i);
                    if (Son_Object[i].GetComponent<Renderer>() != null && Son_Object[i].GetComponent<Renderer>().material != null)
                    {
                        //拿到子物体 材质球 源属性
                        Object_Material_Test = Son_Object[i].GetComponent<Renderer>().material;

                        //替换为新的材质球 这里应该会有Bug 部分模型会挂载多个材质球 2024.4.28 
                        //可以检测 检测数material组长度来做 暂时不更改
                        Son_Object[i].GetComponent<MeshRenderer>().material = Object_Material_Test;// AssetDatabase.LoadAssetAtPath<Material>(Material_Name)

                        // 拿到子物体的材质球颜色
                        Object_color = Son_Object[i].GetComponent<MeshRenderer>().material.color;
                        // 根据当前颜色，设置新的颜色并调整透明度
                        Object_color.a = Object_color_A;
                        // 改变子物体的颜色
                        Son_Object[i].GetComponent<MeshRenderer>().material.color = Object_color;
                    }
                }
            }
            catch
            {
                Debug.Log(Object_Test.name + "异常返回");//    throw;
            }
        }

        //unity 本地环境
#if UNITY_EDITOR
        Material[] Love_Fqy;
        Debug.Log("当前为本地环境 预处理");
        try
        {
            if (Object_Test.GetComponent<Renderer>() != null && Object_Test.GetComponent<Renderer>().material != null)
            {

                Love_Fqy = Texture_Lucency(Object_GetComponent_MeshRenderer(Object_Test, Material_Path), Object_color_A);//创建新的材质球
                Object3D_Texture(Object_Test, Love_Fqy);//更换材质球
            }

            ///遍历 将所有子物体都拿到
            for (int i = 0; i < Son_Object.Count; i++)
            {
                // Debug.Log(i);
                if (Son_Object[i].GetComponent<Renderer>() != null && Son_Object[i].GetComponent<Renderer>().material != null)
                {
                    //拿到子物体 材质球 源属性
                    //  Object_Material_Test = Son_Object[i].GetComponent<Renderer>().material;

                    //拿到子物体 材质名称
                    Material_Name = Material_Path + Object_Test.name;//子物体材质球位置

                    Love_Fqy = Texture_Lucency(Object_GetComponent_MeshRenderer(Son_Object[i], Material_Name), Object_color_A);//创建新的材质球
                    Object3D_Texture(Son_Object[i], Love_Fqy);//更换材质球
                }
            }
        }
        catch
        {
            Debug.Log(Object_Test.name + "异常返回");//    throw;
        }
#endif
    }

    /// <summary>
    /// 清空这个文件夹下的所有东西
    /// </summary>
    /// <param name="directoryPath"></param>//路径
    private void ClearDirectory_File(string directoryPath)
    {
        // 清空文件
        string[] files = Directory.GetFiles(directoryPath);//拿到获取这个文件夹下的物体
        foreach (string file in files)
        {
            File.Delete(file);
            //  Debug.Log("文件已删除：" + file);
        }

        // 递归清空子文件夹
        string[] subDirectories = Directory.GetDirectories(directoryPath);
        foreach (string subDirectory in subDirectories)
        {
            ClearDirectory_File(subDirectory);
        }
    }


    /// <summary>
    /// 更换材质
    /// </summary>
    /// <param name="Love"></param>
    /// <param name="Fqy"></param>
    public void Object3D_Texture(GameObject Love, Material[] Fqy)
    {
        for (int i = 0; i < Fqy.Length; i++)
        {
            Love.GetComponent<MeshRenderer>().materials[i] = Fqy[i];
        }

    }

    /// <summary>
    /// 改变材质透明度（0-1）
    /// </summary>
    /// <param name="Fqy"></param>
    /// <param name="lucency"></param>
    /// <returns></returns>
    public Material[] Texture_Lucency(Material[] Fqy, float lucency = 1)
    {
        Material[] Love_Fqy = Fqy;
        Color[] Object_colors = new Color[Fqy.Length];

        for (int i = 0; i < Fqy.Length; i++)
        {
            Object_colors[i] = Fqy[i].color;
            Object_colors[i].a = lucency;
            Love_Fqy[i].color = Object_colors[i];
        }
        return Love_Fqy;
    }


    /// <summary>
    /// 输入物体 Gameobject 和材质球路径
    /// 反馈当前物体材质球组
    /// </summary>
    /// <param name="Object_Fqy"></param>
    /// <param name="File_LJ"></param>
    /// <returns></returns>
    public Material[] Object_GetComponent_MeshRenderer(GameObject Object_Fqy, string File_LJ)
    {
        Material[] Object_Materials = new Material[0];//实例化
        string Materials_LJ = null;

        //判断当前是否为空物体
        if (Object_Fqy.GetComponent<MeshRenderer>() == null)
        { Debug.Log("MeshRenderer 没有，直接返回"); return Object_Materials; }

        int Love = 0;
        Love = Object_Fqy.GetComponent<MeshRenderer>().materials.Length;//拿到材质球长度
        Object_Materials = new Material[Love];
#if UNITY_EDITOR
        try
        {
            for (int i = 0; i < Love; i++)
            {
                //实例化创建一个材质球
                Object_Materials[i] = new Material(Shader.Find("Standard"));

                Materials_LJ = File_LJ + this.gameObject.name + "_" + i + ".mat";

                // 在编辑器模式下
                if (AssetDatabase.LoadAssetAtPath<Object>(Materials_LJ) == null)  // 判断文件是否存在 不存在
                {
                    Object_Materials[i] = Object_Fqy.GetComponent<Renderer>().materials[i];
                    Debug.Log(Object_Materials[i].name);
                    AssetDatabase.CreateAsset(Object_Materials[i], Materials_LJ);
                    //   Debug.Log("当前没有找到，已经创建新的材质球");
                }
                else// 在编辑器模式下检查材质球是否存在且不为空
                {
                    //加载指定路径下的材质球 防止之前材质球已经被创建 二次赋值
                    Object_Materials[i] = AssetDatabase.LoadAssetAtPath<Material>(Materials_LJ);
                    //删除指定路径下的
                    AssetDatabase.DeleteAsset(Materials_LJ);
                    Object_Materials[i] = Object_Fqy.GetComponent<Renderer>().materials[i];

                    // 更新替换已有的材质球 2024.5.7 导不出Webgl
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(Object_Materials[i]), Materials_LJ);
                    //重新进行创建
                    AssetDatabase.CreateAsset(Object_Materials[i], Materials_LJ);

                    //  Debug.Log("当前有找到，已经更新材质球");
                }
            }


        }
        catch
        {

        }
#endif
        return Object_Materials;
    }
}
