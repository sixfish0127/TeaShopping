﻿using Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Store> stores {  get; set; } 
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "果汁", DisplayOrder = 1 },
                new Category { Id = 2, Name = "茶", DisplayOrder = 2 },
                new Category { Id = 3, Name = "咖啡", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "台灣水果茶",
                    Size = "大杯",
                    Description = " 天然果飲，迷人多變。",
                    Price = 60,
                    CategoryId=1,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Name = "鐵觀音",
                    Size = "中杯",
                    Description = " 品鐵觀音，享人生的味道。",
                    Price = 35,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Name = "美式咖啡",
                    Size = "中杯",
                    Description = " 用咖啡體悟悠閒時光。",
                    Price = 50,
                    CategoryId = 3,
                    ImageUrl = ""
                }
                );
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "台中一中店", Address = "台中市北區三民路123號", City = "台中市", PhoneNumber = "0987654321", Description = "鄰近一中商圈" },
                new Store { Id = 2, Name = "台北大安店", Address = "台北市大安區大安路123號", City = "台北市", PhoneNumber = "0911111111", Description = "熱鬧台北商圈" },
                new Store { Id = 3, Name = "台南安平店", Address = "台南市安平區安平路123號", City = "台南市", PhoneNumber = "0922222222", Description = "文化台南商圈" }
            );
        }
    }
}
