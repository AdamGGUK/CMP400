using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDirector : MonoBehaviour
{

    public List<CopScript> cops = new List<CopScript>();
    public UIDirector uiDirector;

    // Start is called before the first frame update
    void Start()
    {
        uiDirector = FindObjectOfType<UIDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<CopScript> getCops()
    {
        return cops;
    }

    public void GameOver()
    {
        uiDirector.GameOver();
    }
}
