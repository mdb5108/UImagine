using UnityEngine;

public static class ArrayUtil
{
    public static void ShuffleArray<T>(T[] a)
    {
        //Shuffle elements
        int n = a.Length;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n+1);
            T v  = a[k];  
            a[k] = a[n];  
            a[n] = v;  
        }  
    }
}
