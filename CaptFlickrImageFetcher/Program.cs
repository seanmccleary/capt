/*
 * Copyright 2011 Sean McCleary
 * 
 * This file is part of capt.
 *
 * capt is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * capt is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with capt.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linq.Flickr;
using Capt.Models;

namespace CaptFlickrImageFetcher
{
    /// <summary>
    /// A simple program to connect to Flickr and look for CC-licensed images to use.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Started at " + DateTime.Now);

            // Get the tags to use for search, from the config file.
            var tags = System.Configuration.ConfigurationManager.AppSettings["tags"].Split(
                new char[] { ',', ';', ' '}, 
                StringSplitOptions.RemoveEmptyEntries).ToList().OrderBy(a => Guid.NewGuid());

            FlickrContext context = new FlickrContext();
            IPictureRepository pictureRepo = new Capt.Models.LinqToMySql.PictureRepository();

            Photo newPhoto = null;
            int? licenseId = null;
            Random rand = new Random();

            do
            {
                var tag = tags.ElementAt(rand.Next(tags.Count() - 1));
                System.Console.WriteLine("\nLet's use the tag '" + tag + "' shall we?");

                System.Console.Write("Hittin' the Flickr...");

                var photos = from ph in context.Photos
                             where ph.ViewMode == ViewMode.Public
                             && ph.SearchText == tag
                             && ph.SearchMode == SearchMode.FreeText
                             && ph.Extras == (ExtrasOption.License | ExtrasOption.Owner_Name)
                             && ph.PhotoSize == PhotoSize.Medium
                             orderby PhotoOrder.Date_Posted descending
                             select ph;

                int pageNum = (new Random()).Next(100);
                
                System.Console.Write("shuffling page " + pageNum + " of results...");

                photos.Skip(pageNum).Take(100).OrderBy(x => Guid.NewGuid());

                System.Console.WriteLine("done.");

                System.Console.Write("Searching for a suitable picture...");

                photos = photos.OrderBy(a => Guid.NewGuid());

                foreach (var photo in photos)
                {
                    // Unfortunately we can't restrict license types in our linq query
                    switch (photo.ExtrasResult.License)
                    {
                        case "4": // CC BY 2.0
                            licenseId = License.CC_BY_2;
                            break;

                        case "5": // CC BY-SA 2.0
                            licenseId = License.CC_BYSA_2;
                            break;

                        case "7": // No known copyright
                            licenseId = License.PDM;
                            break;

                        default:
                            continue;
                    }

                    System.Console.WriteLine("found one.");
                    System.Console.Write("Have we already used it? ... ");

                    // OK this photo is acceptable to use.
                    // Let's make sure we haven't used it before.
                    Picture oldPicture = pictureRepo.GetByUrl(photo.Url);

                    if (oldPicture != null)
                    {
                        // d'oh.  I guess we did use it already.
                        System.Console.WriteLine("Yup. :(");
                        continue;
                    }
                    else
                    {
                        System.Console.WriteLine("Nope! :)");
                        newPhoto = photo;
                        break;
                    }

                }

            } while (newPhoto == null);

            // So did we find a photo?
            if (newPhoto == null)
            {
                // Crud
                throw new ApplicationException("Couldn't find a photo to use!");
            }

            System.Console.Write("Saving our new picture...");

            // OK let's save our new pic!
            Picture newPicture = new Picture
            {
                Attribution = String.Format(
                    "<a class=\"flickr_attribution_link\" href=\"http://www.flickr.com/\"><img class=\"flickr_attribution_icon\" src=\"/Content/images/flickr_icon.png\" /></a> Photo by <a target=\"_new\" href=\"{0}\">{1}</a>",
                    newPhoto.WebUrl,
                    newPhoto.ExtrasResult.OwnerName),
                AttributionUrl = newPhoto.WebUrl,
                Guid = Guid.NewGuid(),
                IsNSFW = false,
                IsPrivate = false,
                IsVisible = true,
                Url = newPhoto.Url,
                LicenseId = licenseId ?? 0,
                Activates = DateTime.UtcNow.AddMinutes(rand.Next(690, 750)),
                Event = new Event(EventType.PictureCreated)
            };

            pictureRepo.Save(newPicture);
            System.Console.WriteLine("done.");

            System.Console.WriteLine("Ended at " + DateTime.Now);

        }
    }
}
