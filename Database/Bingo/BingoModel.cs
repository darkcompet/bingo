namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(DbConst.table_bingo)]
[Index(nameof(code), IsUnique = true)]
public class BingoModel {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// Bingo number
	[Column("code", TypeName = "varchar(255)"), MaxLength(255)]
	public string? code { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("deleted_at")]
	public DateTime? deleted_at { get; set; }
}
