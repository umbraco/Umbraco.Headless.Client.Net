using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Management;

namespace Umbraco.Headless.Client.Samples.Console
{
    class Program
    {
        private static readonly IDictionary<string, Func<ContentDeliveryService, string, Task>> CmdOptions =
            new Dictionary<string, Func<ContentDeliveryService, string, Task>>
        {
            { "A", async (service, projectAlias) =>  await FetchContentTree(service, projectAlias)},
            { "B", async (service, projectAlias) =>  await FetchMediaTree(service, projectAlias)},
            { "C", async (service, projectAlias) =>  await ShowRootContent(service, projectAlias)},
            { "D", async (service, projectAlias) =>  await ShowRootMedia(service, projectAlias)},
            { "E", async (service, projectAlias) =>  await ListAllContentUrls(service, projectAlias)},
            { "F", async (service, projectAlias) =>  await UploadImageToMedia(service, projectAlias)},
            { "X", async (service, projectAlias) =>  await ExitOptions(service, projectAlias)}

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
            var service = new ContentDeliveryService(projectAlias);

            //Output options to the console and record the user's choice
            await RenderOptions(service, projectAlias);

            System.Console.WriteLine(" ");
            System.Console.WriteLine("Press ENTER to exit");
            System.Console.ReadLine();
        }

        private static async Task RenderOptions(ContentDeliveryService service, string projectAlias)
        {
            System.Console.WriteLine(" ");
            // List options for Content Delivery (Content + Media)
            System.Console.WriteLine("[A] Fetch and show Content tree");
            System.Console.WriteLine("[B] Fetch and show Media tree");
            System.Console.WriteLine("[C] Show root Content");
            System.Console.WriteLine("[D] Show root Media");
            System.Console.WriteLine("[E] List Content URLs");
            System.Console.WriteLine("[F] Upload image to Media Library");
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
            await CmdOptions[choice](service, projectAlias);

            //Recurse unless we need to escape and quit the console
            if (!choice.Equals("X"))
            {
                await RenderOptions(service, projectAlias);
            }
        }

        public static async Task FetchContentTree(ContentDeliveryService service, string projectAlias)
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

        public static async Task FetchMediaTree(ContentDeliveryService service, string projectAlias)
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

        public static async Task ShowRootContent(ContentDeliveryService service, string projectAlias)
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

        public static async Task ShowRootMedia(ContentDeliveryService service, string projectAlias)
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

        public static async Task ListAllContentUrls(ContentDeliveryService service, string projectAlias)
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

        public static async Task UploadImageToMedia(ContentDeliveryService service, string projectAlias)
        {
            System.Console.WriteLine(" ");
            System.Console.WriteLine("In order to upload an image you need to authenticate against the Umbraco Headless Backoffice");

            System.Console.WriteLine("Enter your username:");
            var username = System.Console.ReadLine();

            System.Console.WriteLine("Enter your password:");
            var password = GetConsolePassword();

            var managementService = new ContentManagementService(projectAlias, username, password);

            System.Console.WriteLine(" ");
            System.Console.WriteLine("Enter path to an image (png, jpg)");
            var imagePath = System.Console.ReadLine();

            System.Console.WriteLine("Enter a name for the Media item to create");
            var mediaName = System.Console.ReadLine();

            if (File.Exists(imagePath) && !string.IsNullOrEmpty(mediaName))
            {
                var fileName = Path.GetFileName(imagePath);
                var extension = Path.GetExtension(imagePath).Trim('.');
                System.Console.WriteLine(" ");
                System.Console.WriteLine("Uploading '{0}' to a new Console folder in the Media Library", fileName);

                var rootMediaItems = await service.Media.GetRoot();
                var folder = rootMediaItems.FirstOrDefault(x => x.Name.Equals("Console"));
                Guid folderId;
                if (folder == null)
                {
                    var createdFolder = await managementService.Media.Create(new Net.Management.Models.Media { MediaTypeAlias = "Folder", Name = "Console" });
                    folderId = createdFolder.Id;
                }
                else
                {
                    folderId = folder.Id;
                }

                var media = new Net.Management.Models.Media {Name = mediaName, MediaTypeAlias = "Image", ParentId = folderId};
                media.SetValue("umbracoFile", new { src = fileName }, new FileInfoPart(new FileInfo(imagePath), fileName, $"image/{extension}"));
                var image = await managementService.Media.Create(media);

                var newlyCreatedImage = await service.Media.GetById(image.Id);
                RenderMediaWithUrl(newlyCreatedImage);
            }
            else
            {
                System.Console.WriteLine(" ");
                System.Console.WriteLine("Path to image not found '{0}'", imagePath);
            }

            System.Console.WriteLine(" ");
        }

        public static async Task ExitOptions(ContentDeliveryService service, string projectAlias)
        {
            System.Console.WriteLine("-= Thank you for trying out the Console sample against your project '{0}' =-", projectAlias);

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

        private static string GetConsolePassword()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = System.Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    System.Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        System.Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                System.Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }
    }
}
