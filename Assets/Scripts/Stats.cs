using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // FROM EXPERIENCE
    //current level
    public int vLevel = 1;
    //current exp amount
    public int vCurrExp = 0;
    //exp amount needed for lvl 1
    public int vExpBase = 10;
    //exp amount left to next levelup
    public int vExpLeft = 10;
    //modifier that increases needed exp each level
    public float vExpMod = 1.15f;
    //current strength modifier
    public int vStrength = 0;
    //current agility modifer
    public int vAgility = 0;
    //current endurance modifer
    public int vEndurance = 0;
    //current perception modifer
    public int vPerception = 0;
    //current luck modifer
    public int vLuck = 0;

    // FROM TILEBASEDMOVER
    public float moveSpeed = 1f;
    public float climbingDifficulty = 2f;
    public float fallSpeedMultiplier = 1f;
    public float setMoveCooldown = 1f;
    public float moveCooldown = 0f;
    public float tileDifficultyMultiplier = 1f, climbingDifficultyMultiplier = 1f, strengthMultiplier = 1f;

    // FROM FOLLOW DWARF
    public float speedMultiplier = 1f;

    // FROM FATIGUE
    public float vFatigue = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
