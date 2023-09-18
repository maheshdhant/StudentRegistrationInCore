using System;
using System.Collections.Generic;

namespace StudentRegistrationInCore.Entity;

public partial class TblImage
{
    public int ImageId { get; set; }

    public string? Title { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
