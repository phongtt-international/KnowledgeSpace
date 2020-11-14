using KnowledgeSpace.BackendServer.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KnowledgeSpace.BackendServer.Extensions.ModelBuilderExtensions;

namespace KnowledgeSpace.BackendServer.Data.ConfigurationFluentAPI
{
    public class VoteConfigs : DbEntityConfiguration<Vote>
    {
        public override void Configure(EntityTypeBuilder<Vote> entity)
        {
            entity.HasKey(c => new { c.KnowledgeBaseId, c.UserId });
        }
    }
}
