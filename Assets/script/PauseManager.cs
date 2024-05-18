using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button buttonClicked;
    public Button resumeButton; // Tambahkan reference untuk tombol resume
    private bool isPaused = false;


    private void Start()
    {
        pauseMenu.SetActive(false);
        buttonClicked.onClick.AddListener(TogglePauseMenu);
        resumeButton.onClick.AddListener(ResumeGame); // Tambahkan event listener untuk tombol resume
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        // Atur interaksi tombol sesuai dengan status pause
        buttonClicked.interactable = !isPaused;
    }

    public void ResumeGame()
    {
        TogglePauseMenu(); // Panggil fungsi TogglePauseMenu untuk melanjutkan permainan
    }
}

