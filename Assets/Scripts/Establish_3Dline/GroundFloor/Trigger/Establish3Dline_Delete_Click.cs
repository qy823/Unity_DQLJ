using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Establish3Dline_Delete_Click : MonoBehaviour
{
  //����¼� �����صײ�ű�
  //ͨ�����ִ�����ȥɾ��
  //����ɾ���߶�


  //public Establish3Dline_Bus Establish3Dline_Bus;
  [Header("�м佻���� ���ɾ���߶�")]
  public Establish_3Dline_Interaction Establish_3Dline_Interaction;//������

  private DateTime? lastRightClickTime; // ���ڴ洢��һ���Ҽ������ʱ�� 

  public void OnMouseDown()
  {
    //2024.07.29 ��ʱ������ï�ֵ�ɾ�����ܣ���Ϊ���ɾ���߶�
    //����߽��������ָ�����
    //Establish3Dline_Bus.shanchu(transform.parent.name);//������� �߶����ִ�����ȥ

    //����˫���߶ν����Ƿ�ɾ���߶�
    if (lastRightClickTime.HasValue && (DateTime.Now - lastRightClickTime.Value).TotalMilliseconds < 1000)
    {
      Establish_3Dline_Interaction.Del_Line3D_Win(transform.parent.gameObject);//�������崫����ȥ
      //return;
    }
    else
    {
      Debug.Log("��ǰû������˫���������κβ���");
    }
    lastRightClickTime = DateTime.Now;//�������µ��ʱ��


  }
}
