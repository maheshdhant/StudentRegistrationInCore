using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationInCore.Models.DB;

public partial class TblHobby
{
    [Key]
    public int HobbyId { get; set; }

    public string HobbyName { get; set; } = null!;

    public bool IsActive { get; set; }
}
