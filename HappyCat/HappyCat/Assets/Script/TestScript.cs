using HC.Data;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UGSManager.Init();

        foreach(var a in GuestTable.Guest.GuestList)
        {
            Debug.Log(a.code);
            Debug.Log(a.icon);
            Debug.Log(a.category);
            Debug.Log(a.food.Count);
            Debug.Log(a.food[0]);
            Debug.Log(a.food[1]);
            Debug.Log(a.food[2]);
            Debug.Log(a.ani_eat01.Count);
            Debug.Log("----------------");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
