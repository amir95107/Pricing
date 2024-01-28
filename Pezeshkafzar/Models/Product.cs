namespace Pezeshkafzar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(Order = 1)]
        [StringLength(350)]
        public string Title { get; set; }

        [Column(Order = 2)]
        [StringLength(500)]
        public string ShortDescription { get; set; }

        [Column(Order = 3)]
        public string Text { get; set; }

        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Price { get; set; }

        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PriceAfterDiscount { get; set; }

        [Column(Order = 6)]
        [StringLength(50)]
        public string ImageName { get; set; }

        [Column(Order = 7)]
        public DateTime CreateDate { get; set; }

        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LikeCount { get; set; }

        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Stock { get; set; }

        public double? Point { get; set; }

        public int? SellerID { get; set; }

        [Column(Order = 10)]
        public bool IsAcceptedByAdmin { get; set; }

        [Column(Order = 11)]
        public bool IsActive { get; set; }

        [Column(Order = 12)]
        [StringLength(450)]
        public string SefUrl { get; set; }

        public DateTime? LastUpdated { get; set; }

        public int? Visit { get; set; }

        public string Garanty { get; set; }
    }
}
