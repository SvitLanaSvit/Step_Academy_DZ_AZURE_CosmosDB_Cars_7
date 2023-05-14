using DZ_AZURE_CosmosDB_Cars_7;
using Microsoft.Azure.Cosmos;
using System;

string endpointUrl = "https://cosmossvit.documents.azure.com:443/";
string accountKey = "TJgMjMJBwCqRAd4i0lqyBsRupLJYFfy7TGxwGWAHlYQJOif59MxIXFPhOxUICrUn7HG5H2GkMCIaACDbSVijjQ==";

string dataBaseId = "blogDatabase";
string containerId = "blogContainer";

List<Blog> blogs = createBlogs();


try
{
    Database database = await CreateAndGetDatabaseAsync(endpointUrl, accountKey, dataBaseId);
    Container container = await CreateAndGetContainerAsync(database, containerId);
    await WriteDataAsync(blogs, container);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


async Task<Database> CreateAndGetDatabaseAsync(string endpoint, string accountKey, string dataBaseId)
{
    CosmosClient cosmosClient = new CosmosClient(endpointUrl, accountKey);
    Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(dataBaseId);
    return database;
}

async Task<Container> CreateAndGetContainerAsync(Database database, string containerId)
{
    Container container = await database.CreateContainerIfNotExistsAsync(containerId, partitionKeyPath: "/Manufacturer");
    return container;
}

async Task WriteDataAsync(List<Blog> blogs, Container container)
{
    foreach (Blog blog in blogs)
    {
        try
        {
            ItemResponse<Blog> response = await container.CreateItemAsync(blog, partitionKey: new PartitionKey(blog.Manufacturer));
            Console.WriteLine($"Item {response.Resource.Id} was added to DB with Request Charge: {response.RequestCharge}");
        }
        catch (CosmosException ex)
        when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            Console.WriteLine("This element is already in database");
        }
    }
}

List<Blog> createBlogs()
{
    List<Blog> blogs = new List<Blog>();
    Blog blog1 = new Blog()
    {
        Id = Guid.NewGuid().ToString(),
        Manufacturer = "Bugati",
        Autos = new List<Auto>() 
        { 
            new Auto
            {
                Title = "Veyron",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Bugatti_veyron.jpg"
                    }
                },
                Price = 1490000.0F,
                PS = 1001,
                Description = "The Bugatti Veyron is an iconic hypercar renowned for its incredible speed and striking design. " +
                "Made by Bugatti, the Veyron boasts a powerful engine and exceptional performance capabilities. " +
                "With its sleek and aerodynamic body, the Veyron exudes a perfect blend of elegance and aggression. " +
                "Inside, the car offers a luxurious and driver-focused cockpit, featuring high-quality materials " +
                "and advanced technology. As one of the fastest and most sought-after production cars, " +
                "the Bugatti Veyron is a true symbol of automotive excellence and craftsmanship.",
                Country = new Country{Name = "France"}
            },
            new Auto
            {
                Title = "Divo",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Bugatti_divo.jpg"
                    },
                    new Photo 
                    { 
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Bugatti_divo_2.jpg"
                    }
                },
                Price = 5800000.0F,
                PS = 1500,
                Description = "The Bugatti Divo is a limited-edition hypercar designed for high-performance driving and track use. " +
                "Made by Bugatti, the Divo is based on the Bugatti Chiron but features a more aggressive and aerodynamic design. " +
                "Its sleek body is made of lightweight carbon fiber, with a distinctive front end " +
                "and a striking rear wing that enhances downforce and stability. " +
                "The Divo is powered by an 8.0-liter quad-turbocharged W16 engine, producing 1,500 horsepower " +
                "and enabling it to reach a top speed of 236 miles per hour (380 kilometers per hour). " +
                "The interior is driver-focused, with a sporty steering wheel and advanced technology for precision driving. " +
                "With only 40 units produced, the Bugatti Divo is a rare and exclusive hypercar that represents " +
                "the ultimate in engineering and performance.",
                Country = new Country{Name = "France"}
            },
            new Auto
            {
                Title = "Chiron",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Bugatti_Chiron.jpg"
                    },
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Bugatti_Chiron_2.jpg"
                    }
                },
                Price = 3200000.0F,
                PS = 1500,
                Description = "The Bugatti Chiron is a high-performance hypercar produced by the French automaker Bugatti. " +
                "It was named after the racing driver Louis Chiron and serves as a successor to the Bugatti Veyron. " +
                "The Chiron is powered by an 8.0-liter quad-turbocharged W16 engine that produces 1,500 horsepower " +
                "and 1,180 pound-feet of torque. This power is transmitted to the wheels through a 7-speed dual-clutch " +
                "transmission, allowing the Chiron to accelerate from 0-60 mph in just 2.5 seconds, " +
                "and reach a top speed of 261 mph. The Chiron features a sleek and aerodynamic exterior design, " +
                "with a curvy body, a prominent Bugatti grille, and a distinctive C-shaped signature line running across " +
                "the side panels. The interior of the Chiron is luxurious and features premium materials such as leather, " +
                "carbon fiber, and aluminum. Only 500 units of the Bugatti Chiron are expected to be produced, " +
                "with a price tag of around $3 million.",
                Country = new Country{Name = "France"}
            },
            new Auto
            {
                Title = "La Voiture Noire",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Bugatti_la_voiture_noire.jpg"
                    }
                },
                Price = 19000000.0F,
                PS = 1500,
                Description = "Bugatti La Voiture Noire is a one-of-a-kind hypercar produced by the French automaker Bugatti. " +
                "The name of the car translates to \"The Black Car\" in French, and it was created as a tribute to " +
                "the Bugatti Type 57 SC Atlantic, a rare vintage car from the 1930s. La Voiture Noire boasts " +
                "a sleek and aerodynamic exterior design, with flowing lines and a seamless body. " +
                "It is finished in black carbon fiber, giving it a striking and menacing appearance. " +
                "Under the hood, La Voiture Noire is powered by a quad-turbocharged 8.0-liter W16 engine, " +
                "which produces 1,500 horsepower and 1,180 pound-feet of torque. The power is transmitted to " +
                "the wheels through a 7-speed dual-clutch transmission, allowing the car to accelerate from 0-60 mph " +
                "in just 2.4 seconds and reach a top speed of 261 mph. The interior of La Voiture Noire " +
                "is luxurious and features premium materials such as leather, carbon fiber, and aluminum. " +
                "The car was sold for a record-breaking price of $19 million, making it the most expensive car ever sold.",
                Country = new Country{Name = "France"}
            }
        }
    };

    Blog blog2 = new Blog()
    {
        Id = Guid.NewGuid().ToString(),
        Manufacturer = "Lamborghini",
        Autos = new List<Auto>()
        {
            new Auto
            {
                Title = "Huracan Evo",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Lamborghini_huracan_evo.jpg"
                    }
                },
                Price = 261274.0F,
                PS = 640,
                Description = "The Lamborghini Huracan Evo is a dynamic and exhilarating sports car that combines " +
                "stunning design with impressive performance. It is equipped with a V10 engine, " +
                "advanced driving dynamics, and cutting-edge technology, offering an engaging " +
                "and thrilling driving experience.",
                Country = new Country{Name = "Italy"}
            },
            new Auto
            {
                Title = "Sian",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Lamborghini_Sian.jpg"
                    }
                },
                Price = 3600000.0F,
                PS = 819,
                Description = "The Lamborghini Sian is a limited-edition hybrid supercar that showcases " +
                "Lamborghini's advanced technologies. It features a V12 engine combined with " +
                "a supercapacitor-based hybrid system, providing impressive power and acceleration. " +
                "With its futuristic design and exclusive production, the Sian represents the pinnacle " +
                "of Lamborghini's engineering prowess.",
                Country = new Country{Name = "Italy"}
            },
            new Auto
            {
                Title = "Centenario",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Lamborghini_Centenario.jpg"
                    }
                },
                Price = 2500000.0F,
                PS = 770,
                Description = "The Lamborghini Centenario is a hypercar created to celebrate the 100th birthday " +
                "of the company's founder, Ferruccio Lamborghini. It boasts a powerful V12 engine, " +
                "lightweight carbon fiber construction, and cutting-edge aerodynamics. " +
                "With only a limited number produced, the Centenario is a rare and exclusive masterpiece " +
                "of automotive engineering.",
                Country = new Country{Name = "Italy"}
            },
            new Auto
            {
                Title = "Aventador SVJ",
                PhotoLink = new List<Photo>()
                {
                    new Photo
                    {
                        Link = "https://svitlanasvitstorage.blob.core.windows.net/autos/Lamborghini_Aventador SVJ.jpg"
                    }
                },
                Price = 573966.0F,
                PS = 770,
                Description = "The Lamborghini Aventador SVJ is a powerful supercar known for " +
                "its aggressive design and exceptional performance. It features a naturally " +
                "aspirated V12 engine, advanced aerodynamics, and innovative technologies, " +
                "making it a thrilling and track-focused driving machine.",
                Country = new Country{Name = "Italy"}
            }
        }
    };

    blogs.Add(blog1);
    blogs.Add(blog2);

    return blogs;
}