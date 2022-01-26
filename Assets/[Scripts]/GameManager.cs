using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    private void Update()
    {
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

    public void GetMouseScrollWheelInput()
    {

    }
}
