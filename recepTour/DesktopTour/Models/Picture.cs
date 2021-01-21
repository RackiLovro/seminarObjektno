﻿using System;
using System.Collections.Generic;

namespace DesktopTour
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string Source
        {
            get
            {
                return "data:image/jpeg;base64," + this.Url;
            }
        }
    }
}
