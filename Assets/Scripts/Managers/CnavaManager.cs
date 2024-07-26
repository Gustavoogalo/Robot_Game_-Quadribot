using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CnavaManager : MonoBehaviour
{
    public Animator animatorFades;

    //public GameObject transitionAnim;

    void Start()
    {
        FadeOut();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeOut()
    {
        animatorFades.SetTrigger("fadeOut");
    }

    public void FadeIn()
    {
        animatorFades.SetTrigger("fadeIn");
    }

}
