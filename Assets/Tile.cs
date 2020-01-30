using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float difficulty;
    public enum Treasure { None, Iron, Topaz, Sapphire, Ruby, Diamond }
    public Treasure treasure;

    public enum TileType { Grass, Dirt, Sand, Clay, Rock, Stone}
    public TileType type;

    public Sprite Grass, Dirt, Dirt_Iron, Dirt_Topaz, Dirt_Sapphire, Dirt_Ruby, Dirt_Diamond, Sand, Sand_Iron, Sand_Topaz, Sand_Sapphire, Sand_Ruby, Sand_Diamond, Clay, Clay_Iron, Clay_Topaz, Clay_Sapphire, Clay_Ruby, Clay_Diamond, Rock, Rock_Iron, Rock_Topaz, Rock_Sapphire, Rock_Ruby, Rock_Diamond, Stone, Stone_Iron, Stone_Topaz, Stone_Sapphire, Stone_Ruby, Stone_Diamond;

    public void setTreasure(Treasure t)
    {
        treasure = t;

        switch(type)
        {
            case TileType.Grass:
                //Only one texture for grass
                this.gameObject.GetComponent<SpriteRenderer>().sprite = Grass;
                break;

            case TileType.Dirt:
                switch (treasure)
                {
                    case Treasure.Iron:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Dirt_Iron;
                        break;

                    case Treasure.Topaz:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Dirt_Topaz;
                        break;

                    case Treasure.Sapphire:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Dirt_Sapphire;
                        break;

                    case Treasure.Ruby:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Dirt_Ruby;
                        break;

                    case Treasure.Diamond:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Dirt_Diamond;
                        break;

                    default:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Dirt;
                        break;
                }
                break;

            case TileType.Sand:
                switch (treasure)
                {
                    case Treasure.Iron:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sand_Iron;
                        break;

                    case Treasure.Topaz:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sand_Topaz;
                        break;

                    case Treasure.Sapphire:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sand_Sapphire;
                        break;

                    case Treasure.Ruby:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sand_Ruby;
                        break;

                    case Treasure.Diamond:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sand_Diamond;
                        break;

                    default:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sand;
                        break;
                }
                break;

            case TileType.Clay:
                switch (treasure)
                {
                    case Treasure.Iron:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clay_Iron;
                        break;

                    case Treasure.Topaz:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clay_Topaz;
                        break;

                    case Treasure.Sapphire:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clay_Sapphire;
                        break;

                    case Treasure.Ruby:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clay_Ruby;
                        break;

                    case Treasure.Diamond:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clay_Diamond;
                        break;

                    default:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Clay;
                        break;
                }
                break;

            case TileType.Rock:
                switch (treasure)
                {
                    case Treasure.Iron:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Rock_Iron;
                        break;

                    case Treasure.Topaz:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Rock_Topaz;
                        break;

                    case Treasure.Sapphire:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Rock_Sapphire;
                        break;

                    case Treasure.Ruby:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Rock_Ruby;
                        break;

                    case Treasure.Diamond:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Rock_Diamond;
                        break;

                    default:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Rock;
                        break;
                }
                break;

            case TileType.Stone:
                switch (treasure)
                {
                    case Treasure.Iron:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Stone_Iron;
                        break;

                    case Treasure.Topaz:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Stone_Topaz;
                        break;

                    case Treasure.Sapphire:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Stone_Sapphire;
                        break;

                    case Treasure.Ruby:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Stone_Ruby;
                        break;

                    case Treasure.Diamond:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Stone_Diamond;
                        break;

                    default:
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = Stone;
                        break;
                }
                break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
