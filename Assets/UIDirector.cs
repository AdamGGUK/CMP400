using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{

    public Text bustedImage;

    // Start is called before the first frame update
    void Start()
    {
        bustedImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        bustedImage.gameObject.SetActive(true);
    }
}
