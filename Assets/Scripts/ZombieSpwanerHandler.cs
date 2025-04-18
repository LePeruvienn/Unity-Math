using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.PackageManager;
#endif
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ZombieSpwanerHandler : MonoBehaviour
{

    private AudioManager AudioManager;

    public int zombieMax;
    public int zombieMin;

    public GameObject objectToSpawn;

    public GameObject bonusMenu;

    private GameHandler gameHandler;
    private List<Transform> listeSpwaner;

    void Start()
    {
        this.AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        this.gameHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>();

        this.listeSpwaner = new List<Transform>();

        foreach (Transform child in transform)
        {
            this.listeSpwaner.Add(child);
        }
        StartCoroutine("StartManche");
    }

    IEnumerator StartManche()
    {
        yield return new WaitForSeconds(0.1f);

        this.gameHandler.addManche();

        yield return new WaitForSeconds(3f);

        Debug.Log("D�but de la Manche ...");

        float timeToWait;
        int indexSpawner;

        int numManche = this.gameHandler.getNumManche();

        int nbZombie = Random.Range(this.zombieMin * numManche, this.zombieMax * numManche);

        Debug.Log("Num Manche = " + numManche + " ; nbZombie = " + nbZombie);

        for (int i = 0; i < numManche; i++)
        {
            for (int j = 0; j < nbZombie; j++)
            {
                indexSpawner = Random.Range(0, this.listeSpwaner.Count - 1);
                SpawnZombie(indexSpawner);

                timeToWait = Random.Range(0.5f, 2f);
                yield return new WaitForSeconds(timeToWait);
            }

            timeToWait = Random.Range(5f, 7f);
            yield return new WaitForSeconds(timeToWait);
        }

        yield return new WaitUntil(() => allZombieKilled());


        Debug.Log("Tout les zombie sont mort !");

        this.bonusMenu.SetActive(true);
        AudioManager.PlayBonusTime();


        Debug.Log("D�but de la prochaine manche");

        StartCoroutine("StartManche");
    }

    private void SpawnZombie(int index)
    {
        Instantiate(this.objectToSpawn, this.listeSpwaner[index].position, this.listeSpwaner[index].rotation);
    }

    private bool allZombieKilled()
    {

        GameObject[] listeZombie = GameObject.FindGameObjectsWithTag("Zombie");

        for (int i = 0; i < listeZombie.Length; i++)
        {
            bool isDead = listeZombie[i].GetComponent<zombie>().getIsDead();
            if (!isDead)
            {
                return false;
            }
        }
        return true;
    }
}
