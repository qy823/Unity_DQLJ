using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Bus : MonoBehaviour
{
  public static bool FollowMovement_Bool = false; // Ԫ�����Ƿ�������ק

  public static int Type_ID;//��ǵ�ǰ�ƶ�����һ�����͵�Դ���壨Ԫ������
  public static bool GuideRail_Bool = false;       // ��ǰ������һ��ģʽ trueΪ����ģʽ  falseΪ����Ԫ����ģʽ 

  // [Header("�õ�����")]
  // public GuideRail_Data GuideRail_Data;
  [Header("������")]
  public GuideRail_Interaction GuideRail_Interaction;

  // public void Start()
  // {

  // }

  public void GuideRail_Bus_Reset()
  {
    //ȫ�ֱ��� ��������
    FollowMovement_Bool = false;
    GuideRail_Bool = false;
    Type_ID = -1;


    //���������� �����˽��������� ���ݲ����� �Լ��ײ�����
    GuideRail_Interaction.GuideRail_Interaction_Reset();


  }

  // Update is called once per frame
  // void Update()
  // {
  //   if (Input.GetKey(KeyCode.P))
  //   {
  //     GuideRail_Data.Confirm_Object3D();
  //   }
  // }

  #region ����
  /// <summary>
  /// ȷ������3D����
  /// </summary>
  // public void Confirm_Object3D()
  // {
  //   for (int i = 0; i < GuideRail_Data.Prototype_Object3D.Length; i++)
  //   {
  //     for (int j = 0; j < GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D.Count; j++)
  //     {
  //       //   GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().
  //       if (GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>() != null)
  //         GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>().enabled = false;//�ر���ײ��

  //       //���ٸ���
  //       if (GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>() != null)
  //         Destroy(GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>());//���ٸ���

  //       //�õ���ǰ�����ٵ�ǰ�����еı�� ˳��
  //       if (GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>() != null)
  //       {
  //         GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().Object_Type_Order = j;
  //         GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().BoxCollider_true(true);//�򿪶�����ײ��
  //       }

  //     }
  //   }
  // }
  #endregion
}
