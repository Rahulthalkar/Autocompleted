using Autocomplete.Domain.Tables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/// <summary>
/// This will serve as Master Table 
/// It will contain list of Different User Types
/// </summary>
public class tblUserType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id { get; set; }
    public required string TypeOfUser { get; set; }

}
