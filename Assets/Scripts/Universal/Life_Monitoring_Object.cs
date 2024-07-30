using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Monitoring_Object : MonoBehaviour
{
    [Header("报文信息")]
    public GameObject Message_2DObject;

    //生命周期结束调用
    public void OnDisable()
    {
        Message_2DObject.SetActive(false);
    }
}
