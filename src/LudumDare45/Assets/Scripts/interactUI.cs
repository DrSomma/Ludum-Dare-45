using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class interactUI : MonoBehaviour
{ 
    public TextMeshProUGUI txtMode;
    public GameObject bgImage;
    public string[] modes = { "use", "pick up" };
    public Color32[] colors = { new Color32(250, 250, 250, 255), new Color32(250, 0, 0, 255) };

    public const int COLOR_RED = 1;
    public const int COLOR_WHITE = 0;

    private bool isActive = false;

    private void Start()
    {
        isActive = false;
        hide();
    }

    public void setText(string text, int mode)
    {
        setText(text, mode, 0, COLOR_WHITE);
    }

    public void setText(string text, int mode, float energyCost)
    {
        setText(text, mode, energyCost, COLOR_WHITE);
    }

    public void setText(string text, int mode, float energyCost, int color)
    {
        if (energyCost < 0)
        {
            txtMode.SetText(modes[mode] + "\n" + text);
        }
        else
        {
            txtMode.SetText(modes[mode] + "\n" + text + "\n(" + energyCost + ")");
        }
        txtMode.color = colors[color];
    }

    public void show()
    {
        isActive = true;
        txtMode.gameObject.SetActive(true);
        bgImage.gameObject.SetActive(true);
    }

    public void hide()
    {
        isActive = false;
        txtMode.gameObject.SetActive(false);
        bgImage.gameObject.SetActive(false);
    }
}
