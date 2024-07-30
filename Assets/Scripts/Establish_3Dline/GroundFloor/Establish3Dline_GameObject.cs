using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Line3D_GameObject
{
    [Header("��Ԥ����")]
    public GameObject ballPrefab; // С���Ԥ���� �սǲ��ֵ���
    [Header("��Ԥ����")]
    public GameObject linePrefab; // Բ������߶� �ߵĴ�С���Ĳ�������
    [Header("��Ź�")]
    public GameObject GameObject_BHG;//�õ����Ը��ı�Źܵ�Ԥ���� ��ͷ���Ӳ���
    // [Header("��Ź��ƶ�����")]
    // public Vector3 BHG_Move_Direction;

    [Header("Բ���������")]
    public Vector3 YZT_OriginalScale; // ԭʼԲ���������
    [Header("С�������")]
    public Vector3 XQ_OriginalScale; // ԭʼС�������
    [Header("��Ź�����")]
    public Vector3 BHG_OriginalScale;



}

public class Establish3Dline_GameObject : MonoBehaviour
{
    /// <summary>
    /// ����������㡢�յ㡢��·��ϸ�ȣ����ţ�����·��ɫ  �����߶�
    /// �����޸� 
    /// </summary>

    [Header("��Ҫȥ��ȡ��ԴԤ����/��Ϣ")]
    public Line3D_GameObject Line3D_GameObject;//����Ԥ�����Line3D_GameObject ���д�������

    //��Ҫ�õ�������
    //��� (GameObject)�� �յ� (GameObject) ����·ϸ�ȣ����� ��λ������·��ɫ ����ѡ��
    //��ϸ��λ��Ϣ��������/��  ��ǰ������һ������ \
    // [Header("��¼��λλ�� ��λ����λ����Ϣ ")][SerializeField]//��λ����λ����Ϣ �ɱ�
    private GameObject[] GuideRail_WZ_List = new GameObject[5];

    [Header("��¼��λλ�� ��λ����λ����Ϣ �Ҳ�")]//��λ����λ����Ϣ
    [SerializeField] private GameObject[] GuideRail_WZ_List_R = new GameObject[5];

    [Header("��¼��λλ�� ��λ����λ����Ϣ ���")]//��λ����λ����Ϣ
    [SerializeField] private GameObject[] GuideRail_WZ_List_L = new GameObject[5];


    [SerializeField]//���л��ɼ�
    [Header("���ڴ����ĳ�ʼ�߶� ��������")] private GameObject Line3D_XD;//���ڴ����ĳ�ʼ�߶� 
    [SerializeField]//���л��ɼ�
    [Header("�������õ��߶Σ��������洢��������")] private GameObject Line3D_XD_Main;//�洢�����������߶θ�����
    private float Line3D_BHG_Position = 0.2f;//��Źܵ�λ��ƫ����

    private Color LS_Color;//�洢��ʱ��ɫ�仯
    private int BHG_ID;//��ʱ�洢��Źܵ�ID
    private float Line3D_localScale = 0;//������·��ϸ�ȴ�С 



    private Vector3 YZT_OriginalScale_Start; // ԭʼԲ����ĳ�ʼ����  // [Header("Բ���������")]
    private Vector3 XQ_OriginalScale_Start; // ԭʼС��ĳ�ʼ����    //[Header("С�������")]
    private Vector3 BHG_OriginalScale_Start; // ԭʼ��Źܵĳ�ʼ����   //[Header("��Ź�����")]

    public void Start()
    {
        //��¼��ʼֵ
        YZT_OriginalScale_Start = Line3D_GameObject.YZT_OriginalScale;
        XQ_OriginalScale_Start = Line3D_GameObject.XQ_OriginalScale;
        BHG_OriginalScale_Start = Line3D_GameObject.BHG_OriginalScale;
        //��ʼ������
    }


    /// <summary>
    /// 3D ���ߵ���Ӧ��������
    /// </summary>
    public void Line3D_Property(Color Line3D_Color, float Line3D_CX = 1, int _BHG_ID = 0)
    {
        // YZT_OriginalScale_Start = Line3D_GameObject.YZT_OriginalScale;
        // XQ_OriginalScale_Start = Line3D_GameObject.XQ_OriginalScale;
        // BHG_OriginalScale_Start = Line3D_GameObject.BHG_OriginalScale;
        LS_Color = Line3D_Color;//�߶���ɫ

        Line3D_GameObject.YZT_OriginalScale = new Vector3(YZT_OriginalScale_Start.x * Line3D_CX, YZT_OriginalScale_Start.y, YZT_OriginalScale_Start.z * Line3D_CX);//����Բ���������
        Line3D_GameObject.XQ_OriginalScale = new Vector3(XQ_OriginalScale_Start.x * Line3D_CX, XQ_OriginalScale_Start.y * Line3D_CX, XQ_OriginalScale_Start.z * Line3D_CX);//����С�������
        Line3D_GameObject.BHG_OriginalScale = new Vector3(BHG_OriginalScale_Start.x * Line3D_CX, BHG_OriginalScale_Start.y * Line3D_CX, BHG_OriginalScale_Start.z * Line3D_CX);//���±�Źܵ�����

        BHG_ID = _BHG_ID; //���±�Źܵ�ID
    }



    /// <summary>
    /// ����һ���������� �ж����������������Ҳ����
    /// ʹ�ø����ķ���ĵ��춨λ��λ
    /// 2024.07.25 �������ҷ����λ�����ж� �����߸�����һ��
    /// </summary>
    /// <param name="DZ_GameObject"></param>
    private void Judge_Distance(GameObject DZ_GameObject)
    {
        float distance_R = Vector3.Distance(DZ_GameObject.transform.position, GuideRail_WZ_List_R[0].transform.position);//��������Ҳ��һ����λ�ľ���
        float distance_L = Vector3.Distance(DZ_GameObject.transform.position, GuideRail_WZ_List_L[0].transform.position);//������������һ����λ�ľ���

        //�ж�ʹ����һ�ߵĵ�λ ˭����˭��
        if (distance_R > distance_L)
        {
            GuideRail_WZ_List = GuideRail_WZ_List_L;//ʹ������λ
        }
        else
        {
            GuideRail_WZ_List = GuideRail_WZ_List_R;//ʹ���Ҳ��λ
        }
    }


    /// <summary>
    /// ������������������Ĵ������߶�,�����ظ��ϲ�ʹ�ã�GameObject��
    /// �����õ��������������壬so �Ϳ����õ�������Ϣ �����ݶ�����Ϣ�����Դ������߶Σ�����λ����ţ�
    /// 2024.07.23 �ع����ش����õ�Line3D
    /// </summary>
    /// <returns></returns>
    public GameObject Establish_Line3D(GameObject DZ_Object1, GameObject DZ_Object2)
    {
        Judge_Distance(DZ_Object1);//���ݵ�һ������λ�ü�����·����߻����ұ�

        //GameObject Line3D = null;
        GameObject XD_Object = Instantiate(Line3D_XD, new Vector3(0, 0, 0), Quaternion.identity);//�������߸�����
        XD_Object.SetActive(true);//�Ѷ�����ʾ���� ����

        //XD_Object.transform.localScale = new Vector3(Line3D_localScale, Line3D_localScale, Line3D_localScale);//���������С

        int[] LS_GuideRail_ID = new int[2];//��������λ��������� ���ڽ���dZ���ӵĵ���λ�����
        //���ݶ������壬��ȡ����λ�����
        LS_GuideRail_ID[0] = DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID;//����1 �������
        LS_GuideRail_ID[1] = DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID;//����2 �������

        //��¼�����϶��ӻ������¶���;Ĭ��0����,1����;���»��Զ��ڵ���id��1  UpDown��Ӱ�����ӷ�ʽ
        if (DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().UpDown == 0) { LS_GuideRail_ID[0]++; }
        if (DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().UpDown == 0) { LS_GuideRail_ID[1]++; }

        MeshRenderer[] DZ_MeshRenderer = new MeshRenderer[2];//����MeshRenderer���� ���ڽ���dZ���ӵ�MeshRenderer
                                                             //���ݶ������壬��ȡMeshRenderer
                                                             // DZ_MeshRenderer[0] = DZ_Object1.GetComponent<MeshRenderer>();//����1 ����������Ⱦ��
                                                             // DZ_MeshRenderer[1] = DZ_Object2.GetComponent<MeshRenderer>();//����2 ����������Ⱦ��

        //2024.07.23 16��30 ���Ľ����������ϲ�ȥ
        //������Ϣ������������ ��֤�������ֵ�Ψһ��  
        //���� ����1�������������֣��ײ���������ַ������Ͷ���ID�������������+����2�������������֣��ײ���������ַ������Ͷ���ID�������������
        // XD_Object.transform.name = DZ_Object1.name + DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID + "_"
        // + DZ_Object2.name + DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().GuideRail_ID + "_wire";

        //��ǰ��ͬһ�������£��򴴽����������߶�
        if (LS_GuideRail_ID[0] == LS_GuideRail_ID[1])
        {
            Line_Object3D_Case2(DZ_Object1, DZ_Object2, GuideRail_WZ_List[LS_GuideRail_ID[0]].transform.position, XD_Object);
            Debug.Log("��ǰ��ͬһ��������");
        }
        //��ǰΪ����ͬһ�������µ����� 
        else
        {
            Line_Object3D_Case1(DZ_Object1, GuideRail_WZ_List[LS_GuideRail_ID[0]].transform.position, GuideRail_WZ_List[LS_GuideRail_ID[1]].transform.position, DZ_Object2, XD_Object);
            Debug.Log("����ͬһ��������");
        }

        #region �������� �����Ƿ���Ҫ��������?

        #endregion

        //��ȡ�����������Ϣ���в���

        XD_Object.transform.parent = Line3D_XD_Main.transform;//�ϸ�����
        return XD_Object;
    }
    #region 3D���ߵײ����
    // 2024.07.23 �ع��ײ����



    /// <summary>
    /// 3D���� ���1����ǰ���Ӳ���ͬһ��������
    /// ���ݴ�����Ϣ����3D���� �����ڲ����м�������
    /// ������㡢�յ㡢������λ����·��ɫ ���������ĸ���������
    /// �����޸����ڣ�2024.07.23 11��30
    /// </summary>
    /// <param name="DZ_Object1"></param> //����1
    /// <param name="DZ_Object2"></param> //����2
    /// <param name="PT1"></param> ;����1
    /// <param name="PT2"></param> ;����2
    /// <param name="Fqy_Object"></param> ���鸸����
    private void Line_Object3D_Case1(GameObject DZ_Object1, Vector3 PT1, Vector3 PT2, GameObject DZ_Object2, GameObject Fqy_Object)
    {
        //�����Ź�λ��
        Vector3 BHG_Object3D_1;
        Vector3 BHG_Object3D_2;

        //��ʱ���յ�ǰ�������ߵķ���
        int[] UpDowns = new int[2];
        //���ݶ������壬��ȡ�������ߵķ���
        UpDowns[0] = DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//��ȡ�ö���1 ���ߵķ���
        UpDowns[1] = DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//��ȡ�ö���2 ���ߵķ���


        if (UpDowns[0] == 0)//��
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y - Line3D_BHG_Position, DZ_Object1.transform.position.z);
        }
        else//��
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y + Line3D_BHG_Position, DZ_Object1.transform.position.z);
        }

        if (UpDowns[1] == 0)
        {
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y - Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }
        else
        {
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y + Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }

        //���ɱ�Ź�1
        // GameObject ball1 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_1, Quaternion.identity, Fqy_Object.transform);
        //Grade_Toggle_position(ball1, );//�޸ı�Ź�λ�� �ͱ�Źܵı�ţ�Text��
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_1, (BHG_ID + 0) + "");

        //���ɱ�Ź�2
        // GameObject ball2 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_2, Quaternion.identity, Fqy_Object.transform);
        // Grade_Toggle_position(ball2, "2");//�޸ı�Ź�λ�� �ͱ�Źܵı�ţ�Text��
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_2, (BHG_ID + 1) + "");



        float zz = DZ_Object1.transform.position.z;   //��¼�߶�
        Vector3 x1 = new Vector3(DZ_Object1.transform.position.x, PT1.y, zz);//��¼�����λ

        //������·����
        Line3D_Establish3Dline(DZ_Object1.transform.position, x1, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x1, PT1, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(PT1, PT2, LS_Color, Fqy_Object);

        Vector3 x2 = new Vector3(DZ_Object2.transform.position.x, PT2.y, zz);//��¼�����λ

        //��������
        Line3D_Establish3Dline(PT2, x2, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x2, DZ_Object2.transform.position, LS_Color, Fqy_Object);
    }

    /// <summary>
    /// 3D���� ���2����ǰ������ͬһ��������
    /// ���ݴ�����Ϣ����3D���� �����ڲ����м�������
    /// ������㡢�յ㡢������λ����·��ɫ ���������ĸ���������
    /// �����޸����ڣ�2024.07.23 14��30
    /// </summary>
    /// <param name="DZ_Object1"></param> //����1
    /// <param name="DZ_Object2"></param> //����2
    /// <param name="PT"></param> //;����
    /// <param name="Fqy_Object"></param> //���鸸����
    private void Line_Object3D_Case2(GameObject DZ_Object1, GameObject DZ_Object2, Vector3 PT, GameObject Fqy_Object)
    {
        //�����Ź�λ��
        Vector3 BHG_Object3D_1;
        Vector3 BHG_Object3D_2;

        //��ʱ���յ�ǰ�������ߵķ���
        int[] UpDowns = new int[2];
        //���ݶ������壬��ȡ�������ߵķ���
        UpDowns[0] = DZ_Object1.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//��ȡ�ö���1 ���ߵķ���
        UpDowns[1] = DZ_Object2.GetComponent<Establish3Dline_Terminal_Click>().UpDown;//��ȡ�ö���2 ���ߵķ���


        if (UpDowns[0] == 0)
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y - Line3D_BHG_Position, DZ_Object1.transform.position.z);
            // BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y - Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }
        else
        {
            BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y + Line3D_BHG_Position, DZ_Object1.transform.position.z);
            //  BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y + Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }

        if (UpDowns[1] == 0)
        {
            // BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y - Line3D_BHG_Position, DZ_Object1.transform.position.z);
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y - Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }
        else
        {
            // BHG_Object3D_1 = new Vector3(DZ_Object1.transform.position.x, DZ_Object1.transform.position.y + Line3D_BHG_Position, DZ_Object1.transform.position.z);
            BHG_Object3D_2 = new Vector3(DZ_Object2.transform.position.x, DZ_Object2.transform.position.y + Line3D_BHG_Position, DZ_Object2.transform.position.z);
        }

        //�����޸ı�Ź�1
        //GameObject ball1 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_1, Quaternion.identity, Fqy_Object.transform);
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_1, (BHG_ID + 0) + "");

        //�����޸ı�Ź�2
        //GameObject ball2 = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_2, Quaternion.identity, Fqy_Object.transform);
        Grade_Toggle_position(Fqy_Object, BHG_Object3D_2, (BHG_ID + 1) + "");



        float zz = DZ_Object1.transform.position.z;        //��¼�߶�
        Vector3 x1 = new Vector3(DZ_Object1.transform.position.x, PT.y, zz);//��¼����
        Vector3 x2 = new Vector3(DZ_Object2.transform.position.x, PT.y, zz);//��¼����

        //������·����
        Line3D_Establish3Dline(DZ_Object1.transform.position, x1, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x1, x2, LS_Color, Fqy_Object);
        Line3D_Establish3Dline(x2, DZ_Object2.transform.position, LS_Color, Fqy_Object);
    }



    ///�����ǵײ�ĵײ���   



    /// <summary>
    /// ���ݴ�����Ϣ����3D���� �����ڲ����м�������
    /// ������㡢�յ㡢��·��ɫ ���������ĸ���������
    /// �����޸����ڣ�2024.07.23
    /// </summary>
    /// <param name="initialPosition"></param>
    /// <param name="currentPosition"></param>
    /// <param name="currentColor"></param>
    /// <param name="Fqy_Object"></param>
    private void Line3D_Establish3Dline(Vector3 initialPosition, Vector3 currentPosition, Color currentColor, GameObject Fqy_Object)
    {
        //currentLine = null;


        // ����С������λ��
        GameObject ball = Instantiate(Line3D_GameObject.ballPrefab, initialPosition, Quaternion.identity, Fqy_Object.transform);
        //�л����� �����߶ε���ɫ
        ball.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
        //�޸�С���С
        ball.transform.localScale = new Vector3(Line3D_GameObject.XQ_OriginalScale.x, Line3D_GameObject.XQ_OriginalScale.y, Line3D_GameObject.XQ_OriginalScale.z);


        // �����߶β����ó�ʼλ��
        GameObject currentLine = Instantiate(Line3D_GameObject.linePrefab, initialPosition, Quaternion.identity, Fqy_Object.transform);
        // �����߶ε���ɫ
        currentLine.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
        // �����������Ȳ������߶ε����ź�λ��
        float distance = Vector3.Distance(initialPosition, currentPosition);
        //�ߴ�С
        currentLine.transform.localScale = new Vector3(Line3D_GameObject.YZT_OriginalScale.x, distance / 2f * Line3D_GameObject.YZT_OriginalScale.y, Line3D_GameObject.YZT_OriginalScale.z);//�޸��߶γ���
                                                                                                                                                                                            //���¼���λ��
        currentLine.transform.position = (initialPosition + currentPosition) / 2f;

        // ������ת����
        Vector3 direction = currentPosition - initialPosition;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
        currentLine.transform.rotation = rotation;
    }


    /// <summary>
    /// �л���Ź�λ�ã��Լ���Źܵı�ţ�Text��
    /// ��ֵ �ı��Ź�λ��
    /// �����޸����ڣ�2024.07.23
    /// </summary>
    private void Grade_Toggle_position(GameObject Fqy_Object, Vector3 BHG_Object3D_Vector3, string BHG_Str)
    {
        GameObject BHG = Instantiate(Line3D_GameObject.GameObject_BHG, BHG_Object3D_Vector3, Quaternion.identity, Fqy_Object.transform);//���ɱ�Ź�
        BHG.transform.localScale = Line3D_GameObject.BHG_OriginalScale;//�޸ĳ���
        BHG.GetComponent<Establish3Dline_Tag_Alter>().ste_Text(BHG_Str + "");//�޸ı�Ź��ı�
    }
    #endregion
}
