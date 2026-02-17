using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{

    public Dictionary<int, float> values = new Dictionary<int, float>
{
    { 0, -4.5f },
    { 1, -3f },
    { 2, -2.4f },
    { 3, 2f },
    { 4, 3f },
    { 5, 45f }
};

    public Dictionary<int, float> values2 = new Dictionary<int, float>
{
    { 0, -3.6f },
    { 1, -2.4f },
    { 2, -1.75f },
    { 3, 2.2f },
    { 4, 3.65f },
    { 5, 45f }
};

    public GameObject CSlope1;
    public GameObject CSlope2;
    public GameObject BSlope1;
    public GameObject BigPlane;

    public float waterRiseDuration = 2f; // seconds
    private Coroutine waterCoroutine;


    private void Start()
    {
        GameManager.Instance.waterLevel = this;
    }

    public void IncreaseWaterLevel(int newWaterLevel)
    {
        if (newWaterLevel == 0)
        {
            CSlope1.SetActive(true);
            CSlope2.SetActive(true);
        }
        else
        {
            CSlope1.SetActive(false);
            CSlope2.SetActive(false);
        }
        if (newWaterLevel == 3)
        {
           // BSlope1.SetActive(true);
        }
        else
        {
            BSlope1.SetActive(false);
        }


        float targetY = values[newWaterLevel];

        if (waterCoroutine != null)
            StopCoroutine(waterCoroutine);

        waterCoroutine = StartCoroutine(MoveWaterPlane(targetY));

        // BigPlane.transform.position = new Vector3(BigPlane.transform.position.x, values[newWaterLevel], BigPlane.transform.transform.position.z);
    }
    IEnumerator MoveWaterPlane(float targetY)
    {
        if (GameManager.Instance.currentWaterLevel != 3 && GameManager.Instance.currentWaterLevel != 0)
        {
            BigPlane.SetActive(true);
            BSlope1.SetActive(false);
            CSlope1.SetActive(false);
            CSlope2.SetActive(false);
        }
        Vector3 startPos = BigPlane.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);

        float elapsed = 0f;

        while (elapsed < waterRiseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / waterRiseDuration;

            BigPlane.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        BigPlane.transform.position = targetPos; // snap exactly at the end

        if(GameManager.Instance.currentWaterLevel == 3 || GameManager.Instance.currentWaterLevel == 5)
        {
            BigPlane.SetActive(false);
            BSlope1.SetActive(true);
        }
    }

}
