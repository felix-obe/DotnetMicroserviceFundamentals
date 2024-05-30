namespace DotnetMicroServiceFundamentals.Execution.Attribute;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ExecuteOnStartupAttribute : System.Attribute
{
}