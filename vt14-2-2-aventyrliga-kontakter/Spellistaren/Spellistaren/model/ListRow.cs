﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spellistaren.model
{
    public class ListRow
    {
        [Required]
        public int ListRowID
        {
            get;
            set;
        }
        [Required]
        public int ListID
        {
            get;
            set;
        }
        [Required]
        public int GameID
        {
            get;
            set;
        }
    }
}