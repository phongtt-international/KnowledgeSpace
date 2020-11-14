using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KnowledgeSpace.BackendServer.Extensions.ModelBuilderExtensions;

namespace KnowledgeSpace.BackendServer.Data.ConfigurationsFluentAPI
{
    public class IdentityRoleConfigs : DbEntityConfiguration<IdentityRole>
    {
        public override void Configure(EntityTypeBuilder<IdentityRole> entity)
        {
            entity.Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        }
    }
}
