using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Items { get; private set; }
    
    void Start()
    {
        Items = new List<GameObject>();
    }
}
