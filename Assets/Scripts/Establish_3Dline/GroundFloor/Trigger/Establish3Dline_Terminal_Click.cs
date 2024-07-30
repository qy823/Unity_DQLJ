using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows;
using System;

public class Establish3Dline_Terminal_Click : MonoBehaviour
{

      /// <summary>
      /// ����3D���ߵĴ���
      /// ��¼���ӵ�λ�����ݵ�
      ///��������� ���ڽ���3D���� ��Ԫ��������
      ///�� Object3D_Informatization ��Ϣ�ű����ݴ�����ȥ
      ///�����޸�ʱ�䣺2024.07.25 
      /// </summary>

      //�Զ����غͲ�����Ҫ�ֶ����صĽű�
      private Establish3Dline_Bus Establish3Dline_Bus;//���� ��ï��
      private Establish_3Dline_Interaction Establish_3Dline_Interaction;//3D���ߴ��� �м佻����
      [Header("������ϸ��Ϣ")]
      public Element_Terminal_Data Element_Terminal_Data;

      [Header("������ӹ���Ԫ������Ӧ�� ��Ϣ�ű���Object3D_Informatization��")]
      public GameObject Object3D_Informatization;//����ͨ�������ȡԭ��������Ϣ


      //��Ҫ���õĶ�������  
      [Header("��¼�����϶��ӻ������¶���;Ĭ��0����,1����;���»��Զ��ڵ���id��1")]
      public int UpDown;// [Header("Ĭ��0����,1����")]// [Header("���»��Զ��ڵ���id��1")]


      //����Ԫ������Ϣ�Զ���ȡ��
      [Header("��¼���ǵڼ��� ����Ψһ��ʶ")]
      public int IndexID;//1�������ֶ����䣨��ͣʹ�ã�/ 2�������ϲ�������������Զ����䣨��ͣʹ���Ѿ���ע�ͣ�/3�������ϲ�������ţ�����ʹ�� ���ȼ�����ߣ�

      [Header("�����ϲ���Ϣ�õ���¼������� ")]//��������µ����������Ϊ�ϲ����µ�����Ϣ
      public int GuideRail_ID;

      [Header("�����ϲ���Ϣ+��������Ϣ ƴ�� �����ַ���")]
      public string GuideRail_String;

      // [Header("��ǰ���ӵ� ��������")]
      // public int Establish3Dline_Quantity;
      // [Header("��ǰ���ӵ� �������� ���嵽����")]
      // public List<GameObject> Establish3Dline_Object3D;

      //�ڲ�����
      private DateTime? lastRightClickTime; // ���ڴ洢��һ���Ҽ������ʱ�� 

      [Header("�洢��ǰ ������������� �Ķ�������")]
      public List<GameObject> Terminal_Object3D;

      /// <summary>
      /// ��ʼ�� �����ϲ�����Զ������� ����ʶλ
      /// </summary>
      // public void Awake()
      // {
      //       IndexID = -1;//��һ����ʼֵ
      //       for (int i = 0; i < Object3D_Informatization.GetComponent<Object3D_Informatization>().Object3D_Terminal.Length; i++)
      //       {
      //             //�����ϲ�洢��������
      //             if (this.gameObject == Object3D_Informatization.GetComponent<Object3D_Informatization>().Object3D_Terminal[i].gameObject)
      //             {
      //                   IndexID = i;
      //             }
      //       }
      //       if (IndexID < 0)
      //       {
      //             Debug.LogAssertion("û�����ϲ㣬�ҵ���Ӧ�Ķ�����Ϣ(���Ų����)����ǰ��������Ϊ��" + this.gameObject.name);
      //       }
      // }

      // Start�����ڳ�������ʱ�����ã�ִֻ��һ��
      public void Start()
      {
            Establish_3Dline_Interaction = GameObject.FindWithTag("Establish_3Dline").GetComponent<Establish_3Dline_Interaction>();//��ȡ3D���ߴ��� �м佻����
            Establish3Dline_Bus = GameObject.FindWithTag("Establish_3Dline").GetComponent<Establish3Dline_Bus>();//��ȡbus�ű�

            GuideRail_ID = Object3D_Informatization.GetComponent<Object3D_Informatization>().GuideRail_ID;//���µ������
                                                                                                          //�ַ���ƴ�ӣ�Ԫ������������+�������+�������                                                                                         
            GuideRail_String = Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type + Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type_Order + IndexID + "_";
      }


      /// <summary>
      /// �ڲ�����¼� ��������
      /// </summary>
      private void OnMouseDown()
      {
            // ���LastRightClickTime��null�������뵱ǰʱ����С��1��  
            if (lastRightClickTime.HasValue && (DateTime.Now - lastRightClickTime.Value).TotalMilliseconds < 1000)
            {
                  Debug.Log("��ǰ����˫���������κβ���");
                  return;
            }
            lastRightClickTime = DateTime.Now;//�������µ��ʱ��

            if (Establish3Dline_Bus == null)
            {
                  Establish3Dline_Bus = GameObject.FindWithTag("Establish_3Dline").GetComponent<Establish3Dline_Bus>();
            }

            GuideRail_ID = Object3D_Informatization.GetComponent<Object3D_Informatization>().GuideRail_ID;//���µ������
            //����һЩ�����������
            //�ַ���ƴ�ӣ�Ԫ������������+�������+�������
            GuideRail_String = Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type + Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type_Order + IndexID + "_";
            Establish_3Dline_Interaction.Line3D(this.gameObject);

            //2024.07.23 ע�� ȡ��ʹ����ï��һ�׽ű�
            //����ַ��� ������������������+������ţ�
            //             string text = Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type
            //             + Object3D_Informatization.GetComponent<Object3D_Informatization>().Object_Type_Order;

            //             Establish3Dline_Bus.OnwutiDown(this.transform.position, GuideRail_ID,
            //  text, IndexID, this.transform.GetComponent<MeshRenderer>(), UpDown);
      }


      /// <summary>
      /// �б��������/�б�ɾ������
      /// 2024.07.25 �������� ��·���ӵĶ��� �����б�
      /// 2024.07.26 ���� ����һ���ϲ�洢���������
      /// </summary>
      /// <param name="obj"></param> ������������
      /// <param name="Fqy"></param> Ĭ��true������� false����ɾ��
      public void Terminal_Object3D_AddDel(GameObject obj, bool Fqy = true)
      {
            if (Fqy)//true ������� false����ɾ��
            {
                  Terminal_Object3D.Add(obj);
            }
            else
            {
                  Terminal_Object3D.Remove(obj);//ɾ��ָ��ֵ
                  // Terminal_Object3D.RemoveAt(0);//ɾ���±�Ϊindex��Ԫ��
                  // Terminal_Object3D.RemoveRange(3, 2);//���±�index��ʼ��ɾ��count��Ԫ��
            }

            //2024.07.26 ���� ����һ���ϲ�洢���������
            Object3D_Informatization.GetComponent<Object3D_Informatization>().Update_GuideRail_Establish3Dline_Bool();//����Ԫ�������� �����������
      }


}
