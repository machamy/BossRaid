using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using Script.Game.Enemy;
using Script.Game.Enemy.Projectile;
using Script.Game.Player;
using Script.Game.Projectile;
using Script.Global;
using UnityEngine;
using Display = Script.Game.Enemy.Projectile.Display;

public class Professor : MonoBehaviour, DBUser
{
    [Header("Projectile")]
    public GameObject attend;  
    public GameObject practice;  
    public GameObject team; 
    [Header("ETC")]
    public GameObject shootPos;
    public PatternController PatternController;
    [SerializeField] private Player player;
    [SerializeField] private Display display;
    public Display Display => display;
    private Direction facing;

    private Animator _animator;
    
    public Direction Facing
    {
        set
        {
            facing = value;
            if (facing.HasFlag(Direction.Left))
                renderer.flipX = false;
            else
                renderer.flipX = true;
        }
        get => facing;
    }
    [SerializeField] public Transform PosSystem;
    public SpriteRenderer renderer;

    private void Awake()
    {
        facing = Direction.Left;
        
    }

    

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        // Debug.Log(PatternController);
        player.OnScoreUpdateEvent.AddListener(PatternController.OnScoreUpdate);
        ApplyDBdata();
        //StartCoroutine(FadeOut());
        StartCoroutine(PatternController.Rountine(this, player));
        PatternController.PhaseUpdateEvent.AddListener(OnPhaseUpdateEvnet);
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ApplyDBdata()
    {

    }

    public float teleport_time = 0.4f;

    public void OnPhaseUpdateEvnet(int num)
    {
        teleport_time = Mathf.Min(0.1f, 0.4f - num * 0.05f);
    }

    public float StartTeleport(Vector3 position) => StartTeleport(position, teleport_time);
    
    /// <summary>
    /// 순간이동과 방향 전환을 한번에
    /// </summary>
    /// <param name="position"></param>
    public float StartTeleport(Vector3 position, float total_time)
    {
        if (position == transform.position)
            return 0;
        StartCoroutine(TeleportRoutine(position,total_time));
        return total_time;
    }

    private IEnumerator TeleportRoutine(Vector3 position, float total_time)
    {
        _animator.SetBool("IsTeleporting", true);
        var time = total_time / 2;
        StartCoroutine(FadeOut(time));
        yield return new WaitForSeconds(time);
        transform.position = position;
        if (transform.position.x < 0) Facing = Direction.Right;
        else Facing = Direction.Left;
        StartCoroutine(FadeIn(time));
        yield return new WaitForSeconds(time);
        _animator.SetBool("IsTeleporting", false);
    }
    
    private IEnumerator FadeOut(float time)
    {
        int i = 10;
        while (i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(time/10);
        }
    }

    private IEnumerator FadeIn(float time)
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(time/10);
        }
    }
    // public void StartFadeIn()
    // {
    //     //StartCoroutine(FadeIn());
    // }


    // Update is called once per frame
    void Update()
    {
        
    }

    internal void tpLeft()
    {
        Vector3 Lp = PosSystem.GetChild(0).position;
        StartTeleport(Lp);
    }

    internal void tpRight()
    {
        Vector3 Rp = PosSystem.GetChild(1).position;
        StartTeleport(Rp);
    }

    internal void tpLeftUp()    //고정Pos 생성해서 tp
    {
        Vector3 LUp = PosSystem.GetChild(2).position;
        StartTeleport(LUp);
    }

    internal void tpRightUp()    
    {
        Vector3 RUp = PosSystem.GetChild(3).position;
        StartTeleport(RUp);
    }


    internal void 과제()
    {
        _animator.SetTrigger("Practice");
    }

    internal void 팀플()
    {
        _animator.SetTrigger("Team");
    }
    
    internal void 출첵(Vector2 startPoint, Vector2 endPoint,int num = 3)
    {
        _tmpAttendStartPos = startPoint;
        _tmpAttendEndPos = endPoint;
        _tmpAttendNum = num;
        _animator.SetTrigger("Attendance");
    }
    
    /// <summary>
    /// animation event로 호출
    /// </summary>
    internal void Shoot과제() => Shoot과제(Facing.Vector());
    internal void Shoot과제(Vector2 dir)
    {
        // Debug.Log("[Professor::과제]");
        ShootDirByPrefab(dir, practice);
    }
    /// <summary>
    /// animation event로 호출
    /// </summary>
    internal void Shoot팀플() => Shoot팀플(Facing.Vector());
    internal void Shoot팀플(Vector2 dir)
    {
        // Debug.Log("[Professor::팀플]");
        ShootDirByPrefab(dir, team);
    }

    private Vector2 _tmpAttendStartPos;
    private Vector2 _tmpAttendEndPos;
    private int _tmpAttendNum;
    /// <summary>
    /// animation event로 호출
    /// </summary>
    internal void Shoot출첵() => Shoot출첵(_tmpAttendStartPos,_tmpAttendEndPos,_tmpAttendNum);
    
    internal void Shoot출첵(Vector2 startPoint, Vector2 endPoint,int num = 3)
    {
        // Debug.Log("[Professor::출첵]");

        // 투사체 그룹 집합
        HashSet<Projectile> group = new HashSet<Projectile>();
        float first_distance = -1;
        foreach (var pos in GetSplitPoss(startPoint, endPoint, num))
        {
            Projectile current = ShootPosByPrefab(pos, attend);
            float distance = (transform.position - (Vector3)pos).magnitude;
            current.Group = group;
            group.Add(current);
            if (first_distance == -1)
            {
                first_distance = distance;
                continue;
            }
            current.speed *= (distance/first_distance); // s1 = (d1/d0)s0
        }
    }

    /// <summary>
    /// direction을 기준으로 좌우 degreerange의 범위에 num개의 방향을 구함.
    /// </summary>
    /// <param name="direction">쏠 방향</param>
    /// <param name="num">쏠 개수</param>
    /// <param name="degreeRange">쏠 범위(degree)</param>
    internal IEnumerable<Vector2> GetArc(Vector2 direction, int num, float degreeRange)
    {
        Vector2 currentDir = Quaternion.AngleAxis(-degreeRange/ num,Vector3.forward) * direction;
        yield return currentDir;
        for (int i = 1; i < num; i++)
        {
            // Debug.Log("[Professor::ShootArc] currentDir : "+currentDir);
            currentDir = Quaternion.AngleAxis(degreeRange / num, Vector3.forward) * currentDir;
            yield return currentDir;
        }
    }

    /// <summary>
    /// 시작 위치와 종료 위치를 동등하게 나눈 num개의 방향을 구함.
    /// </summary>
    /// <param name="start">시작점</param>
    /// <param name="end">종단점</param>
    /// <param name="num">나눌 개수</param>
    /// <returns>장소 이터레이터</returns>
    internal IEnumerable<Vector2> GetSplitPoss(Vector2 start, Vector2 end, int num)
    {
        Vector2 diff = end - start;
        for (int i = 0; i < num; i++)
        {
            yield return start + diff * (i/(float)num);
        }
    }

    private IEnumerator TestPattern()
    {
        while (true)
        {
            GameObject prjt = Instantiate(practice);
            ShootDir(prjt.GetComponent<Projectile>(), Vector2.left);
            yield return new WaitForSeconds(3);
        }
    }

    /// <summary>
    /// 정해진 위치로 발사
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="type"></param>
    public Projectile ShootPosByPrefab(Vector2 pos, GameObject prefab)
    {
        Vector2 dir = (pos - new Vector2(transform.position.x, transform.position.y)).normalized;
        return ShootDirByPrefab(dir,prefab);
    }

    
    
    /// <summary>
    /// 정해진 방향으로 발사
    /// TODO: enum이 아니라 prefeb을 넘기는 방식이 더 생산성 있음.
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="type"></param>
    public void ShootDirByType(Vector2 dir, PrjtType type)
    {
        //type마다 프리팹 복사
        GameObject prjt;
        
        switch(type)
        {
            case PrjtType.Practice:
                prjt = Instantiate(practice);
                break;
            case PrjtType.Team:
                prjt = Instantiate(team);
                break;
            case PrjtType.Attend:
                prjt = Instantiate(attend);
                break;
            default:
                // 예외 처리
                // Debug.LogError("Unhandled PrjtType: " + type);
                return;
        }
        
        Projectile p = prjt.GetComponent<Projectile>();
        //p.speed = 1f;
        ShootDir(p, dir);
    }

    //prefab 넘기는 방식 ?
    public Projectile ShootDirByPrefab(Vector2 dir, GameObject prefab)
    {
        GameObject prjt = Instantiate(prefab);
        Projectile p = prjt.GetComponent<Projectile>();
        return ShootDir(p, dir);
    }

    // [Obsolete]
    // internal void ShootTO(Projectile prjt, Vector2 target)
    // {
    //     prjt.transform.position = shootPos.transform.position;
    //     prjt.target = target;
    //     prjt.UpdateDirection();
    // }

    internal Projectile ShootDir(Projectile prjt, Vector2 dir)
    {
        // Debug.Log("[Professor::ShootDir] dir : " + dir);
        prjt.transform.position = shootPos.transform.position;
        prjt.UpdateDirection(dir);
        prjt.OnSummon();
        return prjt;
    }


}
