using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Test_1 : MonoBehaviour
{
    public int verts; // 顶点数
    public int tris; // 面数（三角形的数量）

    void Start()
    {
        GetAllObjects(); // 获取所有对象的顶点数和面数
    }

    // 获取场景中所有游戏对象的顶点数和面数
    void GetAllObjects()
    {
        verts = 0;
        tris = 0;
        GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[]; // 获取场景中的所有游戏对象
        foreach (GameObject obj in ob)
        {
            GetAllVertsAndTris(obj); // 获取每个 GameObject 中所有 MeshFilter 组件的顶点数和面数
        }
    }

    // 获取单个游戏对象的顶点数和面数
    void GetAllVertsAndTris(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>(); // 获取游戏对象及其所有子物体的MeshFilter组件
        foreach (MeshFilter f in filters)
        {
            tris += f.sharedMesh.triangles.Length / 3; // 计算面数
            verts += f.sharedMesh.vertexCount; // 计算顶点数
        }
    }

    void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;
        bb.normal.textColor = new Color(1.0f, 0.5f, 0.0f);
        bb.fontSize = 40;

        string vertsdisplay = verts.ToString("#,##0 verts-顶点数"); // 格式化显示顶点数
        GUILayout.Label(vertsdisplay, bb);
        string trisdisplay = tris.ToString("#,##0 tris-面数"); // 格式化显示面数
        GUILayout.Label(trisdisplay, bb);
    }
}