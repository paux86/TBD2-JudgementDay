using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Text counterText = gameObject.GetComponent<UnityEngine.UI.Text>();
        counterText.text = "Money: " + player.GetComponent<PlayerStats>().GetMoneyCount();
    }
}
