using System;

using System.Collections.Generic;


public class GenericObjectPool<T> where T : class
{
    private List<PoolItem<T>> poolItems = new List<PoolItem<T>>();

    protected T GetItem()
    {
        if (poolItems.Count > 0)
        {
            PoolItem<T> item = poolItems.Find(item => !item.isUsed);
            if (item != null)
            {
                item.isUsed = true;
                return item.Item;
            }
        }
        return CreateNewPooledItem();

    }

    private T CreateNewPooledItem()
    {
        PoolItem<T> newItem = new PoolItem<T>();
        newItem.Item = CreateItem();
        poolItems.Add(newItem);
        return newItem.Item;


    }
    protected virtual T CreateItem()
    {
        throw new NotImplementedException(" My child class havn't implemented this ");
    }
    public void ReturnItem(T _item)
    {
        PoolItem<T> poolItem = poolItems.Find(item => item.Item.Equals(_item));
        poolItem.isUsed = false;
    }

}
public class PoolItem<T>
{
    public T Item;
    public bool isUsed;
}