namespace MSGym.Domain.ModelErrors
{
    public class ModelErrors
    {
        public string Model { get; }
        private readonly List<ModelError> _errors;

        public ModelErrors(string model)
        {
            Model = model;
            _errors = new List<ModelError>();
        }

        public bool HasErrors => _errors.Count != 0;

        public IReadOnlyCollection<ModelError> Errors => _errors;

        public void AddError(string field, string message)
        {
            _errors.Add(new ModelError(field, message));
        }
    }
}
