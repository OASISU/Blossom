using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
public class MyMessageListener : MonoBehaviour
{
    public AlembicStreamPlayer streamPlayer;
    private bool isUpdating = false;

    // AudioSource 컴포넌트에 대한 참조 추가
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {

        // AudioSource 컴포넌트를 자동으로 찾아 할당
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DelayedLog(float time)
    {
        yield return new WaitForSeconds(time); // 주어진 시간만큼 대기

        Debug.Log("Delayed Log at time: " + time);
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        if (streamPlayer != null && !isUpdating)
        {
            streamPlayer.StartTime = 0.0f;
            streamPlayer.EndTime = 3.0f;

            StartCoroutine(UpdateStreamPlayer());

            // 오디오 재생
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

        

        Debug.Log("Arrived: " + msg);
    }

    IEnumerator UpdateStreamPlayer()
    {
        isUpdating = true;

        float currentTime = 0.0f;
        float duration = 3.0f;
        float updateInterval = 0.1f; // 매 0.1초마다 업데이트

        while (currentTime <= duration)
        {
            streamPlayer.SetAndPlay(currentTime);
            Debug.Log("Updated StreamPlayer at time: " + currentTime);

            yield return new WaitForSeconds(updateInterval);
            currentTime += updateInterval;
        }

        streamPlayer.SetAndPlay(duration);
        Debug.Log("Updated StreamPlayer at time: " + duration);

        isUpdating = false;
    }
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
