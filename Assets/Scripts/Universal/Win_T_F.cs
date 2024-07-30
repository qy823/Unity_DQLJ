using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Win_T_F : MonoBehaviour
{
    [Header("��Ҫ����/�����������")]
    public GameObject[] Object_Win;

    [Header("����������� ������ ")]
    public Button[] Button_F;//����رյ���
    [Header("����������� ������")]
    public Button[] Button_T;



    // [Header("��ע���� �ô�")]
    // public string Remark;//��ע

    private void Start()
    {
        // _false = this.gameObject;
        for (int i = 0; i < Button_F.Length; i++)
        {
            Button_F[i].onClick.AddListener(Object_WinFalse);//
        }
        for (int i = 0; i < Button_T.Length; i++)
        {
            Button_T[i].onClick.AddListener(Object_WinTrue);
        }
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void Object_WinTrue()
    {
        for (int i = 0; i < Object_Win.Length; i++)
        {
            Object_Win[i].SetActive(true);
        }
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void Object_WinFalse()
    {
        for (int i = 0; i < Object_Win.Length; i++)
        {
            Object_Win[i].SetActive(false);
        }
    }


}
