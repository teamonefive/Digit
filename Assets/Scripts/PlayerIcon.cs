using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] playerIcons;
    public Stats stats;
    public Button confirm;
    public GameObject statPanel;
    private Button player;
    private int level;
    private bool active;
    void Start()
    {
        active = false;
        player = GetComponent<Button>();
        level = stats.vLevel;
        statPanel.SetActive(active);
        Button conf = confirm.GetComponent<Button>();
        conf.onClick.AddListener(confirmed);
    }

    void confirmed()
    {
        //print("Confirmed");
        level = stats.vLevel;
        ShowStats();
    }
    // Update is called once per frame
    void Update()
    {
        checkIcon();
    }

    public void ShowStats()
    {
        active = !active;
        statPanel.SetActive(active);
    }
    public void checkIcon()
    {
        if (level < stats.vLevel)
        {
            player.image.sprite = playerIcons[2];
            statPanel.SetActive(true);
        }
        else
        {
            if (stats.vFatigue <= 20f)
            {
                player.image.sprite = playerIcons[1];
            }
            else
            {
                player.image.sprite = playerIcons[0];
            }
        }
    }
}
