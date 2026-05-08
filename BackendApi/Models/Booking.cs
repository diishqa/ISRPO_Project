using System.ComponentModel.DataAnnotations;
using System.Data;
namespace BackendApi.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string  Phone { get; set; } = string.Empty;
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int TableId { get; set; }
    [Required]
    public string Status { get; set; } = "новое" ; 
}