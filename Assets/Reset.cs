using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void deleteSave()
    {
        string path = Path.Combine(Application.persistentDataPath, "dygg.sav");
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
