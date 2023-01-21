var rootDir = new DirectoryInfo("/workspaces/dtree/");

var (tree, depth) = BuildTree(rootDir, 0, maxDepth: 10);

Console.WriteLine("Tree depth: " + depth);

PrintTre(tree, 0);

static void PrintTre(TreeNode tree, int depth)
{
    var indent = new string(' ', depth * 4);

    var ancor =  tree.Children.Count == 0 ? "└── " : "├── ";
    Console.Write($"│{indent}{ancor} ");
    if (tree.Type == ItemType.Directory) Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(tree.Name + Environment.NewLine);
    if (tree.Type == ItemType.Directory) Console.ResetColor();

    foreach (var child in tree.Children)
    {
        PrintTre(child, depth + 1);
    }
}

static (TreeNode tree, int depth) BuildTree(DirectoryInfo dir, int currentDepth, int maxDepth)
{
    var tree = new TreeNode
    {
        Name = dir.Name,
        Type = ItemType.Directory,
        Children = new List<TreeNode>()
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