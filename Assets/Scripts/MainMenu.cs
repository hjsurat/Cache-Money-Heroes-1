using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button startText;
    public Button exitText;
    public Button highscoreText;

    // Use this for initialization
    void Start()
    {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        highscoreText = highscoreText.GetComponent<Button>();
    }

    public void ExitPress() //this function will be used on our Exit button
    {
        startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
        exitText.enabled = false;
        highscoreText.enabled = false;
        Application.Quit(); //this will quit our game. Note this will only work after building the game
    }
    public void StartLevel() //this function will be used on our Play button
    {
        SceneManager.LoadScene("Outdoor"); //this will load our first level from our build settings. "1" is the second scene in our game
    }

    public void HighScorePress()
    {
        SceneManager.LoadScene("High Scores");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
