using System;
using System.Collections.Generic;
using CosmicCuration.Enemy;


public class EnemyPool
{
    private List<PoolEnemy> poolEnemies = new List<PoolEnemy>();
    private EnemyView enemyView;
    private EnemyScriptableObject enemyScriptableObject;

    public EnemyPool(EnemyView enemyView, EnemyScriptableObject enemyScriptableObject)
    {
        this.enemyView = enemyView;
        this.enemyScriptableObject = enemyScriptableObject;
    }

    public EnemyController GetEnemy()
    {
        if (poolEnemies.Count > 0)
        {
            PoolEnemy poolEnemy = poolEnemies.Find(item => !item.isUsed);
            if (poolEnemy != null)
            {
                poolEnemy.isUsed = true;
                return poolEnemy.enemyController;
            }

        }
        return CreateNewEnemy();
    }

    private EnemyController CreateNewEnemy()
    {
        PoolEnemy poolEnemy = new PoolEnemy();
        poolEnemy.enemyController = new EnemyController(enemyView, enemyScriptableObject.enemyData);
        poolEnemy.isUsed = true;
        poolEnemies.Add(poolEnemy);
        return poolEnemy.enemyController;
    }
    public void ReturnEnemyToPool(EnemyController returnEnemyController)
    {
        PoolEnemy poolEnemy = poolEnemies.Find(item => item.enemyController.Equals(returnEnemyController));
        poolEnemy.isUsed = false;
    }
}

public class PoolEnemy
{
    public bool isUsed;
    public EnemyController enemyController;
}
