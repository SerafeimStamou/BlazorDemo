
namespace CRUD.Models
{
    public class DatabaseResult
    {
        public object Result { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasFatalErrors { get; set; }
    }
}
