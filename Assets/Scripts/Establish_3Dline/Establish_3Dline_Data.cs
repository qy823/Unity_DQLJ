using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Establish_3Dline_Data : MonoBehaviour
{
    /// <summary>
    /// ��¼���߲�����һϵ��������Ϣ
    /// ��¼�߶���������¼���������߶�
    /// 2024.07.23 ������������ȡ��
    /// 2024.07. �����޸�
    /// </summary>

    public int css = 0; //��¼�ж����߶���
    // public MeshRenderer[] meshRenderers = new MeshRenderer[2];
    public List<GameObject> Line3D_GameObjects = new List<GameObject>(); // �洢���е��߶ζ���
    public List<string> Line3D_GameObject_Str = new List<string>(); // �洢���е��߶ζ�������

    public void Establish_3Dline_Data_Reset()
    {
        //�����������ӵ��߶�����
        for (int i = 0; i < Line3D_GameObjects.Count; i++)
        {
            Destroy(Line3D_GameObjects[i]);//���ٵ�ǰ�߶�����  
        }


        //  ������� ����б�
        Line3D_GameObjects.Clear(); // �洢���е��߶ζ���
        Line3D_GameObject_Str.Clear(); // �洢���е��߶ζ�������
        //��ֹû�����  ����new һ��
        Line3D_GameObjects = new List<GameObject>();
        Line3D_GameObject_Str = new List<string>();
        //   css = 0; //��¼�ж����߶���
    }


    #region �жϡ���ӡ�ɾ�� 3D �߶�
    /// <summary>
    ///  �жϵ�ǰ���ݿ����Ƿ������ͬ������ ��������ͬ���߶�
    /// true �ɹ���� false ʧ�� ==>��ǰ�������Լ�������ͬ������
    /// �����޸�ʱ�䣺2024.07.23 15��50
    /// </summary>
    public bool Judge_Line3D_GameObject(string Line3D_GameObject_Name)
    {
        if (Line3D_GameObject_Str.Count == 0) { return true; }//��ǰ��û�п�ʼ���ֱ���˳�

        bool Fqy = !Line3D_GameObject_Str.Contains(Line3D_GameObject_Name);//��Ϊ�ҵ���Ϊtrue; ����ȡ�� falseΪ�Ѿ��ҵ��� trueΪ�ҵ�
        //int index = Array.IndexOf(Line3D_GameObject_Str, "orange");

        if (Fqy == false)
        {
            //    Debug.Log("��ǰ�����д�����ͬ�����֣�" + Line3D_GameObject_Name + "������ǰ�߶��Ѵ��ڣ����ʧ�ܣ�");
            return Fqy;
        }
        // Line3D_GameObjects.Add(Line3D_GameObject);//����µ�����
        // Line3D_GameObject_Str.Add(Line3D_GameObject.name);//����µ��߶����� 
        //  Debug.Log("��ǰ�����в�������ͬ�����֣�����ǰ�߶β����ڣ�����ӳɹ���");
        return Fqy;
    }

    /// <summary>
    /// ���������߶μ������
    /// �����޸�ʱ�䣺2024.07.23 16��20
    /// </summary>
    /// <param name="Line3D_GameObject"></param> �����߶�
    public void Addition_Line3D_GameObject(GameObject Line3D_GameObject)
    {
        Line3D_GameObjects.Add(Line3D_GameObject);//����µ�����
        Line3D_GameObject_Str.Add(Line3D_GameObject.name);//����µ��߶�����
    }


    /// <summary>
    /// ɾ��ָ���߶�
    /// �����޸�ʱ�䣺2024.07.23 16��50
    /// </summary>
    /// <param name="Line3D_GameObject"></param>
    public void Delete_Line3D_GameObject(GameObject Line3D_GameObject)
    {
        //ɾ���洢����
        Line3D_GameObject_Str.Remove(Line3D_GameObject.name);//ɾ���߶�����
        Line3D_GameObjects.Remove(Line3D_GameObject);//ɾ������

        //�������������
        Destroy(Line3D_GameObject);//���ٵ�ǰ�߶�����  
    }
    #endregion
}
