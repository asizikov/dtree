var rootDir = new DirectoryInfo(".");
var maxDepth = 10;
var printAll = false;

if (args.Length > 0)
{
    var set = new HashSet<string>(args);
    if (set.Contains("--help") || set.Contains("-h"))
    {
        Console.WriteLine("Usage: dtree [options]");
        Console.WriteLine("Options:");
        Console.WriteLine("  -h, --help\t\tShow this help message and exit");
        Console.WriteLine("  -p, --path\t\tSet root path of tree");
        Console.WriteLine("  -a, --all\t\tPrint all files and directories");
        Console.WriteLine("  -d, --max-depth\tSet max depth of tree");
        return;
    }

    if (set.Contains("--max-depth") || set.Contains("-d"))
    {
        var index = Array.IndexOf(args, "-d");
        if (index == -1) index = Array.IndexOf(args, "--max-depth");
        if (index + 1 < args.Length)
        {
            if (int.TryParse(args[index + 1], out var depth))
            {
                if (depth < 0) ErrorAndExit("😱 Max depth must be greater than 0");
                maxDepth = depth;
            }
        }
    }

    if (set.Contains("--path") || set.Contains("-p"))
    {
        var index = Array.IndexOf(args, "-p");
        if (index == -1) index = Array.IndexOf(args, "--path");
        if (index + 1 < args.Length)
        {
            var path = args[index + 1];
            if (Directory.Exists(path))
            {
                rootDir = new DirectoryInfo(path);
            }
        }
    }

    if (set.Contains("--all") || set.Contains("-a"))
    {
        printAll = true;
    }
}

var tree = BuildTree(rootDir, 0, maxDepth);

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

TreeNode BuildTree(DirectoryInfo dir, int currentDepth, int maxDepth)
{
    var tree = new TreeNode
    {
        Name =  currentDepth == 0 ? "." : dir.Name,
        Type = ItemType.Directory,
        Children = new()
    };

    if (currentDepth < maxDepth)
    {
        foreach (var subDir in dir.GetDirectories())
        {
            if (subDir.Name.StartsWith(".") && !printAll) continue;
            var subTree = BuildTree(subDir, currentDepth + 1, maxDepth);
            tree.Children.Add(subTree);
        }
    }
    dir.GetFiles().Where(f => !f.Name.StartsWith(".") || printAll)
    .ToList()
    .ForEach(f => tree.Children.Add(new TreeNode
    {
        Name = f.Name,
        Type = ItemType.File
    }));

    return tree;
}

void ErrorAndExit(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
    Environment.Exit(1);
}