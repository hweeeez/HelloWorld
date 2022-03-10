using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject spamgame;
    public SpamGame spamsc;

    // Start is called before the first frame update
    void Start()
    {
        spamgame.SetActive(false);
    }
    public void StartG()
    {
        spamgame.SetActive(true);
        spamsc.restart();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
