using System.ComponentModel.DataAnnotations;

namespace Reviews.CommandApi.Infra.Data.Configurations
{
    public record DatabaseConfig
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; init; }
    }
}
