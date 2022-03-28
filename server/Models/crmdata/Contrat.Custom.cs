using System.ComponentModel.DataAnnotations.Schema;

namespace Crm.Models.Crmdata
{
    /* This class is a model for a contract. 
    
    # <a id='model-contract-properties'></a>
    # Contract Properties
    # 
    # The following code is the properties of the contract class. 
    # 
    # The properties are the columns of the table. 
    # 
    # The properties are annotated with a number of attributes that describe the data in the column.
    
    # 
    # The attributes are: 
    # 
    # - **Key**: This is the primary key for the table. 
    # - **DatabaseGenerated**: This is the generator for the primary key. 
    # - **Column**: This is the column in the database. 
    # - **MaxLength**: This is the maximum length of the data in the column. 
    # - **Required**: This is a flag that indicates that the column is required. 
    # - **Data */
    public partial class Contrat
    {
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
