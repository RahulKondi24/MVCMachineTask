using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMachineTask.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter CategoryName"), MaxLength(20)]
        public string CategoryName { get; set; }
    }
}