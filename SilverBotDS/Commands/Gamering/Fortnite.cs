﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Fortnite_API;
using Fortnite_API.Objects;
using Fortnite_API.Objects.V1;
using Fortnite_API.Objects.V2;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SilverBotDS
{
    internal class Fortnite : BaseCommandModule
    {
#pragma warning disable CA1822 // Mark members as static
        private static FortniteApiClient api;

        /// <summary>
        /// fortite mor lioke fartnite am i righe or am i righe
        /// </summary>
        /// <param name="apie"></param>
        public static void Setapi(FortniteApiClient apie)
        {
            api = apie;
        }

        [Command("fortstats")]
        [Description("Get the stats of a person using their username")]
        public async Task Stats(CommandContext ctx, [Description("The username of the person")][RemainingText()] string name)
        {
            Language lang = Language.GetLanguageFromId(ctx.Guild?.Id);
            ApiResponse<BrStatsV2V1> statsV2V1 = await api.V1.Stats.GetBrV2Async(x =>
            {
                x.Name = name;
                x.ImagePlatform = BrStatsV2V1ImagePlatform.All;
            });
            if (statsV2V1.IsSuccess)
            {
                await ctx.RespondAsync(statsV2V1.Data.Image.ToString());
            }
            else
            {
                await ctx.RespondAsync(lang.Fortnite_Search_Fail);
            }
        }

        [Command("fortbrnews")]
        [Description("Get a gif of the latest battle royale news")]
        public async Task Brnews(CommandContext ctx)
        {
            ApiResponse<NewsV2Combined> newsV2 = await api.V2.News.GetAsync();
            await ctx.RespondAsync(newsV2.Data.Br.Image.ToString());
        }

        [Command("fortcrnews")]
        [Description("Get a gif of the latest creative news")]
        public async Task Crnews(CommandContext ctx)
        {
            ApiResponse<NewsV2Combined> newsV2 = await api.V2.News.GetAsync();
            await ctx.RespondAsync(newsV2.Data.Creative.Image.ToString());
        }

        [Command("fortstwnews")]
        [Description("Get a gif of the latest save-the-world news")]
        public async Task Stwnews(CommandContext ctx)
        {
            ApiResponse<NewsV2Combined> newsV2 = await api.V2.News.GetAsync();
            await ctx.RespondAsync(newsV2.Data.Stw.Image.ToString());
        }

        [Command("fortitms")]
        [Description("Try to get the item shop items")]
        public async Task Itm(CommandContext ctx)
        {
            ApiResponse<BrShopV2Combined> shop = await api.V2.Shop.GetBrCombinedAsync();
            StringBuilder sb = new StringBuilder();
            foreach (BrShopV2StoreFrontEntry thing in shop.Data.Daily.Entries)
            {
                sb.Append(thing.DevName + Environment.NewLine);
            }
            await ctx.RespondAsync(sb.ToString());
        }
    }
}