using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace coco.Models;

public partial class CocopureV1Context : DbContext
{
    public CocopureV1Context()
    {
    }

    public CocopureV1Context(DbContextOptions<CocopureV1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<NonCustomer> NonCustomers { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL1002.site4now.net;Initial Catalog=db_ab33d0_login;User Id=db_ab33d0_login_admin;Password=ndtp09082003");
//Data Source=fuco;Initial Catalog=cocopureV1;Persist Security Info=True;User ID=sa;Password=fuco;Trust Server Certificate=True
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__bill__6D903F03CD0CA1CB");

            entity.ToTable("bill");

            entity.Property(e => e.BillId)
                .HasMaxLength(255)
                .HasColumnName("billId");
            entity.Property(e => e.DayBought).HasColumnName("dayBought");
            entity.Property(e => e.Discount)
                .HasMaxLength(50)
                .HasColumnName("discount");
            entity.Property(e => e.EndAddress)
                .HasMaxLength(255)
                .HasColumnName("endAddress");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("fullName");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .HasColumnName("paymentMethod");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Bills)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__bill__userId__2180FB33");
        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__bill_det__83077859C63772D9");

            entity.ToTable("bill_details");

            entity.Property(e => e.DetailId).HasColumnName("detailId");
            entity.Property(e => e.BillId)
                .HasMaxLength(255)
                .HasColumnName("billId");
            entity.Property(e => e.ItemCount).HasColumnName("itemCount");
            entity.Property(e => e.ItemId)
                .HasMaxLength(255)
                .HasColumnName("itemId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("FK__bill_deta__billI__245D67DE");

            entity.HasOne(d => d.Item).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__bill_deta__itemI__25518C17");
        });

        modelBuilder.Entity<NonCustomer>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__non_cust__CB9A1CFF4E15B7F7");

            entity.ToTable("non_customer");

            entity.HasIndex(e => e.Email, "UQ__non_cust__AB6E616420ABC7E2").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("userId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.DaySend).HasColumnName("daySend");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .HasColumnName("note");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__storage__56A128AA3B99517E");

            entity.ToTable("storage");

            entity.Property(e => e.ItemId)
                .HasMaxLength(255)
                .HasColumnName("itemId");
            entity.Property(e => e.ItemAmount).HasColumnName("itemAmount");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .HasColumnName("itemName");
            entity.Property(e => e.ItemPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("itemPrice");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFCE91DECC");

            entity.ToTable("User");

            entity.HasIndex(e => e.UserName, "UQ__User__66DCF95CB90F2FC0").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("userId");
            entity.Property(e => e.PassWord)
                .HasMaxLength(255)
                .HasColumnName("passWord");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserInfo__CB9A1CFF43DB9B97");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("userId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");

            entity.HasOne(d => d.User).WithOne(p => p.UserInfo)
                .HasForeignKey<UserInfo>(d => d.UserId)
                .HasConstraintName("FK__UserInfo__userId__4CA06362");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__voucher__F53389E9873AF208");

            entity.ToTable("voucher");

            entity.HasIndex(e => e.VoucherCode, "UQ__voucher__09FEFFB00DA46B58").IsUnique();

            entity.Property(e => e.VoucherId)
                .HasMaxLength(255)
                .HasColumnName("voucherId");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(255)
                .HasColumnName("voucherCode");
            entity.Property(e => e.VoucherDiscount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("voucherDiscount");
            entity.Property(e => e.VoucherStatus)
                .HasMaxLength(50)
                .HasColumnName("voucherStatus");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
