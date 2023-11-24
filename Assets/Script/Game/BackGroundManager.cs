using System.Collections;
using System.Collections.Generic;
using Script.Game.Enemy;
using UnityEngine;

/// <summary>
/// 배경 관리 클래스
/// </summary>
public class BackGroundManager : MonoBehaviour
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

    /// <summary>
    /// 페이즈 업데이트 리스너
    /// </summary>
    /// <param name="phasenum"></param>
    public void OnPhaseUpdate(int phasenum)
    {
        Debug.Log(test_colors[phasenum]);
        _renderer.color = test_colors[phasenum];
    }
}
