using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class stats : MonoBehaviour
{
    public string Study, Study2, Bedroom;
    public bool started; public Button taskb;
    public Button statsb; public GameObject statspanel;
    // Start is called before the first frame update
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] stats = GameObject.FindGameObjectsWithTag("stats");

        if (stats.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);


    }
    public void statsOpen()
    {
        taskb.enabled = false;
        statspanel.SetActive(true);

    }
    public void statsClose()
    {
        statspanel.SetActive(false);
        taskb.enabled = true;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
