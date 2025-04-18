using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using bonus;
using Unity.VisualScripting;
using TMPro;

public class BonusMenuHandler : MonoBehaviour
{

    private AudioManager AudioManager;

    private PlayerStats playerStats;
    
    private List<Bonus> listeBonus;

    private GameObject card1;
    private GameObject card2;

    private GameObject border1;
    private GameObject border2;

    private Bonus bonus1;
    private Bonus bonus2;


    void Start()
    {
        
        this.AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        this.playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        // On r�cup�re nos 2 game Object Card

        this.card1 = this.transform.Find("Card1").gameObject;
        this.card2 = this.transform.Find("Card2").gameObject;

        this.border1 = this.card1.transform.Find("border").gameObject;
        this.border2 = this.card2.transform.Find("border").gameObject;


        // ON MET EN PLACE LA LISTE DES BONUS :

        listeBonus = new List<Bonus>();

        listeBonus.Add(new BonusRegen(5));
        listeBonus.Add(new BonusSpeed(1));
        listeBonus.Add(new BonusMaxhealth(50));
        listeBonus.Add(new BonusDash());
        listeBonus.Add(new BonusPower(10));
        listeBonus.Add(new BonusPowerBattery(30));
        listeBonus.Add(new BonusPowerReloadSpeed(5));

        RandomBonus();

    }

    // Get random Bonus

    public void RandomBonus()
    {
        int randomIndex;
        int randomIndex2;
        System.Random rand = new System.Random();

        // 1 // On r�cup�re le bonus qu'on va mettre dans la premi�re carte :

        randomIndex = rand.Next(0, listeBonus.Count);

        this.bonus1 = listeBonus[randomIndex];

        // 2 // On r�cup�re le bonus qu'on va mettre dans la deuxi�me carte :


        randomIndex2 = rand.Next(0, listeBonus.Count);
        while(randomIndex == randomIndex2)
        {
            randomIndex2 = rand.Next(0, listeBonus.Count);
        }

        this.bonus2 = listeBonus[randomIndex2];

        // On affiche les bonus sur les cartes

        // Card 1

        GameObject TextPower1 = card1.transform.Find("Name").gameObject;
        TextPower1.GetComponent<TextMeshProUGUI>().text = bonus1.getName();

        GameObject TextDesc1 = card1.transform.Find("Desc").gameObject;
        TextDesc1.GetComponent<TextMeshProUGUI>().text = bonus1.getDescription();

        GameObject Image1 = card1.transform.Find("PowerImage").gameObject;
        Sprite Sprite1 = Resources.Load<Sprite>(bonus1.getImagePath());
        Image1.GetComponent<Image>().sprite = Sprite1;

        this.border2.SetActive(false);


        // Card 2

        GameObject TextPower2 = card2.transform.Find("Name").gameObject;
        TextPower2.GetComponent<TextMeshProUGUI>().text = bonus2.getName();

        GameObject TextDesc2 = card2.transform.Find("Desc").gameObject;
        TextDesc2.GetComponent<TextMeshProUGUI>().text = bonus2.getDescription();

        GameObject Image2 = card2.transform.Find("PowerImage").gameObject;
        Sprite Sprite2 = Resources.Load<Sprite>(bonus2.getImagePath());
        Image2.GetComponent<Image>().sprite = Sprite2;

        this.border2.SetActive(false);
    }

    // UI function

        // 1
    public void PointerEnterCard1()
    {
        this.border1.SetActive(true);
    }
    public void PointerExitCard1()
    {
        this.border1.SetActive(false);
    }

        // 2
    public void PointerEnterCard2()
    {
        this.border2.SetActive(true);
    }
    public void PointerExitCard2()
    {
        this.border2.SetActive(false);
    }


    // Apply function
    public void ApplyBonus1()
    {
        this.bonus1.apply(this.playerStats);
        this.gameObject.SetActive(false);
        RandomBonus();
        AudioManager.PlayBonusSelect();
    }

    public void ApplyBonus2()
    {
        this.bonus2.apply(this.playerStats);
        this.gameObject.SetActive(false);
        RandomBonus();
        AudioManager.PlayBonusSelect();
    }
}


