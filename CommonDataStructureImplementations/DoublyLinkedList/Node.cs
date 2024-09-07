namespace CommonDataStructureImplementations.DoublyLinkedList;

public class Node
{
    public int Value { get; set; }
    public Node Next { get; set; }
    public Node Prev { get; set; }

    public Node(){}

    public Node(int val, Node nxt, Node prev)
    {
        Value = val;
        Next = nxt;
        Prev = prev;
    }
    
    public Node(int val)
    {
        Value = val;
    }
}