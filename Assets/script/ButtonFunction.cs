using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void About()
    {
        SceneManager.LoadScene("about");
    }

    

    public void Storyline()
    {
        SceneManager.LoadScene("storyline");
    }

    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }

        public void Scores()
    {
        SceneManager.LoadScene("scores");
    }

    public void Level1()
    {
        SceneManager.LoadScene("level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("level 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("level 3");
    }

    public void Close()
    {
        Application.Quit();
    }
}
