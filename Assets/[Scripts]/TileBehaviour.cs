using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector2Int coordinates;
    public int tileNumber = 0;

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
      // Debug.Log("Tile number " + tileNumber + " at coordinates " + coordinates.x + "," + coordinates.y + " selected.");
       GridManager.GetInstance().ScanTiles(this);
    }
}
