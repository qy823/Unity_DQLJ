using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line3D_Informatization : MonoBehaviour
{
    /// <summary>
    /// �߶���Ϣ
    /// 2024.07.23  ��Ҫ���ڼ�¼���ߵ���Ϣ
    ///�����޸ģ�2024.07.24 �޸���Ϣ�ṹ 
    /// �����޸ģ�
    /// </summary>

    [Header("����·��������Ϣ")]
    public Line3D_Informatization_Data Line3D_Informatization_Data;

    // [Header("������ﵱǰ���������֮�����ӹ������")]
    // public bool Line3D_Informatization_Bool = false;//false ������ true ����
    // [Header("������·�������������������ӵ�")]
    // public GameObject[] Line3D_Terminal_Object3D = new GameObject[2];
    // [Header("����·�Ķ��ѹ")]
    // public int Line3D_Voltage = 0;
    // [Header("����·�Ķ����")]
    // public int Line3D_Electricity = 0;

    public void Awake()
    {
        Line3D_Informatization_Data = new Line3D_Informatization_Data();//ʵ����
    }

    void Start()
    {
        //������ʱ��Ϳ�ʼ��ֵ���� �������·�Ƿ���ȷ����
        //��������ʱ���뿪ʼ������·�����⸳ֵ������
        StartCoroutine(Delayed(2));//��ʱ���뿪ʼ������·�Ƿ���ȷ
    }
    //StartCoroutine(Delayed(1));  
    IEnumerator Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //��ʱ��ִ�е�

        //�����������ӵ���Ϣ������Ԫ�� ������Ļ�  Line3D_Boool = true;���� Line3D_Boool = false;
    }
}
