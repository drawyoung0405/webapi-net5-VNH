using Microsoft.EntityFrameworkCore;

namespace MyApiApp.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { 
                
        }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> donHangChiTiets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.NguoiNhanHang).IsRequired().HasMaxLength(100);

            });
            modelBuilder.Entity<DonHangChiTiet>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e => new { e.MaDh, e.MaHh });
                entity.HasOne(e => e.DonHang).WithMany(e => e.DonHangChiTiets).HasForeignKey(e => e.MaDh).HasConstraintName("FK_DonHangCT_DonHang");
               
                entity.HasOne(e => e.HangHoa).WithMany(e => e.DonHangChiTiets).HasForeignKey(e => e.MaHh).HasConstraintName("FK_DonHangCT_HangHoa");
            });
        }
        #region DbSet
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }  
        #endregion
    }
}
