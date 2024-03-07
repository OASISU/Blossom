using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
public class MyMessageListener : MonoBehaviour
{
    public AlembicStreamPlayer streamPlayer;
    // Use this for initialization
    void Start()
    {
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
        if (streamPlayer != null)
        {
            streamPlayer.StartTime = 0.0f;
            streamPlayer.EndTime = 3.0f;

            // 각 메시지마다 0초에서 3초까지 흐르도록 업데이트합니다.
            StartCoroutine(UpdateStreamPlayer());
        }

        Debug.Log("Arrived: " + msg);
    }

    IEnumerator UpdateStreamPlayer()
    {
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

        // 마지막에 정확히 3.0초에 도달하도록 설정
        streamPlayer.SetAndPlay(duration);
        Debug.Log("Updated StreamPlayer at time: " + duration);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
