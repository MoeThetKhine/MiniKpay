namespace MiniKpay.Database.Models;

public partial class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblDepositWithDraw> TblDepositWithDraws { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblWallet> TblWallets { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=.;Database=DigitalWallet;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        #region TblDepositWithDraw

        modelBuilder.Entity<TblDepositWithDraw>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PK__TblDepos__AB60DF714CAC3803");

            entity.ToTable("TblDepositWithDraw");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        #endregion

        #region TblTransaction

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("PK__TblTrans__954900915CBDA496");

            entity.ToTable("TblTransaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Notes).HasMaxLength(50);
            entity.Property(e => e.ReceiverMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SenderMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        #endregion

        #region TblWallet

        modelBuilder.Entity<TblWallet>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__TblWalle__1788CC4CB659FCAD");

            entity.ToTable("TblWallet");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        #endregion

        OnModelCreatingPartial(modelBuilder);
    }

    #endregion

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
