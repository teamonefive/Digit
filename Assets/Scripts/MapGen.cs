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

    private Dictionary<int, int> mountainHeights;

    //Holds the list of all tiles that have been destroyed by the player
    public Dictionary<Vector2, bool> destroyedTiles;

    //Holds the GameObjects for tiles that are being rendered
    public Dictionary<Vector2, GameObject> activeTiles;

    private Dictionary<Vector2, bool> isWaterNotLava;
    private Dictionary<Vector2, bool> generatedStone;

    public GameObject dwarf;

    public Stats stat;

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

        //old mapgen used 2d tile grid array
        //return tileGrid[(int)(pos.x + 70.5), (int)(pos.y * -1) + 48];
    }

    public Vector2 tilePos(Vector3 mapPos)
    {
        Vector2 pos = new Vector2((int)(mapPos.x + 70.5), (int)(mapPos.y * -1 + 48));
        return pos;
    }

    //Need to convert from mapPos to tilePos to call this function
    public void expandLiquid(Vector2 pos)
    {
        if (!isWaterNotLava.ContainsKey(pos))
        {
            return;
        }
        else if (isWaterNotLava[pos])
        {
            //water
            Vector2 left = pos + new Vector2(-1f, 0f);
            Vector2 right = pos + new Vector2(1f, 0f);
            Vector2 down = pos + new Vector2(0f, 1f);

            if (activeTiles.ContainsKey(left))
            {
                if (activeTiles[left] == null)
                {
                    isWaterNotLava[left] = true;
                    activeTiles[left] = generateTile(left, new Vector2(transform.position.x + left.x, transform.position.y - left.y));
                    expandLiquid(left);
                }
                else if (isWaterNotLava.ContainsKey(left))
                {
                    if (!isWaterNotLava[left])
                    {
                        //there is lava to the left of the expanding water
                        isWaterNotLava.Remove(left);
                        generatedStone[left] = true;
                        Destroy(activeTiles[left]);
                        activeTiles[left] = generateTile(left, new Vector2(transform.position.x + left.x, transform.position.y - left.y));
                    }
                }
            }
            else
            {
                activeTiles.Add(left, generateTile(left, new Vector2(transform.position.x + left.x, transform.position.y - left.y)));
                expandLiquid(pos);
                Destroy(activeTiles[left]);
                activeTiles.Remove(left);
            }
            if (activeTiles.ContainsKey(right))
            {
                if (activeTiles[right] == null)
                {
                    isWaterNotLava[right] = true;
                    activeTiles[right] = generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y));
                    expandLiquid(right);
                }
                else if (isWaterNotLava.ContainsKey(right))
                {
                    if (!isWaterNotLava[right])
                    {
                        //there is lava to the right of the expanding water
                        isWaterNotLava.Remove(right);
                        generatedStone[right] = true;
                        Destroy(activeTiles[right]);
                        activeTiles[right] = generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y));
                    }
                }
            }
            else
            {
                activeTiles.Add(right, generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y)));
                expandLiquid(pos);
                Destroy(activeTiles[right]);
                activeTiles.Remove(right);
            }
            if (activeTiles.ContainsKey(down))
            {
                if (activeTiles[down] == null)
                {
                    isWaterNotLava[down] = true;
                    activeTiles[down] = generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y));
                    expandLiquid(down);
                }
                else if (isWaterNotLava.ContainsKey(down))
                {
                    if (!isWaterNotLava[down])
                    {
                        //there is lava to the down of the expanding water
                        isWaterNotLava.Remove(down);
                        generatedStone[down] = true;
                        Destroy(activeTiles[down]);
                        activeTiles[down] = generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y));
                    }
                }
            }
            else
            {
                activeTiles.Add(down, generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y)));
                expandLiquid(pos);
                Destroy(activeTiles[down]);
                activeTiles.Remove(down);
            }
        }
        else
        {
            //lava
            Vector2 left = pos + new Vector2(-1f, 0f);
            Vector2 right = pos + new Vector2(1f, 0f);
            Vector2 down = pos + new Vector2(0f, 1f);

            if (activeTiles.ContainsKey(left))
            {
                if (activeTiles[left] == null)
                {
                    isWaterNotLava[left] = false;
                    activeTiles[left] = generateTile(left, new Vector2(transform.position.x + left.x, transform.position.y - left.y));
                    expandLiquid(left);
                }
                else if (isWaterNotLava.ContainsKey(left))
                {
                    if (isWaterNotLava[left])
                    {
                        //there is water to the left of the expanding lava
                        isWaterNotLava.Remove(pos);
                        generatedStone[pos] = true;
                        Destroy(activeTiles[pos]);
                        activeTiles[pos] = generateTile(pos, new Vector2(transform.position.x + pos.x, transform.position.y - pos.y));

                        return;
                    }
                }
            }
            else
            {
                activeTiles.Add(left, generateTile(left, new Vector2(transform.position.x + left.x, transform.position.y - left.y)));
                expandLiquid(pos);
                Destroy(activeTiles[left]);
                activeTiles.Remove(left);
            }
            if (activeTiles.ContainsKey(right))
            {
                if (activeTiles[right] == null)
                {
                    if (isWaterNotLava.ContainsKey(right + new Vector2(1f, 0f)))
                    {
                        if (isWaterNotLava[right + new Vector2(1f, 0f)])
                        {
                            //There is water to the right of the space which the lava wants to expand into. Let water go first.
                            isWaterNotLava[right] = true;
                            activeTiles[right] = generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y));
                            expandLiquid(right);
                        }
                        else
                        {
                            isWaterNotLava[right] = false;
                            activeTiles[right] = generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y));
                            expandLiquid(right);
                        }
                    }
                    else
                    {
                        isWaterNotLava[right] = false;
                        activeTiles[right] = generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y));
                        expandLiquid(right);
                    }

                }
                else if (isWaterNotLava.ContainsKey(right))
                {
                    if (isWaterNotLava[right])
                    {
                        //there is water to the right of the expanding lava
                        isWaterNotLava.Remove(pos);
                        generatedStone[pos] = true;
                        Destroy(activeTiles[pos]);
                        activeTiles[pos] = generateTile(pos, new Vector2(transform.position.x + pos.x, transform.position.y - pos.y));

                        return;
                    }
                }
            }
            else
            {
                activeTiles.Add(right, generateTile(right, new Vector2(transform.position.x + right.x, transform.position.y - right.y)));
                expandLiquid(pos);
                Destroy(activeTiles[right]);
                activeTiles.Remove(right);
            }
            if (activeTiles.ContainsKey(down))
            {
                if (activeTiles[down] == null)
                {
                    if (isWaterNotLava.ContainsKey(down + new Vector2(1f, 0f)))
                    {
                        if (isWaterNotLava[down + new Vector2(1f, 0f)])
                        {
                            //There is water to the right of the space which the lava wants to expand into. Let water go first.
                            isWaterNotLava[down] = true;
                            activeTiles[down] = generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y));
                            expandLiquid(down);

                            isWaterNotLava.Remove(pos);
                            generatedStone[pos] = true;
                            Destroy(activeTiles[pos]);
                            activeTiles[pos] = generateTile(pos, new Vector2(transform.position.x + pos.x, transform.position.y - pos.y));

                            return;
                        }
                    }
                    if (isWaterNotLava.ContainsKey(down + new Vector2(-1f, 0f)))
                    {
                        if (isWaterNotLava[down + new Vector2(-1f, 0f)])
                        {
                            //There is water to the right of the space which the lava wants to expand into. Let water go first.
                            isWaterNotLava[down] = true;
                            activeTiles[down] = generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y));
                            expandLiquid(down);

                            isWaterNotLava.Remove(pos);
                            generatedStone[pos] = true;
                            Destroy(activeTiles[pos]);
                            activeTiles[pos] = generateTile(pos, new Vector2(transform.position.x + pos.x, transform.position.y - pos.y));

                            return;
                        }
                    }
                    if (isWaterNotLava.ContainsKey(down + new Vector2(0f, 1f)))
                    {
                        if (isWaterNotLava[down + new Vector2(0f, 1f)])
                        {
                            //There is water below the space which the lava wants to expand into. Let water go first.
                            isWaterNotLava[down] = true;
                            activeTiles[down] = generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y));
                            expandLiquid(down);

                            isWaterNotLava.Remove(pos);
                            generatedStone[pos] = true;
                            Destroy(activeTiles[pos]);
                            activeTiles[pos] = generateTile(pos, new Vector2(transform.position.x + pos.x, transform.position.y - pos.y));

                            return;
                        }
                    }

                    isWaterNotLava[down] = false;
                    activeTiles[down] = generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y));
                    expandLiquid(down);
                    
                }
                else if (isWaterNotLava.ContainsKey(down))
                {
                    if (isWaterNotLava[down])
                    {
                        //there is water below the expanding lava
                        isWaterNotLava.Remove(pos);
                        generatedStone[pos] = true;
                        Destroy(activeTiles[pos]);
                        activeTiles[pos] = generateTile(pos, new Vector2(transform.position.x + pos.x, transform.position.y - pos.y));
                    }
                }
            }
            else
            {
                activeTiles.Add(down, generateTile(down, new Vector2(transform.position.x + down.x, transform.position.y - down.y)));
                expandLiquid(pos);
                Destroy(activeTiles[down]);
                activeTiles.Remove(down);
            }
        }
    }

    //Tile Generation uses integer codes to determine which tile to generate
    //0 = NULL
    //1 = Grass
    //2 = Dirt
    //3 = Sand
    //4 = Clay
    //5 = Rock
    //6 = Stone
    //returns the generated tile
    public GameObject generateTile(Vector2 createPos, Vector2 mapPos)
    {
        if (isWaterNotLava.ContainsKey(createPos))
        {
            if (isWaterNotLava[createPos])
            {
                //Water
                return Instantiate(allTiles[7], mapPos, Quaternion.identity);
            }
            else
            {
                //Lava
                return Instantiate(allTiles[8], mapPos, Quaternion.identity);
            }
        }
        if (generatedStone.ContainsKey(createPos))
        {
            //Stone
            return Instantiate(allTiles[6], mapPos, Quaternion.identity);
        }
        if (destroyedTiles.ContainsKey(createPos))
        {
            return null;
        }

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

        int thousandPerlin = (int)(perlin * 1000);
        int d50 = thousandPerlin % (int)(50f - (stat.vLuck/10));

        if (createPos.y < 100)
        {
            if (j == mountainHeights[i])
            {
                //Grass
                GameObject tile = Instantiate(allTiles[1], mapPos, Quaternion.identity);

                if (mapPos.y == -2 && mapPos.x != -51.5 && mapPos.x >= -70.5 && mapPos.x <= -39.5)
                {
                    tile.GetComponent<Tile>().setUnbrekable();
                }

                return tile;
            }
            else if (j > mountainHeights[i])
            {
                if (perlin < 0.33f)
                {
                    //Clay
                    GameObject tile = Instantiate(allTiles[4], mapPos, Quaternion.identity);
                    switch (d50)
                    {
                        case 0:
                            tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                            break;
                        case 1:
                            tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                            break;
                        default:
                            break;
                    }
                    return tile;
                }
                else if (perlin < 0.85f)
                {
                    //Dirt
                    GameObject tile = Instantiate(allTiles[2], mapPos, Quaternion.identity);
                    switch (d50)
                    {
                        case 0:
                            tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                            break;
                        case 1:
                            tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                            break;
                        default:
                            break;
                    }
                    return tile;
                }
                else
                {
                    //Dirt
                    GameObject tile = Instantiate(allTiles[2], mapPos, Quaternion.identity);
                    switch (d50)
                    {
                        case 0:
                            tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                            break;
                        case 1:
                            tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                            break;
                        default:
                            break;
                    }
                    return tile;
                }
            }
            else
            {
                //Ensure that every empty tile is initialized to the NULL value
                return null;
            }
        }
        else if (createPos.y >= 100 && createPos.y < 150)
        {
            if (perlin < 0.1f)
            {
                //Water
                isWaterNotLava[createPos] = true;
                return Instantiate(allTiles[7], mapPos, Quaternion.identity);
            }
            else if (perlin < 0.35f)
            {
                //Clay
                GameObject tile = Instantiate(allTiles[4], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else if (perlin < 0.67f)
            {
                //Dirt
                GameObject tile = Instantiate(allTiles[2], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else
            {
                //Sand
                GameObject tile = Instantiate(allTiles[3], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    default:
                        break;
                }
                return tile;
            }
        }
        else if (createPos.y >= 150 && createPos.y < 200)
        {
            if (perlin < 0.1f)
            {
                //Water
                isWaterNotLava[createPos] = true;
                return Instantiate(allTiles[7], mapPos, Quaternion.identity);
            }
            else if (perlin < 0.35f)
            {
                //Dirt
                GameObject tile = Instantiate(allTiles[2], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Ruby);
                        break;
                    case 4:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    case 5:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Gold);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else if (perlin < 0.67f)
            {
                //Clay
                GameObject tile = Instantiate(allTiles[4], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Ruby);
                        break;
                    case 4:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    case 5:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Gold);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else
            {
                //Rock
                GameObject tile = Instantiate(allTiles[5], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Ruby);
                        break;
                    case 4:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    case 5:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Gold);
                        break;
                    default:
                        break;
                }
                return tile;
            }
        }
        else
        {
            if (perlin < 0.3f)
            {
                //NULL
                return null;
            }
            else if (perlin < 0.55f)
            {
                //Clay
                GameObject tile = Instantiate(allTiles[4], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Ruby);
                        break;
                    case 4:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Diamond);
                        break;
                    case 5:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    case 6:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Gold);
                        break;
                    case 7:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Mithril);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else if (perlin < 0.75f)
            {
                //Rock
                GameObject tile = Instantiate(allTiles[5], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Ruby);
                        break;
                    case 4:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Diamond);
                        break;
                    case 5:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    case 6:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Gold);
                        break;
                    case 7:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Mithril);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else if (perlin < 0.85f)
            {
                //Stone
                GameObject tile = Instantiate(allTiles[6], mapPos, Quaternion.identity);
                switch (d50)
                {
                    case 0:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Topaz);
                        break;
                    case 1:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Iron);
                        break;
                    case 2:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Sapphire);
                        break;
                    case 3:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Ruby);
                        break;
                    case 4:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Diamond);
                        break;
                    case 5:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Silver);
                        break;
                    case 6:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Gold);
                        break;
                    case 7:
                        tile.GetComponent<Tile>().setTreasure(Tile.Treasure.Mithril);
                        break;
                    default:
                        break;
                }
                return tile;
            }
            else
            {
                //Lava
                isWaterNotLava[createPos] = false;
                return Instantiate(allTiles[8], mapPos, Quaternion.identity);
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

            GameObject tile = generateTile(createPos, mapPos);

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

            GameObject tile = generateTile(createPos, mapPos);

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

            GameObject tile = generateTile(createPos, mapPos);

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


            GameObject tile = generateTile(createPos, mapPos);

            activeTiles.Add(createPos, tile);
        }
    }

    public void UnrenderAllTiles()
    {
        int xPos = (int)(dwarf.transform.position.x + 70.5);
        int yPos = (int)(dwarf.transform.position.y * -1) + 48;

        for (int i = xPos - renderDistance; i <= xPos + renderDistance; i++)
        {
            for (int j = yPos - renderDistance; j <= yPos + renderDistance; j++)
            {
                Vector2 tilePos = new Vector2(i, j);
                Destroy(activeTiles[tilePos]);
                activeTiles.Remove(tilePos);
            }
        }
    }

    public void generateStartingTiles()
    {
        dwarf.transform.position = new Vector3(-53.5f, -1.16f, 0f);
        int xPos = (int)(dwarf.transform.position.x + 70.5);
        int yPos = (int)(dwarf.transform.position.y * -1) + 48;

        Debug.Log(xPos + ", " + yPos);

        for (int i = xPos - renderDistance; i <= xPos + renderDistance; i++)
        {
            for (int j = yPos - renderDistance; j <= yPos + renderDistance; j++)
            {
                Vector2 mapPos = new Vector2(transform.position.x + i, transform.position.y - j);
                Vector2 tilePos = new Vector2(i, j);

                GameObject tile = generateTile(tilePos, mapPos);

                activeTiles.Add(tilePos, tile);
            }
        }
    }

    private void Start()
    {
        seed = Random.Range(-2000000, 2000000);

        mountainHeights = new Dictionary<int, int>();
        destroyedTiles = new Dictionary<Vector2, bool>();
        activeTiles = new Dictionary<Vector2, GameObject>();
        isWaterNotLava = new Dictionary<Vector2, bool>();
        generatedStone = new Dictionary<Vector2, bool>();

        //Generate initial mountain heights
        for (int i = 0; i < width; i++)
        {
            //determine the mountain height at a given level
            if (i >= width / 2 - 16 && i <= width / 2 + 15 && guaranteeFlatSection)
            {
                mountainHeights.Add(i, 50);
            }
        }

        //Render Initial Tiles
        generateStartingTiles();

    }

    public void Update()
    {

    }
}
