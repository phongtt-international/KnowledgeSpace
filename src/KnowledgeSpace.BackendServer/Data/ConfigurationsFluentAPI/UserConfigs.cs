using KnowledgeSpace.BackendServer.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KnowledgeSpace.BackendServer.Extensions.ModelBuilderExtensions;

namespace KnowledgeSpace.BackendServer.Data.ConfigurationsFluentAPI
{
    public class UserConfigs : DbEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        }
    }
}
