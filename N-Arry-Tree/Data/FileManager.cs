using N_Arry_Tree.Entity;
using Newtonsoft.Json;

namespace N_Arry_Tree.Data;

public static class FileManager
{
    public static Node LoadJSON()
    {
        var fileContent = File.ReadAllText(@"C:\Users\abdel\RiderProjects\N-Arry-Tree\N-Arry-Tree\Data\N-Arry-Tree.json");
        var upperNode = JsonConvert.DeserializeObject<Node>(fileContent);
        
        return upperNode;
    }

    public static void Overwrite(Node node)
    {
        var fileContent = JsonConvert.SerializeObject(node, Formatting.Indented);
        File.WriteAllText(@"C:\Users\abdel\RiderProjects\N-Arry-Tree\N-Arry-Tree\Data\N-Arry-Tree.json", 
                    fileContent);
    }
}
