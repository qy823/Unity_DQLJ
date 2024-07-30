using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System.IO;
//using UnityEngine.UIElements;
//using UnityEditor;

public class GuideRail_Object3D_Texture : MonoBehaviour
{
    /// <summary>
    /// �ı䵼����������� ��ȷ��״̬��ȷ��״̬��͸��״̬�Ͱ�͸��״̬��
    /// ȷ��״̬�Ͱ�ȷ��״̬
    /// </summary>

    // Start is called before the first frame update

    [Header("�ҵ����е�������")]
    public List<GameObject> Son_Object = new List<GameObject>();//����������б�

    //��ԭ�л����Ͻ��иı� ����1
    [Header("��ɫ")]
    public Color Object_color;
    [Header("������")]
    public Material Object_Material;


    //���������� ����2
    [Header("����������")]
    public Material Object_Material_Test;
    [Header("·��")]
    public string Material_Path = "Assets/Model/NewlyIncreased/";
    [Header("������")]
    public string Material_Type = ".mat";


    public void Start()
    {

#if UNITY_EDITOR
        ClearDirectory_File("Assets/Model/NewlyIncreased");
        Material_Path += "/";
        Directory.CreateDirectory(Material_Path);//����
        //ˢ��
        AssetDatabase.Refresh();
        //  ClearDirectory_File(Material_Path);
        // Directory.Delete(Material_Path, true); //�ڶ�����������������ݲ�Ϊ���Ƿ�ҲҪɾ���������Ͳ��ᱨ����
#endif
    }

    /// <summary>
    /// �������ݣ�
    /// Son_Object 
    /// </summary>
    public void Reset()
    {

    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    public void Gain_Texture(GameObject Fqy)
    {
        // List<GameObject> CH = new List<GameObject>();//����������б�

        //����forѭ�� ��ȡ�����µ�ȫ��������
        for (int c = 0; c < Fqy.transform.childCount; c++)
        {
            //����������»��������� �ͽ������崫����лص����� ֱ������û��������Ϊֹ
            if (Fqy.transform.GetChild(c).childCount > 0)
            {
                Gain_Texture(Fqy.transform.GetChild(c).gameObject);

            }
            Son_Object.Add(Fqy.transform.GetChild(c).gameObject);
        }
    }

    /// <summary>
    /// ����1 ��ԭ�в���������Ͻ��иı� ���һ��������Ҫ���ö�ݵĻ� ����һ����Bug
    /// ���ʸ���Ϊ��͸������ / ��͸������֮���л�
    /// </summary>
    public void Replace_Texture(GameObject Object_Test, bool Fqy = false)
    {
        Son_Object.Clear();//����б�
        Gain_Texture(Object_Test);//���������¼���е�������

        float Object_color_A = 1;//Ĭ��״̬ ��͸��
        if (Fqy)//ΪTrue �ı�͸���Ȱ�͸��
        {
            Object_color_A = 0.5f;
        }


        // �����Ϸ�������Ƿ��в������ ������
        if (Object_Test.GetComponent<Renderer>() != null && Object_Test.GetComponent<Renderer>().material != null)
        {
            //�õ�������
            Object_Material = Object_Test.GetComponent<Renderer>().material;
            //�õ���������ɫ
            Object_color = Object_Test.GetComponent<MeshRenderer>().material.color;
            // ���ݵ�ǰ��ɫ�������µ���ɫ������͸����
            Object_color.a = Object_color_A;
            //  Debug.Log("�Ѿ��ɹ��ı���ɫ");

            //�ı�ԭ������ɫ
            Object_Test.GetComponent<MeshRenderer>().material.color = Object_color;
        }

        try
        {
            for (int i = 0; i < Son_Object.Count; i++)
            {
                // Debug.Log(i);
                if (Son_Object[i].GetComponent<Renderer>() != null && Son_Object[i].GetComponent<Renderer>().material != null)
                {
                    // �õ�������Ĳ�����
                    Object_Material = Son_Object[i].GetComponent<Renderer>().material;
                    // �õ�������Ĳ�������ɫ
                    Object_color = Son_Object[i].GetComponent<MeshRenderer>().material.color;
                    // ���ݵ�ǰ��ɫ�������µ���ɫ������͸����
                    Object_color.a = Object_color_A;
                    // �ı����������ɫ
                    Son_Object[i].GetComponent<MeshRenderer>().material.color = Object_color;
                }
            }
        }
        catch
        {

            Debug.Log(Object_Test.name + "�쳣����");
            //    throw;
        }
    }


    /// <summary>
    /// ����2 ��ԭ�в���������Ͻ��и��� �����µĲ����� ���ı���ɫ�Ͳ���Ӱ�������Ĳ�����
    /// ���ʸ���Ϊ��͸������ / ��͸������֮���л�
    /// ���滹��Ҫ�Ż������������ �Ƿ���Ҫɾ�� ����ɾ��·���µģ���
    /// �ظ���ʼ��͸��״̬�� ֱ���ҵ�֮ǰ�Ĳ����򣿣�
    /// </summary>
    public void Establish_Textur(GameObject Object_Test, bool Fqy = false)
    {
        return;//2024.5.13 ��ʱ�ر�

        Son_Object.Clear();//����б�
        Gain_Texture(Object_Test);//���������¼���е�������

        string Material_Name = null;
        float Object_color_A = 1;//Ĭ��״̬ ��͸��
        if (Fqy)//ΪTrue �ı�͸���Ȱ�͸��
        {
            Object_color_A = 0.5f;
        }
        //ʵ��������һ��������
        Object_Material_Test = new Material(Shader.Find("Standard"));

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("��ǰΪWebGL IF�ж�");
            try
            {
                if (Object_Test.GetComponent<Renderer>() != null && Object_Test.GetComponent<Renderer>().material != null)
                {
                    //��������
                    Material_Name = Material_Path + Object_Test.gameObject.name + Material_Type;
                    //�õ������� Դ����
                    Object_Material_Test = Object_Test.GetComponent<Renderer>().material;

                    //�滻Ϊ�µĲ����� ����Ӧ�û���Bug ����ģ�ͻ���ض�������� 2024.4.28 
                    //���Լ�� �����material�鳤������ ��ʱ������
                    Object_Test.GetComponent<MeshRenderer>().material = Object_Material_Test;// AssetDatabase.LoadAssetAtPath<Material>(Material_Name)

                    // �õ�������Ĳ�������ɫ
                    Object_color = Object_Test.GetComponent<MeshRenderer>().material.color;
                    // ���ݵ�ǰ��ɫ�������µ���ɫ������͸����
                    Object_color.a = Object_color_A;
                    // �ı����������ɫ
                    Object_Test.GetComponent<MeshRenderer>().material.color = Object_color;
                }

                ///���� �����������嶼�õ�
                for (int i = 0; i < Son_Object.Count; i++)
                {
                    // Debug.Log(i);
                    if (Son_Object[i].GetComponent<Renderer>() != null && Son_Object[i].GetComponent<Renderer>().material != null)
                    {
                        //�õ������� ������ Դ����
                        Object_Material_Test = Son_Object[i].GetComponent<Renderer>().material;

                        //�滻Ϊ�µĲ����� ����Ӧ�û���Bug ����ģ�ͻ���ض�������� 2024.4.28 
                        //���Լ�� �����material�鳤������ ��ʱ������
                        Son_Object[i].GetComponent<MeshRenderer>().material = Object_Material_Test;// AssetDatabase.LoadAssetAtPath<Material>(Material_Name)

                        // �õ�������Ĳ�������ɫ
                        Object_color = Son_Object[i].GetComponent<MeshRenderer>().material.color;
                        // ���ݵ�ǰ��ɫ�������µ���ɫ������͸����
                        Object_color.a = Object_color_A;
                        // �ı����������ɫ
                        Son_Object[i].GetComponent<MeshRenderer>().material.color = Object_color;
                    }
                }
            }
            catch
            {
                Debug.Log(Object_Test.name + "�쳣����");//    throw;
            }
        }

        //unity ���ػ���
#if UNITY_EDITOR
        Material[] Love_Fqy;
        Debug.Log("��ǰΪ���ػ��� Ԥ����");
        try
        {
            if (Object_Test.GetComponent<Renderer>() != null && Object_Test.GetComponent<Renderer>().material != null)
            {

                Love_Fqy = Texture_Lucency(Object_GetComponent_MeshRenderer(Object_Test, Material_Path), Object_color_A);//�����µĲ�����
                Object3D_Texture(Object_Test, Love_Fqy);//����������
            }

            ///���� �����������嶼�õ�
            for (int i = 0; i < Son_Object.Count; i++)
            {
                // Debug.Log(i);
                if (Son_Object[i].GetComponent<Renderer>() != null && Son_Object[i].GetComponent<Renderer>().material != null)
                {
                    //�õ������� ������ Դ����
                    //  Object_Material_Test = Son_Object[i].GetComponent<Renderer>().material;

                    //�õ������� ��������
                    Material_Name = Material_Path + Object_Test.name;//�����������λ��

                    Love_Fqy = Texture_Lucency(Object_GetComponent_MeshRenderer(Son_Object[i], Material_Name), Object_color_A);//�����µĲ�����
                    Object3D_Texture(Son_Object[i], Love_Fqy);//����������
                }
            }
        }
        catch
        {
            Debug.Log(Object_Test.name + "�쳣����");//    throw;
        }
#endif
    }

    /// <summary>
    /// �������ļ����µ����ж���
    /// </summary>
    /// <param name="directoryPath"></param>//·��
    private void ClearDirectory_File(string directoryPath)
    {
        // ����ļ�
        string[] files = Directory.GetFiles(directoryPath);//�õ���ȡ����ļ����µ�����
        foreach (string file in files)
        {
            File.Delete(file);
            //  Debug.Log("�ļ���ɾ����" + file);
        }

        // �ݹ�������ļ���
        string[] subDirectories = Directory.GetDirectories(directoryPath);
        foreach (string subDirectory in subDirectories)
        {
            ClearDirectory_File(subDirectory);
        }
    }


    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="Love"></param>
    /// <param name="Fqy"></param>
    public void Object3D_Texture(GameObject Love, Material[] Fqy)
    {
        for (int i = 0; i < Fqy.Length; i++)
        {
            Love.GetComponent<MeshRenderer>().materials[i] = Fqy[i];
        }

    }

    /// <summary>
    /// �ı����͸���ȣ�0-1��
    /// </summary>
    /// <param name="Fqy"></param>
    /// <param name="lucency"></param>
    /// <returns></returns>
    public Material[] Texture_Lucency(Material[] Fqy, float lucency = 1)
    {
        Material[] Love_Fqy = Fqy;
        Color[] Object_colors = new Color[Fqy.Length];

        for (int i = 0; i < Fqy.Length; i++)
        {
            Object_colors[i] = Fqy[i].color;
            Object_colors[i].a = lucency;
            Love_Fqy[i].color = Object_colors[i];
        }
        return Love_Fqy;
    }


    /// <summary>
    /// �������� Gameobject �Ͳ�����·��
    /// ������ǰ�����������
    /// </summary>
    /// <param name="Object_Fqy"></param>
    /// <param name="File_LJ"></param>
    /// <returns></returns>
    public Material[] Object_GetComponent_MeshRenderer(GameObject Object_Fqy, string File_LJ)
    {
        Material[] Object_Materials = new Material[0];//ʵ����
        string Materials_LJ = null;

        //�жϵ�ǰ�Ƿ�Ϊ������
        if (Object_Fqy.GetComponent<MeshRenderer>() == null)
        { Debug.Log("MeshRenderer û�У�ֱ�ӷ���"); return Object_Materials; }

        int Love = 0;
        Love = Object_Fqy.GetComponent<MeshRenderer>().materials.Length;//�õ������򳤶�
        Object_Materials = new Material[Love];
#if UNITY_EDITOR
        try
        {
            for (int i = 0; i < Love; i++)
            {
                //ʵ��������һ��������
                Object_Materials[i] = new Material(Shader.Find("Standard"));

                Materials_LJ = File_LJ + this.gameObject.name + "_" + i + ".mat";

                // �ڱ༭��ģʽ��
                if (AssetDatabase.LoadAssetAtPath<Object>(Materials_LJ) == null)  // �ж��ļ��Ƿ���� ������
                {
                    Object_Materials[i] = Object_Fqy.GetComponent<Renderer>().materials[i];
                    Debug.Log(Object_Materials[i].name);
                    AssetDatabase.CreateAsset(Object_Materials[i], Materials_LJ);
                    //   Debug.Log("��ǰû���ҵ����Ѿ������µĲ�����");
                }
                else// �ڱ༭��ģʽ�¼��������Ƿ�����Ҳ�Ϊ��
                {
                    //����ָ��·���µĲ����� ��ֹ֮ǰ�������Ѿ������� ���θ�ֵ
                    Object_Materials[i] = AssetDatabase.LoadAssetAtPath<Material>(Materials_LJ);
                    //ɾ��ָ��·���µ�
                    AssetDatabase.DeleteAsset(Materials_LJ);
                    Object_Materials[i] = Object_Fqy.GetComponent<Renderer>().materials[i];

                    // �����滻���еĲ����� 2024.5.7 ������Webgl
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(Object_Materials[i]), Materials_LJ);
                    //���½��д���
                    AssetDatabase.CreateAsset(Object_Materials[i], Materials_LJ);

                    //  Debug.Log("��ǰ���ҵ����Ѿ����²�����");
                }
            }


        }
        catch
        {

        }
#endif
        return Object_Materials;
    }
}
