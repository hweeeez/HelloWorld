using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Work : MonoBehaviour
{
    public bool endmiss = false;
    public GameObject workB;
    public GameObject spamStart;
    public Button taskb;
    public Button statsb;
    public Button leftarrow;
    public Button rightarrow;
    public bool onetime = false;
    public Slider intslider;
    public float intsgain;
    public int starsgain;
    public float starsper;
    public Slider starsSlider;
    public Slider workSlider;
    public Slider restSlider;
    public float FillSpeed = 0.2f;
    private float targetProgress = 0;
    private float restProgress = 0;
    public float workTimeDec = 0.01f;
    public const float maxWork = 1.0f;
    public float workval;
    public float restval;
    public float decreasePerMinute;
    public bool wasPressed = true;
    public GameObject workpanel;
    public float coolDownT;
    private float startT;
    public bool aGrade;
    public GameObject results;
    public GameObject intText;
    public Text missionText, rankText, starsText, intplus;
    public bool unlock = false;
    public GameObject lockb;
    public bool enoughst;
    public bool twotime;
    public AudioSource roomp;
    public bool enableS = true;
    public bool threetime = false;
    public bool fourtime = false;
    public SpamGame spamsc;
    public GameObject arrows;
    public GameObject newtask;

    // Start is called before the first frame update
    void Awake()
    {
        //workSlider = gameObject.GetComponent<Slider>();
    }
    private void Start()
    {
        newtask.SetActive(false);
        arrows.SetActive(false);
        endmiss = false;
        StartCoroutine(startpop());
        rightarrow.enabled = false;
        leftarrow.enabled = false;
        intText.SetActive(false);
        workSlider.value = 0f;
        restSlider.value = 1f;
        if (results != null)
        {
            starsText.text = "STARS GAINED: \n 0";
        }
        missionText.text = "Your companion has some work to do!\nMake sure they complete their work but do not overwork them!\nDeadline: 12:30PM";
    }

    IEnumerator startpop()
    {
        yield return new WaitForSeconds(1f);
        workpanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        arrows.SetActive(true);
        yield return new WaitForSeconds(5f);
        arrows.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        string rankVal = "F";

        if (workSlider.value < targetProgress && enableS == true)
        {

            workSlider.value += FillSpeed * Time.deltaTime;
            restSlider.value -= FillSpeed * Time.deltaTime;

        }
        if (workSlider.value >= workSlider.maxValue)
        {
            wasPressed = false;
            //workProgress(0.0f);
            targetProgress = 0f;

        }
        /* workSlider.value = workval;*/

        if (restSlider.value > 0 && enableS == true)
        {
            restSlider.value -= Time.deltaTime * decreasePerMinute / 300f;
        }
        if (workSlider.value > 0.00f && restSlider.value > 0.00f)
        {
            rankVal = "F";
            starsgain = 0;
            if (workSlider.value > 0.30f && restSlider.value > 0.30f)
            {
                rankVal = "D";
                //starsText.text = "STARS GAINED: 200";
                starsgain = 200;
                starsper = 0.25f;
                intsgain = 0.01f;


                if (workSlider.value > 0.50f && restSlider.value > 0.50f)
                {
                    rankVal = "C";
                    starsgain = 400;
                    starsper = 0.5f;
                    intsgain = 0.05f;
                    if (workSlider.value > 0.75f && restSlider.value > 0.75f)
                    {
                        rankVal = "B";
                        starsgain = 600;
                        starsper = 0.75f;
                        intsgain = 0.08f;
                        if (workSlider.value > 0.95f && restSlider.value > 0.95f)
                        {
                            rankVal = "A";
                            starsgain = 800;
                            intsgain = 0.1f;
                            starsper = 1f;
                            unlock = true;

                        }
                    }
                }
            }
        }


        if (results.activeSelf)
        {
            enableS = false;
            statsb.enabled = false;
            taskb.enabled = false;
            intplus.text = "KNOWLEDGE +" + (intsgain * 100).ToString();
            rankText.text = rankVal;
            starsText.text = "STARS GAINED:" + "\n" + starsgain.ToString();
            starsSlider.value = starsper;
            intslider.value = intsgain;
            if (!onetime)
            {
                StartCoroutine(intup());
            }
            missionText.text = "Play some games to increase your companion's mood!";

            endmiss = true;
        }
        else
        {
            statsb.enabled = true;
            taskb.enabled = true;
            if (starsSlider.value == 1f)
            {
                if (!threetime)
                {
                    StartCoroutine(resetStars());
                    threetime = true;
                }
            }
        }

        print(enoughst);
        print(unlock);
        rankText.text = rankVal;
        if (spamsc.spamwin == true && starsSlider.value == 1f)
        {
            if (!fourtime)
            {
                roomp.Play();
                StartCoroutine(unlockr());
                fourtime = true;
            }
        }
        if (endmiss == true && starsSlider.value == 1f && !results.activeSelf)
        {
            if (!twotime)
            {
                roomp.Play();
                StartCoroutine(unlockr());
            }
        }
        if (endmiss == true)
        {
            workB.SetActive(false);
            spamStart.SetActive(true);

        }
    }
    IEnumerator resetStars()
    {
        yield return new WaitForSeconds(2f);
        starsSlider.value = 0;
    }
    public IEnumerator unlockr()
    {

        rightarrow.enabled = true;
        leftarrow.enabled = true;

        lockb.SetActive(true);
        yield return new WaitForSeconds(4f);
        lockb.SetActive(false);
        twotime = true;
    }
    IEnumerator intup()
    {
        newtask.SetActive(true);
        intText.SetActive(true);
        yield return new WaitForSeconds(2f);
        intText.SetActive(false);
        newtask.SetActive(false);
        onetime = true;
    }
    /* private IEnumerator DecWork()
     {
         yield return new WaitForSeconds(1);
         decwork = workSlider.value - workTimeDec;
         workSlider.value = decwork;
         workSlider.value -= FillSpeed * Time.deltaTime;
         yield return new WaitForSeconds(1);
     }*/
    public void increasework()
    {
        if (wasPressed == true)
        {
            workProgress(0.2f);
        }
        /* workval += 0.2f;*/
        /*  workSlider.value += FillSpeed * Time.deltaTime;
          restSlider.value -= FillSpeed * Time.deltaTime;*/

    }
    public void increaserest()
    {
        restSlider.value += 0.2f;
    }

    public void workProgress(float newProgress)
    {
        targetProgress = workSlider.value + newProgress;
        restProgress = restSlider.value - newProgress;

    }
    public void rtProgress(float rProgress)
    {
        //targetProgress = workSlider.value  rProgress;
        restProgress = restSlider.value + rProgress;

    }
}
