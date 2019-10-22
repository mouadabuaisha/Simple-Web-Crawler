using System;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebCrawler
{
    public partial class _WebCrawlerContext : WebCrawlerContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //This is the default connection credentials for the localdb (a feature of SQL Server Express), change it to your proper DB
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebCrawler;Trusted_Connection=True;");
        }
    }
}
