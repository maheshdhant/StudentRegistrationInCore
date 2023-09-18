using System;
using System.Collections.Generic;

namespace StudentRegistrationInCore.Entity;

public partial class TblClass
{
    public int ClassId { get; set; }

    public int Grade { get; set; }

    public string ClassTeacher { get; set; } = null!;

    public int? Total { get; set; }

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
