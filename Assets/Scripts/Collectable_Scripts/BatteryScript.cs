using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    public int batteryPoints = 0;

    public bool withItem01;
    public bool withItem02;

    public Transform localItem03;

    private void Start()
    {
        batteryPoints = 0;
    }
    public void IncrementBattery()
    {
        batteryPoints++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item_01") && !withItem02)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.Skin_01();
                withItem01 = true;

            }
        }
        else if (other.CompareTag("Item_02") && !withItem01)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.Skin_02();
            withItem02 = true;

        }
        else if (other.CompareTag("Item_01") && withItem02 || other.CompareTag("Item_02") && withItem01)
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.Skin_Complete();
        }

    }

}
