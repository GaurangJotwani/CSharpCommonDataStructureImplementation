using System.Collections;
using System.Diagnostics;

namespace CommonDataStructureImplementations.DynamicArray;

interface DynamicArray : IEnumerable<int>
{
    void Add(int num);
    bool IsEmpty();
    int Count { get; }
    void RemoveAtIndex(int i);
    int this[int index] { get; set; }
    void Sort(Comparison<int>? comparison = null);
    void Reverse();
}

public class DynamicArrayImpl : DynamicArray
{
    private int cnt;
    private int size = 1;
    private int[] arr;
    
    
    public DynamicArrayImpl()
    {
        arr = new int[size];
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return new DynamicArrayEnumerator(arr, cnt);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(int num)
    {
        if (cnt == size) Resize();
        arr[cnt++] = num;
    }
    
    private void Resize()
    {
        size *= 2;
        var newArr = new int[size];
        for (var i = 0; i < cnt; i++) newArr[i] = arr[i];
        arr = newArr;
    }

    public bool IsEmpty() => cnt == 0;

    public int Count => cnt;

    public void RemoveAtIndex(int i)
    {
        if (i < 0 || i >= cnt) throw new ArgumentOutOfRangeException();
        var newArr = new int[size];
        var idx = 0;
        for (var j = 0; j < cnt; j++)
        {
            if (j == i) continue;
            newArr[idx++] = arr[j];
        }
        arr = newArr;
        cnt--;
    }

    public int this[int index]
    {
        get
        {
            if (index >= cnt || cnt < 0) throw new IndexOutOfRangeException();
            return arr[index];
        }
        set
        {
            if (index >= cnt || cnt < 0) throw new IndexOutOfRangeException();
            arr[index] = value;
        }
    }

    public void Sort(Comparison<int>? comparison = null)
    {
        if (comparison == null) Array.Sort(arr, 0, cnt);
        else Array.Sort(arr, 0, cnt, Comparer<int>.Create(comparison));
    } 

    public void Reverse() => Array.Reverse(arr, 0, cnt);

    private class DynamicArrayEnumerator(int[] array, int count) : IEnumerator<int>
    {
        private int _position = -1;

        public bool MoveNext()
        {
            _position++;
            return _position < count;
        }

        public void Reset()
        {
            _position = -1;
        }

        public int Current
        {
            get
            {
                if (_position < 0 || _position >= count) throw new InvalidOperationException();
                return array[_position];
            }
        }

        object? IEnumerator.Current => Current;

        public void Dispose() { }
    }
}

public static class Test
{
    static void Main(string[] args)
    {
        var da = new DynamicArrayImpl();
        Debug.Assert(da.Count == 0);
        Debug.Assert(da.IsEmpty());
        da.Add(10);
        da.Add(30);
        da.Add(5);
        da.Add(2);
        Debug.Assert(da.Count == 4);
        Debug.Assert(da[0] == 10);
        Debug.Assert(da[1] == 30);
        Debug.Assert(da[3] == 2);
        Debug.Assert(!da.IsEmpty());
        da.RemoveAtIndex(1);
        Debug.Assert(da.Count == 3);
        Debug.Assert(da[1] == 5);
        foreach (int num in da) Console.Write(num + " ");
        Console.WriteLine();
        da.Sort();
        foreach (int num in da) Console.Write(num + " ");
        Console.WriteLine();
        da.Sort((x, y) => y.CompareTo(x));
        foreach (int num in da) Console.Write(num + " ");
        Console.WriteLine();
        da.Reverse();
        foreach (int num in da) Console.Write(num + " ");
        Console.WriteLine();
    }
}