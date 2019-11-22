using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public GameObject[] skyIsland;
    public GameObject[] top;
    public GameObject[] earth;
    public GameObject[] subEarth;
    public GameObject[] bottom;
    public GameObject[,] tileGrid;
    public int width;
    public int height;
    private int seed;
    public float scale;
    public bool guaranteeFlatSection;

    public GameObject getTile(Vector3 pos)
    {
        return tileGrid[(int)(pos.x + 70.5), (int)(pos.y * -1) + 48];
    }

    private void Start()
    {
        seed = Random.Range(-2000000, 2000000);

        tileGrid = new GameObject[width, height];

        //Generate Perlin Biomes
        for (int i = 0; i < width; i++)
        {
            //determine the mountain height at a given level
            int mountainHeight = -1;
            if (i >= width / 2 - 15 && i <= width / 2 + 15 && guaranteeFlatSection)
            {
                mountainHeight = 75;
            }

            for (int j = 0; j < height; j++)
            {
                float x = i / (float)width * scale;
                float y = j / (float)height * scale;
                float perlin = Mathf.PerlinNoise(x + seed, y + seed);
                Vector2 pos = new Vector2(transform.position.x + i, transform.position.y - j);

                if (j < 100)
                {
                    if (mountainHeight == -1)
                    {
                        //find height of previous mountain
                        int previousMountain = 99;
                        if (i > 0)
                        {
                            for (previousMountain = 0; previousMountain < 100; previousMountain++)
                            {
                                if (tileGrid[i - 1, previousMountain] == top[0])
                                {
                                    break;
                                }
                            }
                        }

                        mountainHeight = ((int)Mathf.Ceil(100 * perlin) + previousMountain) / 2;
                        Debug.Log("Perlin value: " + perlin);
                        Debug.Log("mountain value: " + mountainHeight);
                    }

                    if (j == mountainHeight)
                    {
                        tileGrid[i, j] = Instantiate(top[0], pos, Quaternion.identity);
                    }
                    else if (j > mountainHeight)
                    {
                        if (perlin < 0.33f)
                        {
                            tileGrid[i, j] = Instantiate(top[1], pos, Quaternion.identity);
                        }
                        else if (perlin < 0.66f)
                        {
                            tileGrid[i, j] = Instantiate(top[2], pos, Quaternion.identity);
                        }
                        else
                        {
                            tileGrid[i, j] = Instantiate(top[2], pos, Quaternion.identity);
                        }
                    }
                }
                else if (j >= 100 && j < 150)
                {
                    //int rand = Random.Range(0, earth.Length);
                    if (perlin < 0.33f)
                    {
                        tileGrid[i, j] = Instantiate(earth[0], pos, Quaternion.identity);
                    }
                    else if (perlin < 0.66f)
                    {
                        tileGrid[i, j] = Instantiate(earth[1], pos, Quaternion.identity);
                    }
                    else
                    {
                        tileGrid[i, j] = Instantiate(earth[1], pos, Quaternion.identity);
                    }
                }
                else if (j >= 150 && j < 200)
                {
                    //int rand = Random.Range(0, earth.Length);
                    if (perlin < 0.33f)
                    {
                        tileGrid[i, j] = Instantiate(subEarth[0], pos, Quaternion.identity);
                    }
                    else if (perlin < 0.66f)
                    {
                        tileGrid[i, j] = Instantiate(subEarth[1], pos, Quaternion.identity);
                    }
                    else
                    {
                        tileGrid[i, j] = Instantiate(subEarth[2], pos, Quaternion.identity);
                    }
                }
                else
                {
                    //int rand = Random.Range(0, earth.Length);

                    if (perlin < 0.33f)
                    {
                        tileGrid[i, j] = null;
                        //Instantiate(bottom[0], pos, Quaternion.identity);
                    }
                    else if (perlin < 0.66f)
                    {
                        tileGrid[i, j] = Instantiate(bottom[0], pos, Quaternion.identity);
                    }
                    else
                    {
                        tileGrid[i, j] = Instantiate(bottom[1], pos, Quaternion.identity);
                    }
                }

                if (tileGrid[i, j] != null)
                {
                    Debug.Log("(i, j): (" + i + ", " + j + ") " + " Pos: " + tileGrid[i, j].transform.position);
                }


            }
        }
    }

    public void Update()
    {
        
    }
}
