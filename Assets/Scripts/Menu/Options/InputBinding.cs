using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputBinding : MonoBehaviour
{
    [SerializeField] private InputInfos[] baseInputs;
    private Dictionary<string, char> inputsDictionary;

    private string bindingAxis = "";

    [Header("Boutons Deplacement")]
    public GameObject BtnHaut;
    public GameObject BtnBas;
    public GameObject BtnGauche;
    public GameObject BtnDroite;
    [Header("Boutons Armes")]
    public GameObject btnAspirer;
    public GameObject BtnExpluser;
    [Header("Boutons Pouvoirs")]
    public GameObject BtnDash;
    [Header("Boutons Autres")]
    public GameObject BtnInterragir;

    void Start()
    {
        string json = PlayerPrefs.GetString("inputs");
        if (!string.IsNullOrEmpty(json))
        {
            InputsData data = JsonUtility.FromJson<InputsData>(json);
            inputsDictionary = new Dictionary<string, char>();
            for (int i = 0; i < data.keys.Count; i++)
            {
                inputsDictionary[data.keys[i]] = data.values[i];
            }
        }
        else
        {
            Debug.Log("No saved inputs found, using defaults.");
            LoadDefaultInputs();
        }

        UpdateUiButton();
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(bindingAxis) && Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    inputsDictionary[bindingAxis] = (char)keyCode;
                    bindingAxis = "";
                    SaveInputs();
                    UpdateUiButton();
                    return;
                }
            }
        }
    }

    public void Bind(string axis)
    {
        bindingAxis = axis;
    }

    public void SaveInputs()
    {
        InputsData data = new InputsData();
        data.keys = new List<string>(inputsDictionary.Keys);
        data.values = new List<char>(inputsDictionary.Values);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("inputs", json);
        PlayerPrefs.Save();
    }

    private void LoadDefaultInputs()
    {
        inputsDictionary = new Dictionary<string, char>();
        foreach (InputInfos input in baseInputs)
        {
            inputsDictionary.Add(input.Name, input.Key);
        }
        SaveInputs();
    }

    private void UpdateUiButton()
    {
        //Déplacement
        this.BtnHaut.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["haut"]).ToString();
        this.BtnBas.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["bas"]).ToString();
        this.BtnGauche.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["gauche"]).ToString();
        this.BtnDroite.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["drite"]).ToString();

        // Arme
        this.btnAspirer.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["aspirer"]).ToString();
        this.BtnExpluser.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["expulser"]).ToString();

        // Pouvoirs
        this.BtnDash.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode)inputsDictionary["dash"]).ToString();

        //Autres
        this.BtnInterragir.transform.Find("Button").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = ((KeyCode) inputsDictionary["intéragir"]).ToString();
    }

    private class InputsData
    {
        public List<string> keys;
        public List<char> values;
    }
}

[System.Serializable]
public struct InputInfos
{
    [SerializeField] private string inputName;
    [SerializeField] private char key;

    public string Name => inputName;
    public char Key => key;
}
