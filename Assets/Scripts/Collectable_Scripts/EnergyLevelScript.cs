using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyLevelScript : MonoBehaviour
{
    BatteryScript _battery;
    public int PiecesRequired;
    public int currentpieces;

    public bool canPass;
    //public int nextSceneNumber;
    [Header("Light Part")]
    public GameObject lightaboveB;
    private Light childLight;
    private Animator animChild;
    private GameManager gameManager;

    void Start()
    {
        _battery = FindObjectOfType<BatteryScript>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        VerifyPieces();
    }

    void VerifyPieces()
    {
        currentpieces = _battery.batteryPoints;

        if(currentpieces < PiecesRequired)
        {
            canPass = false;
            childLight = GetComponentInChildren<Light>();
            childLight.color = Color.red;
            
        }
        else if (currentpieces >= PiecesRequired)
        {

            canPass = true;
            lightaboveB.SetActive(true);
            childLight = GetComponentInChildren<Light>();
            childLight.color = Color.green;
            
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("player entrou!");
            animChild = GetComponentInChildren<Animator>();
            if (animChild != null)
            {
                if (canPass)
                {
                    animChild = GetComponentInChildren<Animator>();
                    animChild.SetTrigger("victory");
                    StartCoroutine(RechargingVictory());
                }
                else
                {
                    animChild = GetComponentInChildren<Animator>();
                    animChild.SetTrigger("errorlight");
                    StartCoroutine(ResetAnimation());
                }
            }
            else
            {
                Debug.LogWarning("Animator não encontrado no objeto filho.");
            }

        }
    }

    private IEnumerator ResetAnimation()
    {
        // Atraso para garantir que a animação 'errorlight' seja tocada
        yield return new WaitForSeconds(1f);// Ajuste o tempo conforme necessário
        animChild = GetComponentInChildren<Animator>();
        animChild.SetTrigger("reset");
    }

    IEnumerator RechargingVictory()
    {
        Debug.Log("Recharging");
        yield return new WaitForSeconds(2);
        gameManager = FindObjectOfType<GameManager>();
        gameManager.Victory();
    }



}
