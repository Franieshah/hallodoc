using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocApp.Entities.DataModels;

[PrimaryKey("Userid", "Roleid")]
[Table("aspnetuserroles")]
public partial class Aspnetuserrole
{
    [Key]
    [Column("roleid")]
    public int Roleid { get; set; }

    [Key]
    [Column("userid")]
    public int Userid { get; set; }
}
