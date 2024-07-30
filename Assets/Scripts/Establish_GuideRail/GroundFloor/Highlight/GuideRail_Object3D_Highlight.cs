using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Highlight : MonoBehaviour
{
    /// <summary>
    ///����ָ������Ŀո��� ���������ı����Ϊ͸��
    ///��¼�ռ����� 
    ///��¼��ǰ����������������
    /// </summary>

    ///����ָ���������� ����
    ///ͳ�� �õ�����������
    ///����ʣ��������

    [Header("����")]
    public GuideRail_Data GuideRail_Data;
    [Header("������")]
    public List<GameObject> List_Object3D;
    [Header("���ո�������")]
    public GameObject[] Object_FB;

    [Header("��ʼλ��")]
    public GameObject[] Object_KS;

    [Header("���ֵ BoxCollider")]
    public GameObject[] Object_Max;

    [Header("������� һ���м������� �Ҽ���·���µĿ�����")]//����
    public GameObject[] GuideRail_Object;

    public Vector3 _Position;
    public void Start()
    {
        List_Object3D = new List<GameObject>();//ʵ����

        GuideRail_Data.GuideRail_Object3D = new GuideRail_Object3D[Object_KS.Length];// ��������
        for (int i = 0; i < Object_KS.Length; i++)
        {
            GuideRail_Data.GuideRail_Object3D[i] = new GuideRail_Object3D();//ʵ����
        }
        //    Test(Object_FB[1]);
        //   Row_generate(Object_1, Object_3, Object_4);
    }


    //����2
    public void Outline_TJ(int ID)
    {
        // Generate_Ouline(Object_FB[ID]);//�����������
    }

    /// <summary>
    /// ����
    /// </summary>
    public void GuideRail_Object3D_Highlight_Reset()
    {
        //����֮ǰ��
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            //List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//������Ϊ������
            Destroy(List_Object3D[i]);//����
        }
        //   List_Object3D = new List<GameObject>();//ʵ����
        List_Object3D.Clear();//���

    }

    /// <summary>
    /// �ϲ�ֱ������ �ո���
    /// </summary>
    /// <param name="Object_Test"></param>
    public void Generate_Ouline(GameObject Object_Test)
    {
        //����֮ǰ��
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            //List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//������Ϊ������
            Destroy(List_Object3D[i]);//����
        }
        //   List_Object3D = new List<GameObject>();//ʵ����
        List_Object3D.Clear();//���
        for (int i = 0; i < GuideRail_Object.Length; i++)
        {
            Row_generate(Object_Test, Object_KS[i], Object_Max[i], GuideRail_Object[i], i);
        }

        //    List_Object3D.Clear();// = new List<GameObject>();//ʵ����
        StartCoroutine(DelayedOperation_Destroy(2));//��ʱ2s����
    }

    /// <summary>
    /// ���ݴ����������� ���д���  ���Կ����������ϴ������ٸ� �������Է��ö�������
    /// </summary>
    /// <param name="Object_ID"></param>
    public void Generate_Ouline_One(int Object_ID)
    {
        //û���ƶ�����ֱ�ӷ���
        if (GuideRail_Bus.FollowMovement_Bool == false) { return; }

        // ���֮ǰ��
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            //List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//������Ϊ������
            Destroy(List_Object3D[i]);//����
        }
        //   List_Object3D = new List<GameObject>();//ʵ����
        List_Object3D.Clear();//���

        //Debug.Log("��ǰ�����ǣ�" + Object_ID);//����õ���һ�����������˶��ٸ��������� 
        int Fqy = Row_generate(Object_FB[GuideRail_Bus.Type_ID], Object_KS[Object_ID], Object_Max[Object_ID], GuideRail_Object[Object_ID], Object_ID);

        GuideRail_Data.GuideRail_Object3D[Object_ID].Quantity = Fqy - List_Object3D.Count;//�õ����������ϵ�����Ԫ������
        Debug.Log("��ǰ����" + Object_ID + ", ��������Ϊ��" + GuideRail_Data.GuideRail_Object3D[Object_ID].Quantity);
        StartCoroutine(DelayedOperation_Destroy(0.1f));//��ʱ2s����
    }

    /// <summary>
    /// ��ʱ����
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayedOperation_Destroy(float Time_destroy)
    {
        // Debug.Log("��ʼ��ʱ����");
        yield return new WaitForSeconds(Time_destroy);
        Debug.Log("��ʱ���ٲ������");
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            List_Object3D[i].GetComponent<BoxCollider>().isTrigger = false;//������Ϊ������

            Destroy(List_Object3D[i].GetComponent<GuideRail_Object3D_Highlight>());//���ٽű�
        }
    }


    /// <summary>
    ///  ���ᣬ��һ������ �������ꣿ
    /// ���긴�ӻ���ʹ���������+�������ꣿ��
    /// <summary>
    /// <param name="Object3D_FB"></param> //����Դ����
    /// <param name="Object3D"></param>//��ʼλ��
    /// <param name="Initial_Position"></param>//���λ��
    /// <param name="Parent_Object"></param>//����
    public int Row_generate(GameObject Object3D_FB, GameObject Object3D, GameObject Initial_Position, GameObject Parent_Object, int DG_ID = 0)
    {
        // Debug.Log("��ʼ��������Ϊ��" + Object3D.name);
        // Debug.Log("��ʼλ�ã�" + Object3D.transform.localPosition);

        Vector3 Fqy = Object3D_FB.GetComponent<BoxCollider>().size;//�õ���С
        Vector3 Qy = Object3D.transform.localPosition;//�õ���ʼλ��
        float Love = Fqy.x + 0.2f;//�õ�ÿһ���ľ���

        int like = 0;//����
        float F_Value = 0;//ֵ

        while (true)
        {
            GameObject Object_test = Instantiate(Object3D_FB);//����һ������
            Object_test.transform.parent = Parent_Object.transform;//�� Object_1 ��������
            Object_test.name = DG_ID + "_" + Object3D.name + "_" + like;//�������� ����ID
            List_Object3D.Add(Object_test);//���� ��ȥ

            Object_test.SetActive(true);//��������
                                        // List_Object3D[like] = Object_test;//��¼����

            Object_test.gameObject.transform.localPosition = Qy;//�ı�λ��

            Qy.x += Love;
            like++;
            F_Value = Love * like;
            //  Debug.Log("��ǰ�仯ֵΪ��" + like);
            if (F_Value > Initial_Position.GetComponent<BoxCollider>().size.x)
            {

                //  Debug.Log("��ǰ�ﵽ���ֵ");
                return like;
            }
        }
        // return like;


    }

    /// <summary>
    /// ɾ��ָ�����ֵ�
    /// </summary>
    public void Delete_Name(string Fqy)
    {
        int Like = 0;
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            if (List_Object3D[i].name == Fqy)//�ҵ�����
            {
                // �ҵ�Ҫɾ�������壬ִ�����ٲ���
                Destroy(List_Object3D[i]);
                // DestroyImmediate(List_Object3D[i]);//����ɾ��
                List_Object3D.RemoveAt(i); // ���б����Ƴ�����
                Debug.Log("ɾ���ɹ� :" + Fqy);
                break; // �ҵ���ɾ�������壬����Ҫ����ѭ��
            }
            else
            {
                Like++;
            }
        }

        if (Like == List_Object3D.Count)
        {
            Debug.Log("���ִ���δ����ȷ�ҵ�������");
        }
    }

    /// <summary>
    /// ����ָ������
    /// </summary>
    public int Inquire_Name(string Fqy)
    {
        int Love = 0;
        int Like = 0;
        for (int i = 0; i < List_Object3D.Count; i++)
        {
            if (List_Object3D[i].name == Fqy)//�ҵ�����
            {
                Love = i;
                Debug.Log("���б��У��ҵ��������λ��");
            }
            else
            {
                Like++;
            }
        }

        if (Like == List_Object3D.Count)
        {
            Debug.Log("��ǰ�������λ�ò���ȷ, ��������Ϊ��" + Fqy);
            Love = -1;
            //    return Love;
        }
        return Love;
    }
}
