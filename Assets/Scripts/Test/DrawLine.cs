using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    [Header("Line 画布")]
    public Canvas canvas;
    [Header("根据这个连线来画手指或鼠标的连线")]
    public RectTransform fingerLine;

    //测试起点位置
    [Header("测试起始位置")]
    public RectTransform start;

    //手指或鼠标在屏幕上的点击位置
    private Vector3 touchPos;

    private bool isPress = false;

    private void Update()
    {
        //下面为测试连接鼠标位置

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


    //针对手指位置和对应UI控件之间的连线需要转换坐标处理
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

            //UI世界的起点世界坐标转换为UGUI坐标
            Vector2 screenStartPos = RectTransformUtility.WorldToScreenPoint(camera, startPos);
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), screenStartPos,
                camera, out uiStartPos);

            //鼠标坐标转换为UGUI坐标
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(), touchPos,
                camera, out uitouchPos);
        }

        fingerLine.pivot = new Vector2(0, 0.5f);
        fingerLine.position = startPos;
        fingerLine.eulerAngles = new Vector3(0, 0, GetAngle(uiStartPos, uitouchPos));
        fingerLine.sizeDelta = new Vector2(GetDistance(uiStartPos, uitouchPos), fingerLine.sizeDelta.y);
    }



    //通用设置线的，如果只设置两点之间连线，只需要初入对应的ui控件的Position
    //针对手指位置和对应UI控件之间的连线需要转换坐标处理
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



