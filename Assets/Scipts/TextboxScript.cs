using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using TMPro;

public class TextboxScript : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    public void Start() //public so it can be called by testing
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void AddText(string textToAdd)
    {
        textMeshPro.text += textToAdd;
    }

    public string DebugViewText()
    {
        return textMeshPro.text;
    }

    public void DebugClearText()
    {
        textMeshPro.text = "";
    }
}

