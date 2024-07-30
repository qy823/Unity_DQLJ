using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//��¼�������ϸ��Ϣ 
public class Generate_Object3D
{
    [Header("�洢����������")]
    public string Name;

    [Header("3D ����")]
    public GameObject This_Object3D;

    [Header("�洢����������")]
    public Vector3 World_Position;
    [Header("�洢��������ת")]
    public Quaternion World_Rotation;


    [Header("�洢�ľֲ�����")]
    public Vector3 This_Position;
    [Header("�洢�ľֲ���ת")]
    public Quaternion This_Rotation;
}

[System.Serializable]//��¼ԭ������ Gameobject+��������
public class Prototype_Object3D
{
    [Header("��ȡ3D ����ԭ��")]
    public GameObject Object3D;
    [Header("��������Ӧ��3D ��������")]
    public string Object3D_Name;

    [Header("�������������� һ�������������������ǵ�ǰ����")]
    public int Object3D_Quantity;

    [Header("�洢3D ������������ϸ����")]
    public List<Generate_Object3D> List_Generate_Object3D;
}

[System.Serializable]//��¼һ�������ϵ�����
public class GuideRail_Object3D
{
    [Header("��������Ӧ��3D ��������")]
    public List<string> Object3D_Name;
    [Header("Դ����ID")]
    public List<int> Generate_Object3D_ID;
    // [Header("�����ĸ���ID")]
    // public List<int> Prototype_Object3D_ID;
    [Header("����������")]
    public int Quantity;
}

public class GuideRail_Data : MonoBehaviour
{
    /// <summary>
    /// ���ݲ� ��¼�����ϵ���������
    /// �����޸�ʱ�䣺2024.07.29
    /// </summary>

    [Header("�� �ڿ���������")]
    public GameObject Main_Camera;

    [Header("3D ����Դ����")]
    public GameObject[] Main_Object3D;

    [Header("3D ����Դ���� ����")]
    public GameObject[] Main_Object3D_FB;

    [Header("��¼���� ����Դ����+��Ӧ�����б�����")]
    public Prototype_Object3D[] Prototype_Object3D;
    [Header("��¼ÿһ�����������")]
    public GuideRail_Object3D[] GuideRail_Object3D;

    public void Awake()
    {
        GuideRail_Data_Reset();
    }

    /// <summary>
    /// ��������
    /// 2024.07.29
    /// </summary>
    public void GuideRail_Data_Reset()
    {
        //�������������

        //�������ݵ�����
        Prototype_Object3D = new Prototype_Object3D[Main_Object3D.Length];
        //����������ּ�¼���� ����Ҳ�õ� 
        for (int i = 0; i < Main_Object3D.Length; i++)
        {
            Prototype_Object3D[i] = new Prototype_Object3D();//����ʵ����
            Prototype_Object3D[i].Object3D = Main_Object3D[i];
            Prototype_Object3D[i].Object3D_Name = Main_Object3D[i].name;//�õ�Դ��������

            Prototype_Object3D[i].List_Generate_Object3D = new List<Generate_Object3D>();//����ʵ����
        }
    }

    #region  ��������

    /// <summary>
    /// ȷ������3D����
    /// ��������ģʽ �رշ�������ģʽ
    /// 2024.07.26 
    /// </summary>
    public void Confirm_Object3D()
    {
        for (int i = 0; i < Prototype_Object3D.Length; i++)
        {
            for (int j = 0; j < Prototype_Object3D[i].List_Generate_Object3D.Count; j++)
            {
                //   GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().
                if (Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>() != null)
                    Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>().enabled = false;//�ر���ײ��

                //���ٸ���
                if (Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>() != null)
                    Destroy(Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>());//���ٸ���

                //�õ���ǰ�����ٵ�ǰ�����еı�� ˳��
                if (Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>() != null)
                {
                    Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().Object_Type_Order = j;
                    Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().BoxCollider_true(true);//�򿪶�����ײ��
                }

            }
        }

        GuideRail_Bus.GuideRail_Bool = true;//��ʶ��������ģʽ �������ٷ���Ԫ����
    }

    #endregion


    #region  ��¼����
    /// <summary>
    /// ��¼����
    /// ����һ��Generate_Object3D����һ��GameObject
    /// ��GameObject����ϸ���ݼ�¼�� Generate_Object3D����
    /// </summary>
    /// <param name="Fqy"></param>
    /// <param name="Object_3D"></param>
    public void Record_Generate_Object3D(Generate_Object3D Fqy, GameObject Object_3D)
    {
        Fqy.This_Object3D = Object_3D;
        Fqy.Name = Object_3D.name;//��ȡ����������

        Fqy.World_Position = Object_3D.transform.TransformDirection(transform.position);//�ֲ��ռ�ת��Ϊ����ռ��е�λ��
        Fqy.World_Rotation = Object_3D.transform.rotation;//��¼����������ת

        Fqy.This_Position = Object_3D.transform.localPosition;//�ֲ��ֲ�ת��Ϊ����ռ��е�λ��
        Fqy.This_Rotation = Object_3D.transform.localRotation;//��¼�ֲ�������ת
    }

    /// <summary>
    /// ���ָ�����͵��е� ָ���������ֵ� �����Լ�������¼����
    /// </summary>
    public void Establish_Object3D_Name(int Fqy, string Name, GameObject T_Object_3D)
    {
        if ((Fqy > Prototype_Object3D.Length || Fqy < 0) && Name == "")
        {
            Debug.Log("�������������������");
            return;
        }
        List<Generate_Object3D> T1_Generate_Object3D = new List<Generate_Object3D>();
        T1_Generate_Object3D = Prototype_Object3D[Fqy].List_Generate_Object3D;//�õ�ID��ǵ�����

        //ʹ�ò������ֵķ���
        int Love = 0;
        for (int i = 0; i < T1_Generate_Object3D.Count; i++)
        {
            if (T1_Generate_Object3D[i].Name != Name)
            {

                Love++;
            }
            else
            {
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Object3D = T_Object_3D;
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].Name = T_Object_3D.name;//��ȡ����������

                Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Position = T_Object_3D.transform.TransformDirection(transform.position);//�ֲ��ռ�ת��Ϊ����ռ��е�λ��
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Rotation = T_Object_3D.transform.rotation;//��¼����������ת

                Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Position = T_Object_3D.transform.localPosition;//�ֲ��ֲ�ת��Ϊ����ռ��е�λ��
                Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Rotation = T_Object_3D.transform.localRotation;//��¼�ֲ�������ת
                Debug.Log("��ǰ��¼���ݵ��������ƣ�" + Name);
                return;
            }
        }

        if (Love >= T1_Generate_Object3D.Count)
        {
            Debug.Log("û�����б��ҵ���� ���������");
        }
    }
    #endregion

    #region ɾ������

    /// <summary>
    /// ��ִ��ɾ�����壬�����ָ���б�
    /// </summary>
    /// <param name="T_Generate_Object3D"> �б�</param>
    /// <param name="Name">����</param>
    /// <param name="Fqy">ɾ��������</param>
    public void Delete_Generate_Object3D(List<Generate_Object3D> T_Generate_Object3D, string Name = "", int Fqy = -1)
    {
        //ʹ��ID ֱ��ɾ��
        if (Fqy > -1)//����ID ֵ��ȷֱ�Ӳ���ɾ�� ��Ӧ����
        {
            // Destroy(List_Generate_Object3D[Fqy].This_Object3D);//ֱ��ɾ������ �첽����Ӱ�����߳�
            DestroyImmediate(T_Generate_Object3D[Fqy].This_Object3D);//����ɾ�� ����˳��ִ�л�Ӱ�����߳�
            T_Generate_Object3D.RemoveAt(Fqy);
            return;
        }

        //ʹ�ò������ֵķ���
        int Love = 0;
        for (int i = 0; i < T_Generate_Object3D.Count; i++)
        {
            if (T_Generate_Object3D[i].Name != Name)
            {
                Love++;
            }
            else
            {
                //Destroy(List_Generate_Object3D[i].This_Object3D);//ֱ��ɾ������ �첽����Ӱ�����߳�
                DestroyImmediate(T_Generate_Object3D[i].This_Object3D);//����ɾ�� ����˳��ִ�л�Ӱ�����߳�
                T_Generate_Object3D.RemoveAt(i); //ɾ��ָ������
                return;
            }
        }

        if (Love >= T_Generate_Object3D.Count)
        {
            Debug.Log("û�����б��ҵ���� ���������");
        }
    }
    /// <summary>
    /// ɾ��ָ�����͵��е�ָ���������ֵ� �����Լ�������¼����
    /// �����Ƴ�ɾ��
    /// </summary>
    public void Delete_Object_Name(int Fqy, string Name = "")
    {

        if ((Fqy > Prototype_Object3D.Length || Fqy < 0) && Name == "")
        {
            Debug.Log("�������������������");
            return;
        }
        List<Generate_Object3D> T1_Generate_Object3D = new List<Generate_Object3D>();
        T1_Generate_Object3D = Prototype_Object3D[Fqy].List_Generate_Object3D;//�õ�ID��ǵ�����

        //ʹ�ò������ֵķ���
        int Love = 0;
        for (int i = 0; i < T1_Generate_Object3D.Count; i++)
        {
            if (T1_Generate_Object3D[i].Name != Name)
            {
                Love++;
            }
            else
            {
                //2024.07.21 ����ɾ���������� �����ֵ��Ϊ��ǰ����������ܴ��� ������ֻ��
                //Prototype_Object3D[Fqy].Object3D_Quantity--;//��¼��������������-1
                if (Prototype_Object3D[Fqy].Object3D_Quantity <= 0)//��¼��ǰ��ǰ����
                {
                    Prototype_Object3D[Fqy].Object3D_Quantity = 0;
                }
                //Destroy(List_Generate_Object3D[i].This_Object3D);//ֱ��ɾ������ �첽����Ӱ�����߳�
                DestroyImmediate(T1_Generate_Object3D[i].This_Object3D);//����ɾ�� ����˳��ִ�л�Ӱ�����߳�
                //T1_Generate_Object3D.RemoveAt(i); //ɾ��ָ������
                Prototype_Object3D[Fqy].List_Generate_Object3D.RemoveAt(i); //ɾ��ԭ����ָ������
                //Prototype_Object3D[Fqy].List_Generate_Object3D = T1_Generate_Object3D;//��ֵ������ԭ����

                Debug.Log("ɾ��ָ���������֣�" + Name);
                return;
            }
        }

        if (Love >= T1_Generate_Object3D.Count)
        {
            Debug.Log("û�����б��ҵ���� ���������");
        }
    }

    #endregion
}
