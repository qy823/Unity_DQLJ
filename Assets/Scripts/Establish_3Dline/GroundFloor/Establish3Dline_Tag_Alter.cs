using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Establish3Dline_Tag_Alter : MonoBehaviour
{
   //底层代码，用于修改标号
   //声明一个Text组件，用于传入字符串将标号修改为指定内容
   //修改标号管

   public Text text;
   //调用函数修改名称
   public void ste_Text(string gradeIndexText)
   {
      text.text = gradeIndexText;
   }
}
