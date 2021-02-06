﻿using SteamStoreQuery;
using System.Collections.Generic;

namespace SilverBotDS
{
    internal class Steam
    {
        /// <summary>
        /// Search for something on steam
        /// </summary>
        /// <param name="searchQuery">The search query like "call of duty"</param>
        /// <returns>A list of listings</returns>
        public static List<Listing> Search(string searchQuery)
        {
            return Query.Search(searchQuery);
        }
    }
}