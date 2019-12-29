using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    public GameObject[] allTiles;
    public GameObject[] top;
    public GameObject[] earth;
    public GameObject[] subEarth;
    public GameObject[] bottom;
    public GameObject[,] tileGrid;

    private Dictionary<int, int> mountainHeights;

    //Uses integers to mark what each tile should be
    //0 = NULL
    //1 = Grass
    //2 = Dirt
    //3 = Clay
    //4 = Rock
    public Dictionary<Vector2, int> tileMap;

    //Holds the GameObjects for tiles that are being rendered
    private Dictionary<Vector2, GameObject> activeTiles;

    public GameObject dwarf;

    public int renderDistance;

    public int minWidth;
    public int minHeight;
    public int width;
    public int height;
    private int seed;
    public float biomeSize;
    public float scale;
    public bool guaranteeFlatSection;

    public GameObject getTile(Vector3 pos)
    {
        Vector2 tilePos = new Vector2((int)(pos.x + 70.5), (int)(pos.y * -1 + 48));
        return activeTiles[tilePos];

        //return tileGrid[(int)(pos.x + 70.5), (int)(pos.y * -1) + 48];
    }

    public void generateTile(Vector2 createPos, Vector2 mapPos)
    {
        if ((int)createPos.x > width)
        {
            width = (int)createPos.x;
        }
        if ((int)createPos.x < minWidth)
        {
            minWidth = (int)createPos.x;
        }
        if ((int)createPos.y * -1 > height)
        {
            height = (int)createPos.y * -1;
        }
        if ((int)createPos.y * -1 < minHeight)
        {
            minHeight = (int)createPos.y * -1;
        }

        float x = createPos.x / biomeSize * scale;
        float y = createPos.y / biomeSize * scale;
        float perlin = Mathf.PerlinNoise(x + seed, y + seed);

        if (!mountainHeights.ContainsKey((int)createPos.x))
        {
            if (mountainHeights.ContainsKey((int)createPos.x + 1))
            {
                mountainHeights.Add((int)createPos.x, ((int)Mathf.Ceil(100 * perlin) + mountainHeights[(int)createPos.x + 1]) / 2);
                Debug.Log(createPos.x + ", " + mountainHeights[(int)createPos.x]);
            }
            else if (mountainHeights.ContainsKey((int)createPos.x - 1))
            {
                mountainHeights.Add((int)createPos.x, ((int)Mathf.Ceil(100 * perlin) + mountainHeights[(int)createPos.x - 1]) / 2);
                Debug.Log(createPos.x + ", " + mountainHeights[(int)createPos.x]);
            }
        }

        int i = (int)createPos.x;
        int j = (int)createPos.y;

        if (createPos.y < 100)
        {
            if (j == mountainHeights[i])
            {
                //Grass
                tileMap.Add(new Vector2(i, j), 1);
            }
            else if (j > mountainHeights[i])
            {
                if (perlin < 0.33f)
                {
                    //Clay
                    tileMap.Add(new Vector2(i, j), 3);
                }
                else if (perlin < 0.66f)
                {
                    //Dirt
                    tileMap.Add(new Vector2(i, j), 2);
                }
                else
                {
                    //Dirt
                    tileMap.Add(new Vector2(i, j), 2);
                }
            }
            else
            {
                //Ensure that every empty tile is initialized to the NULL value
                tileMap.Add(new Vector2(i, j), 0);
            }
        }
        else if (createPos.y >= 100 && createPos.y < 150)
        {
            if (perlin < 0.33f)
            {
                //Clay
                tileMap.Add(new Vector2(i, j), 3);
            }
            else if (perlin < 0.66f)
            {
                //Dirt
                tileMap.Add(new Vector2(i, j), 2);
            }
            else
            {
                //Dirt
                tileMap.Add(new Vector2(i, j), 2);
            }
        }
        else if (createPos.y >= 150 && createPos.y < 200)
        {
            if (perlin < 0.33f)
            {
                //Dirt
                tileMap.Add(new Vector2(i, j), 2);
            }
            else if (perlin < 0.66f)
            {
                //Clay
                tileMap.Add(new Vector2(i, j), 3);
            }
            else
            {
                //Rock
                tileMap.Add(new Vector2(i, j), 4);
            }
        }
        else
        {
            if (perlin < 0.33f)
            {
                //NULL
                tileMap.Add(new Vector2(i, j), 0);
            }
            else if (perlin < 0.66f)
            {
                //Clay
                tileMap.Add(new Vector2(i, j), 3);
            }
            else
            {
                //Rock
                tileMap.Add(new Vector2(i, j), 4);
            }
        }
    }

    public void renderLeft(Vector2 pos)
    {
        int xPos = (int)(pos.x + 70.5);
        int yPos = (int)(pos.y * -1) + 48;

        //The Dwarf has already moved one space to the right, so calculations are adjusted accordingly
        int newXPos = xPos - renderDistance;
        int oldXPos = xPos + renderDistance + 1;

        for (int j = yPos - renderDistance; j <= yPos + renderDistance; j++)
        {
            //Delete rendered tiles outside of render distance
            Vector2 deletePos = new Vector2(oldXPos, j);
            GameObject deleteTile = activeTiles[deletePos];

            activeTiles.Remove(deletePos);
            Destroy(deleteTile);

            //Instantiate new tiles within render distance
            Vector2 createPos = new Vector2(newXPos, j);
            Vector2 mapPos = new Vector2(transform.position.x + newXPos, transform.position.y - j);


            if (!tileMap.ContainsKey(createPos))
            {
                generateTile(createPos, mapPos);
            }

            GameObject tile;

            if (tileMap[createPos] == 0)
            {
                tile = null;
            }
            else
            {
                tile = Instantiate(allTiles[tileMap[createPos]], mapPos, Quaternion.identity);
            }

            activeTiles.Add(createPos, tile);


        }
    }

    public void renderRight(Vector2 pos)
    {
        int xPos = (int)(pos.x + 70.5);
        int yPos = (int)(pos.y * -1) + 48;

        //The Dwarf has already moved one space to the right, so calculations are adjusted accordingly
        int newXPos = xPos + renderDistance;
        int oldXPos = xPos - renderDistance - 1;

        for (int j = yPos - renderDistance; j <= yPos + renderDistance; j++)
        {
            //Delete rendered tiles outside of render distance
            Vector2 deletePos = new Vector2(oldXPos, j);
            GameObject deleteTile = activeTiles[deletePos];

            activeTiles.Remove(deletePos);
            Destroy(deleteTile);

            //Instantiate new tiles within render distance
            Vector2 createPos = new Vector2(newXPos, j);
            Vector2 mapPos = new Vector2(transform.position.x + newXPos, transform.position.y - j);


            if (!tileMap.ContainsKey(createPos))
            {
                generateTile(createPos, mapPos);
            }

            GameObject tile;

            if (tileMap[createPos] == 0)
            {
                tile = null;
            }
            else
            {
                tile = Instantiate(allTiles[tileMap[createPos]], mapPos, Quaternion.identity);
            }

            activeTiles.Add(createPos, tile);


        }
    }

    public void renderUp(Vector2 pos)
    {
        int xPos = (int)(pos.x + 70.5);
        int yPos = (int)(pos.y * -1) + 48;

        //The Dwarf has already moved one space to the right, so calculations are adjusted accordingly
        int newYPos = yPos - renderDistance;
        int oldYPos = yPos + renderDistance + 1;

        for (int i = xPos - renderDistance; i <= xPos + renderDistance; i++)
        {
            //Delete rendered tiles outside of render distance
            Vector2 deletePos = new Vector2(i, oldYPos);
            GameObject deleteTile = activeTiles[deletePos];

            activeTiles.Remove(deletePos);
            Destroy(deleteTile);

            //Instantiate new tiles within render distance
            Vector2 createPos = new Vector2(i, newYPos);
            Vector2 mapPos = new Vector2(transform.position.x + i, transform.position.y - newYPos);


            if (!tileMap.ContainsKey(createPos))
            {
                generateTile(createPos, mapPos);
            }

            GameObject tile;

            if (tileMap[createPos] == 0)
            {
                tile = null;
            }
            else
            {
                tile = Instantiate(allTiles[tileMap[createPos]], mapPos, Quaternion.identity);
            }

            activeTiles.Add(createPos, tile);


        }
    }

    public void renderDown(Vector2 pos)
    {
        int xPos = (int)(pos.x + 70.5);
        int yPos = (int)(pos.y * -1) + 48;

        //The Dwarf has already moved one space to the right, so calculations are adjusted accordingly
        int newYPos = yPos + renderDistance;
        int oldYPos = yPos - renderDistance - 1;

        for (int i = xPos - renderDistance; i <= xPos + renderDistance; i++)
        {
            //Delete rendered tiles outside of render distance
            Vector2 deletePos = new Vector2(i, oldYPos);
            GameObject deleteTile = activeTiles[deletePos];

            activeTiles.Remove(deletePos);
            Destroy(deleteTile);

            //Instantiate new tiles within render distance
            Vector2 createPos = new Vector2(i, newYPos);
            Vector2 mapPos = new Vector2(transform.position.x + i, transform.position.y - newYPos);


            if (!tileMap.ContainsKey(createPos))
            {
                generateTile(createPos, mapPos);
            }

            GameObject tile;

            if (tileMap[createPos] == 0)
            {
                tile = null;
            }
            else
            {
                tile = Instantiate(allTiles[tileMap[createPos]], mapPos, Quaternion.identity);
            }

            activeTiles.Add(createPos, tile);
        }
    }

    private void Start()
    {
        seed = Random.Range(-2000000, 2000000);

        tileGrid = new GameObject[width, height];

        mountainHeights = new Dictionary<int, int>();
        tileMap = new Dictionary<Vector2, int>();
        activeTiles = new Dictionary<Vector2, GameObject>();

        //Generate Perlin Biomes
        for (int i = 0; i < width; i++)
        {
            //determine the mountain height at a given level
            if (i >= width / 2 - 16 && i <= width / 2 + 15 && guaranteeFlatSection)
            {
                mountainHeights.Add(i, 50);
            }

            for (int j = 0; j < height; j++)
            {
                float x = i / biomeSize * scale;
                float y = j / biomeSize * scale;
                float perlin = Mathf.PerlinNoise(x + seed, y + seed);
                Vector2 pos = new Vector2(transform.position.x + i, transform.position.y - j);

                if (j < 100)
                {
                    if (!mountainHeights.ContainsKey(i))
                    {
                        //find height of previous mountain
                        int previousMountain = 50;
                        if (i > 0)
                        {
                            previousMountain = mountainHeights[i - 1];
                        }

                        mountainHeights.Add(i, ((int)Mathf.Ceil(100 * perlin) + previousMountain) / 2);
                    }

                    if (j == mountainHeights[i])
                    {
                        //Grass
                        tileMap.Add(new Vector2(i, j), 1);
                    }
                    else if (j > mountainHeights[i])
                    {
                        if (perlin < 0.33f)
                        {
                            //Clay
                            tileMap.Add(new Vector2(i, j), 3);
                        }
                        else if (perlin < 0.66f)
                        {
                            //Dirt
                            tileMap.Add(new Vector2(i, j), 2);
                        }
                        else
                        {
                            //Dirt
                            tileMap.Add(new Vector2(i, j), 2);
                        }
                    }
                    else
                    {
                        //Ensure that every empty tile is initialized to the NULL value
                        tileMap.Add(new Vector2(i, j), 0);
                    }
                }
                else if (j >= 100 && j < 150)
                {
                    //int rand = Random.Range(0, earth.Length);
                    if (perlin < 0.33f)
                    {
                        //Clay
                        tileMap.Add(new Vector2(i, j), 3);
                    }
                    else if (perlin < 0.66f)
                    {
                        //Dirt
                        tileMap.Add(new Vector2(i, j), 2);
                    }
                    else
                    {
                        //Dirt
                        tileMap.Add(new Vector2(i, j), 2);
                    }
                }
                else if (j >= 150 && j < 200)
                {
                    //int rand = Random.Range(0, earth.Length);
                    if (perlin < 0.33f)
                    {
                        //Dirt
                        tileMap.Add(new Vector2(i, j), 2);
                    }
                    else if (perlin < 0.66f)
                    {
                        //Clay
                        tileMap.Add(new Vector2(i, j), 3);
                    }
                    else
                    {
                        //Rock
                        tileMap.Add(new Vector2(i, j), 4);
                    }
                }
                else
                {
                    //int rand = Random.Range(0, earth.Length);

                    if (perlin < 0.33f)
                    {
                        //NULL
                        tileMap.Add(new Vector2(i, j), 0);
                    }
                    else if (perlin < 0.66f)
                    {
                        //Clay
                        tileMap.Add(new Vector2(i, j), 3);
                    }
                    else
                    {
                        //Rock
                        tileMap.Add(new Vector2(i, j), 4);
                    }
                }


            }
        }

        //Render Initial Tiles
        int xPos = (int)(dwarf.transform.position.x + 70.5);
        int yPos = (int)(dwarf.transform.position.y * -1) + 48;

        Debug.Log(xPos + ", " + yPos);

        for (int i = xPos - renderDistance; i <= xPos + renderDistance; i++)
        {
            for (int j = yPos - renderDistance; j <= yPos + renderDistance; j++)
            {
                Vector2 mapPos = new Vector2(transform.position.x + i, transform.position.y - j);
                Vector2 tilePos = new Vector2(i, j);

                GameObject tile;

                if (tileMap[tilePos] == 0)
                {
                    tile = null;
                }
                else
                {
                    tile = Instantiate(allTiles[tileMap[tilePos]], mapPos, Quaternion.identity);
                }
                
                activeTiles.Add(tilePos, tile);
            }
        }


    }

    public void Update()
    {

    }
}
