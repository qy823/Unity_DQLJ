using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Monitoring_Object : MonoBehaviour
{
    [Header("������Ϣ")]
    public GameObject Message_2DObject;

    //�������ڽ�������
    public void OnDisable()
    {
        Message_2DObject.SetActive(false);
    }
}
