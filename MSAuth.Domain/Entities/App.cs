namespace MSAuth.Domain.Entities
{
    public class App : BaseEntity
    {
        public string AppKey { get; set; } 

        public App()
        {
            AppKey = Guid.NewGuid().ToString();
        }
    }
}
