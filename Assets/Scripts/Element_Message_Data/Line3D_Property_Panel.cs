using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using System;


/// <summary>
/// ����3D����������� ��
/// </summary>
[Serializable]
public class Line3D_Property
{
    //[Header("ת���������")]
    [Header("��ɫ")]
    public Color Line3D_Color_Data;
    [Header("��Ź�")]
    public int Line3D_BHG_Data;
    [Header("��·��С")]
    public float Line3D_Size_Data;
}


public class Line3D_Property_Panel : MonoBehaviour
{
    /// <summary>
    /// ��ȡ��������ֵ����ת��
    /// 2024.07.27 20��30
    /// </summary>

    [Header("��ȡ������ɫ")]
    public UnityEngine.UI.Image Line3D_Color;
    [Header("��ȡ��Ź���")]
    public UnityEngine.UI.Text Line3D_BHG_text;
    [Header("��ȡ��·��С")]
    public UnityEngine.UI.Text Line3D_Size;

    private Line3D_Property Line3D_Property;//�������

    // [Header("��ȡ�����ű� ����·���������ù�ȥ")]
    // public Establish_3Dline_Interaction Establish_3Dline_Interaction;
    //ת���������



    /// <summary>
    /// �������л�������Ҫ�� 
    /// �õ������������� ת�����
    /// 2024.07.27 20��50
    /// </summary>
    public Line3D_Property Toggle_SHUJU()
    {
        Line3D_Property = new Line3D_Property();//ʵ����

        Line3D_Property.Line3D_Color_Data = Line3D_Color.color;
        Line3D_Property.Line3D_BHG_Data = 1; Line3D_Property.Line3D_Size_Data = 1;//�����ֵ��Ĭ��ֵ
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
