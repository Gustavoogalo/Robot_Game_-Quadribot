using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    Animator player_anim;
    GameManager gameManager;
    [Header("Life_Bar Part")]
    public GameObject healthBar_10;
    public GameObject healthBar_09;
    public GameObject healthBar_08;
    public GameObject healthBar_07;
    public GameObject healthBar_06;
    public GameObject healthBar_05;
    public GameObject healthBar_04;
    public GameObject healthBar_03;
    public GameObject healthBar_02;
    public GameObject healthBar_01;
    public GameObject healthBar_00;
    [Header("health Part")]
    public int _MaxHealth = 10;
    public int _health;
    int damageAmount = 1;

    [Header("Timer Part")]
    public int _TimerToLose = 5;
    public float timer;
    public bool timerstop;

    void Start()
    {
        timerstop = false;
        player_anim = GetComponentInChildren<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        timer = _TimerToLose;
        _health = _MaxHealth;
    }


    void Update()
    {
        LosingHbyTime();

        Death();
    }

    void LosingHbyTime() //o timer desce e se ele for menor que zero, seu valor reseta e perde 1 de vida
    {
        //gameManager = GetComponent<GameManager>();
        if (gameManager.GameStart)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0 && !timerstop)
        {
            TakeDamage(damageAmount);

            timer = _TimerToLose;
        }
       
        

        switch (_health)
        {
            case 10:
                healthBar_10.SetActive(true);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 09:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(true);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 08:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(true);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 07:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(true);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 06:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(true);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 05:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(true);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 04:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(true);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 03:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(true);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 02:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(true);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(false);
                break;
            case 01:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(true);
                healthBar_00.SetActive(false);
                break;
            case 00:
                healthBar_10.SetActive(false);
                healthBar_09.SetActive(false);
                healthBar_08.SetActive(false);
                healthBar_07.SetActive(false);
                healthBar_06.SetActive(false);
                healthBar_05.SetActive(false);
                healthBar_04.SetActive(false);
                healthBar_03.SetActive(false);
                healthBar_02.SetActive(false);
                healthBar_01.SetActive(false);
                healthBar_00.SetActive(true);
                break;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;

        if (_health <= 0)
        {
            _health = 0;
            // Adicione lógica para lidar com a morte do jogador, se necessário
            Debug.Log("Player died.");
        }
    }

    public void Heal(int healAmount)
    {
        _health += healAmount;

        if (_health > _MaxHealth)
        {
            _health = _MaxHealth;
        }
    }

    public void Death()
    {
        if (_health == 0)
        {
            PlayerControl player = GetComponent<PlayerControl>();
            player.enabled = false;
            StartCoroutine(DeathPlayer());
        }
    }

    IEnumerator DeathPlayer()
    {
        player_anim = GetComponentInChildren<Animator>();
        player_anim.SetBool("Death", true);
        yield return new WaitForSeconds(3);
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Failed();
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
