using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Envision.DAL.Models
{
    public class AircraftLogImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int AircraftLogId { get; set; }

        [Required]
        public Guid ImageName { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string ImageExtension { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int UploadBy { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UploadDate { get; set; }

        [ForeignKey("AircraftLogId")]
        public virtual AircraftLog AircraftLogs { get; set; }
    }
}
