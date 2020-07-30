using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public GameObject menuPanel;
    public GameObject instructionsPanel;
    public GameObject creditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton(){
         SceneManager.LoadScene("SampleScene");
    }
    public void InstructionsButton(){
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }
    public void QuitInstructionsButton(){
        menuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
    }
    public void CreditsButton(){
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void CreditsQuitButton(){
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    public void QuitButton(){
        Application.Quit();
    }
}
