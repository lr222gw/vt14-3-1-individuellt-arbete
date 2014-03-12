using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spellistaren.model
{
    public class Game
    {
        public int GameID
        {
            get;
            set;
        }
        [Required]
        public int? ScoreID
        {
            get;
            set;
        }
        [Required]
        public int UserID
        {
            get { return 1; }
            set { value = 1; }
        }

        [Required]
        [StringLength(65)]
        public string GameName
        {
            get;
            set;
        }
        [StringLength(35)]
        public string CompanyName
        {
            get;
            set;
        }

        [StringLength(1000)]
        public string Story
        {
            get;
            set;
        }
        public int? PlayersOnline
        {
            get;
            set;
        }
        public short? PlayersOffline //"short" ska vara en datatyp som innebär samma sak som SmallInt!...
        {
            get;
            set;
        }
        public DateTime? ReleaseDate //TODO: Kolla om det finns något mer snarlikt slq's Date.
        {
            get;
            set;
        }
        [StringLength(2000)]
        public string CustomNote
        {
            get;
            set;
        }

    }
}