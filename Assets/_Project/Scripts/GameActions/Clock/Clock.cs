using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class Clock : MonoBehaviour
{
    public const int hoursInDay = 24, minutesInHour = 60;

    [SerializeField] private Transform hourHand;
    [SerializeField] private Transform minuteHand;
    [SerializeField] private SoundObject soundObject;
    [SerializeField] private AudioClip tickSound;

    public int hours;
    public int minutes;

    public float speedTime;

    const float hoursToDegrees = 360 / 12, minutesToDegrees = 360 / 60;

    public float totalTime;
    private float currentTime;
    public float dayDuration = 30f;

    public Vector3 rotatePoint;


    private int prevMinute;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        totalTime = dayDuration / hoursInDay * hours + minutes * dayDuration / (hoursInDay * minutesInHour);
        currentTime = -totalTime % dayDuration;

        prevMinute = minutes;

        SetRotateByTime();
    }

    private void SetRotateByTime()
    {
        hourHand.localRotation = Quaternion.Euler(rotatePoint * (GetHour() * hoursToDegrees));
        minuteHand.localRotation = Quaternion.Euler(rotatePoint * (GetMinutes() * minutesToDegrees));
    }

    public void ChangeTime(float time = 0)
    {
        if(time != 0) totalTime += time * Time.deltaTime * speedTime;

        currentTime = -totalTime % dayDuration;

        hours = -(int)GetHour();
        minutes = -(int)GetMinutes();

        if(Mathf.Abs(minutes - prevMinute) > 5)
        {
            SoundObject soundObj = Instantiate(soundObject.gameObject, transform).GetComponent<SoundObject>();
            soundObj.PlayAudio(tickSound, 0.2f);
            prevMinute = minutes;
        }



        SetRotateByTime();
    }


    public float GetHour()
    {
        return currentTime * hoursInDay / dayDuration;
    }

    public float GetMinutes()
    {
        return (currentTime * hoursInDay * minutesInHour / dayDuration) % minutesInHour;
    }

}
