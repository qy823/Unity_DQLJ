using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_Test_2 : MonoBehaviour
{

    public GameObject _object_1;
    public GameObject _object_2;

    public int Fqy = 1;

    public Text ShuLiang;

    public void TJ()
    {
        Fqy++;
        GameObject Object_test = Instantiate(_object_1);//����һ������
        Object_test.transform.position = _object_1.transform.position;//�� Object_1 ��λ��
        Object_test.transform.rotation = _object_1.transform.rotation;//�� Object_1 ��λ��
        Object_test.transform.parent = _object_2.transform;//�� Object_1 ��������
        ShuLiang.text = "��ǰ����Ϊ��" + (Fqy * 0.5f) + " M";
    }
    // Start is called before the first frame update public Text fpsText;

    private int count;
    private float deltaTime;

    public Text fpsText;

    void Update()
    {
        count++;
        deltaTime += Time.deltaTime;

        if (count % 60 == 0)
        {
            count = 1;
            var fps = 60f / deltaTime;
            deltaTime = 0;
            fpsText.text = $"FPS��֡�ʣ�: {Mathf.Ceil(fps)}";
        }
    }
}
