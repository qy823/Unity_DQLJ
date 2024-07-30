using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;


/// <summary>
/// 3D 已连接线路的具体信息
/// </summary>
[Serializable]
public class Line3D_Informatization_Data
{
    [Header("仅仅表达当前端子与端子之间连接规则符合")]
    public bool Line3D_Informatization_Bool = false;//false 不符合 true 符合
    [Header("着条线路是由哪两个端子所连接的")]
    public GameObject[] Line3D_Terminal_Object3D = new GameObject[2];
    [Header("本线路的额定电压")]
    public int Line3D_Voltage = 0;
    [Header("本线路的额定电流")]
    public int Line3D_Electricity = 0;
}

#region 端子和元器件部分

//表示当前枚举值的描述信息
//元器件的类型 
[Serializable]
public enum Element_Type
{
    WYB = 2,
    JDQ,
    KQKG
}

//表示当前枚举值的描述信息
//元器件的端子类型
[Serializable]
public enum Element_Terminal_Type
{
    WYB = 0,
    JDQ,
    KQKG
}

/// <summary>
/// 表示 数据中元器件端子 数据
/// </summary>

[Serializable]
public class Element_Terminal_Data
{
    [Header("元器件端子引脚类型")]
    public Element_Terminal_Type Element_Terminal_Type;

    [Header("当前端子是否可连接")]
    public bool Execute_Bool;

    [Header("当前端子电压 额定值")]
    public float Element_Terminal_Voltage;
    [Header("当前元器件电流 额定值")]
    public float Element_Terminal_Electricity;

    [Header("元器中端子/引脚序号（唯一标志）")]
    public int Element_Terminal_Count;

    [Header("元器端子引脚类型 ID（枚举中的索引/值）")]
    public int Element_Terminal_ID;

}

/// <summary>
/// 元器件中的详细信息
/// </summary>
[Serializable]
public class Element_Message_Data
{

    //[Description("元器件类型")]
    // public Element_Type element_Types { get; private set; }//外部代码不能直接对 element_Types 赋值，但可以读取其值
    [Header("元器件类型")]
    public Element_Type Element_Type;//{ get; private set; }//外部代码不能直接对 element_Types 赋值，但可以读取其值

    [Header("当前元器件电压 额定值")]
    public float Element_Voltage;
    [Header("当前元器件电流 额定值")]
    public float Element_Electricity;

    [Header("元器件类型 ID（枚举中的索引/值）")]
    public int Element_ID;
    [Header("上层存储端子信息 备份一份")]
    public Element_Terminal_Data[] Element_Terminal_Data;

    /// <summary>
    /// 根据值返回枚举类型值 元器件
    /// </summary>
    /// <param name="Fqy"></param>
    /// <returns></returns>
    public int ElementType_Return_INT(Element_Type Fqy)
    {
        //GetHashCode() 方法返回的哈希码通常对应于枚举成员的基础整数值。
        return Fqy.GetHashCode();

    }

    /// <summary>
    /// 根据值返回枚举类型值 端子
    /// </summary>
    /// <param name="Fqy"></param>
    /// <returns></returns>
    public int ElementType_Terminal_Return_INT(Element_Terminal_Type Fqy)
    {
        //GetHashCode() 方法返回的哈希码通常对应于枚举成员的基础整数值。
        return Fqy.GetHashCode();

        //获取在Enum内对应名字 根据索引拿到名字
        // int type = 0;
        // //获取枚举类型 type1 中与整数值 4 相关联的枚举常量的名称。如果找不到对应的枚举常量，则返回 null
        // string name = Enum.GetName(typeof(Element_Type), type);

        //     //获取对应Enum值
        //     int type = 0;
        //     //整数值   转换为枚举类型  。在 C# 中，如果整数值在枚举类型中有定义，可以将其强制转换为该枚举类型
        //     Element_Type pos = (Element_Type)type；
        // //将字符串形式的枚举名称转换为 PosType 枚举类型
        // Element_Type pos = (Element_Type)Enum.Parse(typeof(Element_Type), Enum.GetName(typeof(Element_Type), type));
    }
}
#endregion
