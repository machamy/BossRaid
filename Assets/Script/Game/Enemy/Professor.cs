using System.Collections;
using System.Collections.Generic;
using Script.Game.Enemy;
using Script.Game.Projectile;
using UnityEngine;

public class Professor : MonoBehaviour
{
    public PatternController PatternController;
    public GameObject prefeb;
    public GameObject shootPos;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestPattern());
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void ShootTO(Projectile prjt, GameObject target)
    {
        prjt.transform.position = shootPos.transform.position;
        prjt.target = target.transform.position;
        prjt.UpdateDirection();
    }

    void ShootDir(Projectile prjt, Vector2 dir)
    {
        prjt.transform.position = shootPos.transform.position;
        prjt.UpdateDirection(dir);
    }
    
}
