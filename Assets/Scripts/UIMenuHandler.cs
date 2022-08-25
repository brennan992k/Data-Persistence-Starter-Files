using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class UIMenuHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField NameInputField;
    [SerializeField] TMP_Text BestScoreText;
    [SerializeField] Button StartButton;
    [SerializeField] Button QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.LoadData();
        BestScoreText.gameObject.SetActive(false);
        StartButton.onClick.AddListener(startGame);
        QuitButton.onClick.AddListener(quitGame);
        NameInputField.onValueChanged.AddListener(changedName);
        shouldShowBestScore();
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void startGame()
    {
        SceneManager.LoadScene(1);
    }

    private void quitGame()
    {
        DataManager.Instance.SaveData();

#if (UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void changedName(string name)
    {
        DataManager.Instance.ChangeName(name);
    }

    void shouldShowBestScore()
    {
        if (DataManager.Instance.BestScore != 0 && DataManager.Instance.BestScoreName != "")
        {
            BestScoreText.text = "Best score: " + DataManager.Instance.BestScoreName + " " + DataManager.Instance.BestScore;
            BestScoreText.gameObject.SetActive(true);
        }
    }

    void defaultName()
    {
        if (DataManager.Instance.Name != "")
        {
            NameInputField.text = DataManager.Instance.Name;
        }
    }
}
