using System.Collections;
using System.Collections.Generic;
using Script.Game.Enemy;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private PatternController pc;
    public List<Color> test_colors;
    private SpriteRenderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        pc.OnPhaseUpdateEvent.AddListener(OnPhaseUpdate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhaseUpdate(int phasenum)
    {
        _renderer.color = test_colors[phasenum];
    }
}
