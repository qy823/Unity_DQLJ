using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideRail_Object3D_Highlight_Delete : MonoBehaviour
{
    /// <summary>
    /// ���Ѿ��д���������ո��� ����ɾ��
    /// </summary>
    // Start is called before the first frame update
    public GuideRail_Object3D_Highlight GuideRail_Object3D_Highlight;
    //�������Ƴ�
    void OnTriggerEnter(Collider other)
    {
        GuideRail_Object3D_Highlight.Delete_Name(this.gameObject.name);//����ɾ��
        //  Destroy(this.gameObject);
        Debug.Log("����ô������Ķ����ǣ�" + gameObject.name);
    }
}
