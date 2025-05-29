using HC.Data;
using HC.Resource;
using UGS;
using UnityEngine;
using UnityEngine.AI;

public class Cat_Actor : MonoBehaviour
{
    int catCode;
    SpriteRenderer spriteRenderer;
    Animator animator;

    NavMeshAgent agent;

    static float agentDrift = 0.0001f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //test
        //Init(101);
    }
    public async void Init(int catCode)
    {
        this.catCode = catCode;

        //UGSManager.Init();
        //var catTableData = GuestTable.Data.DataMap[101];
        var data = UGSManager.GetData<GuestTable.Data>(catCode);

        animator.runtimeAnimatorController = await LoadAddressableManager.Load_AnimController<RuntimeAnimatorController>(data.icon);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.localPosition.x);
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        //    SetDestination(mousePos);
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    agent.ResetPath();
        //}
    }

    void SetDestination(Vector3 target)
    {
        var driftPos = target;
        if (Mathf.Abs(transform.position.x - target.x) < agentDrift)
            driftPos = target + new Vector3(agentDrift, 0f, 0f);
        agent.SetDestination(driftPos);
    }


}
