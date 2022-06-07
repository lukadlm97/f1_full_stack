﻿// <auto-generated />
using System;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220607201355_Add-Season-Table")]
    partial class AddSeasonTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
          

            modelBuilder.Entity("Domain.Season.Season", b =>
                {
                    b.HasOne("Domain.RacingChampionship.RacingChampionship", "RacingChampionship")
                        .WithMany()
                        .HasForeignKey("RacingChampionshipId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RacingChampionship");
                });
            
#pragma warning restore 612, 618
        }
    }
}
