using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Bus : MonoBehaviour
{
  public static bool FollowMovement_Bool = false; // 元器件是否正在拖拽

  public static int Type_ID;//标记当前移动是哪一种类型的源物体（元器件）
  public static bool GuideRail_Bool = false;       // 当前处于哪一种模式 true为连线模式  false为放置元器件模式 

  // [Header("拿到数据")]
  // public GuideRail_Data GuideRail_Data;
  [Header("交互层")]
  public GuideRail_Interaction GuideRail_Interaction;

  // public void Start()
  // {

  // }

  public void GuideRail_Bus_Reset()
  {
    //全局变量 控制重置
    FollowMovement_Bool = false;
    GuideRail_Bool = false;
    Type_ID = -1;


    //交互层重置 包括了交互层重置 数据层重置 以及底层重置
    GuideRail_Interaction.GuideRail_Interaction_Reset();


  }

  // Update is called once per frame
  // void Update()
  // {
  //   if (Input.GetKey(KeyCode.P))
  //   {
  //     GuideRail_Data.Confirm_Object3D();
  //   }
  // }

  #region 放置
  /// <summary>
  /// 确定放置3D物体
  /// </summary>
  // public void Confirm_Object3D()
  // {
  //   for (int i = 0; i < GuideRail_Data.Prototype_Object3D.Length; i++)
  //   {
  //     for (int j = 0; j < GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D.Count; j++)
  //     {
  //       //   GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().
  //       if (GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>() != null)
  //         GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<BoxCollider>().enabled = false;//关闭碰撞体

  //       //销毁刚体
  //       if (GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>() != null)
  //         Destroy(GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Rigidbody>());//销毁刚体

  //       //拿到当前物体再当前类型中的标记 顺序
  //       if (GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>() != null)
  //       {
  //         GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().Object_Type_Order = j;
  //         GuideRail_Data.Prototype_Object3D[i].List_Generate_Object3D[j].This_Object3D.GetComponent<Object3D_Informatization>().BoxCollider_true(true);//打开端子碰撞体
  //       }

  //     }
  //   }
  // }
  #endregion
}
