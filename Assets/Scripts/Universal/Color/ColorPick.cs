using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorPick : MonoBehaviour
{
    // ���Ͷȡ�ɫ��ͻ��ʵ�ͼ�����
    [Header("���Ͷ�")]
    public Image Saturation;
    [Header("ɫ��")]
    public Image Hue;
    [Header("�������")]
    public Image Paint;

    // ���ͶȺ�ɫ���ѡ���λ��
    [Header("���Ͷ� ѡ���")]
    public RectTransform Point_Stauration;
    [Header("ɫ�� ѡ���")]
    public RectTransform Point_Hue;

    // ���ͶȺ�ɫ��ľ���
    private Sprite Saturation_Sprite;
    private Sprite Hue_Sprite;

    // ��ǰѡ���ɫ��
    private Color32 currentHue = Color.red;

    private void Start()
    {
        // �ڿ�ʼʱ���±��ͶȺ�ɫ��ͼ��
        UpdateStauration();
        UpdateHue();
    }

    // ���±��Ͷ�ͼ��
    private void UpdateStauration()
    {
        // �������Ͷ�ͼ��ľ���
        Saturation_Sprite = Sprite.Create(new Texture2D(200, 200), new Rect(0, 0, 200, 200), Vector2.zero);

        for (int y = 0; y <= 200; y++)
        {
            for (int x = 0; x < 200; x++)
            {
                var pixColor = GetSaturation(currentHue, x / 200f, y / 200f);
                Saturation_Sprite.texture.SetPixel(x, 200 - y, pixColor);
            }
        }
        Saturation_Sprite.texture.Apply();

        Saturation.sprite = Saturation_Sprite;
    }

    // ����ɫ��ͼ��
    private void UpdateHue()
    {
        // ����ɫ��ͼ��ľ���
        Hue_Sprite = Sprite.Create(new Texture2D(50, 50), new Rect(0, 0, 50, 50), Vector2.zero);

        for (int y = 0; y <= 50; y++)
        {
            for (int x = 0; x < 50; x++)
            {
                var pixColor = GetHue(y / 50f);
                Hue_Sprite.texture.SetPixel(x, 50 - y, pixColor);
            }
        }
        Hue_Sprite.texture.Apply();

        Hue.sprite = Hue_Sprite;
    }

    // ��������Ͷ�ͼ��ʱ���õķ���
    public void OnStaurationClick(ColorPickClick sender)
    {
        var size2 = Saturation.rectTransform.sizeDelta / 2;
        var pos = Vector2.zero;
        pos.x = Mathf.Clamp(sender.ClickPoint.x, -size2.x, size2.x);
        pos.y = Mathf.Clamp(sender.ClickPoint.y, -size2.y, size2.y);
        Point_Stauration.anchoredPosition = pos;

        UpdateColor();
    }

    // ���»�����ɫ
    public void UpdateColor()
    {
        var size2 = Saturation.rectTransform.sizeDelta / 2;
        var pos = Point_Stauration.anchoredPosition + size2;

        var color = GetSaturation(currentHue, pos.x / Saturation.rectTransform.sizeDelta.x, 1 - pos.y / Saturation.rectTransform.sizeDelta.y);
        Paint.color = color;
    }

    // �����ɫ��ͼ��ʱ���õķ���
    public void OnHueClick(ColorPickClick sender)
    {
        var h = Hue.rectTransform.sizeDelta.y / 2.0f;
        var y = Mathf.Clamp(sender.ClickPoint.y, -h, h);
        Point_Hue.anchoredPosition = new Vector2(0, y);

        y += h;
        currentHue = GetHue(1 - y / Hue.rectTransform.sizeDelta.y);
        UpdateStauration();
        UpdateColor();
    }

    // ��ȡָ��ɫ��ͱ��Ͷȵ���ɫֵ
    private static Color GetSaturation(Color color, float x, float y)
    {
        Color newColor = Color.white;
        for (int i = 0; i < 3; i++)
        {
            if (color[i] != 1)
            {
                newColor[i] = (1 - color[i]) * (1 - x) + color[i];
            }
        }

        newColor *= (1 - y);
        newColor.a = 1;
        return newColor;
    }

    // ��ȡָ��ɫ�����ɫ
    private readonly static int[] hues = new int[] { 2, 0, 1, 2, 0, 1 };
    private readonly static Color[] colors = new Color[] { Color.red, Color.blue, Color.blue, Color.green, Color.green, Color.red };
    private readonly static float c = 1.0f / hues.Length;

    private static Color GetHue(float y)
    {
        y = Mathf.Clamp01(y);

        var index = (int)(y / c);
        var h = hues[index];
        var newColor = colors[index];

        float less = (y - index * c) / c;
        newColor[h] = index % 2 == 0 ? less : 1 - less;

        return newColor;
    }
}
