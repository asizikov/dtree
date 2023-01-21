var rootDir = new DirectoryInfo("/workspaces/dtree/");

var tree = BuildTree(rootDir, 0, maxDepth: 10);

PrintTree(tree, "", false);

static void PrintTree(TreeNode tree, string prefix, bool isLast)
{
    var ancor = tree.Name == "." ? "" : isLast ? "└── " : "├── ";
    var nextAncor = tree.Name == "." ? "" : isLast ? "   " : "│   ";
    var currentPrefix = $"{prefix}{ancor}";
    var nextPrefix = $"{prefix}{nextAncor}";

    Console.Write($"{currentPrefix}");
    if (tree.Type == ItemType.Directory) {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("📁 ");
    }

    Console.Write(tree.Name + Environment.NewLine);
    if (tree.Type == ItemType.Directory) Console.ResetColor();

    tree.Children.Sort((a, b) => a.Type == b.Type ? a.Name.CompareTo(b.Name) : a.Type == ItemType.Directory ? -1 : 1);
    for(var i = 0; i < tree.Children.Count; i++)
    {
        PrintTree(tree.Children[i], nextPrefix, i == tree.Children.Count - 1);
    }
}

static TreeNode BuildTree(DirectoryInfo dir, int currentDepth, int maxDepth)
{
    var tree = new TreeNode
    {
        Name =  currentDepth == 0 ? "." : dir.Name,
        Type = ItemType.Directory,
        Children = new()
    };


    if (currentDepth <= maxDepth)
    {
        foreach (var subDir in dir.GetDirectories())
        {
            if (subDir.Name == ".git") continue;
            var subTree = BuildTree(subDir, currentDepth + 1, maxDepth);
            tree.Children.Add(subTree);
        }
    }
    dir.GetFiles().ToList().ForEach(f => tree.Children.Add(new TreeNode
    {
        Name = f.Name,
        Type = ItemType.File
    }));

    return tree;
}