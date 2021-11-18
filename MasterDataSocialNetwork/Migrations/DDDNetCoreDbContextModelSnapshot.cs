﻿// <auto-generated />
using System;
using DDDNetCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDNetCore.Migrations
{
    [DbContext(typeof(DDDNetCoreDbContext))]
    partial class DDDNetCoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("LAPR5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDDNetCore.Domain.Connections.Connection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("decision")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("requester")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Introductions.Introduction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Enabler")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("decisionStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Introductions");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Missions.Mission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Users.Friendship", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("requester")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("friend")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "requester");

                    b.HasIndex("requester");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Connections.Connection", b =>
                {
                    b.OwnsOne("DDDNetCore.Domain.Shared.Description", "description", b1 =>
                        {
                            b1.Property<string>("ConnectionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ConnectionId");

                            b1.ToTable("Connections");

                            b1.WithOwner()
                                .HasForeignKey("ConnectionId");
                        });

                    b.Navigation("description");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Introductions.Introduction", b =>
                {
                    b.OwnsOne("DDDNetCore.Domain.Shared.Description", "MessageFromIntermediateToTargetUser", b1 =>
                        {
                            b1.Property<string>("IntroductionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IntroductionId");

                            b1.ToTable("Introductions");

                            b1.WithOwner()
                                .HasForeignKey("IntroductionId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Shared.Description", "MessageToIntermediate", b1 =>
                        {
                            b1.Property<string>("IntroductionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IntroductionId");

                            b1.ToTable("Introductions");

                            b1.WithOwner()
                                .HasForeignKey("IntroductionId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Shared.Description", "MessageToTargetUser", b1 =>
                        {
                            b1.Property<string>("IntroductionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IntroductionId");

                            b1.ToTable("Introductions");

                            b1.WithOwner()
                                .HasForeignKey("IntroductionId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.UserId", "Requester", b1 =>
                        {
                            b1.Property<string>("IntroductionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("IntroductionId");

                            b1.ToTable("Introductions");

                            b1.WithOwner()
                                .HasForeignKey("IntroductionId");
                        });

                    b.Navigation("MessageFromIntermediateToTargetUser");

                    b.Navigation("MessageToIntermediate");

                    b.Navigation("MessageToTargetUser");

                    b.Navigation("Requester");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Missions.Mission", b =>
                {
                    b.OwnsOne("DDDNetCore.Domain.Missions.DificultyDegree", "dificultyDegree", b1 =>
                        {
                            b1.Property<string>("MissionId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("level")
                                .HasColumnType("int");

                            b1.HasKey("MissionId");

                            b1.ToTable("Missions");

                            b1.WithOwner()
                                .HasForeignKey("MissionId");
                        });

                    b.Navigation("dificultyDegree");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Users.Friendship", b =>
                {
                    b.HasOne("DDDNetCore.Domain.Users.User", null)
                        .WithMany("friendsList")
                        .HasForeignKey("requester")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DDDNetCore.Domain.Users.ConnectionStrength", "connection_strength", b1 =>
                        {
                            b1.Property<string>("FriendshipId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Friendshiprequester")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FriendshipId", "Friendshiprequester");

                            b1.ToTable("Friendship");

                            b1.WithOwner()
                                .HasForeignKey("FriendshipId", "Friendshiprequester");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.RelationshipStrength", "relationship_strength", b1 =>
                        {
                            b1.Property<string>("FriendshipId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Friendshiprequester")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FriendshipId", "Friendshiprequester");

                            b1.ToTable("Friendship");

                            b1.WithOwner()
                                .HasForeignKey("FriendshipId", "Friendshiprequester");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.Tag", "friendshipTag", b1 =>
                        {
                            b1.Property<string>("FriendshipId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Friendshiprequester")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("FriendshipId", "Friendshiprequester");

                            b1.ToTable("Friendship");

                            b1.WithOwner()
                                .HasForeignKey("FriendshipId", "Friendshiprequester");
                        });

                    b.Navigation("connection_strength");

                    b.Navigation("friendshipTag");

                    b.Navigation("relationship_strength");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Users.User", b =>
                {
                    b.OwnsOne("DDDNetCore.Domain.Users.Email", "Email", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("EmailAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.EmotionTime", "EmotionTime", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<DateTime>("LastEmotionalUpdate")
                                .HasColumnType("datetime2");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.EmotionalState", "emotionalState", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("emotion")
                                .HasColumnType("int");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.Name", "Name", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("text")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.Password", "Password", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Strength")
                                .HasColumnType("int");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDNetCore.Domain.Users.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Number")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("DDDNetCore.Domain.Users.Tag", "tags", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("name")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UserId", "Id");

                            b1.ToTable("Users_tags");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email");

                    b.Navigation("emotionalState");

                    b.Navigation("EmotionTime");

                    b.Navigation("Name");

                    b.Navigation("Password");

                    b.Navigation("PhoneNumber");

                    b.Navigation("tags");
                });

            modelBuilder.Entity("DDDNetCore.Domain.Users.User", b =>
                {
                    b.Navigation("friendsList");
                });
#pragma warning restore 612, 618
        }
    }
}
