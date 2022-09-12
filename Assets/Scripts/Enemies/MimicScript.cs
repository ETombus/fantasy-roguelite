using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicScript : MonoBehaviour
{
    GameObject player;
    public int stolenCoins;
    bool eating = false;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //if mimic has stolen coins and isn't eating, starts EatCoins()
        if (!eating && stolenCoins > 0)
            StartCoroutine(EatCoins());
    }

    private void OnTriggerEnter2D(Collider2D contact)
    {
        if (contact.gameObject.name == "Player")
        {
            //takes between 20-30% of players coins on touch
            PlayerScript playerSC = player.GetComponent<PlayerScript>();
            int stolenCoinTemp = Mathf.RoundToInt(playerSC.coinCount * Random.Range(0.3f, 0.2f));
            stolenCoins += stolenCoinTemp;
            playerSC.coinCount -= stolenCoinTemp;
        }
    }

    IEnumerator EatCoins()
    {
        //removes coins every other second
        eating = true;
        yield return new WaitForSeconds(2f);
        stolenCoins--;
        eating = false;
    }
}
