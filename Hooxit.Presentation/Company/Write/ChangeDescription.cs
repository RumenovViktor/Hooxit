﻿using Hooxit.Presentation.Company.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Hooxit.Presentation.Company.Write
{
    public class ChangeDescription : IPresentationSegment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
