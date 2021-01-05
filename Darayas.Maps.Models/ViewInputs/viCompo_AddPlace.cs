using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Darayas.Maps.Models.ViewInputs
{
    public class viCompo_AddPlace
    {
        [Display(Name="دسته")]
        [Required]
        public string CateId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        [Display(Name = "نام مکان")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "زوم")]
        [Required]
        public int Zoom { get; set; }

    }
}
