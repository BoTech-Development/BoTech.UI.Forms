namespace BoTech.UI.Forms.Models.PropertyAnnotations;

public class GroupIDManager
{
    public static GroupIDManager Instance = new GroupIDManager();
    private List<string> DeclaredGroupIDs  = new List<string>();

    public void AddGroupID(string groupName)
    {
        if (DeclaredGroupIDs.Contains(groupName))
            throw new ArgumentException($"GroupName {groupName} already exists! GroupName must be unique."); 
        else 
            DeclaredGroupIDs.Add(groupName);
    }
}