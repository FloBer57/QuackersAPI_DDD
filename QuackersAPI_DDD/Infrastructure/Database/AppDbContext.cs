﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<ChannelPersonRoleXPersonXChannel> Channelpersonrolexpersonxchannels { get; set; }

    public virtual DbSet<ChannelType> ChannelTypes { get; set; }
    public virtual DbSet<ChannelPersonRole> ChannelPersonRoles { get; set; }


    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageXReactionXPerson> Messagexreactionxpeople { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PersonJobTitle> Personjobtitles { get; set; }

    public virtual DbSet<PersonRole> Personroles { get; set; }

    public virtual DbSet<PersonStatut> Personstatuts { get; set; }

    public virtual DbSet<PersonXChannel> Personxchannels { get; set; }

    public virtual DbSet<PersonXMessage> Personxmessages { get; set; }

    public virtual DbSet<PersonXNotification> Personxnotifications { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<ResetTokenPassword> ResetTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("5.7.24-mysql"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Attachment_Id).HasName("PRIMARY");

            entity.ToTable("attachment");

            entity.HasIndex(e => e.Attachment_Name, "Attachment_Name").IsUnique();

            entity.HasIndex(e => e.Message_Id, "Message_ID");

            entity.Property(e => e.Attachment_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Attachment_Id");
            entity.Property(e => e.AttachmentThing)
                .HasMaxLength(255)
                .HasColumnName("Attachment_Attachment");
            entity.Property(e => e.Attachment_Name).HasColumnName("Attachment_Name");
            entity.Property(e => e.Message_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Message_ID");

            entity.HasOne(d => d.Message).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.Message_Id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("attachment_ibfk_1");
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.HasKey(e => e.Channel_Id).HasName("PRIMARY");

            entity.ToTable("channel");

            entity.HasIndex(e => e.ChannelType_Id, "ChannelType_Id");

            entity.HasIndex(e => e.Channel_Name, "Channel_Name").IsUnique();

            entity.Property(e => e.Channel_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Channel_ID");
            entity.Property(e => e.Channel_ImagePath)
                .HasMaxLength(50)
                .HasColumnName("Channel_ImagePath");
            entity.Property(e => e.Channel_Name)
                .HasMaxLength(50)
                .HasColumnName("Channel_Name");
            entity.Property(e => e.ChannelType_Id)
                .HasColumnType("int(11)")
                .HasColumnName("ChannelType_Id");

            entity.HasOne(d => d.ChannelType).WithMany(p => p.Channels)
                .HasForeignKey(d => d.ChannelType_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("channel_ibfk_1");
        });

        modelBuilder.Entity<ChannelPersonRole>(entity =>
        {
            entity.HasKey(e => e.ChannelPersonRole_Id).HasName("PRIMARY");

            entity.ToTable("channelpersonrole");

            entity.HasIndex(e => e.ChannelPersonRole_Name, "ChannelPersonRole_Name").IsUnique();

            entity.Property(e => e.ChannelPersonRole_Id)
                .HasColumnType("int(11)")
                .HasColumnName("ChannelPersonRole_Id");
            entity.Property(e => e.ChannelPersonRole_Name)
                .HasMaxLength(50)
                .HasColumnName("ChannelPersonRole_Name");
        });
        modelBuilder.Entity<ChannelPersonRoleXPersonXChannel>(entity =>
        {
            entity.HasKey(e => new { e.Person_Id, e.Channel_Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("channelpersonrolexpersonxchannel");

            entity.HasIndex(e => e.Channel_Id, "Channel_ID");

            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");
            entity.Property(e => e.Channel_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Channel_ID");
            entity.Property(e => e.ChannelPersonRoleXpersonXchannelAffectDate)
                .HasColumnType("datetime")
                .HasColumnName("ChannelPersonRoleXPersonXChannel_AffectDate");

            entity.HasOne(d => d.Channel).WithMany(p => p.Channelpersonrolexpersonxchannels)
                .HasForeignKey(d => d.Channel_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("channelpersonrolexpersonxchannel_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Channelpersonrolexpersonxchannels)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("channelpersonrolexpersonxchannel_ibfk_1");

            entity.HasOne(d => d.ChannelPersonRole).WithMany(p => p.ChannelPersonRolesXPersonsXChannels)
              .HasForeignKey(d => d.ChannelPersonRole_Id)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("channelpersonrolexpersonxchannel_ibfk_3");
        });

        modelBuilder.Entity<ChannelType>(entity =>
        {
            entity.HasKey(e => e.ChannelType_Id).HasName("PRIMARY");

            entity.ToTable("channeltype");

            entity.HasIndex(e => e.ChannelType_Name, "ChannelType_Name").IsUnique();

            entity.Property(e => e.ChannelType_Id)
                .HasColumnType("int(11)")
                .HasColumnName("ChannelType_Id");
            entity.Property(e => e.ChannelType_Name)
                .HasMaxLength(50)
                .HasColumnName("ChannelType_Name");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Message_Id).HasName("PRIMARY");


            entity.ToTable("message");

            entity.HasIndex(e => e.Channel_Id, "Channel_ID");

            entity.HasIndex(e => e.Person_Id, "Person_Id");

            entity.Property(e => e.Message_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Message_ID");
            entity.Property(e => e.Channel_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Channel_ID");
            entity.Property(e => e.Message_Date)
                .HasColumnType("datetime")
                .HasColumnName("Message_Date");
            entity.Property(e => e.Message_IsNotArchived).HasColumnName("Message_IsNotArchived");
            entity.Property(e => e.Message_Text)
                .HasColumnType("text")
                .HasColumnName("Message_Text");
            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");

            entity.HasOne(d => d.Channel).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Channel_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("message_ibfk_1");

            entity.HasOne(d => d.Person).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("message_ibfk_2");


        });

        modelBuilder.Entity<MessageXReactionXPerson>(entity =>
        {
            entity.HasKey(e => new { e.Person_Id, e.Message_Id, e.Reaction_Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("messagexreactionxperson");

            entity.HasIndex(e => e.Message_Id, "Message_ID");

            entity.HasIndex(e => e.Reaction_Id, "Reaction_ID");

            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");
            entity.Property(e => e.Message_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Message_ID");
            entity.Property(e => e.Reaction_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Reaction_ID");
            entity.Property(e => e.MessageXreactionXpersonReactionDate)
                .HasColumnType("datetime")
                .HasColumnName("MessageXReactionXPerson_ReactionDate");

            entity.HasOne(d => d.Message).WithMany(p => p.Messagexreactionxpeople)
                .HasForeignKey(d => d.Message_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messagexreactionxperson_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Messagexreactionxpeople)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messagexreactionxperson_ibfk_1");

            entity.HasOne(d => d.Reaction).WithMany(p => p.Messagexreactionxpeople)
                .HasForeignKey(d => d.Reaction_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messagexreactionxperson_ibfk_3");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Notification_Id).HasName("PRIMARY");

            entity.ToTable("notification");

            entity.HasIndex(e => e.Notification_TypeId, "Notification_Type_ID");

            entity.Property(e => e.Notification_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Notification_Id");
            entity.Property(e => e.Notification_DatePost).HasColumnName("Notification_DatePost");
            entity.Property(e => e.Notification_Name)
                .HasMaxLength(50)
                .HasColumnName("Notification_Name");
            entity.Property(e => e.Notification_Text)
                .HasMaxLength(255)
                .HasColumnName("Notification_Text");
            entity.Property(e => e.Notification_TypeId)
                .HasColumnType("int(11)")
                .HasColumnName("Notification_Type_ID");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Notification_TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notification_ibfk_1");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotificationType_Id).HasName("PRIMARY");

            entity.ToTable("notification_type");

            entity.Property(e => e.NotificationType_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Notification_Type_ID");
            entity.Property(e => e.NotificationType_Name)
                .HasMaxLength(255)
                .HasColumnName("NotificationType_Name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Person_Id).HasName("PRIMARY");

            entity.ToTable("person");

            entity.HasIndex(e => e.PersonJobTitle_Id, "PersonJobTitle_Id");

            entity.HasIndex(e => e.PersonRole_Id, "PersonRole_Id");

            entity.HasIndex(e => e.PersonStatut_Id, "PersonStatut_Id");

            entity.HasIndex(e => e.Person_Email, "Person_Email").IsUnique();

            entity.HasIndex(e => e.Person_PhoneNumber, "Person_PhoneNumber").IsUnique();

            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");
            entity.Property(e => e.Person_CreatedTimePerson)
                .HasColumnType("datetime")
                .HasColumnName("Person_CreatedTimePerson");
            entity.Property(e => e.Person_Description)
                .HasMaxLength(255)
                .HasColumnName("Person_Description");
            entity.Property(e => e.Person_Email)
                .HasMaxLength(100)
                .HasColumnName("Person_Email");
            entity.Property(e => e.Person_FirstName)
                .HasMaxLength(50)
                .HasColumnName("Person_FirstName");
            entity.Property(e => e.Person_IsTemporaryPassword).HasColumnName("Person_IsTemporaryPassword");
            entity.Property(e => e.PersonJobTitle_Id)
                .HasColumnType("int(11)")
                .HasColumnName("PersonJobTitle_Id");
            entity.Property(e => e.Person_LastName)
                .HasMaxLength(50)
                .HasColumnName("Person_LastName");
            entity.Property(e => e.Person_Password)
                .HasMaxLength(255)
                .HasColumnName("Person_Password");
            entity.Property(e => e.Person_PhoneNumber)
                .HasMaxLength(16)
                .HasColumnName("Person_PhoneNumber");
            entity.Property(e => e.Person_ProfilPicturePath)
                .HasMaxLength(255)
                .HasColumnName("Person_ProfilPicturePath");
            entity.Property(e => e.PersonRole_Id)
                .HasColumnType("int(11)")
                .HasColumnName("PersonRole_Id");
            entity.Property(e => e.PersonStatut_Id)
                .HasColumnType("int(11)")
                .HasColumnName("PersonStatut_Id");
            entity.Property(e => e.Person_TokenResetPassword)
                .HasMaxLength(255)
                .HasColumnName("Person_TokenResetPassword");

            entity.HasOne(d => d.PersonJobTitle).WithMany(p => p.People)
                .HasForeignKey(d => d.PersonJobTitle_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_ibfk_1");

            entity.HasOne(d => d.PersonRole).WithMany(p => p.People)
                .HasForeignKey(d => d.PersonRole_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_ibfk_3");

            entity.HasOne(d => d.PersonStatut).WithMany(p => p.People)
                .HasForeignKey(d => d.PersonStatut_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_ibfk_2");
        });

        modelBuilder.Entity<PersonJobTitle>(entity =>
        {
            entity.HasKey(e => e.PersonJob_TitleId);

            entity.ToTable("personjobtitle");

            entity.Property(e => e.PersonJob_TitleId)
                .HasColumnType("int(11)")
                .HasColumnName("PersonJobTitle_Id");
            entity.Property(e => e.PersonJobTitle_Name)
                .HasMaxLength(255)
                .HasColumnName("PersonJobTitle_Name");
        });

        modelBuilder.Entity<PersonRole>(entity =>
        {
            entity.HasKey(e => e.PersonRole_Id);

            entity.ToTable("personrole");

            entity.Property(e => e.PersonRole_Id)
                .HasColumnType("int(11)")
                .HasColumnName("PersonRole_Id");
            entity.Property(e => e.PersonRole_Name)
                .HasMaxLength(50)
                .HasColumnName("PersonRole_Name");
        });

        modelBuilder.Entity<PersonStatut>(entity =>
        {
            entity.HasKey(e => e.PersonStatut_Id);

            entity.ToTable("personstatut");

            entity.Property(e => e.PersonStatut_Id)
                .HasColumnType("int(11)")
                .HasColumnName("PersonStatut_Id");
            entity.Property(e => e.PersonStatut_Name)
                .HasMaxLength(50)
                .HasColumnName("PersonStatut_Name");
        });

        modelBuilder.Entity<PersonXChannel>(entity =>
        {
            entity.HasKey(e => new { e.Person_Id, e.Channel_Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("personxchannel");

            entity.HasIndex(e => e.Channel_Id, "Channel_ID");

            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");
            entity.Property(e => e.Channel_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Channel_ID");
            entity.Property(e => e.PersonXchannelSignInDate)
                .HasColumnType("datetime")
                .HasColumnName("PersonXChannel_SignInDate");

            entity.HasOne(d => d.Channel).WithMany(p => p.Personxchannels)
                .HasForeignKey(d => d.Channel_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personxchannel_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Personxchannels)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personxchannel_ibfk_1");
        });

        modelBuilder.Entity<PersonXMessage>(entity =>
        {
            entity.HasKey(e => new { e.Person_Id, e.Message_Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("personxmessage");

            entity.HasIndex(e => e.Message_Id, "Message_ID");

            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");
            entity.Property(e => e.Message_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Message_ID");
            entity.Property(e => e.PersonXmessageReadDate)
                .HasColumnType("datetime")
                .HasColumnName("PersonXMessage_ReadDate");

            entity.HasOne(d => d.Message).WithMany(p => p.Personxmessages)
                .HasForeignKey(d => d.Message_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personxmessage_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Personxmessages)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personxmessage_ibfk_1");
        });

        modelBuilder.Entity<PersonXNotification>(entity =>
        {
            entity.HasKey(e => new { e.Person_Id, e.Notification_Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("personxnotification");

            entity.HasIndex(e => e.Notification_Id, "Notification_Id");

            entity.Property(e => e.Person_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Person_Id");
            entity.Property(e => e.Notification_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Notification_Id");
            entity.Property(e => e.PersonXnotification_ReadDate)
                .HasColumnType("datetime")
                .HasColumnName("PersonXNotification_ReadDate");

            entity.HasOne(d => d.Notification).WithMany(p => p.Personxnotifications)
                .HasForeignKey(d => d.Notification_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personxnotification_ibfk_2");

            entity.HasOne(d => d.Person).WithMany(p => p.Personxnotifications)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personxnotification_ibfk_1");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.HasKey(e => e.Reaction_Id).HasName("PRIMARY");

            entity.ToTable("reaction");

            entity.HasIndex(e => e.Reaction_Name, "Reaction_Name").IsUnique();

            entity.Property(e => e.Reaction_Id)
                .HasColumnType("int(11)")
                .HasColumnName("Reaction_ID");
            entity.Property(e => e.Reaction_Name)
                .HasMaxLength(50)
                .HasColumnName("Reaction_Name");
            entity.Property(e => e.Reaction_PicturePath)
                .HasMaxLength(255)
                .HasColumnName("Reaction_PicturePath");
        });


        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Token_Id).HasName("PRIMARY");

            entity.ToTable("refreshtokens");

            // Indexes
            entity.HasIndex(e => e.Person_Id, "Person_Id"); // Add an index on Person_Id

            // Properties
            entity.Property(e => e.Token_Id)
                .HasColumnType("int")
                .HasColumnName("Token_Id")
                .ValueGeneratedOnAdd(); // Ensures Token_Id is auto-incremented

            entity.Property(e => e.Person_Id)
                .HasColumnType("int")
                .HasColumnName("Person_Id");

            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Token");

            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("ExpiresAt");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); 

            entity.Property(e => e.Revoked)
                .HasColumnName("Revoked");

            entity.HasOne(e => e.Person) 
                .WithMany(p => p.RefreshToken) 
                .HasForeignKey(e => e.Person_Id) 
                .OnDelete(DeleteBehavior.Cascade); 
        });

        modelBuilder.Entity<ResetTokenPassword>(entity =>
        {
            entity.ToTable("resettokenpassword");

            entity.HasKey(e => e.Token_Id);
            entity.Property(e => e.Token_Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relation to Person
            entity.HasOne(d => d.Person)
                .WithMany(p => p.ResetTokenPassword)
                .HasForeignKey(d => d.Person_Id)
                .OnDelete(DeleteBehavior.Cascade);
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
