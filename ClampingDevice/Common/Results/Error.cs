namespace ClampingDevice.Common.Results;

public record Error(string Code, string Message)
{
    public static readonly Error None = new("", "");
    public static readonly Error Unknown = new("Unknown", "An unknown error occurred.");

    public override string ToString() => Code;
}
