using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewsFinder.DataAccess.Data.Telegram;

public class TelegramSupportedChannels
{
    public string Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime LastTimeChecked { get; set; } = DateTime.UtcNow;
}

public class TelegramSupportedChannelsConfiguration : IEntityTypeConfiguration<TelegramSupportedChannels>
{
    public void Configure(EntityTypeBuilder<TelegramSupportedChannels> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.UserName)
            .IsRequired();
        builder.Property(x => x.LastTimeChecked)
            .IsRequired();
    }
}