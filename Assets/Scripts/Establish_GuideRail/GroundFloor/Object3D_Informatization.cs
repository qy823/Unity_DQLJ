using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3D_Informatization : MonoBehaviour
{
    /// <summary>
    /// �洢��ǰԪ����Ϣ
    /// �洢������ǰ�������Ϣ
    /// �����޸�ʱ�䣺2024.07.25 
    /// </summary>

    //��Ҫ�ֶ����õ�ֵ
    [Header("Ԫ������ϸ��Ϣ���� �Լ�������¼")]
    public Element_Message_Data Element_Message_Data;

    [Header("��������")]
    public string Object_Type;
    [Header("������ ��λ�ã�")]
    public int GuideRail_ID = -1;
    [Header("���� ��������Ľӿڶ��� ����")]
    public GameObject[] Object3D_Terminal;


    //�������������Զ���ֵ

    [Header("���� ����˳����")]
    public int Object_Type_Order;//�ڼ���

    [Header("����Ķ���������Ϣ Bool")]//�� ��������Ľӿڶ������� һһ��Ӧ
    public bool[] GuideRail_Establish3Dline_Bool;//true ��ǰ����������;false ��ǰ����û������;

    public void Start()
    {
        GuideRail_Establish3Dline_Bool = new bool[Object3D_Terminal.Length];//���ȸ�����Ķ�������һ��
    }


    /// <summary>
    /// �򿪶�����ײ���ʾ�ſ�ʼ����
    /// ���������еĽӿڶ���
    /// ��/�ر� ���ӵ���ײ��
    /// </summary>
    /// <param name="Fqy"></param>
    public void BoxCollider_true(bool Fqy)
    {
        for (int i = 0; i < Object3D_Terminal.Length; i++)
        {
            if (Object3D_Terminal[i].GetComponent<BoxCollider>() != null)
            {
                Object3D_Terminal[i].GetComponent<BoxCollider>().enabled = Fqy;
            }
            // else if (Object3D_Terminal[i].GetComponent<MeshCollider>() != null)
            // {
            //     Object3D_Terminal[i].GetComponent<MeshCollider>().enabled = Fqy;
            // }
            else if (Object3D_Terminal[i].GetComponent<SphereCollider>() != null)
            {
                Object3D_Terminal[i].GetComponent<SphereCollider>().enabled = Fqy;
            }
        }


        //2024.07.25 ����λ�ϲ����Ψһ��ʶ ��ʹ���ֶ�����
        //��ǰ ����ײ�������򣿣� ��ʱʹ���ϲ����ķ��� 
        for (int i = 0; i < Object3D_Terminal.Length; i++)
        {
            Object3D_Terminal[i].GetComponent<Establish3Dline_Terminal_Click>().IndexID = i;
        }
    }


    /// <summary>
    /// �����²����¶����������
    /// 2024.07.26 
    /// </summary>
    public void Update_GuideRail_Establish3Dline_Bool()
    {
        GuideRail_Establish3Dline_Bool = new bool[Object3D_Terminal.Length];
        for (int i = 0; i < Object3D_Terminal.Length; i++)
        {
            //�������ʶ��ǰ����������·��
            if (Object3D_Terminal[i].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D.Count > 0)
            {
                GuideRail_Establish3Dline_Bool[i] = true;
            }
        }
    }


    /// <summary>
    /// ���ص�ǰԪ�������ӵĶ�������
    /// </summary>
    /// <returns></returns>
    public int Return_3Dline()
    {
        int fqy = 0;

        foreach (bool i in GuideRail_Establish3Dline_Bool)
        {
            if (i)
            {
                fqy++;
            }
        }

        return fqy;

        // for (int i = 0; i < GuideRail_Establish3Dline_Bool.Length; i++)
        // {
        //     if (GuideRail_Establish3Dline_Bool[i])
        //     {
        //         fqy++;
        //     }
        // }
    }

}
