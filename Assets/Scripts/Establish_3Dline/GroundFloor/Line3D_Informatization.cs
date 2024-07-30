using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line3D_Informatization : MonoBehaviour
{
    /// <summary>
    /// 线段信息
    /// 2024.07.23  主要用于记录连线的信息
    ///最新修改：2024.07.24 修改信息结构 
    /// 最新修改：
    /// </summary>

    [Header("本线路的连接信息")]
    public Line3D_Informatization_Data Line3D_Informatization_Data;

    // [Header("仅仅表达当前端子与端子之间连接规则符合")]
    // public bool Line3D_Informatization_Bool = false;//false 不符合 true 符合
    // [Header("着条线路是由那两个端子所连接的")]
    // public GameObject[] Line3D_Terminal_Object3D = new GameObject[2];
    // [Header("本线路的额定电压")]
    // public int Line3D_Voltage = 0;
    // [Header("本线路的额定电流")]
    // public int Line3D_Electricity = 0;

    public void Awake()
    {
        Line3D_Informatization_Data = new Line3D_Informatization_Data();//实例化
    }

    void Start()
    {
        //创建的时候就开始赋值？？ 计算该线路是否正确？？
        //或者是延时两秒开始计算线路，避免赋值过慢？
        StartCoroutine(Delayed(2));//延时两秒开始计算线路是否正确
    }
    //StartCoroutine(Delayed(1));  
    IEnumerator Delayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        //延时后执行的

        //根据两个端子的信息＋两个元件 都满足的话  Line3D_Boool = true;否则 Line3D_Boool = false;
    }
}
