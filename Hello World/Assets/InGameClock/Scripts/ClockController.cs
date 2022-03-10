using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ClockController : MonoBehaviour
{
    public GameObject workSlider;
    public GameObject restSlider;
    private const float DEGREES_DELTA_X_SECOND = 10f;
    private const float DEGREES_DELTA_X_MINUTE = 2f;
    private const float DEGREES_IN_SECOND = 6f;
    private const float DEGREES_IN_MINUTE = 6f;
    private const float DEGREES_IN_HOUR = 30f;

    private const int MAX_SECOND_MINUTE = 60;
    private const int MAX_HOUR = 23;

    public enum ClockType { Analog, Digital }

    private enum Meridiem { AM, PM }

    public enum TimeSpeed { HourForDay, TimeScale }

    private float couter;

    private Meridiem meridiem;

    [Header("Clock Settings")]
    [HideInInspector] [SerializeField] private ClockType clockType;
    [HideInInspector] [SerializeField] private bool useSystemTime;
    [HideInInspector] [SerializeField] private bool use12HourFormat;
    [HideInInspector] [SerializeField] private bool showSeconds = true;
    [HideInInspector] [SerializeField] private TimeSpeed timeSpeed;

    [Header("Set Time")]
    [HideInInspector] [SerializeField] private int hours = 10;
    [HideInInspector] [SerializeField] private int minutes = 10;
    [HideInInspector] [SerializeField] private int seconds;

    [Header("Set Time Scale")]
    [HideInInspector] [SerializeField] private float timeScale = 1f;

    [Header("hourInDay")]
    [HideInInspector] [SerializeField] private float hoursForDay = 24f;

    // For Analog clock
    private float hourHandRotation;
    private float minuteHandRotation;
    private float secondHandRotation;

    private GameObject analogClock;
    private Transform hourHand;
    private Transform minuteHand;
    private Transform secondHand;
    private TextMeshPro meridiemText;
    public string Study, Study2, Bedroom;
    // For Digital Clock
    private GameObject digitalClock;
    private TextMeshPro clockTime;

    public GameObject results;
    public bool resultstrue = false;
    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        Initialize();
        InitializeAnalogClock();
        InitializeDigitalClock();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("clock");

        if (scene.name != Study && objs.Length > 1)
        {
            Destroy(this.gameObject);

        }
        if (scene.name != Study)
        {
            DontDestroyOnLoad(this.gameObject);

        }
    }

    private void Update()
    {
        if (timeSpeed == TimeSpeed.HourForDay)
            timeScale = 24f / hoursForDay;

        couter += Time.unscaledDeltaTime * timeScale;

        if (couter >= 1f)
        {
            couter = 0f;
            CalculateTime();
        }

        UpdateAnalogClock();
        UpdateDigitalClock();
        if (minutes == 30 && hours == 12 && meridiem == Meridiem.PM)
        {
            print("timesup");
            results.SetActive(true);
            //StartCoroutine(exitslide());
            /*workSlider.SetActive(false);
            restSlider.SetActive(false);*/
            Destroy(workSlider);
            Destroy(restSlider);
            resultstrue = true;
            //restSlider.enabled = false;
        }


    }
    /* IEnumerator exitslide()
     {
         yield return new WaitForSeconds(1f);
         workSlider.SetActive(false);
         restSlider.SetActive(false);
     }*/
    public void closeResults()
    {
        results.SetActive(false);

    }
    private void Initialize()
    {
        if (timeSpeed == TimeSpeed.TimeScale)
            timeScale = timeScale <= 0f ? 1f : timeScale;
        else if (timeSpeed == TimeSpeed.HourForDay)
            hoursForDay = hoursForDay <= 0f ? 1f : hoursForDay;

        if (useSystemTime)
        {
            DateTime currentSystemTime = DateTime.Now;
            seconds = currentSystemTime.Second;
            minutes = currentSystemTime.Minute;
            hours = currentSystemTime.Hour;
        }

        analogClock = transform.Find("AnalogClock").gameObject;
        digitalClock = transform.Find("DigitalClock").gameObject;
    }

    private void InitializeAnalogClock()
    {
        if (clockType != ClockType.Analog)
            return;

        analogClock.SetActive(true);
        digitalClock.SetActive(false);

        hourHand = analogClock.transform.Find("HourHand");
        minuteHand = analogClock.transform.Find("MinuteHand");
        secondHand = analogClock.transform.Find("SecondHand");
        meridiemText = analogClock.transform.Find("Meridiem").gameObject.GetComponent<TextMeshPro>();

        secondHand.gameObject.SetActive(showSeconds);

        SetClockHandRotation();
    }

    private void InitializeDigitalClock()
    {
        if (clockType != ClockType.Digital)
            return;

        digitalClock.SetActive(true);
        analogClock.SetActive(false);

        clockTime = GameObject.Find("ClockTime").GetComponent<TextMeshPro>();
    }

    private void CalculateTime()
    {
        if (hours >= 12f)
        {
            meridiem = Meridiem.PM;
        }
        else
        {
            meridiem = Meridiem.AM;
        }

        seconds += (int)(1f * timeScale);
        minutes += seconds / MAX_SECOND_MINUTE;
        seconds = seconds % MAX_SECOND_MINUTE;
        hours += minutes / MAX_SECOND_MINUTE;
        minutes = minutes % MAX_SECOND_MINUTE;

        if (hours > MAX_HOUR)
        {
            hours = 0;
        }
    }

    private void SetClockHandRotation()
    {
        secondHandRotation = seconds * DEGREES_IN_SECOND;
        minuteHandRotation = minutes * DEGREES_IN_MINUTE + seconds / DEGREES_DELTA_X_SECOND;
        hourHandRotation = hours * DEGREES_IN_HOUR + minutes / DEGREES_DELTA_X_MINUTE;

        secondHand.gameObject.SetActive(showSeconds);

        if (timeScale > 1f && timeScale <= 10f)
        {
            float dampTime = .15f;
            Quaternion newSecondRotation = Quaternion.Euler(new Vector3(0f, 0f, -secondHandRotation));
            secondHand.rotation = Quaternion.Slerp(secondHand.rotation, newSecondRotation, dampTime);
        }
        else
        {
            secondHand.eulerAngles = new Vector3(0f, 0f, -secondHandRotation);
        }

        minuteHand.eulerAngles = new Vector3(0f, 0f, -minuteHandRotation);
        hourHand.eulerAngles = new Vector3(0f, 0f, -hourHandRotation);
    }

    private void UpdateAnalogClock()
    {
        if (clockType != ClockType.Analog)
            return;

        SetClockHandRotation();

        meridiemText.text = meridiem.ToString();
    }

    private void UpdateDigitalClock()
    {
        if (clockType != ClockType.Digital)
            return;

        string timeSeperator = ":";
        string meridiemSeperator = "  ";

        int hour12format;

        if (use12HourFormat)
        {
            if (hours > 12f)
                hour12format = hours - 12;
            else if (hours == 0f)
                hour12format = 12;
            else
                hour12format = hours;

            clockTime.text = hour12format.ToString("00") + timeSeperator + minutes.ToString("00")/* + (showSeconds ? timeSeperator + seconds.ToString("00") : "") */+ meridiemSeperator + meridiem;
        }
        else
        {
            clockTime.text = hours.ToString("00") + timeSeperator + minutes.ToString("00") + (showSeconds ? timeSeperator + seconds.ToString("00") : "");
        }
    }

    /// <summary>
    /// Gets the current in-game hours
    /// </summary>
    /// <returns>Hours in int</returns>
    public int GetHours()
    {
        return hours;
    }

    /// <summary>
    /// Gets the current in-game minutes
    /// </summary>
    /// <returns>Minutes in int</returns>
    public int GetMinutes()
    {
        return minutes;
    }

    /// <summary>
    /// Gets the current in-game seconds
    /// </summary>
    /// <returns>Seconds in int</returns>
    public int GetSeconds()
    {
        return seconds;
    }

    /// <summary>
    /// Gets the current in-game time meridiem
    /// </summary>
    /// <returns>Meridiem in string</returns>
    public string GetMeridiem()
    {
        return meridiem.ToString();
    }
}