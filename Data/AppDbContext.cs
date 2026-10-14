using Microsoft.EntityFrameworkCore;

namespace SMS.Data;

public class StockItemEntity
{
    public int Id { get; set; }
    public string StockCode { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class PurchaseEntity
{
    public int Id { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public int TotalQty { get; set; }
    public decimal TotalAmount { get; set; }

    public List<PurchaseDetailEntity> Details { get; set; } = new();
}

public class PurchaseDetailEntity
{
    public int Id { get; set; }
    public int PurchaseId { get; set; }
    public string StockCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }

    public PurchaseEntity? Purchase { get; set; }
}

public class SaleEntity
{
    public int Id { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public int TotalQty { get; set; }
    public decimal TotalAmount { get; set; }

    public List<SaleDetailEntity> Details { get; set; } = new();
}

public class SaleDetailEntity
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public string StockCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }

    public SaleEntity? Sale { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<StockItemEntity> StockItems => Set<StockItemEntity>();
    public DbSet<PurchaseEntity> Purchases => Set<PurchaseEntity>();
    public DbSet<PurchaseDetailEntity> PurchaseDetails => Set<PurchaseDetailEntity>();
    public DbSet<SaleEntity> Sales => Set<SaleEntity>();
    public DbSet<SaleDetailEntity> SaleDetails => Set<SaleDetailEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Stock Items
        modelBuilder.Entity<StockItemEntity>().ToTable("stock_items");
        modelBuilder.Entity<StockItemEntity>().Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<StockItemEntity>().Property(e => e.StockCode).HasColumnName("stock_code");
        modelBuilder.Entity<StockItemEntity>().Property(e => e.Description).HasColumnName("description");

        // Purchases & Details
        modelBuilder.Entity<PurchaseEntity>().ToTable("purchases");
        modelBuilder.Entity<PurchaseEntity>().Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<PurchaseEntity>().Property(e => e.InvoiceNo).HasColumnName("invoice_no");
        modelBuilder.Entity<PurchaseEntity>().Property(e => e.PurchaseDate).HasColumnName("purchase_date");
        modelBuilder.Entity<PurchaseEntity>().Property(e => e.TotalQty).HasColumnName("total_qty");
        modelBuilder.Entity<PurchaseEntity>().Property(e => e.TotalAmount).HasColumnName("total_amount");

        modelBuilder.Entity<PurchaseDetailEntity>().ToTable("purchase_details");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.PurchaseId).HasColumnName("purchase_id");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.StockCode).HasColumnName("stock_code");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.Description).HasColumnName("description");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.Qty).HasColumnName("qty");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.Price).HasColumnName("price");
        modelBuilder.Entity<PurchaseDetailEntity>().Property(e => e.Amount).HasColumnName("amount").ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<PurchaseEntity>()
            .HasMany(p => p.Details)
            .WithOne(d => d.Purchase)
            .HasForeignKey(d => d.PurchaseId);

        // Sales & Details
        modelBuilder.Entity<SaleEntity>().ToTable("sales");
        modelBuilder.Entity<SaleEntity>().Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<SaleEntity>().Property(e => e.InvoiceNo).HasColumnName("invoice_no");
        modelBuilder.Entity<SaleEntity>().Property(e => e.SaleDate).HasColumnName("sale_date");
        modelBuilder.Entity<SaleEntity>().Property(e => e.TotalQty).HasColumnName("total_qty");
        modelBuilder.Entity<SaleEntity>().Property(e => e.TotalAmount).HasColumnName("total_amount");

        modelBuilder.Entity<SaleDetailEntity>().ToTable("sale_details");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.Id).HasColumnName("id");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.SaleId).HasColumnName("sale_id");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.StockCode).HasColumnName("stock_code");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.Description).HasColumnName("description");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.Qty).HasColumnName("qty");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.Price).HasColumnName("price");
        modelBuilder.Entity<SaleDetailEntity>().Property(e => e.Amount).HasColumnName("amount").ValueGeneratedOnAddOrUpdate();

        modelBuilder.Entity<SaleEntity>()
            .HasMany(s => s.Details)
            .WithOne(d => d.Sale)
            .HasForeignKey(d => d.SaleId);
    }
}