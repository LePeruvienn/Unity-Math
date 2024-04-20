using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Stats : MonoBehaviour
{

    private GameHandler gameHandler;

    private PlayerStats playerStats;

    private TextMeshProUGUI txtScore;
    private TextMeshProUGUI txtManche;

    private TextMeshProUGUI txtSpeed;

    private TextMeshProUGUI txtHP;
    private TextMeshProUGUI txtRegen;

    private TextMeshProUGUI txtPower;
    private TextMeshProUGUI txtBattery;
    private TextMeshProUGUI txtRelaodSpeed;

    public GameObject newManche;

    void Start()
    {
        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        this.playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        this.txtScore = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        this.txtManche = GameObject.FindGameObjectWithTag("Manche").GetComponent<TextMeshProUGUI>();

        Transform playerInfo = this.transform.Find("PlayerInfo");

        this.txtSpeed = playerInfo.Find("Speed").gameObject.GetComponent<TextMeshProUGUI>();
        
        this.txtHP = playerInfo.Find("HP").gameObject.GetComponent<TextMeshProUGUI>();
        this.txtRegen = playerInfo.Find("Regen").gameObject.GetComponent<TextMeshProUGUI>();

        this.txtPower = playerInfo.Find("Power").gameObject.GetComponent<TextMeshProUGUI>();
        this.txtBattery = playerInfo.Find("Battery").gameObject.GetComponent<TextMeshProUGUI>();
        this.txtRelaodSpeed = playerInfo.Find("ReloadSpeed").gameObject.GetComponent<TextMeshProUGUI>();

        newManche.SetActive(false);
    }

    void Update()
    {
        txtScore.text = "Score : " + gameHandler.getScore();
        txtManche.text = "Manche : " + gameHandler.getNumManche();

        this.txtSpeed.text = "Speed : " + playerStats.getSpeed();

        this.txtHP.text = "HP : " + playerStats.getHealth() +  "/" + playerStats.getMaxHealth();
        this.txtRegen.text = "Regen : " + playerStats.getRegen() + "/s";
        
        this.txtPower.text = "Power : " + playerStats.getPullPower();
        this.txtBattery.text = "Battery : " + playerStats.getBattery();
        this.txtRelaodSpeed.text = "Reload Speed : " + playerStats.getReloadSpeed();

    }

    public void playNewManche()
    {
        StartCoroutine(animNewManche());
        this.playerStats.setFullHealth();
    }

    IEnumerator animNewManche()
    {
        this.newManche.GetComponent<TextMeshProUGUI>().text = "Manche " + gameHandler.getNumManche();
        this.newManche.SetActive(true);
        yield return new WaitForSeconds(5f);
        this.newManche.GetComponent<Animator>().SetTrigger("exit");
        yield return new WaitForSeconds(2f);
        this.newManche.SetActive(false);
    }


}
