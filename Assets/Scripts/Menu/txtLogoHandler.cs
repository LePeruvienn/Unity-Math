using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class txtLogoHandler : MonoBehaviour
{
    public List<string> listTxt;
    private TextMeshProUGUI txtComp;

    private void Start()
    {
        txtComp = GetComponent<TextMeshProUGUI>();
        txtComp.text = listTxt[Random.Range(0, listTxt.Count)];
    }
}
