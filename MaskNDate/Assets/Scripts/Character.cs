using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    Animator animator;
    [SerializeField] public string characterName;
    [SerializeField] public Color nameColor;
    [SerializeField] public int love;
    Image image;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponentInChildren<Image>();
    }

    public void CharacterEnter(int n)
    {
        animator.SetInteger("Enter", n);

    }

    public void CharacterExit()
    {
        animator.SetTrigger("FadeOut");
    }

    public void ChangeImage(int n)
    {
        image.sprite = sprites[n];
    }

}
