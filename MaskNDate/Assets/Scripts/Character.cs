using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] List<GameObject> sprites;
    Animator animator;
    [SerializeField] public string characterName;
    [SerializeField] public Color nameColor;
    [SerializeField] public int love;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CharacterEnter(int n)
    {
        animator.SetInteger("Enter", n);

    }

    public void CharacterExit()
    {
        animator.SetTrigger("FadeOut");
    }


}
