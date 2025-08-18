using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Update()
    {
        DateTime time = DateTime.Now;

        float hourAngle = (time.Hour % 12 + time.Minute / 60f) * 30f; // 360 / 12 = 30
        float minuteAngle = (time.Minute + time.Second / 60f) * 6f;   // 360 / 60 = 6
        float secondAngle = time.Second * 6f;

        hourHand.localRotation = Quaternion.Euler(hourAngle, 0f, 0f);
        minuteHand.localRotation = Quaternion.Euler(minuteAngle, 0f, 0f);
        secondHand.localRotation = Quaternion.Euler(secondAngle, 0f, 0f);
    }
}