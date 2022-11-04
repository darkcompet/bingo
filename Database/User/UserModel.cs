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

	/// Bingo number which was assigned to this user.
	[Column("bingo_code", TypeName = "varchar(255)"), MaxLength(255)]
	public string? bingo_code { get; set; }

	/// When the bingo_code of this user was hit (at that time, user became to winner).
	[Column("bingo_hit_at")]
	public DateTime? bingo_hit_at { get; set; }

	[Required]
	[Column("created_at")]
	public DateTime created_at { get; set; }

	[Column("updated_at")]
	public DateTime? updated_at { get; set; }
}
