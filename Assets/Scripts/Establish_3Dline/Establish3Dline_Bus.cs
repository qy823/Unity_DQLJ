using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Establish3Dline_Bus : MonoBehaviour
{
    // /// <summary>
    // /// �����߶� ��������
    // /// ����webgl ��bug
    // /// </summary>

    public static bool Establish3Dline_Bool = false;//3D����ģʽ�Ƿ����
    [Header("3D����ģʽ������")]
    public Establish_3Dline_Interaction Establish_3Dline_Interaction;

    /// <summary>
    /// 3D��������
    /// �������� 
    /// </summary>
    public void Establish3Dline_Bus_Reset()
    {
        Establish3Dline_Bool = false;
        Establish_3Dline_Interaction.Establish_3Dline_Interaction_Reset();//���ý��������� + data��������

    }

    //���������ʱ������Ҫ
    // [Header("����ɫ")]
    // public Color currentColor;//����ɫ
    // [Header("ԭ����ɫ")]
    // public Color ysColor;//����ԭ����ɫ
    // [Header("��Ԥ����")]
    // public GameObject ballPrefab; // С���Ԥ����
    // [Header("��Ԥ����")]
    // public GameObject linePrefab; // Բ������߶�

    // private List<GameObject> linesList = new List<GameObject>(); // �洢���е��߶ζ���
    // [Header("Բ���������")]
    // public Vector3 originalScale; // ԭʼԲ���������
    // [Header("С�������")]
    // public Vector3 originalScale_; // ԭʼС�������
    // [Header("�������")]
    // public Camera Camera_3D;
    // public GameObject kwt;
    // public int kwtNameIndex;
    // //��¼���ڵ�����߶Σ���s�����Ҷ�Ӧ���Ƶ��߶β�ɾ��

    // // public GameObject EmptyObject;
    // [Header("��Ź�")]
    // public GameObject GameObject_BHG;
    // [Header("��Ź�����")]
    // public Vector3 BHG_SF; // ԭʼԲ���������
    // [Header("��Ź�����λ��")]

    // public float BHG_WZ;
    // //public float desiredZ = 1f; // Ԥ��������ƽ���Z���꣨����������
    // [Header("��¼��λλ�� ��λ����λ����Ϣ")]//��λ����λ����Ϣ
    // public GameObject[] gameObject_WZ_list = new GameObject[2];



    // [Header("��¼�������ӵ�����λ��")]
    // public Vector3[] cc = new Vector3[2];
    // public GameObject kwt_;//������
    // [Header("��¼���ӵ�����Ԫ������")]
    // public string[] parentNames_;//��¼���ӵ�����Ԫ������
    // public ArrayList parentNames = new ArrayList();//��¼�Ѿ����ӵ��߶ε�����
    // public int[] terminal_ints_list = new int[2];//��¼����������Ԫ�����Ǽ���

    // public int[] list_Datas = new int[2];//��¼����Ԫ���ֱ�����һ����
    // public int css = 0; //��¼�ж����߶���
    // private MeshRenderer[] meshRenderers = new MeshRenderer[2];
    // private int[] UpDowns = new int[2];


    // /// <summary>
    // /// ��������λ�ã��ڼ������죬Ԫ�����ƣ����Ŷ���
    // /// </summary>
    // /// <param name="wzlist">��������λ��</param>
    // /// <param name="listID">�ڼ�������</param>
    // /// <param name="parentName">Ԫ������</param>
    // /// <param name="int_list">���Ŷ���</param>
    // public void OnwutiDown(Vector3 wzlist, int listID, string parentName, int int_list, MeshRenderer LS_MeshRenderer, int UpDown)
    // {
    //     //�������������ӣ���λ�����ƶ�һ��
    //     if (UpDown == 0) { listID++; };


    //     // listID++;
    //     //  int_list++;
    //     if (parentNames_[0] == "")//ѡ��һ��
    //     {
    //         LS_MeshRenderer.material.color = currentColor;
    //         list_Datas[0] = listID;
    //         //����
    //         UpDowns[0] = UpDown;
    //         //���Ӳ������
    //         meshRenderers[0] = LS_MeshRenderer;
    //         //Ԫ���ϵļ��Ŷ���
    //         terminal_ints_list[0] = int_list;
    //         //  Debug.Log("Ԫ������" + parentName);
    //         //Ԫ������
    //         parentNames_[0] = parentName;
    //         //����λ��
    //         cc[0] = wzlist;
    //     }
    //     //����һ�����Ӻ͵ڶ������Ӳ���ͬһ��Ԫ����ʱ��������
    //     else if (parentName != parentNames_[0])//�ڶ��ν�������
    //     {
    //         list_Datas[1] = listID;
    //         UpDowns[1] = UpDown;
    //         meshRenderers[1] = LS_MeshRenderer;
    //         parentNames_[1] = parentName;
    //         terminal_ints_list[1] = int_list;

    //         //  kwtNameIndex++;
    //         // Debug.Log(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire");
    //         //Debug.Log(parentNames.Contains(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire"));
    //         //Debug.Log(parentNames.Contains(parentNames_[1] + terminal_ints_list[1] + "_" + parentNames_[0] + terminal_ints_list[0] + "_wire"));
    //         for (int i = 0; i < parentNames.Count; i++)
    //         {
    //             Debug.Log(parentNames[i]);
    //         }
    //         //���û�����ߣ��ʹ������ߣ�����˳���ж�һ��
    //         if (!parentNames.Contains(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire") && !parentNames.Contains(parentNames_[1] + terminal_ints_list[1] + "_" + parentNames_[0] + terminal_ints_list[0] + "_wire"))
    //         {
    //             //�߶�����1
    //             css++;
    //             //�����߶εĸ�����
    //             GameObject kwt_mb = Instantiate(kwt, new Vector3(0, 0, 0), Quaternion.identity);
    //             kwt_ = kwt_mb;
    //             Debug.Log("���߳ɹ�");
    //             //�����ּ�¼����������
    //             parentNames.Add(parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire");
    //             kwt_.transform.name = parentNames_[0] + terminal_ints_list[0] + "_" + parentNames_[1] + terminal_ints_list[1] + "_wire";
    //             // terminal_ints_list[1] = listID;
    //             cc[1] = wzlist;
    //             //  locationCalculate(cc[0], gameObject_WZ_list[terminal_ints_list[0]].transform.position, gameObject_WZ_list[terminal_ints_list[1]].transform.position, cc[1]);
    //             //�ж���ͬһ�Ż��ǲ�ͬ���������߼�������
    //             if (list_Datas[0] != list_Datas[1])
    //             {
    //                 locationCalculate(cc[0], gameObject_WZ_list[list_Datas[0]].transform.position, gameObject_WZ_list[list_Datas[1]].transform.position, cc[1]);
    //             }
    //             else
    //             {
    //                 typ(cc[0], cc[1], gameObject_WZ_list[list_Datas[0]].transform.position);
    //             }

    //             //�������ݣ�������һ�δ���

    //             parentNames_[0] = "";
    //             parentNames_[1] = "";
    //             //�ָ�������ɫ
    //             meshRenderers[0].material.color = ysColor;
    //             meshRenderers[1].material.color = ysColor;
    //             //  return;
    //         }
    //         else
    //         {
    //             meshRenderers[0].material.color = ysColor;
    //             meshRenderers[1].material.color = ysColor;
    //             parentNames_[0] = "";
    //             parentNames_[1] = "";
    //             Debug.Log("������");
    //         }

    //         //xian(gameObjectq.transform.position, gameObjecth.transform.position);
    //     }
    //     else//ͬһ��Ԫ����
    //     {

    //         //�������ݣ�������һ�δ���
    //         parentNames_[0] = "";
    //         parentNames_[1] = "";
    //         meshRenderers[0].material.color = ysColor;
    //         // meshRenderers[1].material.color = ysColor;
    //         //  meshRenderers[1].material.color = ysColor;
    //         // Debug.Log(parentNames_[0]);
    //         // Debug.Log(parentNames_[1]);
    //         Debug.Log("����ͬһ��Ԫ��");
    //         //xian(gameObjectq.transform.position, gameObjecth.transform.position);
    //     }
    // }
    // /// <summary>
    // /// ����1������2����λ
    // /// </summary>
    // /// <param name="q">����1</param>
    // /// <param name="q1">����2</param>
    // /// <param name="index">��λ</param>
    // public void typ(Vector3 q, Vector3 q1, Vector3 index)
    // {
    //     //�����Ź�λ��
    //     Vector3 bhg_1;
    //     Vector3 bhg_2;
    //     if (UpDowns[0] == 0)
    //     {
    //         bhg_1 = new Vector3(q.x, q.y - BHG_WZ, q.z);
    //     }
    //     else
    //     {
    //         bhg_1 = new Vector3(q.x, q.y + BHG_WZ, q.z);
    //     }
    //     GameObject ball1 = Instantiate(GameObject_BHG, bhg_1, Quaternion.identity, kwt_.transform);
    //     ball1.transform.localScale = BHG_SF;
    //     if (UpDowns[1] == 0)
    //     {
    //         bhg_2 = new Vector3(q1.x, q1.y - BHG_WZ, q1.z);
    //     }
    //     else
    //     {
    //         bhg_2 = new Vector3(q1.x, q1.y + BHG_WZ, q1.z);
    //     }

    //     GameObject ball2 = Instantiate(GameObject_BHG, bhg_2, Quaternion.identity, kwt_.transform);
    //     ball2.transform.localScale = BHG_SF;//�޸ĳ���

    //     //���ɱ�Ź�


    //     //�޸ı�Ź�
    //     ball1.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("1");
    //     //�޸ı�Ź�
    //     ball2.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("2");

    //     // Debug.Log("q" + q);
    //     // Debug.Log("q1" + q1);
    //     //  Debug.Log("index" + index);
    //     float zz = q.z;
    //     Vector3 x1 = new Vector3(q.x, index.y, zz);
    //     Vector3 x2 = new Vector3(q1.x, index.y, zz);
    //     xian(q, x1);
    //     xian(x1, x2);
    //     xian(x2, q1);
    // }

    // /// <summary>
    // /// ����λ��1����λ1����λ2������λ��2��
    // /// </summary>
    // /// <param name="q">����λ��1</param>
    // /// <param name="q1">��λ1</param>
    // /// <param name="h1">��λ2</param>
    // /// <param name="h">����λ��2</param>
    // public void locationCalculate(Vector3 DZ1, Vector3 DW1, Vector3 DW2, Vector3 DZ2)
    // {
    //     //�����Ź�λ��
    //     Vector3 bhg_1;
    //     Vector3 bhg_2;

    //     if (UpDowns[0] == 0)//��
    //     {
    //         bhg_1 = new Vector3(DZ1.x, DZ1.y - BHG_WZ, DZ1.z);
    //     }
    //     else//��
    //     {
    //         bhg_1 = new Vector3(DZ1.x, DZ1.y + BHG_WZ, DZ1.z);
    //     }

    //     if (UpDowns[1] == 0)
    //     {
    //         bhg_2 = new Vector3(DZ2.x, DZ2.y - BHG_WZ, DZ2.z);
    //     }
    //     else
    //     {
    //         bhg_2 = new Vector3(DZ2.x, DZ2.y + BHG_WZ, DZ2.z);
    //     }
    //     //���ɱ�Ź�
    //     GameObject ball1 = Instantiate(GameObject_BHG, bhg_1, Quaternion.identity, kwt_.transform);
    //     ball1.transform.localScale = BHG_SF;//�޸ĳ���
    //     //�޸ı�Ź�
    //     ball1.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("1");
    //     GameObject ball2 = Instantiate(GameObject_BHG, bhg_2, Quaternion.identity, kwt_.transform);
    //     ball2.transform.localScale = BHG_SF;//�޸ĳ���
    //     //�޸ı�Ź�
    //     ball2.GetComponent<Establish3Dline_Tag_Alter>().ste_Text("2");
    //     //  Debug.Log("q" + q);
    //     // Debug.Log("q1" + q1);
    //     // Debug.Log("h1" + h1);
    //     // Debug.Log("h" + h);
    //     //��¼�߶�
    //     float zz = DZ1.z;
    //     Vector3 x1 = new Vector3(DZ1.x, DW1.y, zz);
    //     xian(DZ1, x1);
    //     xian(x1, DW1);
    //     xian(DW1, DW2);
    //     Vector3 x2 = new Vector3(DZ2.x, DW2.y, zz);
    //     xian(DW2, x2);
    //     xian(x2, DZ2);

    // }

    // private GameObject currentLine; // ��ǰ���߶ζ���

    // //��������λ�ô����߶�
    // public void xian(Vector3 initialPosition, Vector3 currentPosition)
    // {
    //     //  Debug.Log(initialPosition);
    //     //  Debug.Log(currentPosition);
    //     // ����С������λ��
    //     GameObject ball = Instantiate(ballPrefab, initialPosition, Quaternion.identity, kwt_.transform);
    //     //�л����� �����߶ε���ɫ
    //     ball.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
    //     //�޸�С���С
    //     ball.transform.localScale = new Vector3(originalScale_.x, originalScale_.y, originalScale_.z);
    //     // �����߶β����ó�ʼλ��
    //     currentLine = Instantiate(linePrefab, initialPosition, Quaternion.identity, kwt_.transform);
    //     // �����߶ε���ɫ
    //     currentLine.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
    //     // �����������Ȳ������߶ε����ź�λ��
    //     float distance = Vector3.Distance(initialPosition, currentPosition);
    //     //�ߴ�С
    //     currentLine.transform.localScale = new Vector3(originalScale.x, distance / 2f * originalScale.y, originalScale.z);//�޸��߶γ���
    //     //���¼���λ��
    //     currentLine.transform.position = (initialPosition + currentPosition) / 2f;

    //     // ������ת����
    //     Vector3 direction = currentPosition - initialPosition;
    //     Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
    //     currentLine.transform.rotation = rotation;




    // }
    // private string fjName; // ��¼ɾ����Ԫ������

    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.S) && parentNames.Contains(fjName))
    //     {
    //         //ɾ��Ԫ��
    //         Destroy(GameObject.Find(fjName));
    //         //��������ɾ�����
    //         parentNames.Remove(fjName);
    //         css--;
    //     }
    // }

    // //����ɾ������
    // //����߶μ�¼��������� 
    // public void shanchu(string a)
    // {
    //     Debug.Log(a);
    //     fjName = a;
    //     //css--;
    //     //  Destroy(GameObject.Find(a));
    // }

}
