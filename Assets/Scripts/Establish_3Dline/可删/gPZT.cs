using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gPZT : MonoBehaviour
{
    //��ʱ�����������Լ�������ײ�����
    public GameObject[] GameObject;
    public void gpzt()
    {
        for (int i = 0; i < GameObject.Length; i++)
        {
            for (int a = 0; a < GameObject[i].transform.childCount; a++)
            {
                GameObject[i].transform.GetChild(a).GetComponent<BoxCollider>().enabled = false; 
                   Destroy(GameObject[i].transform.GetChild(a).GetComponent<Rigidbody>());     
              //  Debug.Log(GameObject[i].transform.GetChild(a).name);
            }
        }
    }
}
