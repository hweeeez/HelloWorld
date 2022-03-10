using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menumusic : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("intromusic");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Study")
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Study2")
        {
            Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Bedroom")
        {
            Destroy(this.gameObject);
        }

    }
}
