using System;
using System.Collections.Generic;

namespace StudentRegistrationInCore.Entity;

public partial class TblGender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
