using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Item : MonoBehaviour
{
    private Collider _collider;
    public Transform followPosition; // Empty game object to determine the position above the player

    public string targetTag = "item03";

    public bool plugued;
    void Start()
    {
        plugued = false;
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        // Optionally add any update logic here if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !plugued)
        {
            plugued = false;
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            if (playerControl != null && !playerControl._Item_In)
            {
                playerControl._Item_In = true;
                playerControl.currentItem = this.gameObject; // vira o currentitem do player

                // Move item above player
                transform.position = followPosition.position;
                transform.parent = other.transform;
            }
        }
        else if (other.CompareTag("controleporta") && !plugued)
        {
            plugued = true;
            transform.position = other.transform.position;
            transform.parent = other.transform;
        }
        
        else if (other.CompareTag("Battery") && this.CompareTag("item_03"))
        {
            BatteryScript battery = FindObjectOfType<BatteryScript>();

            transform.position = battery.localItem03.transform.position;
            transform.parent = other.transform;
        }
        

    }

    
}
