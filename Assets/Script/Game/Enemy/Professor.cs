using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using Script.Game.Enemy;
using Script.Game.Player;
using Script.Game.Projectile;
using Script.Global;
using UnityEngine;

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
    public Direction facing;
    [SerializeField] public Transform PosSystem;
    new public SpriteRenderer renderer;

    private void Awake()
    {
        facing = Direction.Left;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        Debug.Log(PatternController);
        player.OnScoreUpdateEvent.AddListener(PatternController.OnScoreUpdate);
        ApplyDBdata();
        StartCoroutine("FadeOut");
        StartCoroutine(PatternController.Rountine(this, player));
    }

    public void ApplyDBdata()
    {
        var objs = new [] { attend, practice, team };
        var dbs = new [] { DB.AttendProjectile, DB.PracticeProjectile, DB.TeamProjectile };
        for (int i = 0; i< 3;i ++)
        {
            var prjt = objs[i].GetComponent<Projectile>();
            var datas = dbs[i];
            if(datas == null)
                continue;
            prjt.speed = float.Parse(datas[0]);
            prjt.parringScore = int.Parse(datas[1]);
            prjt.damage = int.Parse(datas[2]);
        }
    }
    
    private IEnumerator FadeOut()
    {
        int i = 10;
        while (i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator FadeIn()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    internal void tpLeft()
    {
        Vector3 Lp = PosSystem.GetChild(0).position;
        if(shootPos.transform.position != Lp)
        {
            transform.position = Lp;
        }
    }

    internal void tpRight()
    {
        Vector3 Rp = PosSystem.GetChild(1).position;
        if(shootPos.transform.position != Rp)
        {
            transform.position = Rp;
        }
    }

    internal void tpLeftUp()    //고정Pos 생성해서 tp
    {
        Vector3 LUp = PosSystem.GetChild(2).position;
        transform.position = LUp;
    }

    internal void tpRightUp()    
    {
        Vector3 RUp = PosSystem.GetChild(3).position;
        transform.position = RUp;
    }

    
    internal void 과제() => 과제(facing.Vector());
    internal void 과제(Vector2 dir)
    {
        Debug.Log("[Professor::과제]");
        ShootDirByPrefab(dir, practice);
    }
    
    internal void 팀플() => 팀플(facing.Vector());
    internal void 팀플(Vector2 dir)
    {
        Debug.Log("[Professor::팀플]");
        ShootDirByPrefab(dir, team);
    }
    
    internal void 출첵(int num = 3, float degreeRange =  120f)
    {
        Debug.Log("[Professor::출첵]");
        Vector2 startPoint = Vector2.zero;
        Vector2 endPoint = Vector2.zero;
        if (facing == Direction.Left)
        {
            startPoint = new Vector2(-10, -1);
            endPoint = new Vector2(0, -1);
        }
        else if (facing == Direction.Right)
        {
            startPoint = new Vector2(0, -1);
            endPoint = new Vector2(10, -1);
        }

        // 투사체 그룹 집합
        HashSet<Projectile> group = new HashSet<Projectile>();
        foreach (var pos in GetSplitPoss(startPoint, endPoint, num))
        {
            Projectile current = ShootPosByPrefab(pos, attend);
            current.Group = group;
            group.Add(current);
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
            Debug.Log("[Professor::ShootArc] currentDir : "+currentDir);
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
                Debug.LogError("Unhandled PrjtType: " + type);
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
        Debug.Log("[Professor::ShootDir] dir : " + dir);
        prjt.transform.position = shootPos.transform.position;
        prjt.UpdateDirection(dir);
        return prjt;
    }


}
