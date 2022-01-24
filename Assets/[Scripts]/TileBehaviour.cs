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

    // Start is called before the first frame update
    void Start()
    {
        
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
        GridManager.GetInstance().ExtractTile(this);
        Debug.Log("Tile strength of clicked tile = " + tileStrength);
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
        if (tileStrength == TileStrength.FULL)
        {
            tileStrength = TileStrength.HALF;
            GetComponent<Image>().sprite = GridManager.GetInstance().HalfResourcesSprite;
        }
        else if (tileStrength == TileStrength.HALF)
        {
            tileStrength = TileStrength.QUARTER;
            GetComponent<Image>().sprite = GridManager.GetInstance().QuarterResourcesSprite;
        }
        else if (tileStrength == TileStrength.QUARTER)
        {
            tileStrength = TileStrength.MINIMAL;
            GetComponent<Image>().sprite = GridManager.GetInstance().MinimalResourcesSprite;
        }
        else if (tileStrength == TileStrength.MINIMAL)
        {
            return;
        }
    }
}
