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