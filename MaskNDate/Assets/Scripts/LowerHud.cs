using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class LowerHud : MonoBehaviour
{
    static public LowerHud Instance;
    [SerializeField] TextMeshProUGUI speacherNameTXT, speachTXT;
    [SerializeField] float typingDelay;
    Animator animator;
    AudioSource audioSource;

    string speach, speacher;
    [SerializeField] Slider heartSlider;
    Color nameColor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }

    public void ShowSpeach(string txt, string speacher, Color speacherColor, int hearts)
    {
        speach = txt;
        this.speacher = speacher;
        this.nameColor = speacherColor;
        heartSlider.value = hearts * 1.0f/10;
        StartCoroutine(ShowSpeach());
    }

    public void SetHearts(int hearts)
    {
        heartSlider.value = hearts * 1.0f / 10;
    }

    public IEnumerator ShowSpeach()
    {
        speachTXT.text = "";
        animator.SetBool("Show", false);
        speacherNameTXT.text = speacher;
        speacherNameTXT.color = nameColor;

        animator.SetBool("Show", true);

        foreach (char c in speach)
        {
            speachTXT.text += c;
            audioSource.Play();
            yield return new WaitForSeconds(typingDelay);
        }

        GameControl.Instance.AllowAdvance();
    }





}
