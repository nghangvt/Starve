using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuFunction : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void Setting()
    {
        SceneManager.LoadScene("SettingMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
