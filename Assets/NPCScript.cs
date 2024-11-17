using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public enum NPCState { calm, rushingToCop, reportingCrime};

public class NPCScript : MonoBehaviour
{

    public NPCState myState;
    public GameObject speechBubble;
    public bool canSee;
    public CopScript nearestCop;
    public GameObject player;
    public playermovement playerMov;
    public LayerMask npcMask, layoutMask, layerMasks;
    public List<CopScript> cops = new List<CopScript>();
    public AIDirector AIdirector;
    public NavMeshAgent myAgent;
    public Transform[] walkPoints = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.updateRotation = false;
        myAgent.updateUpAxis = false;
        speechBubble.SetActive(false);
        playerMov = FindObjectOfType<playermovement>();
        AIdirector = FindObjectOfType<AIDirector>();
        layerMasks = npcMask | layoutMask;
    }

    // Update is called once per frame
    void Update()
    {

        if (myState == NPCState.rushingToCop)
        {
            if (Vector3.Distance(transform.position, nearestCop.transform.position) < 1.0f)
            {
                StartCoroutine(ReportCrime());
                myState = NPCState.reportingCrime;
                return;
            }
            else
            {
                myAgent.destination = nearestCop.transform.position;
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

        if (canSee && playerMov.commmitingCrime == true && myState == NPCState.calm)
        {
            StartCoroutine(ExclamationBubble());
        }

    }

    void ReactToCrime()
    {
        cops = AIdirector.getCops();
        int i = 0;
        foreach (CopScript cop in cops)
        {
            if (i == 0)
            {
                nearestCop = cop;
            }
            else
            {
                if (Vector3.Distance(transform.position, nearestCop.gameObject.transform.position) > (Vector3.Distance(transform.position, cop.gameObject.transform.position)))
                {
                    nearestCop = cop;
                }
            }
        }

        myState = NPCState.rushingToCop;
    }

    public IEnumerator ReportCrime()
    {
        yield return new WaitForSeconds(2.0f);
        {
            nearestCop.ApprehendSuspect();
        }
        myAgent.destination = transform.position;
        StopAllCoroutines();
    }

    public IEnumerator ExclamationBubble()
    {
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        speechBubble.SetActive(false);
        ReactToCrime();
        StopAllCoroutines();
    }

}
