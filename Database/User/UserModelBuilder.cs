namespace App;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class UserModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<UserModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
