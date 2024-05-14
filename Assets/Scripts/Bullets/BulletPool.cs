using System;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Bullets
{
    // 1. Create class of a pooledBullet 
    // 2. Create this pool via Playerservice
    //3. Create Consrtuctor of pool
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;

        private List<pooledBullet> pooledBullets = new List<pooledBullet>();

        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;

        }
        public BulletController GetBullet()
        {
            if (pooledBullets.Count > 0)
            {
                pooledBullet pooledBullet = pooledBullets.Find(item => !item.isUsed);
                if (pooledBullet != null)
                {
                    pooledBullet.isUsed = true;
                    return pooledBullet.bullet;
                }

            }
            return CreateNewPooledBullet();
        }

        private BulletController CreateNewPooledBullet()
        {
            pooledBullet pooledBullet = new pooledBullet();
            pooledBullet.bullet = new BulletController(bulletView, bulletScriptableObject);
            pooledBullet.isUsed = true;
            return pooledBullet.bullet;
        }

        public class pooledBullet
        {
            public BulletController bullet;
            public bool isUsed;

        }
    }
}