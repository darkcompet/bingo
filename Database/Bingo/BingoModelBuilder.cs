namespace App;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

public class BingoModelBuilder {
	public static void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<BingoModel>().Property(model => model.created_at).HasDefaultValueSql("getutcdate()");
	}
}
