using System;
using System.Collections;
using System.Collections.Generic;
using Script.Game;
using Script.Game.Enemy;
using Script.Game.Player;
using Script.Game.Projectile;
using UnityEngine;

public class Professor : MonoBehaviour
{
    public GameObject prefeb;
    public GameObject shootPos;
    public PatternController PatternController;
    [SerializeField] private Player player;
    public Direction facing;

    
    private void Awake()
    {
        facing = Direction.Left;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PatternController);
        player.OnScoreUpdate.AddListener(PatternController.OnScoreUpdate);
        StartCoroutine(PatternController.Rountine(this, player));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void tpLeft()
    {
        
    }

    internal void tpRight()
    {
        
    }

    internal void 과제()
    {
        TestShoot(facing.Vector());
    }

    internal void 팀플()
    {
        TestShoot(facing.Vector());
    }
    
    internal void 출첵(int num = 3, float degreeRange =  180f)
    {
        ShootArc(Vector2.down,num,degreeRange);
    }

    internal void ShootArc(Vector2 direction, int num, float degreeRange)
    {
        Vector2 currentDir = Quaternion.AngleAxis(-degreeRange/ num,Vector3.forward) * direction;
        for (int i = 0; i < num; i++)
        {
            direction = Quaternion.AngleAxis(degreeRange / num, Vector3.forward) * currentDir;
            TestShoot(direction);
        }
    }

    private IEnumerator TestPattern()
    {
        while (true)
        {
            
            GameObject prjt = Instantiate(prefeb);
            ShootDir(prjt.GetComponent<Projectile>(), Vector2.left);
            yield return new WaitForSeconds(3);
        }
    }
    
    public void TestShoot(Vector2 dir)
    {
        GameObject prjt = Instantiate(prefeb);
        Projectile p = prjt.GetComponent<Projectile>();
        p.speed = 0.05f;
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
        prjt.transform.position = shootPos.transform.position;
        prjt.UpdateDirection(dir);
    }
    
}
