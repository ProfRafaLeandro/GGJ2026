using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    bool canGo = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        if(canGo)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene("StartScreen");
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10);
        canGo = true;
    }
}
