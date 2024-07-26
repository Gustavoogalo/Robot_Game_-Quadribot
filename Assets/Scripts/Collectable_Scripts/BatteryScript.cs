using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    public int batteryPoints = 0;

    public void IncrementBattery()
    {
        batteryPoints++;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            if (playerControl != null && playerControl._Item_In)
            {
                batteryPoints++;
        Debug.Log("Battery points: " + batteryPoints);
                //playerControl. = this.gameObject; 
                //transform.parent = other.transform;
            }
        }
    }
    */
}
