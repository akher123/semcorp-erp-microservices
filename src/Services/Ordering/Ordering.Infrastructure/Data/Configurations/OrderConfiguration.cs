namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId)
            );

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(oi => oi.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
           .WithOne()
           .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName, namebuilder =>
            {
                namebuilder.Property(n => n.Value)
                           .HasColumnName(nameof(Order.OrderName))
                           .HasMaxLength(100)
                           .IsRequired();
            });

        builder.ComplexProperty(
           o => o.EmailAddress, emailbuilder =>
           {
               emailbuilder.Property(n => n.Value)
                          .HasColumnName(nameof(Order.EmailAddress))
                          .HasMaxLength(256)
                          .IsRequired();
           });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
               s => s.ToString(),
               dbstatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbstatus));

        builder.Property(o => o.TotalPrice);
    }
}
