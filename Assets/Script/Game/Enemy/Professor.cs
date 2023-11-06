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
        player.OnScoreUpdate.AddListener(PatternController.OnScoreUpdate);
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

    internal void 과제()
    {
        Debug.Log("[Professor::과제]");
        TestShoot(facing.Vector(), PrjtType.Practice);
    }

    internal void 팀플()
    {
        Debug.Log("[Professor::팀플]");
        TestShoot(facing.Vector(), PrjtType.Team);
    }
    
    internal void 출첵(int num = 3, float degreeRange =  120f)
    {
        Debug.Log("[Professor::출첵]");
        foreach (Vector2 direction in GetArc(Vector2.down,num,degreeRange))
        {
            TestShoot(direction, PrjtType.Attend);
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

    private IEnumerator TestPattern()
    {
        while (true)
        {
            
            GameObject prjt = Instantiate(practice);
            ShootDir(prjt.GetComponent<Projectile>(), Vector2.left);
            yield return new WaitForSeconds(3);
        }
    }
    
    public void TestShoot(Vector2 dir, PrjtType type)
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

    internal void ShootTO(Projectile prjt, GameObject target)
    {
        prjt.transform.position = shootPos.transform.position;
        prjt.target = target.transform.position;
        prjt.UpdateDirection();
    }

    internal void ShootDir(Projectile prjt, Vector2 dir)
    {
        Debug.Log("[Professor::ShootDir] dir : " + dir);
        prjt.transform.position = shootPos.transform.position;
        prjt.UpdateDirection(dir);
    }


}
