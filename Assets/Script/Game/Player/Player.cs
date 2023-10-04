using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int DEAFAULT_HP;
    private int hp;
    
    public UnityEvent<Action<int>> HpUpdatEventLis

    public int HP
    {
        get => hp;
        
        set
        {
            hp = value;
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        hp = DEAFAULT_HP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
