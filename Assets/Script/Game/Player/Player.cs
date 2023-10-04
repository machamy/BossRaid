using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int DEAFAULT_HP;
    private int hp;

    public UnityEvent<int> OnHPUpdate { get; } = new UnityEvent<int>();
    public BaseSkill[] skills;

    public int HP
    {
        get => hp;
        
        set
        {
            hp = value;
            OnHPUpdate.Invoke(value);
        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        hp = DEAFAULT_HP;
        skills = new BaseSkill[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
