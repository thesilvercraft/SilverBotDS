﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SilverBotDS.Objects;
using SilverBotDS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilverBotDS.Commands
{
    internal class ModCommands : BaseCommandModule
    {
#pragma warning disable CA1822 // Mark members as static

        [Command("kick")]
        [Description("Kick a specified user")]
        [RequirePermissions(Permissions.KickMembers)]
        public async Task Kick(CommandContext ctx, [Description("the user like duh")] DiscordMember a, [Description("the reason")][RemainingText] string reason = "The kick boot has spoken")
        {
            var lang = (await Language.GetLanguageFromCtxAsync(ctx));
            var b = new DiscordEmbedBuilder();
            var thing = "";
            int bp = (await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id)).Hierarchy, up = ctx.Member.Hierarchy, ap = a.Hierarchy;
            if (up < ap)
            {
                thing += lang.UserHasLowerRole + lang.Kick;
            }
            if (up == ap)
            {
                thing += lang.UserHasLowerRole + lang.Kick;
            }
            if (ap > bp)
            {
                thing += lang.BotHasLowerRole;
            }
            if (up > bp && bp > ap)
            {
                await a.RemoveAsync(reason);
                thing += "The bot has attempted to kick the user";
            }
            b.WithDescription(thing);
            b.WithColor(await ColorUtils.GetSingleAsync());
            b.WithFooter(lang.RequestedBy + ctx.User.Username, ctx.User.GetAvatarUrl(ImageFormat.Png));
            await ctx.RespondAsync(embed: b.Build());
        }

        [Command("ban")]
        [Description("bans a specified user")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task Ban(CommandContext ctx, [Description("the user like duh")] DiscordMember a, [Description("the reason")][RemainingText] string reason = "The ban hammer has spoken")
        {
            var lang = (await Language.GetLanguageFromCtxAsync(ctx));
            var b = new DiscordEmbedBuilder();
            var thing = "";
            int bp = (await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id)).Hierarchy, up = ctx.Member.Hierarchy, ap = a.Hierarchy;
            if (up < ap)
            {
                thing += lang.UserHasLowerRole + lang.Ban;
            }
            if (up == ap)
            {
                thing += lang.UserHasLowerRole + lang.Ban;
            }
            if (ap > bp)
            {
                thing += lang.BotHasLowerRole;
            }
            if (up > bp && bp > ap)
            {
                await a.BanAsync(reason: reason);
                thing += "The bot has attempted to ban the user";
            }
            b.WithDescription(thing);
            b.WithColor(await ColorUtils.GetSingleAsync());
            b.WithFooter(lang.RequestedBy + ctx.User.Username, ctx.User.GetAvatarUrl(ImageFormat.Png));
            await ctx.RespondAsync(embed: b.Build());
        }

        [Command("purge")]
        [Aliases("clean", "clear")]
        [Description("Downloads and removes X messages from the current channel.")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task Ban(CommandContext ctx, [Description("number of messages")] int amount)
        {
            var lang = (await Language.GetLanguageFromCtxAsync(ctx));
            if (amount is < 0 or 0)
            {
                await new DiscordMessageBuilder()
                                                .WithReply(ctx.Message.Id)
                                                .WithEmbed(new DiscordEmbedBuilder()
                                                                                    .WithTitle(lang.PurgeNumberNegative)
                                                                                    .WithColor(color: await ColorUtils.GetSingleAsync()))
                                                .SendAsync(ctx.Channel);
                return;
            }
            List<DiscordMessage> messages = (await ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id, amount)).Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14).ToList();
            if (messages.Count == 0)
            {
                await new DiscordMessageBuilder()
                                                .WithReply(ctx.Message.Id)
                                                .WithEmbed(new DiscordEmbedBuilder()
                                                                                    .WithTitle(lang.PurgeNothingToDelete)
                                                                                    .WithColor(color: await ColorUtils.GetSingleAsync()))
                                                .SendAsync(ctx.Channel);
                return;
            }
            await ctx.Channel.DeleteMessagesAsync(messages, string.Format(lang.PurgedBySilverBotReason, ctx.Member.Username));

            await new DiscordMessageBuilder()
                                               .WithReply(ctx.Message.Id)
                                               .WithEmbed(new DiscordEmbedBuilder()
                                                                                   .WithTitle(lang.PurgeRemovedFront + messages.Count + (messages.Count > 1 ? lang.PurgeRemovedPlural : lang.PurgeRemovedSingle))
                                                                                   .WithColor(color: await ColorUtils.GetSingleAsync()))
                                               .SendAsync(ctx.Channel);
        }
    }
}