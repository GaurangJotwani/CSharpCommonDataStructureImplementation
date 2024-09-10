using System.Collections;
using System.Diagnostics;

namespace CommonDataStructureImplementations.DoublyLinkedList;

public class LinkedList : IEnumerable
{
    public int Count { get; private set; }
    private Node Head;
    private Node Dummy = new Node();
    
    public LinkedList()
    {
        Head = Dummy;
    }
    
    public IEnumerator<int> GetEnumerator()
    {
        return new LinkedListEnumerator(Dummy);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(int val)
    {
        var tmp = Head;
        var node = new Node
        {
            Prev = tmp,
            Value = val
        };
        
        tmp.Next = node;
        Head = node;
        Count++;
    }
    public void RemoveFromBack()
    {
        if (Count == 0) throw new ArgumentException();
        var tmp = Head.Prev;
        Head.Prev = null;
        tmp.Next = null;
        Head = tmp;
        Count--;
    }
    
    public void RemoveFromFront()
    {
        if (Count == 0) throw new ArgumentException();

        var tmp = Dummy.Next.Next;
        
        Dummy.Next.Prev = null;
        Dummy.Next.Next = null;
        
        Dummy.Next = tmp;
        if (tmp != null) tmp.Prev = Dummy;
        else Head = Dummy;
        Count--;
    }
    
    public void AddFromFront(int val)
    {
        var tmp = new Node(val, Dummy.Next, Dummy);
        if (Count != 0) Dummy.Next.Prev = tmp;
        else Head = tmp;
        Dummy.Next = tmp;
        Count++;
    }
    
    public void RemoveFromBackIterate()
    {
        if (Count == 0) throw new ArgumentException();
        var curr = Dummy;
        while (curr.Next.Next != null) curr = curr.Next;
        Head.Prev = null;
        Head = curr;
        Head.Next = null;
        Count--;
    }

    public void PrintFromFront()
    {
        var curr = Dummy.Next;
        while (curr != null)
        {
            Console.Write(curr.Value + " ");
            curr = curr.Next;
        }
        Console.WriteLine();
    }
    
    public void PrintFromBack()
    {
        var curr = Head;
        while (curr != Dummy)
        {
            Console.Write(curr.Value + " ");
            curr = curr.Prev;
        }
        Console.WriteLine();
    }

    private void DeleteNode(Node n)
    {
        if (n.Prev == Dummy) RemoveFromFront();
        else if (n.Next == null) RemoveFromBack();
        else
        {
            var prev = n.Prev;
            var nxt = n.Next;
            prev.Next = nxt;
            nxt.Prev = prev;
            n.Prev = null;
            n.Next = null;
        }
    }

    public void Filter(Func<int, bool> predicate)
    {
        var curr = Dummy.Next;
        while (curr != null)
        {
            var tmp = curr.Next;
            if (!predicate(curr.Value)) DeleteNode(curr);
            curr = tmp;
        }
    }
    
    public void Reverse()
    {
        if (Count <= 1) return; // No need to reverse if the list has 1 or 0 nodes.

        var l = Dummy.Next;
        var r = Head;

        // We will iterate until l and r cross or meet
        while (l != r && l.Prev != r)
        {
            // Swap the l and r node values (we could also swap the nodes themselves but simpler to swap values)
            var temp = l.Value;
            l.Value = r.Value;
            r.Value = temp;

            // Move l towards the right and r towards the left
            l = l.Next;
            r = r.Prev;
        }

        // After the swap, set the new Head to be Dummy.Next
        Head = Dummy.Next;
        while (Head.Next != null)
        {
            Head = Head.Next;
        }
    }

    public int this[int idx]
    {
        get
        {
            if (idx >= Count || idx < 0) throw new ArgumentException();
            var curr = Dummy;
            while (idx != -1)
            {
                curr = curr.Next;
                idx--;
            }

            return curr.Value;
        }
        set
        {
            if (idx >= Count || idx < 0) throw new ArgumentException();
            var curr = Dummy;
            while (idx != -1)
            {
                curr = curr.Next;
                idx--;
            }

            curr.Value = value;
        }
    }

    private class LinkedListEnumerator : IEnumerator<int>
    {
        private Node _node; // Current node
        private readonly Node _dummy; // Sentinel node to reset the iterator
    
        public LinkedListEnumerator(Node node)
        {
            _dummy = node;
            _node = _dummy; // Start before the first element (at Dummy)
        }

        public bool MoveNext()
        {
            _node = _node.Next;
            return _node != null;
        }

        public void Reset()
        {
            _node = _dummy;
        }

        object? IEnumerator.Current => Current;

        public int Current
        {
            get
            {
                if (_node == null) throw new InvalidOperationException();
                return _node.Value;

            }
        }

        public void Dispose()
        {
            
        }
    }
}

public static class Test
{
    // public static void Main()
    // {
    //     var ll = new LinkedList();
    //     Debug.Assert(ll.Count == 0);
    //     ll.Add(5);
    //     ll.Add(10);
    //     ll.Add(15);
    //     Debug.Assert(ll.Count == 3);
    //     ll.PrintFromFront();
    //     ll.PrintFromBack();
    //     
    //     ll.AddFromFront(2);
    //     ll.AddFromFront(3);
    //     ll.PrintFromFront();
    //     Debug.Assert(ll.Count == 5);
    //     
    //     
    //     ll.RemoveFromBack();
    //     ll.PrintFromFront();
    //     ll.RemoveFromBackIterate();
    //     ll.PrintFromFront();
    //     ll.RemoveFromFront();
    //     ll.PrintFromFront();
    //     ll.RemoveFromBack();
    //     ll.RemoveFromFront();
    //     Debug.Assert(ll.Count == 0);
    //     
    //     ll.Add(10);
    //     ll.Add(15);
    //     ll.Add(20);
    //     ll.Add(25);
    //     ll.PrintFromFront();
    //     ll.Reverse();
    //     ll.PrintFromFront();
    //     ll.Filter((x) => x <= 1);
    //     ll.PrintFromFront();
    //     ll.Add(10);
    //     ll.Add(15);
    //     ll.Add(20);
    //     ll.Add(25);
    //     ll.PrintFromFront();
    //     Debug.Assert(ll[2] == 20);
    //     Debug.Assert(ll[0] == 10);
    //     
    //     Debug.Assert(ll[3] == 25);
    //     ll[3] = 30;
    //     ll.PrintFromFront();
    //     foreach (var num in ll)
    //     {
    //         Console.WriteLine(num);
    //     }
    //     
    //     foreach (var num in ll)
    //     {
    //         Console.WriteLine(num);
    //     }
    //     
    //     foreach (var num in ll)
    //     {
    //         Console.WriteLine(num);
    //     }
    // }
}

























