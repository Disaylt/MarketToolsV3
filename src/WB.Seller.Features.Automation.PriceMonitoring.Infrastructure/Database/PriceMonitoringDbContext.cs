using Microsoft.EntityFrameworkCore;
using WB.Seller.Features.Automation.PriceMonitoring.Domain.Entities;
using WB.Seller.Features.Automation.PriceMonitoring.Domain.Entities.Actions;
using WB.Seller.Features.Automation.PriceMonitoring.Domain.Entities.Events;

namespace WB.Seller.Features.Automation.PriceMonitoring.Infrastructure.Database;

public class PriceMonitoringDbContext(DbContextOptions<PriceMonitoringDbContext> options) : DbContext(options)
{
    public DbSet<CardEntity> Cards { get; set; } = null!;
    public DbSet<TriggerEntity> Triggers { get; set; } = null!;

    public DbSet<EventEntity> Events { get; set; } = null!;
    public DbSet<CardPriceEventEntity> CardPriceEvents { get; set; } = null!;
    public DbSet<StockEventEntity> StockEvents{ get; set; } = null!;
    public DbSet<TimeRangeEventEntity> TimeRangeEvents { get; set; } = null!;

    public DbSet<ActionEntity> Actions { get; set; } = null!;
    public DbSet<ChangePriceActionEntity> ChangePriceActions{ get; set; } = null!;
    public DbSet<RangePriceActionEntity> RangePriceActions { get; set; } = null!;
    public DbSet<SinglePriceActionEntity> SinglePriceActions { get; set; } = null!;

}