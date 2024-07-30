
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;


/// <summary>
/// ��·��·����
/// </summary>
[Serializable]
public class Line3D_Loop
{
    [Header("һ������������")]
    public List<GameObject> lineIndexList = new List<GameObject>();//һ������������ ����֮��Ӧ����Ҫ��������


    public int AA;
    //��ǰ��·״̬ ͨ·����·����·
    //ö��/str/int


}


public class Establish3Dline_Judge : MonoBehaviour
{
    /// <summary>
    /// �ҵ����߻�·
    /// 2024.07.25 23��00����
    /// </summary>

    [Header("3D�������ݲ� ��Ҫ����ȡ��������")]
    public Establish_3Dline_Data Establish_3Dline_Data;


    [Header("�����б� ��������������·��ѡ�ж�Ӧ����·")]
    public Dropdown dropDown;

    // public List<Line3D_Loop> line3D_Loops;
    public List<GameObject> line3D_Loops;

    // �Ƿ��Ǵ����������� Item ֵ
    private bool isCodeSetItemValue = false;

    void Start()
    {
        //line3D_Loops = ListGameObject_Sort(line3D_Loops);
        // ����
        //  line3D_Loops.lineIndexList.Sort((x, y) => x.CompareTo(y));
        //�Զ�����ɸ��� �������ĳ��ֵ��������
        //  list.Sort((x, y) => { return x.level.CompareTo(y.level); });//��������
        // list.Sort((x, y) => { return -x.salary.CompareTo(y.salary); });//��������


        //ClearDropDownOptionsData();//������
        // ���ü���
        //  SetDropDownAddListener(OnValueChange);
        // SetDropDownItemValue(1);
    }

    #region ����������
    /// <summary>
    /// �������ֵ�ı��Ǵ��� (�л�����ѡ��)
    /// </summary>
    /// <param name="v">�ǵ����ѡ����OptionData�µ�����ֵ</param>
    void OnValueChange(int v)
    {
        //�л�ѡ�� ʱ�����������߼�...
        Debug.Log("��������ؼ���������..." + v);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         List<Dropdown.OptionData> listOptions = new List<Dropdown.OptionData>();
    //         listOptions.Add(new Dropdown.OptionData("Option 0"));
    //         listOptions.Add(new Dropdown.OptionData("Option 1"));
    //         AddDropDownOptionsData(listOptions);
    //     }
    //     // if (Input.GetKeyDown(KeyCode.A))
    //     // {
    //     //     AddDropDownOptionsData("Option " + dropDown.options.Count);
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.R))
    //     // {
    //     //     RemoveAtDropDownOptionsData(dropDown.options.Count - 1);
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.C))
    //     // {
    //     //     ClearDropDownOptionsData();
    //     // }
    // }

    /// <summary>
    /// ����ѡ�������Item
    /// </summary>
    /// <param name="ItemIndex"></param>
    void SetDropDownItemValue(int ItemIndex)
    {
        // �������õ�ֵ
        isCodeSetItemValue = true;

        if (dropDown.options == null)
        {

            Debug.Log(GetType() + "/SetDropDownItemValue()/�����б�Ϊ�գ�����");
            return;
        }
        if (ItemIndex >= dropDown.options.Count)
        {
            ItemIndex = dropDown.options.Count - 1;
        }

        if (ItemIndex < 0)
        {
            ItemIndex = 0;
        }

        dropDown.value = ItemIndex;
    }


    /// <summary>
    /// �Ƿ���Ե��
    /// </summary>
    void SetDropDownInteractable()
    {
        //�Ƿ���Ե��
        dropDown.interactable = true;
    }

    /// <summary>
    /// ������ʾ�����С
    /// </summary>
    /// <param name="fontSize"></param>
    void SetDropDownCaptionTextFontSize(int fontSize)
    {
        //������ʾ�����С
        dropDown.captionText.fontSize = fontSize;
    }

    /// <summary>
    /// ��������Item��ʾ�����С
    /// </summary>
    /// <param name="fontSize"></param>
    void SetDropDownItemTextFontSize(int fontSize)
    {
        //��������Item��ʾ�����С
        dropDown.itemText.fontSize = fontSize;
    }

    /// <summary>
    /// ���һ���б���������
    /// </summary>
    /// <param name="listOptions"></param>
    void AddDropDownOptionsData(List<Dropdown.OptionData> listOptions)
    {
        dropDown.AddOptions(listOptions);
    }

    /// <summary>
    /// ���һ����������
    /// </summary>
    /// <param name="itemText"></param>
    void AddDropDownOptionsData(string itemText)
    {
        //���һ������ѡ��
        Dropdown.OptionData data = new Dropdown.OptionData();
        data.text = itemText;
        //data.image = "ָ��һ��ͼƬ��������ָ����ʹ��Ĭ��"��
        dropDown.options.Add(data);
    }


    /// <summary>
    /// �Ƴ�ָ��λ��   ����:����
    /// </summary>
    /// <param name="index"></param>
    void RemoveAtDropDownOptionsData(int index)
    {
        // ��ȫУ��
        if (index >= dropDown.options.Count || index < 0)
        {
            return;
        }
        //�Ƴ�ָ��λ��   ����:����
        dropDown.options.RemoveAt(index);
    }


    /// <summary>
    /// ֱ����������е�����ѡ��
    /// </summary>
    void ClearDropDownOptionsData()
    {
        //ֱ����������е�����ѡ�
        dropDown.ClearOptions();
    }

    /// <summary>
    /// �������ֵ�ı��Ǵ��� (�л�����ѡ��)
    /// </summary>
    void SetDropDownAddListener(UnityAction<int> OnValueChangeListener)
    {
        //�������ֵ�ı��Ǵ��� (�л�����ѡ��)
        dropDown.onValueChanged.AddListener((value) =>
        {
            // �ֶ��������õ�ֵ�������¼���������Ҫ���Ա�������ȥ����
            if (isCodeSetItemValue == true)
            {

                isCodeSetItemValue = false;

                return;
            }
            OnValueChangeListener(value);
        });
    }
    #endregion

    #region  ʹ��ð������������б�����+�����б����Ƿ������ͬ�ģ�����о�ɾ���� 

    /// <summary>
    /// �����������б�)����ð������(��������Ϊ��������)
    /// </summary>
    /// <param name="Fqy_GameObject"></param>
    /// <param name="isAscendingOrder"></param> true���� false���� Ĭ������
    /// <returns></returns>
    private List<GameObject> ListGameObject_Sort(List<GameObject> Fqy_GameObject, bool isAscendingOrder = true)
    {
        bool isSort;
        for (int i = 0; i < Fqy_GameObject.Count - 1; i++)
        {
            isSort = false;
            for (int j = 0; j < Fqy_GameObject.Count - 1 - i; j++)
            {
                // ʹ�� string.Compare �����ַ����Ƚ�
                int comparison = string.Compare(Fqy_GameObject[j].name, Fqy_GameObject[j + 1].name);
                bool shouldSwap = isAscendingOrder ? comparison > 0 : comparison < 0;

                if (shouldSwap)
                {
                    isSort = true;
                    // ����Ԫ��
                    GameObject temp = Fqy_GameObject[j];
                    Fqy_GameObject[j] = Fqy_GameObject[j + 1];
                    Fqy_GameObject[j + 1] = temp;
                }
            }
            if (!isSort)
            {
                break;
            }
        }
        return Fqy_GameObject;
    }
    #endregion

    //����1 ���� 3D ���ߴ������ �ж��Ƿ���ͨ


    //����2 ����Ԫ�����ϵ�����������·��Ȼ���ж��Ƿ��л�·


    //����3 ������������ȫ����ͨ �ҵ����е�������·

    /// <summary>
    /// ��������
    /// </summary>
    public void Test()
    {
        //�ȴӵ�һ����·��ʼ���б��� ��ͨ·
        GameObject line = Establish_3Dline_Data.Line3D_GameObjects[0];//���õ���һ����· ��ǰֻ�ǲ���

        //�����õ�����· ���б��� �ҳ��������ӵ���·
        //���ݵ�һ�����ӿ�ʼ�ҡ�
        GameObject[] Fqy = line.GetComponent<Line3D_Informatization>().Line3D_Informatization_Data.Line3D_Terminal_Object3D;//�õ���ǰ��·���ӵĶ���

        ///�о�������������һ�������ˣ��� 2024.07.25 23��30
        ///����ͬʱ��һ���ҵ��� 
        ///���������Ӧ���� 1���õ���һ������ 2�����������ҵ����ӵ�2������ 3���������������ҵ���Ӧ������Ԫ�� �ж��Ƿ����������ߣ�Ԫ������Ϣ�м�¼�� 
        ///4��������������� ��������ݶ����ҵ����ӵ���· ֱ�����ж��Ӷ��������  
        ///�����и����ʣ��������ÿ�����Ӷ��������������ӽ��������� ���ܻ���ж���ɺܴ��Ӱ�죡������������˺ܾ�û����ͨ��
        ///���һ�����Ӳ����Ա���������̫��������˼·�Ҿ;��ÿ���

        //�����õ��ϲ�Ԫ����
        GameObject[] YQJ = new GameObject[2];
        YQJ[0] = Fqy[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;
        YQJ[1] = Fqy[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;

        //

        //�����뷨��ʱ���� 2024.07.25 23��30
        //���ݵ�һ�����ӿ�ʼ�ҡ�//���ҵ�һ�����ӵ�������· 
        // �õ��ϲ�Ԫ�� �����õ�Ԫ���Ķ�������Щ���������� ==> �����õ���ǰ���������˼�����·  �Լ�����Щ���ӽ���������   /
        // ���ʣ�����ͬʱ�ң��� ����������һ������ ����һ����· Ȼ��������һ����· ֱ���������ӵ���·������ 


    }



    public void Line3D_Find_Element(Line3D_Informatization Fqy)
    {
        GameObject[] line3D = Fqy.Line3D_Informatization_Data.Line3D_Terminal_Object3D;//�õ���ǰ��·���ӵĶ���

        //�õ��ϲ�Ԫ������Ϣ
        GameObject[] YQJ = new GameObject[2];
        YQJ[0] = line3D[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;
        YQJ[1] = line3D[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;

        //��ʾ��ǰԪ����  �Ѿ������ߵĶ��Ӳ�ֹ һ��
        if (YQJ[1].GetComponent<Object3D_Informatization>().Return_3Dline() > 1)
        {
 
        }
        //���Ԫ���������Ӷ��Ӿ�ֻ����һ�� ����Ҫȥ�������Ӳ�ѯ
        else
        {

        }
    }

}
