using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationInCore.Models.DB;

public partial class TblClass
{
    [Key]
    public int ClassId { get; set; }

    public int Grade { get; set; }

    public string ClassTeacher { get; set; } = null!;

    public int? Total { get; set; }

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
