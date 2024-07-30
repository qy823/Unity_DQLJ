using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Interaction : MonoBehaviour
{
    /// <summary>
    /// �м���ɲ�
    /// �����޸����ڣ�2024.07.29 ��������
    /// </summary>


    [Header("����")]
    public GuideRail_Data GuideRail_Data;
    [Header("���� �ײ�")]
    public GuideRail_Object3D_Transcript GuideRail_Object3D_Transcript;
    [Header("���� �ײ�")]
    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;

    [Header("���ʱ仯")]
    public GuideRail_Object3D_Texture GuideRail_Object3D_Texture;

    public void Start()
    {
        if (GuideRail_Data == null)
        {
            GuideRail_Data = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Data>();
        }
        if (GuideRail_Object3D_Texture == null)
        {
            GuideRail_Object3D_Texture = GameObject.FindWithTag("GuideRail_GroundFloor").GetComponent<GuideRail_Object3D_Texture>();
        }
    }

    public void GuideRail_Interaction_Reset()
    {

        //���ݲ���Ҫ���õ�
        GuideRail_Data.GuideRail_Data_Reset();
    }

    #region ���ʱ仯
    public void Key_Trigger(bool Fqy)
    {
        //   Debug.Log("��ǰ״̬Ϊ��" + Fqy);
        if (Fqy)
        {
            Trigger_Object3D.SetActive(true);
        }
        else
        {
            Trigger_Object3D.SetActive(false);
        }
        Debug.Log("���ʱ仯 ��ǰ״̬Ϊ��" + Fqy);
    }

    /// <summary>
    /// ��������滻 ͸���Ͳ�͸��֮���л�
    /// </summary>
    public void Toggle_Texture(GameObject target_Object, bool Fqy = true)
    {
        if (Fqy)
        {
            GuideRail_Object3D_Texture.Replace_Texture(target_Object);//����Ϊ��͸��״̬ ��ԭ�л����ϸı�
        }
        else
        {
            GuideRail_Object3D_Texture.Establish_Textur(this.gameObject, true);//����Ϊ��͸��״̬ �����²�����ʽ
        }
    }

    /// <summary>
    /// ȷ��λ�ùرո���
    /// </summary>
    public void Close_Object3D()
    {
        GuideRail_Object3D_Highlight.GuideRail_Object3D_Highlight_Reset();
    }

    #endregion



    /// <summary>
    /// ����һ��
    /// </summary>
    /// <param name="ID"></param>
    public void Toggle_Object3D(int ID)
    {
        //�����������������ģʽ�Ͳ����� �ٷ������� 2024.07.26 00��00
        if (GuideRail_Bus.GuideRail_Bool) { return; }//�����������ģʽ�Ͳ����Դ����µ����� ǿ���˳�

        if (GuideRail_Bus.FollowMovement_Bool) { return; }//�����ǰ���������ڸ����ǾͲ����Դ����µ����� ǿ���˳�

        GuideRail_Object3D_Transcript.TJ(ID);//����
        GuideRail_Object3D_Highlight.Outline_TJ(ID);//������
        GuideRail_Bus.Type_ID = ID;
    }


    [Header("������������ ������")]//��������
    public GameObject Trigger_Object3D;



    /// <summary>
    /// ˫����ɾ�����������ٴδ���
    /// </summary>
    /// <param name="List_ID"></param>
    /// <param name="List_Name"></param>
    public void Click_Recreating(int List_ID, string List_Name)
    {
        GuideRail_Data.Delete_Object_Name(List_ID, List_Name);//�Ƚ�����ɾ����
        GuideRail_Object3D_Transcript.Establish_Object3D_ListID();
        Toggle_Object3D(List_ID);//�����µ�����
        Debug.Log("�涨ʱ����˫��");
    }
}
