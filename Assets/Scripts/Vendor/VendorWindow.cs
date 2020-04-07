using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorWindow : MonoBehaviour
{
    private static VendorWindow instance;

    private CanvasGroup canvasGroup;

    public static VendorWindow MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<VendorWindow>();
            }

            return instance;
        }
    }
    
    public GameObject vwindow;

    [SerializeField]
    private VendorButton[] vendorButtons;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    [SerializeField]
    private Text pageNumber;

    private int pageIndex;

    public void CreatePages(VendorItem[] items)
    {
        List<VendorItem> page = new List<VendorItem>();

        for(int i = 0; i<items.Length; i++)
        {
            page.Add(items[i]);

            if(page.Count == 10 || i == items.Length -1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
            }

        }
        AddItems();
    }

    public void AddItems()
    {
        //pageNumber.text = pageIndex + 1 + "/" + pages.Count;
        if (pages.Count>0)
        {
            for ( int i = 0; i<pages[pageIndex].Count; i++)
            {
                if(pages[pageIndex][i]!=null)
                {
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }

    public void Openvwindow()
    {
        if (vwindow != null)
        {
            bool isActive = vwindow.activeSelf;

            vwindow.SetActive(!isActive);
        }
    }

    public void NextPage()
    {
        if(pageIndex < pages.Count-1)
        {
            ClearButtons();
            pageIndex++;
            AddItems();
        }
    }

    public void PreviousPage()
    {
        if(pageIndex>0)
        {
            ClearButtons();
            pageIndex--;
            AddItems();
        }
    }

    public void ClearButtons()
    {
        foreach(VendorButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }
}
