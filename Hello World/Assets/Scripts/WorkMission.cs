using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WorkMission : MonoBehaviour
{
    public Button taskb;
    public Button statsb;
    public bool workopen;
    public GameObject statspanel;
    public GameObject workpanel;
    public bool open = false;
    // Start is called before the first frame update
    /* public void Popup(string text)
     {
         popUpBox.SetActive(true);
         popUpText.text = text;
         animator.SetTrigger("pop");
     }*/

    void Start()
    {
        /*WorkMission pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WorkMission>();
        pop.Popup(popUp);*/
        /* StartCoroutine(startpop());*/
    }

    // Update is called once per frame
    void Update()
    {
        /* if (workopen == true)
         {
             Time.timeScale = 0;
         }
         if (workopen == false)
         {
             Time.timeScale = 1;
         }*/
    }
    /*  IEnumerator startpop()
      {
          yield return new WaitForSeconds(1f);
          workpanel.SetActive(true);
      }*/
    public void workMis()
    {
        statsb.enabled = false;
        workpanel.SetActive(true);

        workopen = true;
    }
    public void workMisClose()
    {
        statsb.enabled = true;
        workpanel.SetActive(false);

        workopen = false;
    }
    /* public void statsOpen()
     {
         taskb.enabled = false;
         statspanel.SetActive(true);

     }
     public void statsClose()
     {
         statspanel.SetActive(false);
         taskb.enabled = true;

     }*/
}
