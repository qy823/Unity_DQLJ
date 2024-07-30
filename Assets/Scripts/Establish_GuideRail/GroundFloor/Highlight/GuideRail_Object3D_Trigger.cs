using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Trigger : MonoBehaviour
{
    /// <summary>
    /// ���������õ����� ��ʼ���� ���嵼�����λ�ü���
    /// �����޸����ڣ�2024.07.20
    /// </summary>


    [Header("��ǰ������")]
    public int Trigger_ID;
    [Header("����������ƴ��� ")]

    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;

    // Update is called once per frame  void OnTriggerEnter(Collider other)//�Ӵ�ʱ�������������
    // void OnTriggerEnter(Collider other)//�Ӵ�ʱ�������������
    // {
    //     Debug.Log(Time.time + ":����ô������Ķ����ǣ�" + other.gameObject.name);
    //     // Show(prompt);
    // }

    /// <summary>
    /// �����봥��
    /// </summary>
    public void OnMouseEnter()
    {
        GuideRail_Object3D_Highlight.Generate_Ouline_One(Trigger_ID);
        // Debug.Log("���룬���죺" + Trigger_ID);
    }

    /// <summary>
    /// ����Ƴ�
    /// </summary>
    public void OnMouseExit()
    {
        GuideRail_Object3D_Highlight.GuideRail_Object3D_Highlight_Reset();
        //  Debug.Log("�Ƴ������죺" + Trigger_ID);
    }
}
