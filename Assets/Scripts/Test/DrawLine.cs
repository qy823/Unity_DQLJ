using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    [Header("Line ����")]
    public Canvas canvas;
    [Header("�����������������ָ����������")]
    public RectTransform fingerLine;

    //�������λ��
    [Header("������ʼλ��")]
    public RectTransform start;

    //��ָ���������Ļ�ϵĵ��λ��
    private Vector3 touchPos;

    private bool isPress = false;

    private void Update()
    {
        //����Ϊ�����������λ��

        if (Application.isMobilePlatform)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                UnityEngine.Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    isPress = true;
                    touchPos = touch.position;
                }
                else
                {
                    isPress = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                isPress = true;
                touchPos = Input.mousePosition;
            }
            else
            {
                isPress = false;
            }
        }

        if (isPress)
        {
            fingerLine.gameObject.SetActive(true);
            UpdateFingerLine(start.position);
        }
        else
        {
            fingerLine.gameObject.SetActive(false);

        }

    }


    //�����ָλ�úͶ�ӦUI�ؼ�֮���������Ҫת�����괦��
    private void UpdateFingerLine(Vector3 startPos)
    {
        Vector3 uiStartPos = Vector3.zero;
        Vector3 uitouchPos = Vector3.zero;
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            uiStartPos = startPos;
            uitouchPos = touchPos;
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            Camera camera = canvas.worldCamera;

            //UI����������������ת��ΪUGUI����
            Vector2 screenStartPos = RectTransformUtility.WorldToScreenPoint(camera, startPos);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), screenStartPos,
                camera, out uiStartPos);

            //�������ת��ΪUGUI����
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), touchPos,
                camera, out uitouchPos);
        }

        fingerLine.pivot = new Vector2(0, 0.5f);
        fingerLine.position = startPos;
        fingerLine.eulerAngles = new Vector3(0, 0, GetAngle(uiStartPos, uitouchPos));
        fingerLine.sizeDelta = new Vector2(GetDistance(uiStartPos, uitouchPos), fingerLine.sizeDelta.y);
    }



    //ͨ�������ߵģ����ֻ��������֮�����ߣ�ֻ��Ҫ�����Ӧ��ui�ؼ���Position
    //�����ָλ�úͶ�ӦUI�ؼ�֮���������Ҫת�����괦��
    private RectTransform SetLine(RectTransform lineSource, Vector3 startPos, Vector3 endPos)
    {
        RectTransform line = Instantiate(lineSource, lineSource.parent);
        line.pivot = new Vector2(0, 0.5f);
        line.position = startPos;
        line.eulerAngles = new Vector3(0, 0, GetAngle(startPos, endPos));
        line.sizeDelta = new Vector2(GetDistance(startPos, endPos), lineSource.sizeDelta.y);
        return line;
    }

    private float GetAngle(Vector3 startPos, Vector3 endPos)
    {
        Vector3 dir = endPos - startPos;
        float angle = Vector3.Angle(Vector3.right, dir);
        Vector3 cross = Vector3.Cross(Vector3.right, dir);
        float dirF = cross.z > 0 ? 1 : -1;
        angle = angle * dirF;
        return angle;
    }


    private float GetDistance(Vector3 startPos, Vector3 endPos)
    {
        float distance = Vector3.Distance(endPos, startPos);
        return distance * 1 / canvas.transform.localScale.x;
    }
}



