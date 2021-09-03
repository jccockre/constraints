
namespace AnalogDevices.Constraints
{
    public class ValidationResult
    {
        public bool Status { get; }
        public string Warning { get; }
        public int Depth { get; }

        public static readonly ValidationResult OK = new ValidationResult(true, "", 1);

        public ValidationResult(bool status, string warning, int depth)
        {
            Status = status;
            Warning = warning;
            Depth = depth;
        }
    }
}
