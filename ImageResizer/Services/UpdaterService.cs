//------------------------------------------------------------------------------
// <copyright file="UpdaterService.cs" company="Brice Lambson">
//     Copyright (c) 2011-2013 Brice Lambson. All rights reserved.
//
//     The use of this software is governed by the Microsoft Public License
//     which is included with this distribution.
// </copyright>
//------------------------------------------------------------------------------

namespace BriceLambson.ImageResizer.Services
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel.Syndication;
    using System.Threading.Tasks;
    using System.Xml;
    using BriceLambson.ImageResizer.Extensions;
    using BriceLambson.ImageResizer.Models;

    internal class UpdaterService
    {
        public async Task<Update> CheckForUpdatesAsync(string updateUrl)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(updateUrl), "updateUrl is null or empty.");

            var reader = XmlReader.Create(updateUrl);
            var formatter = new Atom10FeedFormatter();

            await formatter.ReadFromAsync(reader);

            return (from i in formatter.Feed.Items
                    let u = FromSyndicationItem(i)
                    where u.Version > Assembly.GetExecutingAssembly().GetName().Version
                    orderby u.LastUpdatedTime descending
                    select u).FirstOrDefault();
        }

        private static Update FromSyndicationItem(SyndicationItem item)
        {
            Debug.Assert(item != null, "item is null.");

            var update = new Update();
            Version version;

            if (Version.TryParse(item.Title.Text, out version))
            {
                update.Version = version;
            }

            update.LastUpdatedTime = item.LastUpdatedTime;

            var link = item.Links.FirstOrDefault(
                l => String.IsNullOrWhiteSpace(l.RelationshipType)
                    || l.RelationshipType.Equals("alternate", StringComparison.OrdinalIgnoreCase));

            if (link != null)
            {
                update.Url = link.GetAbsoluteUri();
            }

            return update;
        }
    }
}