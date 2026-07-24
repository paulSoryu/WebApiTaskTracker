namespace WebApiTaskTracker.Utilities;

public class TaskConstraints
{
    public const int TitleMinLength = 3;
    public const int TitleMaxLength = 50;
    public const int DescriptionMaxLength = 100;
}

public class UserConstraints
{
    public const int NameMinLength = 3;
    public const int NameMaxLength = 50;
    public const int EmailAddressMaxLength = 256;
}

public class CategoryConstraints
{
    public const int TitleMaxLength = 20;
    public const int DescriptionMaxLength = 2000;
    public const int ColorValue = 0;
}
