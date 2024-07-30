using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Win_T_F : MonoBehaviour
{
    [Header("需要隐藏/激活的物体组")]
    public GameObject[] Object_Win;

    [Header("点击按键隐藏 物体组 ")]
    public Button[] Button_F;//点击关闭弹窗
    [Header("点击按键激活 物体组")]
    public Button[] Button_T;



    // [Header("备注功能 用处")]
    // public string Remark;//备注

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
    /// 激活物体组
    /// </summary>
    public void Object_WinTrue()
    {
        for (int i = 0; i < Object_Win.Length; i++)
        {
            Object_Win[i].SetActive(true);
        }
    }

    /// <summary>
    /// 隐藏物体组
    /// </summary>
    public void Object_WinFalse()
    {
        for (int i = 0; i < Object_Win.Length; i++)
        {
            Object_Win[i].SetActive(false);
        }
    }


}
