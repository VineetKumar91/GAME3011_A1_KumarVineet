using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Mode
{
    SCAN,
    EXTRACT
}

public class GameManager : MonoBehaviour
{

    // Scan and Toggle mode components
    [Header("Toggle Mode")]
    [SerializeField] private Toggle scanToggle;
    [SerializeField] private Toggle extractToggle;
    [Header("UI Content")]
    [SerializeField] private Image currentModeImage;
    [SerializeField] private Sprite scanSymbol;
    [SerializeField] private Sprite extractSymbol;

    // current mode
    private Mode currentMode;

    // All the status of the game
    [Header("Status Tracker")]
    public TextMeshProUGUI TotalScore;
    [HideInInspector]
    public int totalScoreValue = 0;

    public TextMeshProUGUI minResCollectedText;
    [HideInInspector]
    public int minResCollected = 0;

    public TextMeshProUGUI quarterResCollectedText;
    [HideInInspector]
    public int quarterResCollected = 0;

    public TextMeshProUGUI halfResCollectedText;
    [HideInInspector]
    public int halfResCollected = 0;

    public TextMeshProUGUI fullResCollectedText;
    [HideInInspector]
    public int fullResCollected = 0;

    public TextMeshProUGUI scansRemainingText;
    public int scansRemaining = 6;

    public TextMeshProUGUI extractsRemainingText;
    public int extractsRemaining = 3;

    [Header("Out Of Scans Prompt")] 
    public TextMeshProUGUI OutOfScansText;

    [Header("Game Over")] 
    public GameObject GameOverPanel;

    public TextMeshProUGUI finalScoreText;

    /// <summary>
    /// Lazy Singleton
    /// </summary>
    private static GameManager _instance;

    /// <summary>
    /// Lazy Singleton get instance method
    /// </summary>
    /// <returns>Game Manager Instance</returns>
    public static GameManager GetInstance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        // Set scan mode on and extract mode off by default
        scanToggle.isOn = true;
        extractToggle.isOn = false;

        // set current mode to 
        currentMode = Mode.SCAN;
    }

    // Update function
    private void Update()
    {
        // Get input from mouse scroll wheel and do the needful
        GetMouseScrollWheelInput();
    }


    /// <summary>
    /// Getter for the current mode
    /// </summary>
    /// <returns></returns>
    public Mode GetCurrentMode()
    {
        return currentMode;
    }

    /// <summary>
    /// Scan Mode on toggle function
    /// </summary>
    public void ScanModeOn()
    {
        // Set current mode
        currentMode = Mode.SCAN;

        // change the toggle color
        scanToggle.GetComponentInChildren<Image>().color = Color.cyan;
        extractToggle.GetComponentInChildren<Image>().color = Color.white;

        // set the current mode image
        currentModeImage.sprite = scanSymbol;
    }

    /// <summary>
    /// Extract Mode on toggle function
    /// </summary>
    public void ExtractModeOn()
    {
        // Set current mode
        currentMode = Mode.EXTRACT;

        // change the toggle color
        extractToggle.GetComponentInChildren<Image>().color = Color.cyan;
        scanToggle.GetComponentInChildren<Image>().color = Color.white;

        // set the current mode image
        currentModeImage.sprite = extractSymbol;
    }


    /// <summary>
    /// Updates overall stats
    /// </summary>
    public void UpdateStats()
    {
        TotalScore.text = totalScoreValue.ToString();
        minResCollectedText.text = minResCollected.ToString();
        quarterResCollectedText.text = quarterResCollected.ToString();
        halfResCollectedText.text = halfResCollected.ToString();
        fullResCollectedText.text = fullResCollected.ToString();
    }

    /// <summary>
    /// Get Mouse scroll Input
    /// </summary>
    public void GetMouseScrollWheelInput()
    {
        // If Mouse scroll wheel goes up or down
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentMode == Mode.SCAN)
            {
                ExtractModeOn();

                if (CursorBehaviour.GetInstance().isOnGrid)
                {
                    CursorBehaviour.GetInstance().ChangeCursor();
                }
            }
            else if (currentMode == Mode.EXTRACT)
            {
                ScanModeOn();

                if (CursorBehaviour.GetInstance().isOnGrid)
                {
                    CursorBehaviour.GetInstance().ChangeCursor();
                }
            }
        }
    }

    /// <summary>
    /// Activate that TMP of out of scans text
    /// </summary>
    public void ActivateOutOfScans(bool status)
    {
        OutOfScansText.gameObject.SetActive(status);
    }

    /// <summary>
    /// Activate game over panel
    /// </summary>
    public void ActivateGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        finalScoreText.text = totalScoreValue.ToString();
    }


    /// <summary>
    /// On Button Press Retry
    /// </summary>
    public void ButtonPress_Retry()
    {
        GridManager.GetInstance().RestartGrid();
        RestartGame();
    }

    /// <summary>
    /// On Button Press Retry
    /// </summary>
    public void ButtonPress_Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif


    }



    /// <summary>
    /// Restart game
    /// </summary>
    public void RestartGame()
    {
        // Set scan mode on and extract mode off by default
        scanToggle.isOn = true;
        extractToggle.isOn = false;

        // set current mode to 
        currentMode = Mode.SCAN;

        // values
        scansRemaining = 6;
        scansRemainingText.text = scansRemaining.ToString();
        extractsRemaining = 3;
        extractsRemainingText.text = extractsRemaining.ToString();
        totalScoreValue = 0;
        minResCollected = 0;
        quarterResCollected = 0;
        halfResCollected = 0;
        fullResCollected = 0;

        // Update TMP stats
        UpdateStats();

        // Deactivate panel and out of scans text
        ActivateOutOfScans(false);
        GameOverPanel.SetActive(false);

        finalScoreText.text = "0";
    }
}
