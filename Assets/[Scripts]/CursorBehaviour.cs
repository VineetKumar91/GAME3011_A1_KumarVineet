using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Cursor Textures
    public Texture2D scanModeCursorTexture;
    public Texture2D extractModeCursorTexture;

    private static CursorBehaviour _instance;

    public bool isOnGrid;


    /// <summary>
    /// Getter for instance
    /// </summary>
    /// <returns></returns>
    public static CursorBehaviour GetInstance()
    {
        return _instance;
    }

    /// <summary>
    /// Start function
    /// </summary>
    public void Start()
    {
        _instance = this;
        isOnGrid = false;
    }

    /// <summary>
    /// When pointer enters the grid, change cursor to convey the current mode
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isOnGrid = true;
        ChangeCursor();
    }

    /// <summary>
    /// When pointer exits, set it to default
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        isOnGrid = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// Change cursor function
    /// </summary>
    public void ChangeCursor()
    {
        // Scan Mode cursors
        if (GameManager.GetInstance().GetCurrentMode() == Mode.SCAN)
        {
            Cursor.SetCursor(scanModeCursorTexture,
                new Vector2(scanModeCursorTexture.width / 4, scanModeCursorTexture.height / 4),
                CursorMode.ForceSoftware);
        }
        else if (GameManager.GetInstance().GetCurrentMode() == Mode.EXTRACT) // Extract mode cursor
        {
            Cursor.SetCursor(extractModeCursorTexture, new Vector2(0, scanModeCursorTexture.height / 2),
                CursorMode.ForceSoftware);
        }
        else // Default one
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
