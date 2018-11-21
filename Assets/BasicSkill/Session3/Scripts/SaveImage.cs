using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Analytics;
//using File = UnityEngine.Windows.File;


public class SaveImage : MonoBehaviour
{

        [SerializeField] private string _folder = "C:\\Users\\zim04\\Desktop\\Key Image\\00";
        [SerializeField] private string _format = "frame_{0:D3}.png";
        [SerializeField] private KeyCode _key = KeyCode.LeftShift;
        [SerializeField] private int _superSize = 1;
        private int _counter = 0;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        if (!Directory.Exists(_folder))
        {
            Directory.CreateDirectory(_folder);
        }
        else
        {
            Debug.Log($"{_folder} Already Exist");
        }
      
    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
          
            var path = string.Format(Path.Combine(_folder, _format), _counter);
            //if (Directory.GetFiles(_folder).Contains(path))
            //{
            //    Debug.Log($"{path} already exists");
            //    path = string.Format(Path.Combine(_folder, _format), _counter++);
            //}

            if (CheckExistedFiles(path))
            {
                path= string.Format(Path.Combine(_folder, _format), _counter);
                ScreenCapture.CaptureScreenshot(path, _superSize);
                Debug.Log($"Screenshot saved to { path }");
                
            }
             

         
        }
    }

    bool CheckExistedFiles(string file)
    {
        if (!Directory.GetFiles(_folder).Contains(file))
        {
            _counter = 0;
            return true;
           
        }
        else
        {
            //Debug.Log($"{file} already exists");
            _counter++;
            file = string.Format(Path.Combine(_folder, _format), _counter);
           return CheckExistedFiles(file);
        }
    }

}
