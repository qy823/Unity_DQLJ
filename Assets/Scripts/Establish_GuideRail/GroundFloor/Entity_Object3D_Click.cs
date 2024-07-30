using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;
using System;

public class Entity_Object3D_Click : MonoBehaviour
{
    /// <summary>
    /// ���˫�����¸����ƶ�
    /// ʵ����3D ���� 
    /// Ч��ɾ�������� �����͵��������¸���һ���ƶ�
    /// ���ƶ����������ʱ�� ����ɾ�������ٴθ����ƶ�
    /// </summary>

    [Header("�������ͱ�� ����Դ�����е���һ��λ��")]
    public int List_ID;//������һ���������� ����ɾ�������´���

    // [Header("��ǰ�������� ��һ�����죨ID��")]
    // public int DG_ID;

    // [Header("����")]
    // public GuideRail_Data GuideRail_Data;
    [Header("������")]
    public GuideRail_Interaction GuideRail_Interaction;
    // [Header("��������")]
    // public GuideRail_Object3D_Transcript GuideRail_Object3D_Transcript;

    private DateTime? lastRightClickTime; // ���ڴ洢��һ���Ҽ������ʱ��  

    private void Start()
    {
        if (GuideRail_Interaction == null)
        {
            GuideRail_Interaction = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Interaction>();
        }

        lastRightClickTime = DateTime.Now;
    }

    //���ú��� ���� ,û�и����ƶ��ſ��Խ��е��
    public void OnMouseDown()
    {
        //����û�й��ص�
        if (GuideRail_Interaction == null)
        {
            GuideRail_Interaction = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Interaction>();
        }

        if (GuideRail_Bus.FollowMovement_Bool == false)
        {
            // ���LastRightClickTime��null�������뵱ǰʱ����С��1��  
            if (lastRightClickTime.HasValue && (DateTime.Now - lastRightClickTime.Value).TotalMilliseconds < 1000)
            {
                GuideRail_Interaction.Click_Recreating(List_ID, this.name);
                // GuideRail_Data.Delete_Object_Name(List_ID, this.name);//�Ƚ�����ɾ����
                // GuideRail_Object3D_Transcript.Establish_Object3D_ListID();
                // GuideRail_Interaction.Toggle_Object3D(List_ID);//�����µ�����
                // Debug.Log("�涨ʱ����˫��");
            }

            // �������һ���Ҽ������ʱ��  
            lastRightClickTime = DateTime.Now;
        }
    }


}
