using KnowledgeSpace.BackendServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KnowledgeSpace.BackendServer.Extensions.ModelBuilderExtensions;

namespace KnowledgeSpace.BackendServer.Data.ConfigurationFluentAPI
{
    public class LabelInKnowledgeBaseConfigs : DbEntityConfiguration<LabelInKnowledgeBase>
    {
        public override void Configure(EntityTypeBuilder<LabelInKnowledgeBase> entity)
        {
            entity.HasKey(c => new { c.LabelId, c.KnowledgeBaseId });
        }
    }
}
