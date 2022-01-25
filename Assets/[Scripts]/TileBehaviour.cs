using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector2Int coordinates;
    public int tileNumber = 0;
    public TileStrength tileStrength = TileStrength.MINIMAL;
    public bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When cursor hovers on a tile, highlight it to convey the information to the player
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(tileNumber);
    }
    
    /// <summary>
    /// When cursor is not hovering on the tile, return back to normal color
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {

    }

    /// <summary>
    /// When player clicks on a tile, highlight the surrounding tiles
    /// Highlighting a total of 9 tiles
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Tile number " + tileNumber + " at coordinates " + coordinates.x + "," + coordinates.y + " selected.");
        //GridManager.GetInstance().ExtractTile(this);
        //GridManager.GetInstance().ScanTiles(this);
        //Debug.Log("Tile strength of clicked tile = " + tileStrength);

        Debug.Log("Current mode is " + GameManager.GetInstance().GetCurrentMode());

        // specifically check for conditions as after 3 extractions the game will be over
        // after 6 scans, scan mode is no longer permitted
        if (GameManager.GetInstance().GetCurrentMode() == Mode.SCAN)
        {
            GridManager.GetInstance().ScanTiles(this);
        }
        else if (GameManager.GetInstance().GetCurrentMode() == Mode.EXTRACT)
        {
            GridManager.GetInstance().ExtractTile(this);
        }
    }

    /// <summary>
    /// Sets tile strength directly, generally useful for initializing
    /// </summary>
    /// <param name="strength"></param>
    public void SetTileStrength(TileStrength strength)
    {
        tileStrength = strength;
    }

    /// <summary>
    /// Get/Returns tile strength
    /// </summary>
    /// <returns></returns>
    public TileStrength GetTileStrength()
    {
        return tileStrength;
    }

    /// <summary>
    /// Extracts, means directly reduces the strength to minimal
    /// </summary>
    public void Extract()
    {
        tileStrength = TileStrength.MINIMAL;

        if (!isVisible)
        {
            isVisible = true;
        }

        GetComponent<Image>().sprite = GridManager.GetInstance().MinimalResourcesSprite;
    }

    /// <summary>
    /// Degrade the tile by 1 level below
    /// Full    becomes half
    /// Half    becomes quarter
    /// Quarter becomes minimal
    /// Minimal stays minimal
    /// </summary>
    public void Degrade()
    {
        // Full
        if (tileStrength == TileStrength.FULL)
        {
            // reduce the tile strength
            tileStrength = TileStrength.HALF;

            if (isVisible)
            {
                GetComponent<Image>().sprite = GridManager.GetInstance().HalfResourcesSprite;
            }
        }
        else if (tileStrength == TileStrength.HALF)         // Half
        {
            // reduce the tile strength
            tileStrength = TileStrength.QUARTER;

            if (isVisible)
            {
                GetComponent<Image>().sprite = GridManager.GetInstance().QuarterResourcesSprite;
            }
        }
        else if (tileStrength == TileStrength.QUARTER)      // Quarter
        {
            // reduce the tile strength
            tileStrength = TileStrength.MINIMAL;

            if (isVisible)
            {
                GetComponent<Image>().sprite = GridManager.GetInstance().MinimalResourcesSprite;
            }
        }
        else if (tileStrength == TileStrength.MINIMAL)      // Minimal
        {
            return;
        }
    }


    /// <summary>
    /// Scan reveals the tile along with one ring of tile around it
    /// it flags the tile of the grid as Visible for the rest of the turns
    /// </summary>
    public void Scan()
    {
        isVisible = true;

        if (tileStrength == TileStrength.FULL)
        { 
            GetComponent<Image>().sprite = GridManager.GetInstance().FullResourcesSprite;
        }
        else if (tileStrength == TileStrength.HALF)
        {
            GetComponent<Image>().sprite = GridManager.GetInstance().HalfResourcesSprite;
        }
        else if (tileStrength == TileStrength.QUARTER)
        {
            GetComponent<Image>().sprite = GridManager.GetInstance().QuarterResourcesSprite;
        }
        else if (tileStrength == TileStrength.MINIMAL)
        {
            GetComponent<Image>().sprite = GridManager.GetInstance().MinimalResourcesSprite;
        }
    }
}
