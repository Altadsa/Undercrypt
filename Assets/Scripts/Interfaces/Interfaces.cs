public interface IAxisInput
{
    void ReadInput();
    bool HasAxisInput { get; }
    float Horizontal { get; }
    float Vertical { get; }
}

public interface ICommandInput
{
    bool CommandKey { get; }

}