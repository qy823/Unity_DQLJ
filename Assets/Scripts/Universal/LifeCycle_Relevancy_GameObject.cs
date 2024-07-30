using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//跟随物体隐藏/激活 而进行相应变化----可复用
public class LifeCycle_Relevancy_GameObject : MonoBehaviour
{
    [Header("跟随变化 的物体数组")]
    public GameObject[] Life_GameObject;

    [Header("状态是否取反")]
    public bool State_Bool;

    private bool temporary_bool;//临时变量

    /// <summary>
    /// 在物体启用激活时 触发相关事件
    /// </summary>
    public void OnEnable()
    {
        temporary_bool = true;
        if (State_Bool)//取反
        {
            temporary_bool = !temporary_bool;
        }


        for (int i = 0; i < Life_GameObject.Length; i++)
        {
            Life_GameObject[i].SetActive(temporary_bool);
        }

    }

    /// <summary>
    /// 在物体禁用隐藏时 触发相关事件
    /// </summary>
    public void OnDisable()
    {
        temporary_bool = false;
        if (State_Bool)//取反
        {
            temporary_bool = !temporary_bool;
        }

        for (int i = 0; i < Life_GameObject.Length; i++)
        {
            Life_GameObject[i].SetActive(temporary_bool);
        }
    }

}
