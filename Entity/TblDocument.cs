using System;
using System.Collections.Generic;

namespace StudentRegistrationInCore.Entity;

public partial class TblDocument
{
    public int DocId { get; set; }

    public string? Title { get; set; }

    public string DocPath { get; set; } = null!;

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
