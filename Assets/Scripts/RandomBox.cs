using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox<T>
{
    struct Box
    {
        public T item;
        public int value;
        public Box(T item, int value)
        {
            this.item = item;
            this.value = value;
        }
    }

    List<Box> list;
    int totalValue;

    public RandomBox()
    {
        list = new List<Box>();
        totalValue = 0;
    }

    public void Push(T item, int value)
    {
        list.Add(new Box(item, value));
        totalValue += value;
    }

    public T Pick()
    {
        float random = Random.value;
        int pickValue = (int)(totalValue * random);

        int min = 0;
        int max = 0;
        for(int i = 0; i<list.Count; i++)
        {
            max += list[i].value;

            // 아이템에 해당하는 값인지 체크.
            if(min <= pickValue && pickValue < max)
                return list[i].item;

            min += list[i].value;
        }

        return default(T);
    }
}
