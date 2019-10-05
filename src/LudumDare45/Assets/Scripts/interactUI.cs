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

    private bool isActiv = false;

    private void Start()
    {
        isActiv = false;
        hide();
    }

    public void setText(string text, int mode)
    {
        if (isActiv)
            return;
        txtMode.SetText(modes[mode] + "\n" + text);
        show();
    }

    public void show()
    {
        isActiv = true;
        txtMode.gameObject.SetActive(true);
        bgImage.gameObject.SetActive(true);
    }

    public void hide()
    {
        isActiv = false;
        txtMode.gameObject.SetActive(false);
        bgImage.gameObject.SetActive(false);
    }
}
