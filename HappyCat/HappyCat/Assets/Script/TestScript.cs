using HC.Data;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        agent.speed += 1000f;

        agent.SetDestination(new Vector3(30f, 276));
        //UGSManager.Init();

        //foreach(var a in GuestTable.Guest.GuestList)
        //{
        //    Debug.Log(a.code);
        //    Debug.Log(a.icon);
        //    Debug.Log(a.category);
        //    Debug.Log(a.foodDatas.Count);
        //    Debug.Log(a.foodDatas[0]);
        //    Debug.Log(a.foodDatas[1]);
        //    Debug.Log(a.foodDatas[2]);
        //    Debug.Log(a.ani_eat01.Count);
        //    Debug.Log("----------------");
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
