using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static GameControl Instance;

    [SerializeField] List<GameObject> characters = new List<GameObject>();
    [SerializeField] List<GameObject> dialogues = new List<GameObject>();
    [SerializeField] List<GameObject> selection = new List<GameObject>();
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    List<GameObject> characterInScene = new List<GameObject>();

    [SerializeField] GameObject charactersParent;
    [SerializeField] Animator faceAnimator;
    [SerializeField] Button nextButton;
    [SerializeField] GameObject hearts, masksAnimation;
    [SerializeField] Image backgroundImage;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] AudioSource faceSource;
    AudioSource audioSource;

    Dialogue actualDialogue;

    int stage = 0;
    public int  masks;
    char expressionChosen;
    char emotionFelt;

    string[] playerAnswers;

    bool canAdvance = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextSpeach();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExpressionButton("h");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ExpressionButton("f");
        }

    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(Instance );
            Instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(DelayToStart());
    }

    IEnumerator DelayToStart()
    {
        yield return new WaitForSeconds(1);
        NextStage();
    }

    void NextStage()
    {
        switch(stage)
        {
            case 0:
                NewDialogue(0);
                ChangeBackground(8);
                audioSource.clip = audioClips[0];
                audioSource.Play();
                break;
            case 1:
                ChangeBackground(7);
                NewDialogue(1);
                break;
            case 2:
                //Bouncer meet
                StartCoroutine(CallCharacter(4, 5));
                NewDialogue(2);
                audioSource.clip = audioClips[1];
                audioSource.Play();
                break;
            case 3:
                //inside the ballroon
                ChangeBackground(5);
                CharacterExit(0);
                NewDialogue(3);
                audioSource.clip = audioClips[2];
                audioSource.Play();
                break;
            case 4:
                //meet the Garlic
                StartCoroutine(CallCharacter(2, 1));
                NewDialogue(4);
                audioSource.clip = audioClips[3];
                audioSource.Play();
                break;
            case 5:
                //pre BAthroom
                CharacterExit(0);
                NewDialogue(5);
                audioSource.clip = audioClips[2];
                audioSource.Play();
                break;
            case 6:
                //Bathroom
                ChangeBackground(6);
                StartCoroutine(CallCharacter(2, 1));
                characterInScene[0].GetComponent<Character>().ChangeImage(1);
                NewDialogue(6);
                audioSource.clip = audioClips[0];
                audioSource.Play();
                break;
            case 7:
                //Exiting from Bathroom
                CharacterExit(0);
                NewDialogue(7);
                break;
            case 8:
                //inside the ballroon, again
                ChangeBackground(5);
                StartCoroutine(CallCharacter(2, 1));
                NewDialogue(8);
                audioSource.clip = audioClips[3];
                audioSource.Play();
                break;


            default:
                SceneManager.LoadScene("GameOver");
                break;
        }

        stage++;
    }

    void NewDialogue(int dialogueN)
    {
        actualDialogue = dialogues[dialogueN].GetComponent<Dialogue>();
        actualDialogue.speachNumber = 0;
        emotionFelt = actualDialogue.feeling[0];
        
        Character actualCharacter = characters[actualDialogue.who[0]].GetComponent<Character>();
        string newSpeach;
        if (actualDialogue.who[0] != 0)
        {
            newSpeach = actualDialogue.speach[0];
        }

        else
        {
            playerAnswers = null;
            playerAnswers = actualDialogue.speach[0].Split("&&", StringSplitOptions.None);

            if (expressionChosen == 'h')
                newSpeach = playerAnswers[0];

            else
                newSpeach = playerAnswers[1];
        }

        LowerHud.Instance.ShowSpeach(newSpeach, actualCharacter.characterName, actualCharacter.nameColor, actualCharacter.love);

        ExpressionButton("h");

    }

    public void NextSpeach()
    {

        if (canAdvance)
        {
            canAdvance = false;
            nextButton.interactable = false;

            if (actualDialogue.who[actualDialogue.speachNumber] != 0)
            {
                if (expressionChosen != emotionFelt)
                {
                    masks++;
                    masksAnimation.SetActive(true);
                    faceSource.clip = audioClips[4];
                    faceSource.Play();
                }

                if (expressionChosen == actualDialogue.expectedExpression[actualDialogue.speachNumber])
                {
                    characters[actualDialogue.who[actualDialogue.speachNumber]].GetComponent<Character>().love += +actualDialogue.lovePoints[actualDialogue.speachNumber];
                    hearts.SetActive(true);
                }
            }

                canAdvance = false;
            actualDialogue.speachNumber++;

            if(actualDialogue.speachNumber < actualDialogue.speach.Count)
            {
                string newSpeach;
                int n = actualDialogue.speachNumber;
                if (actualDialogue.who[n] != 0)
                {
                    newSpeach = actualDialogue.speach[n];
                }

                else
                {
                    playerAnswers = null;
                    playerAnswers = actualDialogue.speach[n].Split("&&", StringSplitOptions.None);

                    if (expressionChosen == 'h')
                        newSpeach = playerAnswers[0];

                    else
                        newSpeach = playerAnswers[1];                       
                }

                emotionFelt = actualDialogue.feeling[n];
                Character actualCharacter = characters[actualDialogue.who[n]].GetComponent<Character>();
                LowerHud.Instance.ShowSpeach(newSpeach, actualCharacter.characterName, actualCharacter.nameColor, actualCharacter.love);

            }

            else
            {
                NextStage();
            }
         
        }
    }

    IEnumerator CallCharacter(int character, int entranceWay)
    {
        Debug.Log("call character");
        GameObject newCharacter = Instantiate(characters[character]);
        newCharacter.transform.SetParent(charactersParent.transform, false);
        RectTransform rt = newCharacter.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition = Vector2.zero; // Posição no canvas
            rt.localScale = Vector3.one; // Scale correto
        }

        yield return new WaitForSeconds(1);
        newCharacter.GetComponent<Character>().CharacterEnter(entranceWay);
        characterInScene.Add(newCharacter);
    }

    IEnumerator CharacterExit(int n)
    {
        characterInScene[n].GetComponent<Character>().CharacterExit();
        yield return new WaitForSeconds(0.5f);
        characterInScene.RemoveAt(n);
        NextStage();
    }

    public void AllowAdvance()
    {
        canAdvance = true;
        nextButton.interactable = true;
    }

    public void ExpressionButton(string c)
    {
        expressionChosen = c[0];

        int n;

        if (expressionChosen == 'h')
        {
            selection[0].SetActive(true);
            selection[1].SetActive(false);

            if (expressionChosen == emotionFelt)
                faceAnimator.SetInteger("Status", 1);

            else
                faceAnimator.SetInteger("Status", 0);

            if(actualDialogue.who[actualDialogue.speachNumber] == 0)
            {
                n = actualDialogue.speachNumber;
                Character actualCharacter = characters[actualDialogue.who[n]].GetComponent<Character>();
                LowerHud.Instance.ShowSpeach(playerAnswers[0], actualCharacter.characterName, actualCharacter.nameColor, actualCharacter.love);
            }
        }

        else if(expressionChosen == 'f')
        {
            selection[1].SetActive(true);
            selection[0].SetActive(false);

            if (expressionChosen == emotionFelt)
                faceAnimator.SetInteger("Status", 3);

            else
                faceAnimator.SetInteger("Status", 2);

            if (actualDialogue.who[actualDialogue.speachNumber] == 0)
            {
                n = actualDialogue.speachNumber;
                Character actualCharacter = characters[actualDialogue.who[n]].GetComponent<Character>();
                LowerHud.Instance.ShowSpeach(playerAnswers[1], actualCharacter.characterName, actualCharacter.nameColor, actualCharacter.love);
            }
        }
    }

    public void SetPlayerName(string newName)
    {
        characters[0].GetComponent<Character>().characterName = newName;
        Debug.Log("New Player Name = " + newName);
    }

    void ChangeBackground(int n)
    {
        backgroundImage.sprite = sprites[n];
    }
}
