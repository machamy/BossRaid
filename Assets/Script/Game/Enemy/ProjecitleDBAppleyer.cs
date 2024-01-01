using Script.Global;
using UnityEngine;

namespace Script.Game.Enemy.Projectile
{
    public class ProjecitleDBAppleyer : MonoBehaviour, DBUser
    {
        public GameObject attend, practice, team, fire;

        public void Start()
        {
            ApplyDBdata();
        }
        public void ApplyDBdata()
        {
            GameObject[] prefebs = new [] { attend, practice, team };
            var dbs = new [] { DB.AttendProjectile, DB.PracticeProjectile, DB.TeamProjectile };
            for (int i = 0; i< 3;i ++)
            {
                var prjt = prefebs[i].GetComponent<Game.Projectile.Projectile>();
                var datas = dbs[i];
                if(datas == null)
                    continue;
                prjt.speed = float.Parse(datas[0]);
                prjt.parringScore = int.Parse(datas[1]);
                prjt.damage = int.Parse(datas[2]);
            }

            FireWall fire = this.fire.GetComponent<FireWall>();
            
            if (DB.FireWall != null)
            {
                fire.lifetime = float.Parse(DB.FireWall[0]);
                fire.damage = int.Parse(DB.FireWall[1]);
            }
            if (DB.FireWallAttack != null)
            {
                fire.warnTime = float.Parse(DB.FireWallAttack[0]);
                fire.PreviewSpeed = float.Parse(DB.FireWallAttack[1]);
            }
            
        }
    }
}