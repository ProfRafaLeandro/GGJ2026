using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    string playerName;
    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputFieldChanged);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if ((SceneManager.GetActiveScene().name == "Game"))
        {
            GameControl.Instance.SetPlayerName(playerName);
            Debug.Log("trying to change the name to " +  playerName);
            Destroy(gameObject);
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnInputFieldChanged(string newName)
    {
        playerName = newName;
        Debug.Log(playerName);
    }
}
