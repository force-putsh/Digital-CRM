using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crmdata
{
  [Table("Tâche", Schema = "dbo")]
  public partial class Tache
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }
    public string Title
    {
      get;
      set;
    }
    public int OpportunityId
    {
      get;
      set;
    }
    public Contrat Contrat { get; set; }
    public DateTime DueDate
    {
      get;
      set;
    }
    public int TypeId
    {
      get;
      set;
    }
    public TypesTache TypesTache { get; set; }
    public int? StatusId
    {
      get;
      set;
    }
    public StatutTache StatutTache { get; set; }
  }
}
