using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    // dimensions of the grid layout will always be same row-wise and column-wise
    [SerializeField] private int dimensions = 32;

    // prefab of the mining tile
    [SerializeField] private GameObject MiningTile;

    public TileBehaviour[,] tileMatrix;

    // Lazy singleton for now
    /// <summary>
    /// **NOTE** Please use professor Falsitta's Singleton template as that seems more better
    /// </summary>
    private static GridManager _instance;

    // Start is called before the first frame update
    void Start()
    {
        tileMatrix = new TileBehaviour[dimensions, dimensions];
        GenerateTileGrid();
        _instance = this;
    }

    public static GridManager GetInstance()
    {
        return _instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generate Tile Grid as per the given dimensions
    /// </summary>
    private void GenerateTileGrid()
    {
        int tileCounter = 0;
        for (int i = 0; i < dimensions; i++)
        {
            for (int j = 0; j < dimensions; j++)
            {
                TileBehaviour tile = Instantiate(MiningTile, transform).GetComponent<TileBehaviour>();
                tile.coordinates = new Vector2Int(i, j);
                tile.tileNumber = tileCounter++;
                tileMatrix[i, j] = tile;
            }
        }
    }


    /// <summary>
    /// Scan the surrounding tiles
    /// </summary>
    /// <param name="clickedTile"></param>
    public void ScanTiles(TileBehaviour clickedTile)
    {
        // get row and column
        int row = clickedTile.coordinates.x;
        int column = clickedTile.coordinates.y;

        // get the origin point of scanning row and column
        int originRow = row - 1;
        int originColumn = column - 1;

        for (int i = originRow; i < originRow + 3; i++)
        {
            for (int j = originColumn; j < originColumn + 3; j++)
            {
                if (i >= 0 && i < dimensions && j >= 0 && j < dimensions)
                {
                    tileMatrix[i, j].GetComponent<Image>().color = Color.green;
                }
                else
                {
                    Debug.Log("Off coordinates found = " + i + "," + j);
                }
            }
        }
    }
}
