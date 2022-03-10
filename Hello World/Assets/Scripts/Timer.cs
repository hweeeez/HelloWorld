using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float val;
    bool srt, stp, rst;
    public Text disvar;

    void Start()
    {
        val = 0;
        srt = true;
        stp = false;
        rst = false;
    }

    void Update()

    {

        if (srt)

        {
            val += Time.deltaTime / 360f;
        }

        double b = System.Math.Round(val, 2);

        disvar.text = b.ToString();
    }
    /*    public void stopbutton()
        {
            srt = false;
        }
        public void resetbutton()
        {
            srt = false;
            val = 0;
        }
        public void startbutton()
        {
            start = true;
        }*/
}
