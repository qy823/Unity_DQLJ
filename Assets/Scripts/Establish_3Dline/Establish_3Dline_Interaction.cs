using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Establish_3Dline_Interaction : MonoBehaviour
{
    /// <summary>
    /// 3D���� ������
    /// 2024.07.25
    /// </summary>

    [Header("��ȡ������")]
    public Establish3Dline_GameObject Establish3Dline_GameObject;
    [Header("��ȡ���ݲ�")]
    public Establish_3Dline_Data Establish_3Dline_Data;




    /// <summary>
    /// ����
    /// </summary>
    public void Establish_3Dline_Interaction_Reset()
    {
        //����������Ҫ���õ�����
        Del_Line3D_GameObject = null;//�����ʱ�洢���߶ζ���
        Execute_Bool = false;//��ǰ���������Ƿ�ִ����� ��ʼ��

        //���ݲ������
        Establish_3Dline_Data.Establish_3Dline_Data_Reset();
    }




    #region ɾ��3D �߶�
    [Header("ɾ��3D �߶εĴ���")]
    public GameObject Del_Line3D_Window;
    private GameObject Del_Line3D_GameObject;//��ʱ�洢��Ҫɾ�����߶ζ���

    /// <summary>
    /// ɾ��ָ����3D �߶� ��ֵ����
    /// ���ֵ���
    /// </summary>
    public void Del_Line3D_Win(GameObject Line3DObject)
    {
        Del_Line3D_GameObject = Line3DObject;//��ʱ����������
                                             //����ɾ����ʾ����
        Del_Line3D_Window.SetActive(true);//�����
    }

    public void Del_Line3D()
    {
        //ɾ��ָ���߶�
        if (Del_Line3D_GameObject != null)
        {
            Establish_3Dline_Data.Delete_Line3D_GameObject(Del_Line3D_GameObject);//ɾ��ָ���߶�
            Del_Line3D_GameObject = null;//�����ʱ�洢���߶ζ���
        }

    }
    #endregion


    #region ��������3D�߶� 
    //��ǰ��û��ɾ���߶εĹ��� 
    //�������� 1���߶δ�С����ϸ�� 2���߶���ɫ  3���߶α�Źܱ�� �����϶�û��д�꡿

    //����ʹ��
    [Header("����3D ���ߵ��������")]
    public Line3D_Property_Panel Line3D_Property_Panel;
    [Header("����������")]
    public GameObject Line3D_Property_Panel_GameObject;

    // private Line3D_Property LS_Line3D_Property;//���մ��ݹ���������ֵ
    // private Color Line3D_Color;// [Header("����3D ���ߵ���ɫ")]
    // private float Line3D_Size;//  [Header("����3D ���ߵĴ�С")]
    // private int Line3D_BHG;// [Header("����3D ���ߵı�Źܱ��")]
    private GameObject[] Establish3Dline = new GameObject[2];//��ʱ��������������
    private Color CurrentColor_Click = Color.black;//���ö��ӵĵ����ɫ
    private Color CurrentColor_Start = Color.white;//���ö��ӵĵ����ɫ
    private bool Execute_Bool = false;//��ǰ�������� �Ƿ�ִ�����

    /// <summary>
    /// ���� ��ǰִ�����
    /// </summary>
    public void Line3D_Execute_Reset()
    {
        Establish3Dline[0].GetComponent<Renderer>().material.color = CurrentColor_Start;// new Color();//�ָ�����1��ʼ��ɫ
        Establish3Dline[1].GetComponent<Renderer>().material.color = CurrentColor_Start;//new Color();//�ָ�����2��ʼ��ɫ
        Establish3Dline = new GameObject[2];
        Execute_Bool = false;//��ǰִ�����
    }


    /// <summary>
    /// ����3D�߶� ������Ӵ���
    /// 2024.07.26
    /// </summary>
    /// <param name="Fqy"></param>
    public void Line3D(GameObject Fqy)
    {
        if (Execute_Bool) { return; }//����ûִ����ϱ����µ��ִ��
        Execute_Bool = true;//��ǰ����ִ��

        //��ǰ������Ҫ���ж϶����Ƿ��ظ����
        //�����һ������
        if (Establish3Dline[0] == null && Establish3Dline[1] == null)//û���κδ洢����
        {
            Establish3Dline[0] = Fqy;//��ֵ������
            Establish3Dline[0].GetComponent<Renderer>().material.color = CurrentColor_Click;//������ɫ �����ɫ
            Execute_Bool = false;//��ǰִ�����
            return;
        }
        //����˵ڶ�������
        else if (Establish3Dline[0] != null && Establish3Dline[1] == null)//ֻ��һ���洢���� 
        {
            Establish3Dline[1] = Fqy;//��ֵ������
            Establish3Dline[1].GetComponent<Renderer>().material.color = CurrentColor_Click;//������ɫ �����ɫ
            //���ε������ͬһ������
            if (Establish3Dline[1] == Establish3Dline[0])
            {
                Debug.Log("���ε������ͬһ������,����ѡ��");
                Line3D_Execute_Reset();//��ǰ����  ִ�����
                return;
            }
        }


        //2024.07.27 ֱ������ ���õ��ȷ�ϰ����ſ��Լ���������ж�
        Line3D_Property_Panel_GameObject.SetActive(true);//����������
        return; //ֱ������ ���õ��ȷ�ϰ����ſ��Լ���������ж�



        //�жϵ�ǰ�Ƿ�������һ��Ԫ�����ϵĶ��� ������Ǿ�������
        // if (Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name ==
        // Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name)
        // {
        //     Debug.Log("��ǰѡ�е���һ��Ԫ�����ϵĶ���,����ѡ��");
        //     Line3D_Execute_Reset();//��ǰ����  ִ�����
        //     return;
        // }

        // //����·����Ψһ���� ��֤������·��Ψһ��  �Ÿ�������ı���˳�����Ӻ����ַ����仯�Ϳ��Խ�������
        // string Line3D_name = null;  //Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_��Line3D��";
        // if (string.Compare(Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String, Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String) > 0)
        // {
        //     Line3D_name = Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
        //     Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_��Line3D��";
        // }
        // else
        // {
        //     Line3D_name = Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
        //     Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_��Line3D��";

        //     // //����һ��˳��
        //     // GameObject[] test = Establish3Dline;
        //     // Establish3Dline[0] = test[1];
        //     // Establish3Dline[1] = test[0];
        // }

        // //û�д�����ͬ���߶ο��Խ�������
        // if (Establish_3Dline_Data.Judge_Line3D_GameObject(Line3D_name))//��׼��Ҫ��������·�������ݿ�����ж�
        // {
        //     Establish3Dline_GameObject.Line3D_Property(Line3D_Color, Line3D_Size, Line3D_BHG);//�����߶���ɫ ��С ��Źܱ��
        //     GameObject Fqy_gameobject = Establish3Dline_GameObject.Establish_Line3D(Establish3Dline[0], Establish3Dline[1]);//���õײ㴴������������
        //     Fqy_gameobject.name = Line3D_name;//��������������������

        //     Establish_3Dline_Data.Addition_Line3D_GameObject(Fqy_gameobject);//������������ data

        //     //Ϊ�߶������Ϣ �ű�
        //     Line3D_Informatization LS_Line3D_Informatization = Fqy_gameobject.AddComponent<Line3D_Informatization>();//Ϊ������ӽű�
        //     // LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = new GameObject[2];//��������Ϣ������ �����ӵĶ��Ӵ��� Line3D_Informatization ��������Ϣ��֤����ʹ�ã�
        //     LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = Establish3Dline;//�����ߵ��������Ӵ洢���߶���

        //     //���͵������ϴ洢  ��һ�����ӾͿ����õ���ǰ���߶�����
        //     Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[1]);
        //     Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[0]);


        // }
        // //������ͬ���߶β����Խ�������
        // else
        // {

        // }

        // Line3D_Execute_Reset();//��ǰ����  ִ�����//����������
    }


    /// <summary>
    /// ȷ����������
    /// </summary>
    public void Yes_Click()
    {
        Line3D_Property LS_Line3D_Property = Line3D_Property_Panel.Toggle_SHUJU();//����������õ�ת��  ��ʱ�洢

        // Line3D_Color = Fqy.Line3D_Color_Data;//��ȡ��ɫ
        // Line3D_Size = Fqy.Line3D_Size_Data;//��ȡ�߶δ�С
        // Line3D_BHG = Fqy.Line3D_BHG_Data;//��ȡ��Źܱ��


        //�жϵ�ǰ�Ƿ�������һ��Ԫ�����ϵĶ��� ������Ǿ�������
        if (Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name ==
        Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization.gameObject.name)
        {
            Debug.Log("��ǰѡ�е���һ��Ԫ�����ϵĶ���,����ѡ��");
            Line3D_Execute_Reset();//��ǰ����  ִ�����
            return;
        }

        //����·����Ψһ���� ��֤������·��Ψһ��  �Ÿ�������ı���˳�����Ӻ����ַ����仯�Ϳ��Խ�������
        string Line3D_name = null;  //Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_��Line3D��";
        if (string.Compare(Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String, Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String) > 0)
        {
            Line3D_name = Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
            Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_��Line3D��";
        }
        else
        {
            Line3D_name = Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String +
            Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().GuideRail_String + "_��Line3D��";

            // //����һ��˳��
            // GameObject[] test = Establish3Dline;
            // Establish3Dline[0] = test[1];
            // Establish3Dline[1] = test[0];
        }

        //û�д�����ͬ���߶ο��Խ�������
        if (Establish_3Dline_Data.Judge_Line3D_GameObject(Line3D_name))//��׼��Ҫ��������·�������ݿ�����ж�
        {
            Establish3Dline_GameObject.Line3D_Property(LS_Line3D_Property.Line3D_Color_Data, LS_Line3D_Property.Line3D_Size_Data, LS_Line3D_Property.Line3D_BHG_Data);//�����߶���ɫ ��С ��Źܱ��
            GameObject Fqy_gameobject = Establish3Dline_GameObject.Establish_Line3D(Establish3Dline[0], Establish3Dline[1]);//���õײ㴴������������
            Fqy_gameobject.name = Line3D_name;//��������������������

            Establish_3Dline_Data.Addition_Line3D_GameObject(Fqy_gameobject);//������������ data

            //Ϊ�߶������Ϣ �ű�
            Line3D_Informatization LS_Line3D_Informatization = Fqy_gameobject.AddComponent<Line3D_Informatization>();//Ϊ������ӽű�
            // LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = new GameObject[2];//��������Ϣ������ �����ӵĶ��Ӵ��� Line3D_Informatization ��������Ϣ��֤����ʹ�ã�
            LS_Line3D_Informatization.Line3D_Informatization_Data.Line3D_Terminal_Object3D = Establish3Dline;//�����ߵ��������Ӵ洢���߶���

            //���͵������ϴ洢  ��һ�����ӾͿ����õ���ǰ���߶�����
            Establish3Dline[0].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[1]);
            Establish3Dline[1].GetComponent<Establish3Dline_Terminal_Click>().Terminal_Object3D_AddDel(Establish3Dline[0]);


        }
        //������ͬ���߶β����Խ�������
        else
        {

        }

        Line3D_Execute_Reset();//��ǰ����  ִ�����//����������
    }
    #endregion
}
