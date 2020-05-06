using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Dynamic;

public class Reset : MonoBehaviour
{
    private string path;
    public GameObject reset;

    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "dygg.sav");

        if (!File.Exists(path))
        {
            reset.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void deleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        SceneManager.LoadScene("MapGen");
    }
}
