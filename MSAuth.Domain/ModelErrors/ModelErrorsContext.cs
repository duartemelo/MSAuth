namespace MSAuth.Domain.ModelErrors
{
    public class ModelErrorsContext
    {
        private readonly List<ModelErrors> _modelErrors;
        public ModelErrorsContext()
        {
            _modelErrors = new List<ModelErrors>();
        }

        public bool HasErrors => _modelErrors.Count != 0;
        public bool HasModelError(string model) => _modelErrors.FirstOrDefault(x => x.Model == model) != null;
        public IReadOnlyCollection<ModelErrors> ModelErrors => _modelErrors;

        public void AddModelError(string model, string field, string message)
        {
            var modelError = _modelErrors.FirstOrDefault(x => x.Model == model);
            if (modelError != null)
            {
                modelError.AddError(field, message);
            } else
            {
                modelError = new ModelErrors(model);
                modelError.AddError(field, message);
                _modelErrors.Add(modelError);
            }
        }
    }
}
