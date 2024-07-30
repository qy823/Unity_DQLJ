
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;


/// <summary>
/// 线路回路数据
/// </summary>
[Serializable]
public class Line3D_Loop
{
    [Header("一条完整的连线")]
    public List<GameObject> lineIndexList = new List<GameObject>();//一条完整的连线 找完之后应该需要进行排序


    public int AA;
    //当前线路状态 通路、短路、断路
    //枚举/str/int


}


public class Establish3Dline_Judge : MonoBehaviour
{
    /// <summary>
    /// 找到连线回路
    /// 2024.07.25 23：00新增
    /// </summary>

    [Header("3D连线数据层 主要是拿取连线数据")]
    public Establish_3Dline_Data Establish_3Dline_Data;


    [Header("下拉列表 用于找完所有线路后选中对应的线路")]
    public Dropdown dropDown;

    // public List<Line3D_Loop> line3D_Loops;
    public List<GameObject> line3D_Loops;

    // 是否是代码设置下拉 Item 值
    private bool isCodeSetItemValue = false;

    void Start()
    {
        //line3D_Loops = ListGameObject_Sort(line3D_Loops);
        // 升序
        //  line3D_Loops.lineIndexList.Sort((x, y) => x.CompareTo(y));
        //自定义类可根据 类里面的某个值进行排序
        //  list.Sort((x, y) => { return x.level.CompareTo(y.level); });//升序排列
        // list.Sort((x, y) => { return -x.salary.CompareTo(y.salary); });//降序排列


        //ClearDropDownOptionsData();//清空组件
        // 设置监听
        //  SetDropDownAddListener(OnValueChange);
        // SetDropDownItemValue(1);
    }

    #region 下拉组件相关
    /// <summary>
    /// 当点击后值改变是触发 (切换下拉选项)
    /// </summary>
    /// <param name="v">是点击的选项在OptionData下的索引值</param>
    void OnValueChange(int v)
    {
        //切换选项 时处理其他的逻辑...
        Debug.Log("点击下拉控件的索引是..." + v);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         List<Dropdown.OptionData> listOptions = new List<Dropdown.OptionData>();
    //         listOptions.Add(new Dropdown.OptionData("Option 0"));
    //         listOptions.Add(new Dropdown.OptionData("Option 1"));
    //         AddDropDownOptionsData(listOptions);
    //     }
    //     // if (Input.GetKeyDown(KeyCode.A))
    //     // {
    //     //     AddDropDownOptionsData("Option " + dropDown.options.Count);
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.R))
    //     // {
    //     //     RemoveAtDropDownOptionsData(dropDown.options.Count - 1);
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.C))
    //     // {
    //     //     ClearDropDownOptionsData();
    //     // }
    // }

    /// <summary>
    /// 设置选择的下拉Item
    /// </summary>
    /// <param name="ItemIndex"></param>
    void SetDropDownItemValue(int ItemIndex)
    {
        // 代码设置的值
        isCodeSetItemValue = true;

        if (dropDown.options == null)
        {

            Debug.Log(GetType() + "/SetDropDownItemValue()/下拉列表为空，请检查");
            return;
        }
        if (ItemIndex >= dropDown.options.Count)
        {
            ItemIndex = dropDown.options.Count - 1;
        }

        if (ItemIndex < 0)
        {
            ItemIndex = 0;
        }

        dropDown.value = ItemIndex;
    }


    /// <summary>
    /// 是否可以点击
    /// </summary>
    void SetDropDownInteractable()
    {
        //是否可以点击
        dropDown.interactable = true;
    }

    /// <summary>
    /// 设置显示字体大小
    /// </summary>
    /// <param name="fontSize"></param>
    void SetDropDownCaptionTextFontSize(int fontSize)
    {
        //设置显示字体大小
        dropDown.captionText.fontSize = fontSize;
    }

    /// <summary>
    /// 设置下拉Item显示字体大小
    /// </summary>
    /// <param name="fontSize"></param>
    void SetDropDownItemTextFontSize(int fontSize)
    {
        //设置下拉Item显示字体大小
        dropDown.itemText.fontSize = fontSize;
    }

    /// <summary>
    /// 添加一个列表下拉数据
    /// </summary>
    /// <param name="listOptions"></param>
    void AddDropDownOptionsData(List<Dropdown.OptionData> listOptions)
    {
        dropDown.AddOptions(listOptions);
    }

    /// <summary>
    /// 添加一个下拉数据
    /// </summary>
    /// <param name="itemText"></param>
    void AddDropDownOptionsData(string itemText)
    {
        //添加一个下拉选项
        Dropdown.OptionData data = new Dropdown.OptionData();
        data.text = itemText;
        //data.image = "指定一个图片做背景不指定则使用默认"；
        dropDown.options.Add(data);
    }


    /// <summary>
    /// 移除指定位置   参数:索引
    /// </summary>
    /// <param name="index"></param>
    void RemoveAtDropDownOptionsData(int index)
    {
        // 安全校验
        if (index >= dropDown.options.Count || index < 0)
        {
            return;
        }
        //移除指定位置   参数:索引
        dropDown.options.RemoveAt(index);
    }


    /// <summary>
    /// 直接清理掉所有的下拉选项
    /// </summary>
    void ClearDropDownOptionsData()
    {
        //直接清理掉所有的下拉选项，
        dropDown.ClearOptions();
    }

    /// <summary>
    /// 当点击后值改变是触发 (切换下拉选项)
    /// </summary>
    void SetDropDownAddListener(UnityAction<int> OnValueChangeListener)
    {
        //当点击后值改变是触发 (切换下拉选项)
        dropDown.onValueChanged.AddListener((value) =>
        {
            // 手动代码设置的值不触发事件（根据需要可以保留或者去掉）
            if (isCodeSetItemValue == true)
            {

                isCodeSetItemValue = false;

                return;
            }
            OnValueChangeListener(value);
        });
    }
    #endregion

    #region  使用冒泡排序给物体列表排序+查找列表中是否存在相同的（如果有就删除） 

    /// <summary>
    /// 将对象（物体列表)进行冒泡排序(以名字作为排序依据)
    /// </summary>
    /// <param name="Fqy_GameObject"></param>
    /// <param name="isAscendingOrder"></param> true升序 false降序 默认升序
    /// <returns></returns>
    private List<GameObject> ListGameObject_Sort(List<GameObject> Fqy_GameObject, bool isAscendingOrder = true)
    {
        bool isSort;
        for (int i = 0; i < Fqy_GameObject.Count - 1; i++)
        {
            isSort = false;
            for (int j = 0; j < Fqy_GameObject.Count - 1 - i; j++)
            {
                // 使用 string.Compare 进行字符串比较
                int comparison = string.Compare(Fqy_GameObject[j].name, Fqy_GameObject[j + 1].name);
                bool shouldSwap = isAscendingOrder ? comparison > 0 : comparison < 0;

                if (shouldSwap)
                {
                    isSort = true;
                    // 交换元素
                    GameObject temp = Fqy_GameObject[j];
                    Fqy_GameObject[j] = Fqy_GameObject[j + 1];
                    Fqy_GameObject[j + 1] = temp;
                }
            }
            if (!isSort)
            {
                break;
            }
        }
        return Fqy_GameObject;
    }
    #endregion

    //方案1 根据 3D 连线传入进来 判断是否连通


    //方案2 计算元器件上的所有连接线路，然后判断是否有回路


    //方案3 根据连线数组全部拉通 找到所有的连接线路

    /// <summary>
    /// 测试用例
    /// </summary>
    public void Test()
    {
        //先从第一条线路开始进行遍历 找通路
        GameObject line = Establish_3Dline_Data.Line3D_GameObjects[0];//先拿到第一条线路 当前只是测试

        //根据拿到的线路 进行遍历 找出所有连接的线路
        //根据第一个端子开始找。
        GameObject[] Fqy = line.GetComponent<Line3D_Informatization>().Line3D_Informatization_Data.Line3D_Terminal_Object3D;//拿到当前线路连接的端子

        ///感觉我这里陷入了一个误区了！！ 2024.07.25 23：30
        ///可以同时找一条找到底 
        ///解决方法：应该是 1、拿到第一条连线 2、根据连线找到连接的2个端子 3、根据两个端子找到对应的两个元件 判断是否有其他连线（元器件信息有记录） 
        ///4、如果有其他连线 则继续根据端子找到连接的线路 直到所有端子都遍历完毕  
        ///可能有个疑问？就是如果每个端子都反复跟其他端子进行了连接 可能会对判断造成很大的影响！！！这个是想了很久没有想通的
        ///如果一个端子不可以被反复连接太多次那这个思路我就觉得可以

        //可以拿到上层元器件
        GameObject[] YQJ = new GameObject[2];
        YQJ[0] = Fqy[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;
        YQJ[1] = Fqy[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;

        //

        //下面想法暂时废弃 2024.07.25 23：30
        //根据第一个端子开始找。//先找第一个端子的连接线路 
        // 拿到上层元件 可以拿到元件的端子有哪些进行了连接 ==> 可以拿到当前端子连接了几条线路  以及跟哪些端子进行了连接   /
        // 疑问：几条同时找？？ 还是先找完一条？？ 先找一条线路 然后再找下一条线路 直到所有连接的线路都找完 


    }



    public void Line3D_Find_Element(Line3D_Informatization Fqy)
    {
        GameObject[] line3D = Fqy.Line3D_Informatization_Data.Line3D_Terminal_Object3D;//拿到当前线路连接的端子

        //拿到上层元器件信息
        GameObject[] YQJ = new GameObject[2];
        YQJ[0] = line3D[0].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;
        YQJ[1] = line3D[1].GetComponent<Establish3Dline_Terminal_Click>().Object3D_Informatization;

        //表示当前元器件  已经有连线的端子不止 一个
        if (YQJ[1].GetComponent<Object3D_Informatization>().Return_3Dline() > 1)
        {
 
        }
        //这个元器件的连接端子就只有这一个 不需要去其他端子查询
        else
        {

        }
    }

}
