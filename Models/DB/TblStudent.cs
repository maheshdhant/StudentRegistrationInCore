using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationInCore.Models.DB;

public partial class TblStudent
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public int ClassId { get; set; }

    public string? Phone { get; set; }

    public int? DocId { get; set; }

    public DateTime? RegisteredDate { get; set; }

    public int? GenderId { get; set; }

    public int? ImageId { get; set; }

    public string? Hobbies { get; set; }

    public bool? Football { get; set; }

    public bool? Basketball { get; set; }

    public bool? Cricket { get; set; }

    public bool? Chess { get; set; }

    public bool? Tennis { get; set; }

    public bool? Drawing { get; set; }

    public virtual TblClass Class { get; set; } = null!;

    public virtual TblDocument? Doc { get; set; }

    public virtual TblGender? Gender { get; set; }

    public virtual TblImage? Image { get; set; }
}
