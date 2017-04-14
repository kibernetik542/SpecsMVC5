namespace SpecsMVC5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(2500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int? Stock { get; set; }

        public virtual Category Category { get; set; }
    }
}
