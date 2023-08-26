using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NewsFinder.DataAccess.Data.Telegram;

public class TelegramSupportedChannels
{
    public string Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string ChannelName { get; set; } = string.Empty;
    public string ChannelLink { get; set; } = string.Empty;
    public string? ChannelDescription { get; set; } = string.Empty;
    public string? ChannelLanguage { get; set; } = string.Empty;
    public string ChannelLastPost { get; set; } = string.Empty;
    public DateTime LastTimeChecked { get; set; }
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
        builder.Property(x => x.ChannelName)
            .IsRequired();
        builder.Property(x => x.ChannelLink)
            .IsRequired();
        builder.Property(x => x.ChannelDescription)
            .IsRequired(false);
        builder.Property(x => x.ChannelLanguage)
            .IsRequired(false);
        builder.Property(x => x.ChannelLastPost)
            .IsRequired();
        builder.Property(x => x.LastTimeChecked)
            .IsRequired();
    }
}