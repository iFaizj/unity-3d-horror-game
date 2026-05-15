using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject extraMenu;
    private GameObject Loading;
    
    public AudioSource buttonSound;
    public AudioSource backgroundMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusic.Play();

        mainMenu = GameObject.Find("MainMenuCanvas");
        extraMenu = GameObject.Find("ExtraMenuCanvas");
        Loading = GameObject.Find("LoadingCanvas");

        mainMenu.GetComponent<Canvas>().enabled = true;
        extraMenu.GetComponent<Canvas>().enabled = false;
        Loading.GetComponent<Canvas>().enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton(){
        Loading.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
        buttonSound.Play();
        backgroundMusic.Stop();
        SceneManager.LoadScene("SampleScene");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExtraButton(){
        buttonSound.Play();
        extraMenu.GetComponent<Canvas>().enabled = true;
        mainMenu.GetComponent<Canvas>().enabled = false;
    }

    public void QuitGameButton(){
        buttonSound.Play();
        Application.Quit();
        Debug.Log("App Exit");
    }

    public void ReturnToMenuButton(){
        buttonSound.Play();
        mainMenu.GetComponent<Canvas>().enabled = true;
        extraMenu.GetComponent<Canvas>().enabled = false;
        Loading.GetComponent<Canvas>().enabled = false;
    }
}
