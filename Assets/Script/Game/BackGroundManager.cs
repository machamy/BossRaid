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
    [SerializeField] private List<Sprite> bg_imgs;
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
        Debug.Log("[BackGroundManager::OnPhaseUpdate]"+test_colors[phasenum]);

        if (bg_imgs[phasenum])
        {
            _renderer.color = Color.white;
            _renderer.sprite = bg_imgs[phasenum];
        }
        else
        {
            _renderer.color = test_colors[phasenum];
        }
    }
}
