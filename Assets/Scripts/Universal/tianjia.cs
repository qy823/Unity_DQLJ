using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class tianjia : MonoBehaviour
{



    public string fjName;

    void Update()
    {
        if (fjName == "")
        {
            return;
        }
        else if (Input.GetKey(KeyCode.B) && Input.GetKey(KeyCode.C))
        {
            tlBC();
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C))
        {
            zjas();
        }
        else if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.S))
        {
            APIS(fjName);
        }
       if (Input.GetKey(KeyCode.D))
        {
            pqzj();
        }
          if (Input.GetKey(KeyCode.X)){
 tl();
          }
       
    }

    public void tlBC(){
        Vector3 t = this.transform.localPosition;
        Debug.Log(t);
          this.transform.parent = GameObject.Find("B").transform;
          this.transform.localPosition = t;
          Debug.Log(this.transform.localPosition);
    } 
    public void zjas(){
        Vector3 t = this.transform.localPosition;
        Debug.Log(t);
          this.transform.parent = GameObject.Find("A").transform;
          this.transform.localPosition = t;
          Debug.Log(this.transform.localPosition);
    }
    //�Ӹ�������
    public void tl()
    {
        //1.�Ͼ�������ϵ
        this.transform.parent = null;
    }
    //��Ϊĳ��������ּ�
    public void zj(string a)
    {
        //2.�Ҹ��¸���(��Ҫ��ֵһ�������transform)
        this.transform.parent = GameObject.Find(a).transform;
    }

    //3.ͨ��API�����и��ӹ�ϵ������
    //����1 �������Transform
    //����2 �Ƿ�������������������ϵ�µ�λ�á��Ƕȡ�������Ϣ
    //      �����true����ᱣ����������ϵ�µ�״̬�͸�������м��� �ó���Ը�����ı�������ϵ����Ϣ
    //      �����false���򲻻���м��㣬ֱ�Ӱ�����������ϵ�µ���Ϣ ��ֵ���ڱ�������ϵ��
    //api
    public void APIS(string a)
    {
        this.transform.SetParent(GameObject.Find(a).transform, true);
    }


    //���Լ��������Ӷ��� �Ͼ���ϵ
    public void pqzj()
    {

        this.transform.DetachChildren();
    }
}
