using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Test_1 : MonoBehaviour
{
    public int verts; // ������
    public int tris; // �����������ε�������

    void Start()
    {
        GetAllObjects(); // ��ȡ���ж���Ķ�����������
    }

    // ��ȡ������������Ϸ����Ķ�����������
    void GetAllObjects()
    {
        verts = 0;
        tris = 0;
        GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[]; // ��ȡ�����е�������Ϸ����
        foreach (GameObject obj in ob)
        {
            GetAllVertsAndTris(obj); // ��ȡÿ�� GameObject ������ MeshFilter ����Ķ�����������
        }
    }

    // ��ȡ������Ϸ����Ķ�����������
    void GetAllVertsAndTris(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>(); // ��ȡ��Ϸ�����������������MeshFilter���
        foreach (MeshFilter f in filters)
        {
            tris += f.sharedMesh.triangles.Length / 3; // ��������
            verts += f.sharedMesh.vertexCount; // ���㶥����
        }
    }

    void OnGUI()
    {
        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;
        bb.normal.textColor = new Color(1.0f, 0.5f, 0.0f);
        bb.fontSize = 40;

        string vertsdisplay = verts.ToString("#,##0 verts-������"); // ��ʽ����ʾ������
        GUILayout.Label(vertsdisplay, bb);
        string trisdisplay = tris.ToString("#,##0 tris-����"); // ��ʽ����ʾ����
        GUILayout.Label(trisdisplay, bb);
    }
}