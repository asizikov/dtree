var rootDir = new DirectoryInfo("/workspaces/dtree/");

var (tree, depth) = BuildTree(rootDir, 0, maxDepth: 10);

PrintTree(tree, "", false);

static void PrintTree(TreeNode tree, string prefix, bool isLast)
{
    var ancor = tree.Name == "." ? "*" :
                 isLast ? "└── " : "├── ";
    var indent = $"{prefix}{ancor}";
    var nextPrefix = $"{prefix}{(isLast ? "   " : "│   ")}";
    Console.Write($"{indent}");
    if (tree.Type == ItemType.Directory) Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(tree.Name + Environment.NewLine);
    if (tree.Type == ItemType.Directory) Console.ResetColor();

    for(var i = 0; i < tree.Children.Count; i++)
    {
        PrintTree(tree.Children[i], nextPrefix, i == tree.Children.Count - 1);
    }
}

static (TreeNode tree, int depth) BuildTree(DirectoryInfo dir, int currentDepth, int maxDepth)
{
    var tree = new TreeNode
    {
        Name =  currentDepth == 0 ? "." : dir.Name,
        Type = ItemType.Directory,
        Children = new()
    };


    var maxSubTreeDepth = 0;
    if (currentDepth <= maxDepth)
    {
        foreach (var subDir in dir.GetDirectories())
        {
            if (subDir.Name == ".git") continue;
            var (subTree, subTreeDepth) = BuildTree(subDir, currentDepth + 1, maxDepth);
            maxSubTreeDepth = Math.Max(maxSubTreeDepth, subTreeDepth);
            tree.Children.Add(subTree);
        }
    }
    dir.GetFiles().ToList().ForEach(f => tree.Children.Add(new TreeNode
    {
        Name = f.Name,
        Type = ItemType.File
    }));

    return (tree, maxSubTreeDepth + 1);
}

public class TreeNode
{
    public string Name { get; set; } = string.Empty;
    public ItemType Type { get; set; } = ItemType.Directory;
    public List<TreeNode> Children { get; set; } = new();
}

public enum ItemType
{
    Directory,
    File
}