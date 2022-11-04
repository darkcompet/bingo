namespace App;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table(DbConst.table_user)]
[Index(nameof(uid), IsUnique = true)]
[Index(nameof(bingo_code))]
public class UserModel : AutoGenerateUpdateTime {
	/// PK
	[Key]
	[Column("id")]
	public long id { get; set; }

	/// From cookie
	[Column("uid", TypeName = "varchar(255)"), MaxLength(255)]
	public string? uid { get; set; }

	/// Bingo number
	[Column("bingo_code", TypeName = "varchar(255)"), MaxLength(255)]
	public string? bingo_code { get; set; }

	/// Indicate the user was choosed as winner.
	[Column("winner_choosed_at")]
	public DateTime? winner_choosed_at { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }
}
