﻿using System.Text;
using Refit;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Samples.Console;

IDictionary<string, Func<ContentDeliveryService, string, Task>> CmdOptions =
    new Dictionary<string, Func<ContentDeliveryService, string, Task>>
{
    { "A", async (service, _) =>  await FetchContentTree(service)},
    { "B", async (service, _) =>  await FetchMediaTree(service)},
    { "C", async (service, _) =>  await ShowRootContent(service)},
    { "D", async (service, _) =>  await ShowRootMedia(service)},
    { "E", async (service, _) =>  await ListAllContentUrls(service)},
    { "F", async (service, projectAlias) =>  await UploadImageToMedia(service, projectAlias)},
    { "X", async (_, projectAlias) =>  await ExitOptions(projectAlias)}
};

Console.Write(Bootloader.Load());
Console.WriteLine(" ");
Console.WriteLine("Booting Umbraco Headless console");
Console.WriteLine(" ");

//Enter Project Alias
Console.WriteLine("Enter the Project Alias of your Headless Project");
var projectAlias = Console.ReadLine();

//New up service with the entered Headless Project Alias
var service = new ContentDeliveryService(projectAlias);

//Output options to the console and record the user's choice
await RenderOptions(service, projectAlias!);

Console.WriteLine(" ");
Console.WriteLine("Press ENTER to exit");
Console.ReadLine();

async Task RenderOptions(ContentDeliveryService service, string projectAlias)
{
    Console.WriteLine(" ");
    // List options for Content Delivery (Content + Media)
    Console.WriteLine("[A] Fetch and show Content tree");
    Console.WriteLine("[B] Fetch and show Media tree");
    Console.WriteLine("[C] Show root Content");
    Console.WriteLine("[D] Show root Media");
    Console.WriteLine("[E] List Content URLs");
    Console.WriteLine("[F] Upload image to Media Library");
    Console.WriteLine("[X] Exit");
    Console.WriteLine(" ");

    Console.WriteLine("Enter your choice:");
    var choice = Console.ReadLine()?.ToUpper();
    if (string.IsNullOrEmpty(choice))
    {
        Console.WriteLine("Please enter option A, B, C, D or E");
        choice = Console.ReadLine()?.ToUpper();
    }

    // Retrieve and show based on choice
    await CmdOptions[choice!](service, projectAlias);

    //Recurse unless we need to escape and quit the console
    if (!choice!.Equals("X"))
    {
        await RenderOptions(service, projectAlias);
    }
}

async Task FetchContentTree(ContentDeliveryService service)
{
    Console.WriteLine(" ");
    Console.WriteLine("Fetching and listing Content tree");
    Console.WriteLine(" ");

    var root = await service.Content.GetRoot();
    foreach (var content in root)
    {
        await PrintContentTree(service.Content, content, "", true);
    }

    Console.WriteLine(" ");
}

async Task FetchMediaTree(ContentDeliveryService service)
{
    Console.WriteLine(" ");
    Console.WriteLine("Fetching and listing Media tree");
    Console.WriteLine(" ");

    var root = await service.Media.GetRoot();
    foreach (var media in root)
    {
        await PrintMediaTree(service.Media, media, "", true);
    }

    Console.WriteLine(" ");
}

async Task ShowRootContent(ContentDeliveryService service)
{
    Console.WriteLine(" ");
    Console.WriteLine("Fetching and showing root Content");
    Console.WriteLine(" ");

    var rootContentItems = await service.Content.GetRoot();
    foreach (var rootContentItem in rootContentItems)
    {
        RenderContentWithUrl(rootContentItem);
    }

    Console.WriteLine(" ");
}

async Task ShowRootMedia(ContentDeliveryService service)
{
    Console.WriteLine(" ");
    Console.WriteLine("Fetching and showing root Media");
    Console.WriteLine(" ");

    var rootMediaItems = await service.Media.GetRoot();
    foreach (var media in rootMediaItems)
    {
        RenderMediaWithUrl(media);
    }

    Console.WriteLine(" ");
}

async Task ListAllContentUrls(ContentDeliveryService service)
{
    Console.WriteLine(" ");
    Console.WriteLine("Fetching and showing all Content Urls");
    Console.WriteLine(" ");

    var rootContentItems = await service.Content.GetRoot();
    foreach (var rootContentItem in rootContentItems)
    {
        RenderContentWithUrl(rootContentItem);
        var descendants = await service.Content.GetDescendants(rootContentItem.Id);
        await PageAndRenderDescendants(service.Content, descendants, rootContentItem.Id, descendants.Page);
    }

    Console.WriteLine(" ");
}

async Task UploadImageToMedia(ContentDeliveryService service, string projectAlias)
{
    Console.WriteLine(" ");
    Console.WriteLine("In order to upload an image you need to authenticate against the Umbraco Headless Backoffice");

    Console.WriteLine("Enter your username:");
    var username = Console.ReadLine();

    Console.WriteLine("Enter your password:");
    var password = GetConsolePassword();

    var managementService = new ContentManagementService(projectAlias, username, password);

    Console.WriteLine(" ");
    Console.WriteLine("Enter path to an image (png, jpg)");
    var imagePath = Console.ReadLine();

    Console.WriteLine("Enter a name for the Media item to create");
    var mediaName = Console.ReadLine();

    if (File.Exists(imagePath) && !string.IsNullOrEmpty(mediaName))
    {
        var fileName = Path.GetFileName(imagePath);
        var extension = Path.GetExtension(imagePath).Trim('.');
        Console.WriteLine(" ");
        Console.WriteLine("Uploading '{0}' to a new Console folder in the Media Library", fileName);

        var rootMediaItems = await service.Media.GetRoot();
        var folder = rootMediaItems.FirstOrDefault(x => x.Name.Equals("Console"));
        Guid folderId;
        if (folder == null)
        {
            var createdFolder = await managementService.Media.Create(new Umbraco.Headless.Client.Net.Management.Models.Media { MediaTypeAlias = "Folder", Name = "Console" });
            folderId = createdFolder.Id;
        }
        else
        {
            folderId = folder.Id;
        }

        var media = new Umbraco.Headless.Client.Net.Management.Models.Media {Name = mediaName, MediaTypeAlias = "Image", ParentId = folderId};
        media.SetValue("umbracoFile", new { src = fileName }, new FileInfoPart(new FileInfo(imagePath), fileName, $"image/{extension}"));
        var image = await managementService.Media.Create(media);

        var newlyCreatedImage = await service.Media.GetById(image.Id);
        RenderMediaWithUrl(newlyCreatedImage);
    }
    else
    {
        Console.WriteLine(" ");
        Console.WriteLine("Path to image not found '{0}'", imagePath);
    }

    Console.WriteLine(" ");
}

async Task ExitOptions(string projectAlias)
{
    Console.WriteLine("-= Thank you for trying out the Console sample against your project '{0}' =-", projectAlias);

    await Task.FromResult<string>(null!);
}

async Task PrintContentTree(IContentDelivery contentDelivery, Content content, string indent, bool last)
{
    Console.WriteLine(indent + "+- " + content.Name);
    indent += last ? "   " : "|  ";

    if (content.HasChildren)
    {
        var paged = await contentDelivery.GetChildren(content.Id);
        var items = paged.Content.Items.ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            await PrintContentTree(contentDelivery, items[i], indent, i == items.Length - 1);
        }
    }
}

async Task PrintMediaTree(IMediaDelivery contentDelivery, Media media, string indent, bool last)
{
    Console.WriteLine(indent + "+- " + media.Name);
    indent += last ? "   " : "|  ";

    if (media.HasChildren)
    {
        var paged = await contentDelivery.GetChildren(media.Id);
        var items = paged.Media.Items.ToArray();
        for (int i = 0; i < items.Length; i++)
        {
            await PrintMediaTree(contentDelivery, items[i], indent, i == items.Length - 1);
        }
    }
}

async Task PageAndRenderDescendants(IContentDelivery contentDelivery, PagedContent pagedContent, Guid parentId, int page)
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

void RenderContentWithUrl(Content content)
{
    Console.WriteLine("'"+ content.Name + "' on " + content.Url);
}

void RenderMediaWithUrl(Media media)
{
    if (media.MediaTypeAlias.Equals("folder", StringComparison.InvariantCultureIgnoreCase))
    {
        Console.WriteLine("'" + media.Name + "' - This is a folder.");
    }
    else
    {
        Console.WriteLine("'" + media.Name + "' can be seen on: " + media.Url);
    }
}

string GetConsolePassword()
{
    StringBuilder sb = new();
    while (true)
    {
        ConsoleKeyInfo cki = Console.ReadKey(true);
        if (cki.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }

        if (cki.Key == ConsoleKey.Backspace)
        {
            if (sb.Length > 0)
            {
                Console.Write("\b\0\b");
                sb.Length--;
            }

            continue;
        }

        Console.Write('*');
        sb.Append(cki.KeyChar);
    }

    return sb.ToString();
}
