using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMachineTask.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter ProductName"), MaxLength(20)]
        public string ProductName { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}