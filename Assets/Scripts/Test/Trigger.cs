using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.ComponentModel;

[Serializable]

public class AAA
{

}



public class Trigger : MonoBehaviour
{
    // [Flags]

    public int mark { set; get; }

    //    / [Serializable]
    public Element_Message_Data Element_Message_Data;

    public Element_Type m_type;
    void Start()
    {
        //拿到标志位的ID
        Element_Message_Data.Element_ID = Element_Message_Data.ElementType_Return_INT(Element_Message_Data.Element_Type);

        switch (m_type)
        {
            case Element_Type.WYB:
                Debug.Log((int)Element_Type.WYB);
                Debug.Log((int)Element_Type.JDQ);
                Debug.Log((int)Element_Type.KQKG);
                break;
            case Element_Type.JDQ:
                Debug.Log((int)Element_Type.JDQ);
                break;

        }

        Element_Type type = Element_Type.WYB;
        int index = type.GetHashCode();
        Debug.Log(index);

        int type2 = 4;
        string name = Enum.GetName(typeof(Element_Type), type2);
        Debug.Log(name);

        int type3 = 5;
        Element_Type pos = (Element_Type)type3;
        Debug.Log(pos);

    }

    /// <summary>
    /// 从枚举类型和它的特性读出并返回一个键值对
    /// </summary>
    /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
    /// <returns>键值对</returns>
    // public static Element_Type GetNVCFromEnumValue(Type enumType)
    // {
    //     Element_Type nvc = new Element_Type();
    //     Type typeDescription = typeof(DescriptionAttribute);
    //     System.Reflection.FieldInfo[] fields = enumType.GetFields();
    //     string strText = string.Empty;
    //     string strValue = string.Empty;
    //     foreach (FieldInfo field in fields)
    //     {
    //         if (field.FieldType.IsEnum)
    //         {
    //             strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
    //             object[] arr = field.GetCustomAttributes(typeDescription, true);
    //             if (arr.Length > 0)
    //             {
    //                 DescriptionAttribute aa = (DescriptionAttribute)arr[0];
    //                 strText = aa.Description;
    //             }
    //             else
    //             {
    //                 strText = field.Name;
    //             }
    //             nvc.Add(strText, strValue);
    //         }
    //     }
    //     return nvc;
    // }
}

