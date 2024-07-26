using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int _health = 10;

    [Header("Timer Part")]
    public int _TimerToLose = 5;
    float timer;

    void Start()
    {
        timer = _TimerToLose;
    }


    void Update()
    {
        LosingHbyTime();

      
    }

    void LosingHbyTime() //o timer desce e se ele for menor que zero, seu valor reseta e perde 1 de vida
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            _health--;

            timer = _TimerToLose;
        }
        if (_health < 0)
        {
            _health = 0;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            timer = _TimerToLose;

        }
    }
    */ //item para de perder vida
}
