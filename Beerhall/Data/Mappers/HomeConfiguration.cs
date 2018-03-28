using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrustTeamVersion4.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrustTeamVersion4.Data.Mapping
{
	class HomeConfiguration : IEntityTypeConfiguration<Home>
	{
		public void Configure(EntityTypeBuilder<Home> builder)
		{
			builder.ToTable("Home");
			// Mappen Primary key
			builder.HasKey(t => new { t.Month, t.Number });
		}

	}
}
