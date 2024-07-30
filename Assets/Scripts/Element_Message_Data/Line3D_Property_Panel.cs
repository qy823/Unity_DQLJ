using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using System;


/// <summary>
/// 配置3D连线属性面板 类
/// </summary>
[Serializable]
public class Line3D_Property
{
    //[Header("转化后的数据")]
    [Header("颜色")]
    public Color Line3D_Color_Data;
    [Header("标号管")]
    public int Line3D_BHG_Data;
    [Header("线路大小")]
    public float Line3D_Size_Data;
}


public class Line3D_Property_Panel : MonoBehaviour
{
    /// <summary>
    /// 拿取面板里面的值进行转换
    /// 2024.07.27 20：30
    /// </summary>

    [Header("获取设置颜色")]
    public UnityEngine.UI.Image Line3D_Color;
    [Header("获取标号管文")]
    public UnityEngine.UI.Text Line3D_BHG_text;
    [Header("获取线路大小")]
    public UnityEngine.UI.Text Line3D_Size;

    private Line3D_Property Line3D_Property;//属性面板

    // [Header("获取交互脚本 将线路配置数据拿过去")]
    // public Establish_3Dline_Interaction Establish_3Dline_Interaction;
    //转化后的数据



    /// <summary>
    /// 将数据切换成我想要的 
    /// 拿到最新配置数据 转化完毕
    /// 2024.07.27 20：50
    /// </summary>
    public Line3D_Property Toggle_SHUJU()
    {
        Line3D_Property = new Line3D_Property();//实例化

        Line3D_Property.Line3D_Color_Data = Line3D_Color.color;
        Line3D_Property.Line3D_BHG_Data = 1; Line3D_Property.Line3D_Size_Data = 1;//避免空值传默认值
        if (Line3D_Size != null)
        {
            Line3D_Property.Line3D_BHG_Data = int.Parse(Line3D_BHG_text.text);
        }
        if (Line3D_Size != null)
        {
            Line3D_Property.Line3D_Size_Data = float.Parse(Line3D_Size.text);
        }
        return Line3D_Property;
    }
}
