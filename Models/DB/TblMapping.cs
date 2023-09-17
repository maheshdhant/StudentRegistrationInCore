using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationInCore.Models.DB;

public partial class TblMapping
{
    [Key]
    public int MapId { get; set; }

    public int StudentId { get; set; }

    public int HobbyId { get; set; }
}
