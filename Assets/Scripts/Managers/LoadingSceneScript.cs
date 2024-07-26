using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadingSceneScript : MonoBehaviour
{

    public GameObject LoadingScreen;

    private void Start()
    {

    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }


    //DEVE FAZER ESPERAR A ANIMAÇÃO TERMINAR PARA OPERATION.IS DONE
    IEnumerator LoadSceneAsync(int sceneId)
    {

        var videoPlayer = LoadingScreen.GetComponent<VideoPlayer>();
        LoadingScreen.SetActive(true);
        //videoPlayer.Play();

        yield return new WaitForSeconds(8);
        videoPlayer.playbackSpeed = 3;
        yield return new WaitUntil(() => videoPlayer.frame >= 460);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {


            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);


            //Mathf.Clamp(videoPlayer.frame, progressValue, 99);
            Debug.Log("000" + progressValue);


            yield return null;
        }
    }


}
