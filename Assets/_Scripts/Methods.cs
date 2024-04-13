using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Methods : MonoBehaviour
{
    //public static List<T> CreateList<T>(int capacity) => Enumerable.Repeat(default(T), capacity).ToList();
    public static void UpgradeCheck<T>(List<T> _list, int _length) where T : new()
    {
        try
        {
            if (_list.Count == 0) _list = new T[3].ToList();
            while(_list.Count < _length) _list.Add(new T());
        }
        catch 
        {
            _list = new T[3].ToList();
        }
    }
}
