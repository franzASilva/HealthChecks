using System.ComponentModel.DataAnnotations;

namespace HealthChecks.Domain.Models;

public class DummyModel
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public int Value { get; set; }

    [Required]
    public int ValueOffset { get; set; }
}
