using N_Arry_Tree.Entity;

namespace N_Arry_Tree.Data;

public class NodeRepository
{
    private static Node upperNode;

    static NodeRepository()
    {
        upperNode = FileManager.LoadJSON();
    }
    
    public static KeyValuePair<Node?, Node?> FindNode(Guid id)
    {
        var nodeStack  = new Stack<Node>();
        var parentNode = upperNode;
        
        nodeStack.Push(upperNode);

        while (nodeStack.Count != 0)
        {
            var currentNode  = nodeStack.Pop();

            if (id.Equals(currentNode.Id))
                return new KeyValuePair<Node?, Node?>(parentNode, currentNode);
                
            foreach (var subNode in currentNode.SubNodes)
                nodeStack.Push(subNode);

            parentNode = currentNode;
        }

        return new KeyValuePair<Node?, Node?>(null, null);
    }

    public static void AddNode(Guid id, Node newNode)
    {
        var childParentPair = FindNode(id);
        var child = childParentPair.Value;
        
        child?.SubNodes.Add(newNode);
        
        FileManager.Overwrite(upperNode);
    }

    public static void AddTag(Guid id, Tag tag)
    {
        var childParentPair = FindNode(id);
        var targetedNode    = childParentPair.Value;
        
        targetedNode.Tags.Add(tag);
        
        FileManager.Overwrite(upperNode);
    }

    private static bool CheckTagOverlap(string input, IList<Tag> tags)
    {
        var clientTags = new HashSet<string>(input.Split());

        foreach (var tag in tags)
            if (clientTags.Contains(tag.Name))
                return true;

        return false;
    }

    public static IList<Node> FindNodesByTags(string input)
    {
        var nodeQueue    = new Queue<Node>();
        var matchedNodes = new List<Node>();

        nodeQueue.Enqueue(upperNode);

        while (nodeQueue.Count != 0)
        {
            var nodesPerLevel = nodeQueue.Count;
            var numberOfMatchedElements = 0;
            var isAtTheSameLevel = false;
            
            while (nodesPerLevel > 0)
            {
                var currentNode = nodeQueue.Dequeue();

                if (CheckTagOverlap(input, currentNode.Tags))
                {
                    if (!isAtTheSameLevel)
                        matchedNodes.Clear();

                    matchedNodes.Add(currentNode);
                    numberOfMatchedElements++;
                    isAtTheSameLevel = true;
                }

                foreach (var subNode in currentNode.SubNodes)
                    nodeQueue.Enqueue(subNode);
                
                nodesPerLevel--;
            }
            
            switch (numberOfMatchedElements)
            {
                case > 1:
                    return matchedNodes;
                case 1 when matchedNodes.Last().SubNodes.Count > 0:
                {
                    nodeQueue.Clear();
                    
                    foreach (var subNode in matchedNodes.Last().SubNodes)
                        nodeQueue.Enqueue(subNode);

                    break;
                }
                case 1:
                    return matchedNodes;
            }
        }

        return matchedNodes;
    }
}
