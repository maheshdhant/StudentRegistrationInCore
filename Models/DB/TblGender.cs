using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationInCore.Models.DB;

public partial class TblGender
{
    [Key]
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
}
