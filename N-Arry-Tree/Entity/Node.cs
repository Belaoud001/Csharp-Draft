namespace N_Arry_Tree.Entity;

public class Node
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<Node> SubNodes { get; set; }
    public IList<Tag> Tags { get; set; }

    public Node()
    {
        
    }
    
    public Node(Guid id, string name, IList<Node> subNodes, IList<Tag> tags)
    {
        Id = id;
        Name = name;
        SubNodes = subNodes;
        Tags = tags;
    }
    
    public Node(string name) : this(Guid.NewGuid(), name, new List<Node>(), new List<Tag>())
    {
        Name = name;
    }

}
