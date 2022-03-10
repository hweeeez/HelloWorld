using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpamGame : MonoBehaviour
{
    public bool twotime;
    public Slider starSlider;
    public bool spamwin;
    public Button startb;
    public Button spamb;
    public Slider spamSlider;
    public GameObject spamSlide;
    public GameObject spamBut;
    public float decreasePerMinute;
    private float targetProgress = 0;
    public float FillSpeed = 0.2f;
    public bool ingame = false;
    public GameObject winscreen;
    public GameObject losescreen;
    public bool onetime = false;
    public GameObject spamgame;
    public Work worksc;
    public Slider mindslider;
    public GameObject mindtext;
    public GameObject arrow;
    // Start is called before the first frame update
    public void restart()
    {
        spamSlider.value = 0.25f;
        spamSlide.SetActive(true);
        spamBut.SetActive(true);
        winscreen.SetActive(false);
        losescreen.SetActive(false);
        onetime = false;
        spamwin = false;
        mindtext.SetActive(false);
        arrow.SetActive(true);
        StartCoroutine(arrowpup());
    }
    IEnumerator arrowpup()
    {
        yield return new WaitForSeconds(2f);
        arrow.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (spamSlider.value > 0)
        {
            spamSlider.value -= Time.deltaTime * decreasePerMinute / 150f;
            ingame = true;
        }
        /*  if (spamSlider.value < targetProgress)
          {
              spamSlider.value += FillSpeed * Time.deltaTime;
          }*/
        if (spamSlider.value == 0f)
        {
            if (!onetime)
            {
                StartCoroutine(losepanel());
                onetime = true;
            }
            spamSlide.SetActive(false);
            spamBut.SetActive(false);


        }
        if (spamSlider.value >= 0.99f && ingame == true)
        {
            spamSlide.SetActive(false);
            spamBut.SetActive(false);
            if (!onetime)
            {
                StartCoroutine(winpanel());
                StartCoroutine(addmind());
                onetime = true;
            }
            if (!spamwin)
            {
                starSlider.value += 0.25f;
                mindslider.value += 0.05f;
                spamwin = true;
            }

        }


        if (starSlider.value == 1f && spamwin == true)
        {
            if (!twotime)
            {
                winscreen.SetActive(false);

                twotime = true;
            }
        }
    }
    /*  public void spamProgress(float newProgress)
      {
          targetProgress = spamSlider.value + newProgress;

      }*/
    IEnumerator addmind()
    {
        mindtext.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        mindtext.SetActive(false);
    }
    IEnumerator losepanel()
    {
        yield return new WaitForSeconds(0.5f);
        losescreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        losescreen.SetActive(false);
        spamgame.SetActive(false);
    }
    IEnumerator winpanel()
    {

        winscreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        winscreen.SetActive(false);
        spamgame.SetActive(false);
    }

    public void increasework()
    {

        spamSlider.value += 0.1f;

    }
}
