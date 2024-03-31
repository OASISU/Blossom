using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseMsgListener : MonoBehaviour
{
    private bool isCollectingData = false;
    private List<int> heartRates = new List<int>();

    public void OnMessageArrived(string msg)
    {
        try
        {
            int heartRate = int.Parse(msg);

            if (isCollectingData)
            {
                heartRates.Add(heartRate);
            }
            else if (heartRate <= 10)
            {
                Debug.Log("평균내기 시작");
                Debug.Log("Heart Rate: " + heartRate);
                isCollectingData = true;
                heartRates.Clear();
                heartRates.Add(heartRate);
                StartCoroutine(CollectDataFor5Seconds());
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    IEnumerator CollectDataFor5Seconds()
    {
        yield return new WaitForSeconds(5);

        if (heartRates.Count > 0)
        {
            float average = 0;
            foreach (int rate in heartRates)
            {
                average += rate;
            }
            average /= heartRates.Count;

            Debug.Log("Average Heart Rate: " + average);

            // 오브젝트 활성화 조건에 따라 처리
            ShowObjectBasedOnAverage(average);
        }

        isCollectingData = false;
    }

    void ShowObjectBasedOnAverage(float average)
    {
        // 모든 오브젝트를 보이지 않게 설정
        for (int i = 1; i <= 5; i++)
        {
            SetObjectAndChildrenVisibility("lily3 (" + i + ")", false);
        }

        // 평균 심박수에 따라 특정 오브젝트를 보이게 설정
        if (average < 320)
        {
            SetObjectAndChildrenVisibility("lily3 (1)", true);
        }
        else if (average >= 320 && average < 340)
        {
            SetObjectAndChildrenVisibility("lily3 (2)", true);
        }
        else if (average >= 340 && average < 360)
        {
            SetObjectAndChildrenVisibility("lily3 (3)", true);
        }
        else if (average >= 360 && average < 380)
        {
            SetObjectAndChildrenVisibility("lily3 (4)", true);
        }
        else if (average >= 380)
        {
            SetObjectAndChildrenVisibility("lily3 (5)", true);
        }
    }

    void SetObjectVisibility(string name, bool visible)
    {
        GameObject obj = GameObject.Find(name);
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = visible;
            }
        }
    }

    public void SetObjectAndChildrenVisibility(string objectName, bool visible)
    {
        GameObject obj = GameObject.Find(objectName);
        if (obj != null)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = visible;
            }
        }
    }
}
