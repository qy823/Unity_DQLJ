using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Transcript : MonoBehaviour
{
    /// <summary>
    /// ������������
    ///ɾ������ ��Data���м�¼ɾ��������
    /// </summary>

    [Header("���ݲ�")]
    public GuideRail_Data GuideRail_Data;

    [Header("����")]
    public int ID;

    public void Start()
    {
        if (GuideRail_Data == null)
        {
            GuideRail_Data = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Data>();
        }
    }
    #region ����ʹ��
    // public void Update()
    // {
    //     // if (Input.GetKeyDown(KeyCode.Alpha9))
    //     // {
    //     //     Establish_Textur(Tset_Object, false);
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.Alpha8))
    //     // {
    //     //     Establish_Textur(Tset_Object, true);
    //     // }
    //     //  Test();//����
    // }

    /// <summary>
    /// ����ʹ��
    /// </summary>
    // public void Test()
    // {
    //     //�л���������
    //     if (Input.GetKeyDown(KeyCode.Alpha0))
    //     {
    //         ID = 0;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha1))
    //     {
    //         ID = 1;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha2))
    //     {
    //         ID = 2;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha3))
    //     {
    //         ID = 3;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Alpha4))
    //     {
    //         ID = 4;
    //     }


    //     if (Input.GetKey(KeyCode.Q))//���
    //     {
    //         GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Add(new Generate_Object3D());//����һ��
    //         GuideRail_Data.Record_Generate_Object3D(GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D[GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count - 1], Establish_Object3D(ID, GuideRail_Data.Main_Object3D_FB[ID]));
    //     }
    //     if (Input.GetKey(KeyCode.A))//ɾ��
    //     {
    //         //ɾ�����һ������
    //         if (GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count > ID)
    //         {
    //             Debug.Log("ɾ�����ݵڣ�" + (GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count - 1) + "��");
    //             GuideRail_Data.Delete_Generate_Object3D(GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D, "", GuideRail_Data.Prototype_Object3D[ID].List_Generate_Object3D.Count - 1);

    //             GuideRail_Data.Prototype_Object3D[ID].Object3D_Quantity--;//��¼��������������-1

    //             if (GuideRail_Data.Prototype_Object3D[ID].Object3D_Quantity <= 0)
    //             {
    //                 GuideRail_Data.Prototype_Object3D[ID].Object3D_Quantity = 0;
    //             }
    //         }
    //     }
    // }


    /// <summary>
    /// �������� ������ԭ�б����ɣ�
    /// FollowMovement_BoolΪtrue Ӧ���������һ�����ڸ���������ټ���
    /// </summary>
    /// <param name="index"></param>
    public void TJ(int index)
    {
        if (GuideRail_Bus.FollowMovement_Bool)
        {
            return;
        }

        GuideRail_Data.Prototype_Object3D[index].List_Generate_Object3D.Add(new Generate_Object3D());//����һ��
        GuideRail_Data.Record_Generate_Object3D(GuideRail_Data.Prototype_Object3D[index].
        List_Generate_Object3D[GuideRail_Data.Prototype_Object3D[index].List_Generate_Object3D.Count - 1], Establish_Object3D(index, GuideRail_Data.Main_Object3D_FB[index]));
    }
    #endregion

    #region ����������������

    /// <summary>
    ///ָ��һ������ ����һ������
    ///��������������ָ�� �ĸ�����
    ///���Ĵ�������������
    ///�������ĸ������ݼ�¼ ����һ�μ�¼һ�����ݣ�
    /// </summary>
    public GameObject Establish_Object3D(int Fqy, GameObject Object_1)
    {
        GuideRail_Data.Prototype_Object3D[Fqy].Object3D_Quantity++;//��¼��������������+1
        GameObject Object_test = Instantiate(GuideRail_Data.Prototype_Object3D[Fqy].Object3D);//����һ������
        Object_test.name = GuideRail_Data.Prototype_Object3D[Fqy].Object3D_Name + "_" + GuideRail_Data.Prototype_Object3D[Fqy].Object3D_Quantity;//���ĸ������� ԭ��������+_+"�ڼ���"
        Object_test.transform.parent = Object_1.transform;//�� Object_1 ��������

        Object_test.SetActive(true);//��������
        return Object_test;
    }

    /// <summary>
    /// �������ʹ���һ��
    /// </summary>
    public void Establish_Object3D_ListID()
    {
        Debug.Log("��ǰ�Ѿ��ɹ�����");
    }

    #endregion


    // #region  ��¼����
    // /// <summary>
    // /// ��¼����
    // /// ����һ��Generate_Object3D����һ��GameObject
    // /// ��GameObject����ϸ���ݼ�¼�� Generate_Object3D����
    // /// </summary>
    // /// <param name="Fqy"></param>
    // /// <param name="Object_3D"></param>
    // public void Record_Generate_Object3D(Generate_Object3D Fqy, GameObject Object_3D)
    // {
    //     Fqy.This_Object3D = Object_3D;
    //     Fqy.Name = Object_3D.name;//��ȡ����������

    //     Fqy.World_Position = Object_3D.transform.TransformDirection(transform.position);//�ֲ��ռ�ת��Ϊ����ռ��е�λ��
    //     Fqy.World_Rotation = Object_3D.transform.rotation;//��¼����������ת

    //     Fqy.This_Position = Object_3D.transform.localPosition;//�ֲ��ֲ�ת��Ϊ����ռ��е�λ��
    //     Fqy.This_Rotation = Object_3D.transform.localRotation;//��¼�ֲ�������ת
    // }

    // /// <summary>
    // /// ���ָ�����͵��е� ָ���������ֵ� �����Լ�������¼����
    // /// </summary>
    // public void Establish_Object3D_Name(int Fqy, string Name, GameObject T_Object_3D)
    // {
    //     if ((Fqy > GuideRail_Data.Prototype_Object3D.Length || Fqy < 0) && Name == "")
    //     {
    //         Debug.Log("�������������������");
    //         return;
    //     }
    //     List<Generate_Object3D> T1_Generate_Object3D = new List<Generate_Object3D>();
    //     T1_Generate_Object3D = GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D;//�õ�ID��ǵ�����

    //     //ʹ�ò������ֵķ���
    //     int Love = 0;
    //     for (int i = 0; i < T1_Generate_Object3D.Count; i++)
    //     {
    //         if (T1_Generate_Object3D[i].Name != Name)
    //         {

    //             Love++;
    //         }
    //         else
    //         {
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Object3D = T_Object_3D;
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].Name = T_Object_3D.name;//��ȡ����������

    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Position = T_Object_3D.transform.TransformDirection(transform.position);//�ֲ��ռ�ת��Ϊ����ռ��е�λ��
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].World_Rotation = T_Object_3D.transform.rotation;//��¼����������ת

    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Position = T_Object_3D.transform.localPosition;//�ֲ��ֲ�ת��Ϊ����ռ��е�λ��
    //             GuideRail_Data.Prototype_Object3D[Fqy].List_Generate_Object3D[i].This_Rotation = T_Object_3D.transform.localRotation;//��¼�ֲ�������ת
    //             Debug.Log("��ǰ��¼���ݵ��������ƣ�" + Name);
    //             return;
    //         }
    //     }

    //     if (Love >= T1_Generate_Object3D.Count)
    //     {
    //         Debug.Log("û�����б��ҵ���� ���������");
    //     }
    // }
    // #endregion
}
