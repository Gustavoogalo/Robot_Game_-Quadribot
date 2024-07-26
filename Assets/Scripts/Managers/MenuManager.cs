using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] public GameObject Menu;
    //[SerializeField] public GameObject preScreen;
    //[SerializeField] public GameObject scores_Finals;
    [SerializeField] public GameObject options_Panel;
    //[SerializeField] public GameObject highscore_panel;
    [SerializeField] public GameObject quit_Sure;

    void Start()
    {

    }


    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");

    }


    public void OpenOptions()
    {
        options_Panel.SetActive(true);
    }

    public void CloseOptions()
    {
        options_Panel.SetActive(false);
    }

    public void OpenQuitOp()
    {
        quit_Sure.SetActive(true);
    }

    public void CloseQuitOp()
    {
        quit_Sure.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("saiu do jogo!");
        Application.Quit();
    }

}
