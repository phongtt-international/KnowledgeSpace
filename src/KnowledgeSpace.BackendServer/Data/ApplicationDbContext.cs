using KnowledgeSpace.BackendServer.Data.ConfigurationFluentAPI;
using KnowledgeSpace.BackendServer.Data.ConfigurationsFluentAPI;
using KnowledgeSpace.BackendServer.Data.Entities;
using KnowledgeSpace.BackendServer.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeSpace.BackendServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddConfiguration(new IdentityRoleConfigs());
            builder.AddConfiguration(new UserConfigs());
            builder.AddConfiguration(new LabelInKnowledgeBaseConfigs());
            builder.AddConfiguration(new PermissionConfigs());
            builder.AddConfiguration(new VoteConfigs());
            builder.AddConfiguration(new CommandInFunctionConfigs());
            builder.HasSequence("KnowledgeBaseSequence");

        }
        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }

        public DbSet<ActivityLog> ActivityLogs { set; get; }
        public DbSet<Category> Categories { set; get; }
        public DbSet<Comment> Comments { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<KnowledgeBase> KnowledgeBases { set; get; }
        public DbSet<Label> Labels { set; get; }
        public DbSet<LabelInKnowledgeBase> LabelInKnowledgeBases { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<Report> Reports { set; get; }
        public DbSet<Vote> Votes { set; get; }

        public DbSet<Attachment> Attachments { get; set; }
    }
}
