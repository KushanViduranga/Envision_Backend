using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Envision.DAL.Models
{
    public class AircraftLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string Make { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string Model { get; set; }

        [Required]
        [Column(TypeName = "varchar(8)")]
        public string Registration { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CreateBy { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        public int? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        [NotMapped]
        public string Base64image { get; set; }

        [NotMapped]
        public string ImageName { get; set; }

        [NotMapped]
        public string ImageExtension { get; set; }

    }
}
