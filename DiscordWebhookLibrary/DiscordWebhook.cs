namespace DiscordWebhookLibrary;

using System;
using System.Text.Json;
using System.Net.Http.Headers;


public class DiscordWebhook
{

    public class Request
    {
        public string? content { get; set; }
        public List<Embed>? embeds { get; set; }
        public string? username { get; set; }
        public string? avatar_url { get; set; }
        public bool? tts { get; set; }
    }

    public class Embed
    {
        public string title { get; set; }
        public string description { get; set; }

        public int? color { get; set; }

        public Author? author { get; set; }
        public Footer? footer { get; set; }
        public Image? image { get; set; }
        public Provider provider { get; set; }
        public List<Field>? fields { get; set; }

        public Embed()
        {
            fields = new List<Field>();
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public void setAuthor(string name, string? icon_url = null, string? url = null)
        {
            Author author1 = new Author();
            author1.name = name;
            author1.icon_url = icon_url;
            author1.url = url;

            author = author1;
        }

        public void setFooter(string text, string? icon_url = null)
        {
            Footer footer = new Footer();
            footer.text = text;
            footer.icon_url = icon_url;

            this.footer = footer;
        }

        public void setProvider(string name = null, string url = null)
        {
            Provider provider = new Provider();
            provider.name = name;
            provider.url = url;

            this.provider = provider;
        }

        public void setImage(string url, int? height = null, int? width = null)
        {
            Image image = new Image();
            image.url = url;
            image.width = width;
            image.height = height;

            this.image = image;
        }

        public void setColor(int red, int green, int blue)
        {
            int color = 0;
            color += red * 65536;
            color += green * 256;
            color += blue;

            if (color > 16777215)
            {
                color = 16777215;
            }

            this.color = color;
        }

        public void addField(string? name = null, string? value = null, bool? inline = false)
        {
            Field field = new Field();
            field.name = name;
            field.value = value;
            field.inline = inline;

            fields.Add(field);
        }

    }

    public class Field
    {
        public string name { get; set; }
        public string value { get; set; }
        public bool? inline { get; set; }


    }

    public class Author
    {
        public string name { get; set; }
        public string url { get; set; }
        public string icon_url { get; set; }
    }

    public class Footer
    {
        public string text { get; set; }
        public string? icon_url { get; set; }

    }

    public class Provider
    {
        public string? name { get; set; }
        public string? url { get; set; }

    }

    public class Image
    {
        public string url { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }

    }




    List<Embed> embeds;
    Request data;

    string url = "";
    string content = "";


    public DiscordWebhook(string url)
    {
        this.url = url;

        data = new Request();
        data.embeds = new List<Embed>();

    }

    // Basic
    public DiscordWebhook setContent(string message)
    {
        data.content = message;

        return this;
    }

    public DiscordWebhook setCustomName(string username)
    {
        data.username = username;

        return this;
    }

    public DiscordWebhook setCustomAvatar(string url)
    {
        data.avatar_url = url;

        return this;
    }

    public DiscordWebhook useTTS(bool value)
    {
        data.tts = value;

        return this;
    }






    public void addEmbed(Embed embed)
    {
        data.embeds.Add(embed);
    }




    public void send()
    {
        HttpClient client = new HttpClient();


        content = JsonSerializer.Serialize(data);
        // Console.WriteLine(content);
        var dataToSend = new StringContent(content);
        dataToSend.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var post = client.PostAsync(url, dataToSend);
        post.Wait();
        var result = post.Result.Content.ReadAsStringAsync();
        result.Wait();

        Console.WriteLine(result.Result);



    }

}




