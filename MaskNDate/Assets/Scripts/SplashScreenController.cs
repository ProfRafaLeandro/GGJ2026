using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] List<GameObject> images = new List<GameObject>();
    [SerializeField] float delay;
    int number = 0;

    private void Start()
    {
        StartCoroutine(Controller());
    }

    IEnumerator Controller()
    {
        if( number < images.Count )     //check if all the imagens was shown 
        {
            Animator animator = images[number].GetComponent<Animator>();

            animator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(delay);
            animator.SetTrigger("FadeOut");
            number++;
            StartCoroutine(Controller());
        }

        else
        {
            SceneManager.LoadScene("StartScreen");
        }


    }
}
