using KnowledgeSpace.BackendServer.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KnowledgeSpace.BackendServer.Extensions.ModelBuilderExtensions;

namespace KnowledgeSpace.BackendServer.Data.ConfigurationsFluentAPI
{
    public class CommandInFunctionConfigs : DbEntityConfiguration<CommandInFunction>
    {
        public override void Configure(EntityTypeBuilder<CommandInFunction> entity)
        {
            entity.HasKey(c => new { c.CommandId, c.FunctionId });
        }
    }
}
