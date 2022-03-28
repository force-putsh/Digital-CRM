using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crmdata
{
  [Table("StatutTâche", Schema = "dbo")]
  public partial class StatutTache
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public IEnumerable<Tache> Taches { get; set; }
    public string Name
    {
      get;
      set;
    }
  }
}
