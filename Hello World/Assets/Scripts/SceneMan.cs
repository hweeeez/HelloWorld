using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    // Start is called before the first frame update
    public void Menu()
    {
        SceneManager.LoadScene("Intro");
    }
    public void Story()
    {
        SceneManager.LoadScene("story");
    }
    public void LoadR1()
    {
        SceneManager.LoadScene("Study");
    }
    public void LoadR2()
    {
        SceneManager.LoadScene("Bedroom");
    }
    public void LoadR3()
    {
        SceneManager.LoadScene("Study2");
    }
}
