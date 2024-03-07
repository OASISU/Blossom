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
        yield return new WaitForSeconds(time); // �־��� �ð���ŭ ���

        Debug.Log("Delayed Log at time: " + time);
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        // �޽����� ������ ������ StartTime�� 0���� 3���� ����
        if (streamPlayer != null)
        {
            streamPlayer.StartTime = 0.0f;
            streamPlayer.EndTime = 3.0f;
            streamPlayer.CurrentTime = 0.0f;

            // �� �ð��� ���� ������Ʈ�� �α� ���
            for (float t = 0.0f; t <= 3.0f; t += 1.0f)
            {
                streamPlayer.SetAndPlay(t);
                Debug.Log("Updated StreamPlayer at time: " + t);
            }

        }

        Debug.Log("Arrived: " + msg);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}
