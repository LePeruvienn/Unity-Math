using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using bonus;
using Unity.VisualScripting;
using TMPro;

public class BonusMenuHandler : MonoBehaviour
{

    public Sprite sprite;

    private List<Bonus> listeBonus;

    private GameObject card1;
    private GameObject card2;

    private Bonus bonus1;
    private Bonus bonus2;


    void Start()
    {
        
        // On récupère nos 2 game Object Card

        this.card1 = this.transform.Find("Card1").gameObject;
        this.card2 = this.transform.Find("Card2").gameObject;


        // ON MET EN PLACE LA LISTE DES BONUS :

        listeBonus = new List<Bonus>();

        listeBonus.Add(new BonusSpeed(10));
        listeBonus.Add(new BonusMaxhealth(10));
        listeBonus.Add(new BonusDash());
        listeBonus.Add(new BonusPower(10));
        listeBonus.Add(new BonusPowerBattery(10));
        listeBonus.Add(new BonusPowerReloadSpeed(10));

        int randomIndex;
        System.Random rand = new System.Random();

        // 1 // On récupère le bonus qu'on va mettre dans la première carte :

        randomIndex = rand.Next(0, listeBonus.Count);

        this.bonus1 = listeBonus[randomIndex];

        // 2 // On récupère le bonus qu'on va mettre dans la deuxième carte :

        randomIndex = rand.Next(0, listeBonus.Count);

        this.bonus2 = listeBonus[randomIndex];

        // On affiche les bonus sur les cartes

        // Card 1

        GameObject TextPower1 = card1.transform.Find("Name").gameObject;
        TextPower1.GetComponent<TextMeshProUGUI>().text = bonus1.getName();

        GameObject TextDesc1 = card1.transform.Find("Desc").gameObject;
        TextDesc1.GetComponent<TextMeshProUGUI>().text = bonus1.getDescription();

        GameObject Image1 = card1.transform.Find("PowerImage").gameObject;
        Sprite Sprite1 = Resources.Load<Sprite>(bonus1.getImagePath());
        Image1.GetComponent<Image>().sprite = Sprite1;


        // Card 2

        GameObject TextPower2 = card2.transform.Find("Name").gameObject;
        TextPower2.GetComponent<TextMeshProUGUI>().text = bonus2.getName();

        GameObject TextDesc2 = card2.transform.Find("Desc").gameObject;
        TextDesc2.GetComponent<TextMeshProUGUI>().text = bonus2.getDescription();

        GameObject Image2 = card2.transform.Find("PowerImage").gameObject;
        Sprite Sprite2 = Resources.Load<Sprite>(bonus2.getImagePath());
        Image2.GetComponent<Image>().sprite = Sprite2;

    }
}


