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
    { 5, 6.2f }
};

    public GameObject CSlope1;
    public GameObject CSlope2;
    public GameObject BSlope1;
    public GameObject BigPlane;

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
            BSlope1.SetActive(true);
        }
        else
        {
            BSlope1.SetActive(false);
        }

        if (newWaterLevel != 0 && newWaterLevel != 3)
        {
            BigPlane.SetActive(true);
        }
        else
        {
            BigPlane.SetActive(false);
        }

            BigPlane.transform.position = new Vector3(BigPlane.transform.position.x, values[newWaterLevel], BigPlane.transform.transform.position.z);
    }
}
