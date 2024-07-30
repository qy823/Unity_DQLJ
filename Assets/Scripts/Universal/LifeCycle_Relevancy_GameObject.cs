using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������������/���� ��������Ӧ�仯----�ɸ���
public class LifeCycle_Relevancy_GameObject : MonoBehaviour
{
    [Header("����仯 ����������")]
    public GameObject[] Life_GameObject;

    [Header("״̬�Ƿ�ȡ��")]
    public bool State_Bool;

    private bool temporary_bool;//��ʱ����

    /// <summary>
    /// ���������ü���ʱ ��������¼�
    /// </summary>
    public void OnEnable()
    {
        temporary_bool = true;
        if (State_Bool)//ȡ��
        {
            temporary_bool = !temporary_bool;
        }


        for (int i = 0; i < Life_GameObject.Length; i++)
        {
            Life_GameObject[i].SetActive(temporary_bool);
        }

    }

    /// <summary>
    /// �������������ʱ ��������¼�
    /// </summary>
    public void OnDisable()
    {
        temporary_bool = false;
        if (State_Bool)//ȡ��
        {
            temporary_bool = !temporary_bool;
        }

        for (int i = 0; i < Life_GameObject.Length; i++)
        {
            Life_GameObject[i].SetActive(temporary_bool);
        }
    }

}
