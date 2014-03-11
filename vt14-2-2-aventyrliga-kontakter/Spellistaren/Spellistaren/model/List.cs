using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spellistaren.model
{
    public class List
    {
        [Required]
        public int ListID
        {
            get;
            set;
        }

        [Required]
        public int UserID
        {
            get;
            set { value = 1; } //Hårdkodar in värdet för användaren..
        }

        [Required]
        [StringLength(30)]
        public string ListName
        {
            get;
            set;
        }

    }
}