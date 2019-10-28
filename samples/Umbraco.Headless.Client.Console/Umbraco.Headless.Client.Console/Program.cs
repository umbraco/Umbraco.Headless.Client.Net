using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Console
{
    class Program
    {
        private static readonly IDictionary<string, Func<ContentDeliveryService, Task>> CmdOptions =
            new Dictionary<string, Func<ContentDeliveryService, Task>>
        {
            { "A", async service =>  await FetchContentTree(service)},
            { "B", async service =>  await FetchMediaTree(service)},
            { "C", async service =>  await ShowRootContent(service)},
            { "D", async service =>  await ShowRootMedia(service)},
            { "E", async service =>  await ListAllContentUrls(service)},
            { "X", async service =>  await ExitOptions(service)}

        };
        static async Task Main(string[] args)
        {
            System.Console.Write(Bootloader.Load());
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Booting Umbraco Headless console");
            System.Console.WriteLine(" ");

            //Enter Project Alias
            System.Console.WriteLine("Enter the Project Alias of your Headless Project");
            var projectAlias = System.Console.ReadLine();

            //New up service with the entered Headless Project Alias
            var service = new ContentDeliveryService(new ContentDeliveryConfiguration(projectAlias));

            //Output options to the console and record the user's choice
            await RenderOptions(service);

            System.Console.WriteLine(" ");
            System.Console.WriteLine("Press ENTER to exit");
            System.Console.ReadLine();
        }

        private static async Task RenderOptions(ContentDeliveryService service)
        {
            System.Console.WriteLine(" ");
            // List options for Content Delivery (Content + Media)
            System.Console.WriteLine("[A] Fetch and show Content tree");
            System.Console.WriteLine("[B] Fetch and show Media tree");
            System.Console.WriteLine("[C] Show root Content");
            System.Console.WriteLine("[D] Show root Media");
            System.Console.WriteLine("[E] List Content URLs");
            System.Console.WriteLine("[X] Exit");
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Enter your choice:");

            var choice = System.Console.ReadLine()?.ToUpper();
            if (string.IsNullOrEmpty(choice))
            {
                System.Console.WriteLine("Please enter option A, B, C, D or E");
                choice = System.Console.ReadLine()?.ToUpper();
            }

            // Retrieve and show based on choice
            await CmdOptions[choice](service);

            //Recurse unless we need to escape and quit the console
            if (!choice.Equals("X"))
            {
                await RenderOptions(service);
            }
        }

        public static async Task FetchContentTree(ContentDeliveryService service)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Fetching and listing Content tree");
            System.Console.WriteLine(" ");

            var root = await service.Content.GetRoot();
            foreach (var content in root)
            {
                await PrintTree(service.Content, content, "", true);
            }

            System.Console.WriteLine(" ");
        }

        public static async Task FetchMediaTree(ContentDeliveryService service)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Fetching and listing Media tree");
            System.Console.WriteLine(" ");

            var root = await service.Media.GetRoot();
            foreach (var media in root)
            {
                await PrintTree(service.Media, media, "", true);
            }

            System.Console.WriteLine(" ");
        }

        public static async Task ShowRootContent(ContentDeliveryService service)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Fetching and showing root Content");
            System.Console.WriteLine(" ");

            var rootContentItems = await service.Content.GetRoot();
            foreach (var rootContentItem in rootContentItems)
            {
                RenderContentWithUrl(rootContentItem);
            }

            System.Console.WriteLine(" ");
        }

        public static async Task ShowRootMedia(ContentDeliveryService service)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Fetching and showing root Media");
            System.Console.WriteLine(" ");

            var rootMediaItems = await service.Media.GetRoot();
            foreach (var media in rootMediaItems)
            {
                RenderMediaWithUrl(media);
            }

            System.Console.WriteLine(" ");
        }

        public static async Task ListAllContentUrls(ContentDeliveryService service)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("Fetching and showing all Content Urls");
            System.Console.WriteLine(" ");

            var rootContentItems = await service.Content.GetRoot();
            foreach (var rootContentItem in rootContentItems)
            {
                RenderContentWithUrl(rootContentItem);
                var descendants = await service.Content.GetDescendants(rootContentItem.Id);
                await PageAndRenderDescendants(service.Content, descendants, rootContentItem.Id, descendants.Page);
            }

            System.Console.WriteLine(" ");
        }

        public static async Task ExitOptions(ContentDeliveryService service)
        {
            await Task.FromResult<string>(null);
        }

        private static async Task PrintTree(IContentDelivery contentDelivery, Content content, string indent, bool last)
        {
            System.Console.WriteLine(indent + "+- " + content.Name);
            indent += last ? "   " : "|  ";

            if (content.HasChildren)
            {
                var paged = await contentDelivery.GetChildren(content.Id);
                var items = paged.Content.Items.ToArray();
                for (int i = 0; i < items.Count(); i++)
                {
                    await PrintTree(contentDelivery, items[i], indent, i == items.Count() - 1);
                }
            }
        }

        private static async Task PrintTree(IMediaDelivery contentDelivery, Media media, string indent, bool last)
        {
            System.Console.WriteLine(indent + "+- " + media.Name);
            indent += last ? "   " : "|  ";

            if (media.HasChildren)
            {
                var paged = await contentDelivery.GetChildren(media.Id);
                var items = paged.Media.Items.ToArray();
                for (int i = 0; i < items.Count(); i++)
                {
                    await PrintTree(contentDelivery, items[i], indent, i == items.Count() - 1);
                }
            }
        }

        private static async Task PageAndRenderDescendants(IContentDelivery contentDelivery, PagedContent pagedContent, Guid parentId, int page)
        {
            foreach (var contentItem in pagedContent.Content.Items)
            {
                RenderContentWithUrl(contentItem);
            }

            if (page < pagedContent.TotalPages)
            {
                int nextPage = page + 1;
                var nextPagedContent = await contentDelivery.GetDescendants(parentId, null, nextPage);
                await PageAndRenderDescendants(contentDelivery, nextPagedContent, parentId, nextPage);
            }
        }

        private static void RenderContentWithUrl(Content content)
        {
            System.Console.WriteLine("'"+ content.Name + "' on " + content.Url);
        }

        private static void RenderMediaWithUrl(Media media)
        {
            if (media.MediaTypeAlias.Equals("folder", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Console.WriteLine("'" + media.Name + "' - This is a folder.");
            }
            else
            {
                System.Console.WriteLine("'" + media.Name + "' can be seen on: " + media.Url);
            }
        }
    }
}
