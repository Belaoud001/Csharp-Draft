using N_Arry_Tree.Data;

class Program
{
    public static void Main(string[] args)
    {
        var nodeList = NodeRepository.FindNodesByTags("sc-1 a-1 b-1 c-1 test this is the major");

        foreach (var node in nodeList)
        {
            Console.WriteLine(node.Name);
        }
    }
}
