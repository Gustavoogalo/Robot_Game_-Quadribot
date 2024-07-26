using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Item : MonoBehaviour
{
    private Collider _collider;
    public Transform followPosition; // Empty game object to determine the position above the player

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        // Optionally add any update logic here if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            if (playerControl != null && !playerControl._Item_In)
            {
                playerControl._Item_In = true;
                playerControl.currentItem = this.gameObject; // vira o currentitem d

                // Deactivate collider
                //_collider.enabled = false;

                // Move item above player
                transform.position = followPosition.position;
                transform.parent = other.transform;
            }
        }
    }

    
}
