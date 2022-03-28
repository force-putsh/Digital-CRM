using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crmdata
{
  [Table("Contrats", Schema = "dbo")]
  public partial class Contrat
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public IEnumerable<Tache> Taches { get; set; }
    public decimal Amount
    {
      get;
      set;
    }
    public string Name
    {
      get;
      set;
    }
    public string UserId
    {
      get;
      set;
    }
    public int ContactId
    {
      get;
      set;
    }
    public Contact Contact { get; set; }
    public int StatusId
    {
      get;
      set;
    }
    public StatutContrat StatutContrat { get; set; }
    public DateTime CloseDate
    {
      get;
      set;
    }
  }
}
