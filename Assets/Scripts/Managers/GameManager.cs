using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    //public EnergyLevelScript EnergyLevel;

    public GameObject VictoryPanel;
    public GameObject FailedPanel;
    [Header("Pause Part")]
    public GameObject pauseMenuUI;
    public bool isPaused;

    [Header("Dialogue_Part")]
    public GameObject dialoguePanel;
    public GameObject HudPanel;

    public bool GameStart;
    //public Animator _animator;

    public int levelNumb;

    [Header("Skins_Parts")]
    public GameObject skindefault;
    public GameObject skin01;
    public GameObject skin02;
    public GameObject skin_complete;

    private void Start()
    {
        levelNumb = SceneManager.GetActiveScene().buildIndex;
        //EnergyLevel = GetComponent<EnergyLevelScript>();
        if (levelNumb == 1)
        {
            Dialogue();
            GameStart = false;
        }
        else
        {
            GameStart = true;
        }
        isPaused = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameStart)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                {
                    PauseGame();
                }
            }
        }
    }




    #region BATTERY_SKINS
    public void Skin_01()
    {
        skin01.SetActive(true);
        skindefault.SetActive(false);
        skin02.SetActive(false);
        skin_complete.SetActive(false);

    }

    public void Skin_02()
    {
        skin01.SetActive(false);
        skindefault.SetActive(false);
        skin02.SetActive(true);
        skin_complete.SetActive(false);

    }

    public void Skin_Complete()
    {
        skin01.SetActive(false);
        skindefault.SetActive(false);
        skin02.SetActive(false);
        skin_complete.SetActive(true);

    }
    #endregion
    public void Victory()
    {

        PlayerControl playercontrol = FindObjectOfType<PlayerControl>();
        HealthSystem health = FindObjectOfType<HealthSystem>();
        health.timerstop = true;
        VictoryPanel.SetActive(true);
        playercontrol.enabled = false;
    }

    public void Failed()
    {

        FailedPanel.SetActive(true);
    }

    public void LoadNextLevel(int level)
    {
        StartCoroutine(AfterSound(level));

    }

    public void ResetLevel(int level)
    {
        //string currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(AfterSound(level));
        // Recarrega a cena atual
        //SceneManager.LoadScene(currentSceneName);
    }

    IEnumerator AfterSound(int level)
    {
        if (Time.timeScale == 0)
        {

            Time.timeScale = 1;
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(level);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            SceneManager.LoadScene(level);

        }




    }

    #region Dialogue Part
    void Dialogue()
    {
        if (levelNumb == 1)
        {
            StartCoroutine(DialogueCoroutine());
        }
    }
    IEnumerator DialogueCoroutine()
    {
        if (levelNumb == 1)
        {
            PlayerControl playercontrol = FindObjectOfType<PlayerControl>();
            playercontrol.enabled = false;
            HudPanel.SetActive(false);

        }
        yield return new WaitForSeconds(1.5f);
        dialoguePanel.SetActive(true);
        yield return new WaitForSeconds(4);


    }

    public void jumpDialogue()
    {
        PlayerControl playercontrol = FindObjectOfType<PlayerControl>();
        dialoguePanel.SetActive(false);
        HudPanel.SetActive(true);
        GameStart = true;
        playercontrol.enabled = true;
    }
    #endregion

    #region PAUSE part
    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(true);
            }

            // Desativar o script de movimento do jogador
            PlayerControl playerControl = FindObjectOfType<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.enabled = false;
            }

            // Desativar o cursor do mouse
            // Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }

    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false);
            }

            // Reativar o script de movimento do jogador
            PlayerControl playerControl = FindObjectOfType<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.enabled = true;
            }

            // Ativar o cursor do mouse
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

    }
    #endregion
}
