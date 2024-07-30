using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;


/// <summary>
/// 3D ��������·�ľ�����Ϣ
/// </summary>
[Serializable]
public class Line3D_Informatization_Data
{
    [Header("������ﵱǰ���������֮�����ӹ������")]
    public bool Line3D_Informatization_Bool = false;//false ������ true ����
    [Header("������·�������������������ӵ�")]
    public GameObject[] Line3D_Terminal_Object3D = new GameObject[2];
    [Header("����·�Ķ��ѹ")]
    public int Line3D_Voltage = 0;
    [Header("����·�Ķ����")]
    public int Line3D_Electricity = 0;
}

#region ���Ӻ�Ԫ��������

//��ʾ��ǰö��ֵ��������Ϣ
//Ԫ���������� 
[Serializable]
public enum Element_Type
{
    WYB = 2,
    JDQ,
    KQKG
}

//��ʾ��ǰö��ֵ��������Ϣ
//Ԫ�����Ķ�������
[Serializable]
public enum Element_Terminal_Type
{
    WYB = 0,
    JDQ,
    KQKG
}

/// <summary>
/// ��ʾ ������Ԫ�������� ����
/// </summary>

[Serializable]
public class Element_Terminal_Data
{
    [Header("Ԫ����������������")]
    public Element_Terminal_Type Element_Terminal_Type;

    [Header("��ǰ�����Ƿ������")]
    public bool Execute_Bool;

    [Header("��ǰ���ӵ�ѹ �ֵ")]
    public float Element_Terminal_Voltage;
    [Header("��ǰԪ�������� �ֵ")]
    public float Element_Terminal_Electricity;

    [Header("Ԫ���ж���/������ţ�Ψһ��־��")]
    public int Element_Terminal_Count;

    [Header("Ԫ�������������� ID��ö���е�����/ֵ��")]
    public int Element_Terminal_ID;

}

/// <summary>
/// Ԫ�����е���ϸ��Ϣ
/// </summary>
[Serializable]
public class Element_Message_Data
{

    //[Description("Ԫ��������")]
    // public Element_Type element_Types { get; private set; }//�ⲿ���벻��ֱ�Ӷ� element_Types ��ֵ�������Զ�ȡ��ֵ
    [Header("Ԫ��������")]
    public Element_Type Element_Type;//{ get; private set; }//�ⲿ���벻��ֱ�Ӷ� element_Types ��ֵ�������Զ�ȡ��ֵ

    [Header("��ǰԪ������ѹ �ֵ")]
    public float Element_Voltage;
    [Header("��ǰԪ�������� �ֵ")]
    public float Element_Electricity;

    [Header("Ԫ�������� ID��ö���е�����/ֵ��")]
    public int Element_ID;
    [Header("�ϲ�洢������Ϣ ����һ��")]
    public Element_Terminal_Data[] Element_Terminal_Data;

    /// <summary>
    /// ����ֵ����ö������ֵ Ԫ����
    /// </summary>
    /// <param name="Fqy"></param>
    /// <returns></returns>
    public int ElementType_Return_INT(Element_Type Fqy)
    {
        //GetHashCode() �������صĹ�ϣ��ͨ����Ӧ��ö�ٳ�Ա�Ļ�������ֵ��
        return Fqy.GetHashCode();

    }

    /// <summary>
    /// ����ֵ����ö������ֵ ����
    /// </summary>
    /// <param name="Fqy"></param>
    /// <returns></returns>
    public int ElementType_Terminal_Return_INT(Element_Terminal_Type Fqy)
    {
        //GetHashCode() �������صĹ�ϣ��ͨ����Ӧ��ö�ٳ�Ա�Ļ�������ֵ��
        return Fqy.GetHashCode();

        //��ȡ��Enum�ڶ�Ӧ���� ���������õ�����
        // int type = 0;
        // //��ȡö������ type1 ��������ֵ 4 �������ö�ٳ��������ơ�����Ҳ�����Ӧ��ö�ٳ������򷵻� null
        // string name = Enum.GetName(typeof(Element_Type), type);

        //     //��ȡ��ӦEnumֵ
        //     int type = 0;
        //     //����ֵ   ת��Ϊö������  ���� C# �У��������ֵ��ö���������ж��壬���Խ���ǿ��ת��Ϊ��ö������
        //     Element_Type pos = (Element_Type)type��
        // //���ַ�����ʽ��ö������ת��Ϊ PosType ö������
        // Element_Type pos = (Element_Type)Enum.Parse(typeof(Element_Type), Enum.GetName(typeof(Element_Type), type));
    }
}
#endregion
