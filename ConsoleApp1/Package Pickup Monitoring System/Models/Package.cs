using System.ComponentModel.DataAnnotations;

namespace Package_Pickup_Monitoring_System.Models
{
    public class Package
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Recipient Name")]
        public string RecipientName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Unit/Office")]
        public string UnitNumber { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Courier Company")]
        public string CourierCompany { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Package Type")]
        public string PackageType { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Arrival Date & Time")]
        public DateTime ArrivalDateTime { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Expected Pickup Date")]
        public DateTime? ExpectedPickupDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Claimed Date & Time")]
        public DateTime? ClaimedDateTime { get; set; }

        [Required]
        [Display(Name = "Received By")]
        public string ReceivedBy { get; set; } = string.Empty;

        public PackageStatus Status { get; set; } = PackageStatus.WaitingForPickup;

        public string? Notes { get; set; }
    }
}