using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum TileStrength
{
    NONE,
    MINIMAL,
    QUARTER,
    HALF,
    FULL
}

public class GridManager : MonoBehaviour
{
    // dimensions of the grid layout will always be same row-wise and column-wise
    [SerializeField] private int dimensions = 32;

    // prefab of the mining tile
    [SerializeField] private GameObject MiningTile;

    public TileBehaviour[,] tileMatrix;

    // Tile number taken after carefully analyzing which ones will be the most user experience oriented
    public static readonly int[] maxResourceTileNumber = { 67, 81, 93, 170, 311, 317, 325, 331, 337, 526, 533, 540, 552, 715, 721, 728, 772, 908, 914, 920};

    // List of the max resource tile
    List<int> maxResourceTileIndices;

    public Sprite MinimalResourcesSprite;
    public Sprite QuarterResourcesSprite;
    public Sprite HalfResourcesSprite;
    public Sprite FullResourcesSprite;

    // Lazy singleton for now
    /// <summary>
    /// Lazy singleton method to keep Monobehavior also
    /// </summary>
    private static GridManager _instance;

    /// <summary>
    /// Get the instance of this grid
    /// </summary>
    /// <returns></returns>
    public static GridManager GetInstance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        maxResourceTileIndices = new List<int>();
        tileMatrix = new TileBehaviour[dimensions, dimensions];
        GenerateTileGrid();
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
                tile.tileStrength = TileStrength.MINIMAL;
                tile.GetComponent<Image>().sprite = MinimalResourcesSprite;
                tileMatrix[i, j] = tile;
            }
        }

        // Generate the 10 index out of the 20 provided in the array Randomly
        GenerateResourceIndexList();

        // Place resources around the generated max tile indices
        PlaceResources();
    }

    /// <summary>
    /// This function places the max (main), half and quarter resources randomly across the grid.
    /// Initially choose max points randomly, and surround them with the degraded resources.
    /// </summary>
    private void GenerateResourceIndexList()
    {
        while (maxResourceTileIndices.Count < 10)
        {
            int generatedIndex = Random.Range(0, maxResourceTileNumber.Length);

            if (!maxResourceTileIndices.Contains(generatedIndex))
            {
                maxResourceTileIndices.Add(generatedIndex);
            }
        }
    }


    /// <summary>
    /// Place the resources at and around the max tiles
    /// </summary>
    private void PlaceResources()
    {
        // Make sure the list of max res. tiles is populated
        if (maxResourceTileIndices.Count > 0)
        {
            // for each of those randomly generated tile index
            for (int i = 0; i < maxResourceTileIndices.Count; i++)
            {
                // compare with the tile number
                foreach (var tile in tileMatrix)
                {
                    // comparision between tile number of its array and the static max resource tile array [randomly generated index]
                    if (tile.tileNumber == maxResourceTileNumber[maxResourceTileIndices[i]])
                    {
                        //tile.GetComponent<Image>().color = Color.yellow;
                        tile.tileStrength = TileStrength.FULL;
                        tile.GetComponent<Image>().sprite = FullResourcesSprite;
                        PlaceSurroundingResources(tile);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Place the surrounding tiles to that tile
    /// </summary>
    /// <param name="tile"></param>
    private void PlaceSurroundingResources(TileBehaviour tile)
    {
        // get Row and Column
        int row = tile.coordinates.x;
        int column = tile.coordinates.y;

        // get the origin point of the row and column
        int originRow = row - 2;
        int originColumn = column - 2;

        for (int i = originRow; i < originRow + 5; i++)
        {
            for (int j = originColumn; j < originColumn + 5; j++)
            {
                // 0 row/column, or n - 1 row/column (border) .... I know 5 - 1 is 4, but this is for algorithmic purposes
                if (i == originRow || i == originRow + 5 - 1 || j == originColumn || j == originColumn + 5 - 1)
                {
                    //tileMatrix[i,j].GetComponent<Image>().color = Color.magenta;
                    tileMatrix[i, j].tileStrength = TileStrength.QUARTER;
                    tileMatrix[i, j].GetComponent<Image>().sprite = QuarterResourcesSprite;
                }
                else if (i != row || j != column)   // if its not the centre tile (origin point of max resource)
                {
                    // these tiles are not the border, but just inside the border AND neither they are the centre
                    //tileMatrix[i, j].GetComponent<Image>().color = Color.red;
                    tileMatrix[i, j].tileStrength = TileStrength.HALF;
                    tileMatrix[i, j].GetComponent<Image>().sprite = HalfResourcesSprite;
                }
                //else
                //{
                //    // this tile is the centre
                //    tileMatrix[i,j].GetComponent<Image>().color = Color.yellow;
                //}
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
                    //tileMatrix[i, j].GetComponent<Image>().color = Color.green;
                }
                else
                {
                    Debug.LogWarning("Off coordinates found = " + i + "," + j);
                }
            }
        }
    }

    /// <summary>
    /// Extract the clicked tile and degrade the 2 rings of tiles around it
    /// </summary>
    public void ExtractTile(TileBehaviour clickedTile)
    {
        // get row and column
        int row = clickedTile.coordinates.x;
        int column = clickedTile.coordinates.y;

        // get the origin point of scanning row and column
        int originRow = row - 2;
        int originColumn = column - 2;

        for (int i = originRow; i < originRow + 5; i++)
        {
            for (int j = originColumn; j < originColumn + 5; j++)
            {
                if (i >= 0 && i < dimensions && j >= 0 && j < dimensions)
                {
                    tileMatrix[i, j].Degrade();
                }
                else
                {
                    Debug.LogWarning("Off coordinates found = " + i + "," + j);
                }
            }
        }

        clickedTile.Extract();
    }
}
