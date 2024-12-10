namespace MSGym.Domain.ModelErrors
{
    public class ModelError
    {
        public string Field { get; }
        public string Message { get; }
        public ModelError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
}
