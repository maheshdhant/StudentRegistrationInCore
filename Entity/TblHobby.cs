using System;
using System.Collections.Generic;

namespace StudentRegistrationInCore.Entity;

public partial class TblHobby
{
    public int HobbyId { get; set; }

    public string HobbyName { get; set; } = null!;

    public bool IsActive { get; set; }
}
