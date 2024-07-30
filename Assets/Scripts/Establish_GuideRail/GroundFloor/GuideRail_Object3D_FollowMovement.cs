using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuideRail_Object3D_FollowMovement : MonoBehaviour
{
    //3D ��������ƶ� 
    //����ʲô��������ָ��λ�÷���  ��¼��λ�õ���Ϣ���������͸��״̬��������>��״̬����������ƶ� ����Ƴ���������ɾ����
    //����ָ������ ����������ڸ�λ��  ���Ƴ�Ҳ����ɾ�������ˣ� ����ָ�������ýű����ã��ű�����
    //����֮�����ȫ���ر������������壨�ո�����ȫ����ʧ ��������>��ʱû�д���

    //ɾ������+ɾ������ ����

    [Header("��ȡ���ݿ�")]
    public GuideRail_Data GuideRail_Data;
    [Header("������")]
    public GuideRail_Interaction GuideRail_Interaction;
    // [Header("���ʱ仯")]
    // public GuideRail_Object3D_Texture GuideRail_Object3D_Texture;
    // [Header("����")]
    // public GuideRail_Object3D_Transcript GuideRail_Object3D_Transcript;


    [Header("��ǰ������ Generate_Object3D λ��")]
    public int Generate_Object3D_ID;//ʵʱ�仯��
    [Header("��ȡ��������")]
    public string Object_Name;//ʵʱ�仯��
    [Header("�Ƿ���Խ���ɾ��")]
    public bool Delete_Bool = false; // �Ƿ�������ק


    //�����ƶ� ����
    [Header("�������������")]
    public float distance = 0.5f;                 //����

    //[Header("�Ƿ�������һ���ƶ�")]
    // public bool GuideRail_Bus.FollowMovement_Bool = false;        // �Ƿ�������ק
    // [Header("�� �ڿ���������")]
    //   public GameObject GuideRail_Data.Main_Camera;
    //  [Header("�õ������ �Ƕ�  ����������ת�Ƕ���ͬ")]
    private Transform Camera_Rotate;//"�õ������ �Ƕ�  ����������ת�Ƕ���ͬ
    //  [Header("��������¸���")]
    private Camera Camera_Main;//��������¸���"
    //[Header("��������ƶ�����")]
    private GameObject Follow;//���Ƶ�����



    // private float Records = 5f; //���巵��ԭλ�õķ�Χ Ĭ��Ϊ5
    private Vector3 originalPosition;       // ����ĳ�ʼλ��

    public void Awake()
    {
        if (GuideRail_Data == null)
        {
            GuideRail_Data = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Data>();
        }
        if (GuideRail_Interaction == null)
        {
            GuideRail_Interaction = GameObject.FindWithTag("Establish_GuideRail").GetComponent<GuideRail_Interaction>();
        }
        // if (GuideRail_Object3D_Texture == null)
        // {
        //     GuideRail_Object3D_Texture = GameObject.FindWithTag("GuideRail_GroundFloor").GetComponent<GuideRail_Object3D_Texture>();
        // }




        Camera_Rotate = GuideRail_Data.Main_Camera.gameObject.transform;//�õ�ֵ
        Camera_Main = GuideRail_Data.Main_Camera.GetComponent<Camera>();//�õ�ֵ
    }


    /// <summary>
    /// �ű���һ�μ�������
    /// </summary>
    public void Start()
    {



        Object_Name = this.gameObject.name;//��ȡ��ǰ��������

        Follow = this.gameObject;//��ȡ��������
        originalPosition = Follow.transform.position;//��¼��ʼλ��
                                                     // lastMousePosition = Follow.transform.position;//��¼��һ֡λ��

        FollowMovement_Object_True();//�����������ƶ�
        Delete_Object_False();//�����Խ���ɾ��

    }

    /// <summary>
    /// ʵʱ���м���
    /// </summary>
    private void Update()
    {
        FollowMovement_Object();//�����ƶ�
        Judge_Name();//���߼�� �����ж�

        if (Input.GetKeyDown(KeyCode.Z))//ֹͣ�����ƶ� ��ʱ����
        {
            Move_TZ();
            //    Debug.Log("Start");
        }
        if (Input.GetKeyDown(KeyCode.X))//����Ϊ����ѡ��λ��
        {
            Move_Delete();
            //   Debug.Log("Start");
        }
    }

    /// <summary>
    /// ��ʱֹͣ�����ƶ� ��ʱ����
    /// </summary>
    public void Move_TZ()
    {
        FollowMovement_Object_False();//��ֹ����
        Delete_Object_True();//���Խ���ɾ��

        //2024.07.21 ���Ľ���д�뵽�м���ɲ�
        // GuideRail_Object3D_Texture.Establish_Textur(this.gameObject, true);//����Ϊ��͸��״̬ �����²�����ʽ
        GuideRail_Interaction.Toggle_Texture(this.gameObject, false);//����Ϊ��͸��״̬ �����²�����ʽ

        //����ǰ���� �õ�����Data
        GuideRail_Data.Establish_Object3D_Name(Generate_Object3D_ID, Object_Name, this.gameObject);

    }

    /// <summary>
    /// ȷ�����ø�λ�� ����������ɾ��
    /// </summary>
    public void Move_Delete()
    {
        this.gameObject.AddComponent<Rigidbody>();//��Ӹ�������
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;//���ø�������
        this.gameObject.GetComponent<BoxCollider>().enabled = true;//������ײ������

        //2024.07.21 ���Ľ���д�뵽�м���ɲ�
        //GuideRail_Object3D_Texture.Replace_Texture(this.gameObject);//����Ϊ��͸��״̬ ��ԭ�л����ϸı�
        //  GuideRail_Object3D_Texture.Replace_Texture(this.gameObject);//����Ϊ��͸��״̬ ��ԭ�л����ϸı�
        GuideRail_Interaction.Toggle_Texture(this.gameObject, true);//����Ϊ��͸��״̬ �����²�����ʽ

        FollowMovement_Object_False();//��ֹ����
        Delete_Object_False();//�����Խ���ɾ��

        GuideRail_Interaction.Close_Object3D();//�رո���
        this.GetComponent<Entity_Object3D_Click>().enabled = true;
        Destroy(this);//�Ƴ��ű�����

    }

    /// <summary>
    /// ���3D ����
    /// </summary>
    // public void OnMouseDown()
    // {
    //     //  Debug.Log("OnMouseDown");
    // }

    /// <summary>
    /// �����������������
    /// </summary>
    // void OnMouseOver()
    // {
    //     //Debug.Log("OnMouseOver");

    // }

    /// <summary>
    /// ������ƿ�ʱ
    /// </summary>
    void OnMouseExit()
    {
        // Debug.Log("OnMouseExit");
        Delete_Object();//�Ƴ�ɾ��
                        // Debug.Log("OnMouseExit");
    }


    #region ��������ƶ�

    /// <summary>
    /// �����������ƶ�
    /// </summary>
    public void FollowMovement_Object()
    {
        if (GuideRail_Bus.FollowMovement_Bool == true)//Ϊtrue �Ÿ��������ƶ�
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;//������ײ������

            Vector3 m_MousePos = new Vector3(Input.mousePosition.x + 50f, Input.mousePosition.y + 50f, distance);
            transform.position = Camera_Main.ScreenToWorldPoint(m_MousePos);

            Follow.transform.rotation = Camera_Rotate.rotation;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;//������ײ������
        }
    }

    /// <summary>
    /// ���� ���Խ��и��������ƶ�
    /// </summary>
    public void FollowMovement_Object_True()
    {
        GuideRail_Bus.FollowMovement_Bool = true;
    }

    /// <summary>
    /// ֹͣ ���岻�ڸ��������ƶ�
    /// </summary>
    public void FollowMovement_Object_False()
    {
        GuideRail_Bus.FollowMovement_Bool = false;
    }

    #endregion


    #region �Ƴ�������������ɾ�� ������

    /// <summary>
    ///ɾ�������Լ�ɾ����¼����
    /// </summary>
    public void Delete_Object()
    {
        if (Delete_Bool == true)
        {
            Debug.Log("��ǰ����Ķ������֣�" + this.gameObject.name);
            GuideRail_Data.Delete_Object_Name(Generate_Object3D_ID, Object_Name);
        }
    }

    /// <summary>
    /// ���Խ���ɾ��
    /// </summary>
    public void Delete_Object_True()
    {
        Delete_Bool = true;
    }

    /// <summary>
    /// �����Խ���ɾ��
    /// </summary>
    public void Delete_Object_False()
    {
        Delete_Bool = false;
    }
    #endregion

    #region ���߼����� ��������� �ǲ��Ǹ���������

    [Header("��¼����λ�õ�")]
    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;


    /// <summary>
    /// �����嶨λ��ȥ�������ƶ�������Ŀո���λ��
    /// 2024.06.21
    /// </summary>
    public void Judge_Name()
    {
        //�ж�
        if (GuideRail_Bus.FollowMovement_Bool == false)//ֻ�� GuideRail_Bus.FollowMovement_BoolΪtrue��ʱ�����ſ��Խ���
        { return; }

        string Fqy = Ray_Detection(Camera_Main);//�õ�ֵ���߷���ֵ
                                                //�ж�
        if (Fqy == null)//Ϊ��ֱ�ӷ��� �ж�
        { return; }

        //�������ֲ���λ�� ID 
        int Love = GuideRail_Object3D_Highlight.Inquire_Name(Fqy);//�õ�����ID 
        if (Love < 0)//С��0 δ�ҵ�ֱ�ӷ���
        { Debug.Log("��ǰ���λ�ò���ȷ"); return; }

        //�õ�λ���ƶ���ȥ
        this.gameObject.transform.position = GuideRail_Object3D_Highlight.List_Object3D[Love].gameObject.transform.position;
        Move_TZ();//��ʱ����

        //�õ������� 
        string qy = Fqy[0].ToString();//��ת��Ϊ�ַ����ٴ���
        //this.GetComponent<Entity_Object3D_Click>().DG_ID = int.Parse(qy);
        this.GetComponent<Object3D_Informatization>().GuideRail_ID = int.Parse(qy);
        Debug.Log("��ǰ�ǵڣ�" + Fqy[0] + "����");

        GuideRail_Interaction.Key_Trigger(false);//�������򿪣��Ƴ���ʧ
        GuideRail_Interaction.Close_Object3D();//��ʱ�رո���
    }


    /// <summary>
    /// ���߼�� ���ص���������������
    /// </summary>
    /// <param name="GuideRail_Data.Main_Camera"></param>
    public string Ray_Detection(Camera Main_Camera)
    {
        string Fqy = null;
        if (Input.GetMouseButtonDown(0))
        {
            GuideRail_Interaction.Key_Trigger(false);
        }
        else
        {
            GuideRail_Interaction.Key_Trigger(true);
        }


        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            // ����һ�����ߴ��������λ���������λ��
            Ray ray = Main_Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ������߻���������
            if (Physics.Raycast(ray, out hit))
            {
                // ��ȡ���е�����
                GameObject hitObject = hit.collider.gameObject;
                // ����������ֵ�����̨
                //     Debug.Log("Clicked object name: " + hitObject.name);
                Fqy = hitObject.name;
                return Fqy;
            }
        }

        return Fqy;
    }
    #endregion
}
