using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CopState { calm, apprehendingPlayer, lostPlayer}

public class CopScript : MonoBehaviour
{
    public CopState myState;
    public playermovement player;
    public AIDirector aiDirector;
    public bool canSee, timerOn;
    public float timeWithoutVision = 10.0f;
    public NavMeshAgent myAgent;
    public GameObject bubble;
    public LayerMask layerMasks, copMask, layoutMask;
    public Sprite[] bubbleSprites = new Sprite[2];

    // Start is called before the first frame update
    void Start()
    {
        canSee = false;
        layerMasks = copMask | layoutMask;
        bubble.SetActive(false);
        player = FindObjectOfType<playermovement>();
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.updateRotation = false;
        myAgent.updateUpAxis = false;
        aiDirector = FindObjectOfType<AIDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        { 
        timeWithoutVision -= Time.deltaTime;
            if (timeWithoutVision <= 0.0f)
            {
                timeWithoutVision = 10.0f;
                timerOn = false;
                myState = CopState.lostPlayer;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 10.0f, ~layerMasks);
        if (hit.collider != null)
        { 
        if (hit.collider.gameObject.name == "Player")
        {
            canSee = true;
        }
        else
        {
            canSee = false;
        }
        }

        if (myState == CopState.apprehendingPlayer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 1.0f)
            {
                aiDirector.GameOver();
            }
            else if (!canSee)
            {
                if (!timerOn)
                { 
                StartTimer();
                }
            }
            { 
                myAgent.destination = player.transform.position;
            }
        }

        else if (myState == CopState.lostPlayer)
        {
            bubble.GetComponent<SpriteRenderer>().sprite = bubbleSprites[1];
            StartCoroutine(StopBubble());
        }
    }

    public void ApprehendSuspect()
    {
        bubble.GetComponent<SpriteRenderer>().sprite = bubbleSprites[0];
        myState = CopState.apprehendingPlayer;
        StartCoroutine(StopBubble());
    }

    public IEnumerator StopBubble()
    {
        bubble.SetActive(true);
        yield return new WaitForSeconds(Random.Range(2, 5));
        bubble.SetActive(false);
        yield return new WaitForSeconds(Random.Range(2, 5));
        if (myState == CopState.apprehendingPlayer)
        { 
        StartCoroutine(StopBubble());
        }
    }

    public void StartTimer()
    {
        timerOn = true;
    }
}
