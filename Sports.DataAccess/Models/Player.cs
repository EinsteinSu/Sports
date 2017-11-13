using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports.DataAccess.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        ///     name use for display
        /// </summary>
        [MaxLength(20)]
        public string DisplayName { get; set; }

        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [MaxLength(5)]
        public string RegistNumber { get; set; }

        [MaxLength(5)]
        public string BibNumber { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Weight(kg)")]
        public float? Weight { get; set; }

        [Display(Name = "Height(cm)")]
        public float? Hight { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Bone Date")]
        public DateTime? BoneDate { get; set; }

        [Display(Name = "Self taken score(original score)")]
        public float? SelfTakenScore { get; set; }

        [Display(Name = "Identity Type")]
        public IdentityType? IdentityType { get; set; }

        [Display(Name = "Identity Number")]
        [MaxLength(50)]
        public string IdentityNumber { get; set; }

        public int? Level { get; set; }

        public string Description { get; set; }
    }

    public class PlayerPhoto
    {
        [Key]
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        public byte[] Photo { get; set; }

        public Player Player { get; set; }
    }

    public enum Gender
    {
        Female,
        Male
    }

    public enum IdentityType
    {
        IDCard,
        Passport,
        Others
    }
}