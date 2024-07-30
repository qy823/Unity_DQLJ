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
    //从父级脱离
    public void tl()
    {
        //1.断绝父级关系
        this.transform.parent = null;
    }
    //作为某个物体的字迹
    public void zj(string a)
    {
        //2.找个新父亲(需要赋值一个对象的transform)
        this.transform.parent = GameObject.Find(a).transform;
    }

    //3.通过API来进行父子关系的设置
    //参数1 父对象的Transform
    //参数2 是否保留本对象在世界坐标系下的位置、角度、缩放信息
    //      如果填true，则会保留世界坐标系下的状态和父对象进行计算 得出想对父对象的本地坐标系的信息
    //      如果填false，则不会进行计算，直接把在世界坐标系下的信息 赋值到在本地坐标系中
    //api
    public void APIS(string a)
    {
        this.transform.SetParent(GameObject.Find(a).transform, true);
    }


    //与自己的所有子对象 断绝关系
    public void pqzj()
    {

        this.transform.DetachChildren();
    }
}
