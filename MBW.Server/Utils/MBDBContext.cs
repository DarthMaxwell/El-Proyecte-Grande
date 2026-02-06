using MBW.Server.Enum;
using MBW.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MBW.Server.Utils;

public class MBDBContext : DbContext
{
    public MBDBContext(DbContextOptions<MBDBContext> options) : base(options) { }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        
        base.OnModelCreating(modelBuilder);

        // Seed Movies
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, ReleaseDate = new DateOnly(1994, 10, 14), Length = 142, Title = "The Shawshank Redemption", Director = "Frank Darabont", Genre = "Drama", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency. Andy Dufresne, a banker wrongly convicted of murder, befriends fellow lifer Red while maintaining his innocence and dignity." },
            new Movie { Id = 2, ReleaseDate = new DateOnly(1972, 3, 24), Length = 175, Title = "The Godfather", Director = "Francis Ford Coppola", Genre = "Crime", Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son. Michael Corleone is drawn deeper into the family business, transforming from war hero to ruthless mob boss in this epic tale of power, loyalty, and corruption." },
            new Movie { Id = 3, ReleaseDate = new DateOnly(2008, 7, 18), Length = 152, Title = "The Dark Knight", Director = "Christopher Nolan", Genre = "Action", Description = "Batman faces the Joker in Gotham City as the Clown Prince of Crime unleashes chaos and forces the Dark Knight to confront his own moral limits. Heath Ledger delivers an unforgettable performance as the anarchic villain who seeks to prove that anyone can be corrupted." },
            new Movie { Id = 4, ReleaseDate = new DateOnly(1994, 7, 6), Length = 154, Title = "Forrest Gump", Director = "Robert Zemeckis", Genre = "Drama", Description = "The life journey of a kind-hearted man with below-average intelligence who witnesses and influences several defining historical events in 20th century America. Despite his limitations, Forrest's genuine nature and unwavering love for Jenny carry him through an extraordinary life." },
            new Movie { Id = 5, ReleaseDate = new DateOnly(2010, 7, 16), Length = 148, Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", Description = "A skilled thief who enters dreams to steal corporate secrets is given the inverse task of planting an idea into the mind of a CEO. Dom Cobb must navigate multiple layers of dreams within dreams, where the laws of physics bend and time dilates, while confronting his own guilt-ridden subconscious." },
            new Movie { Id = 6, ReleaseDate = new DateOnly(1999, 3, 31), Length = 136, Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", Genre = "Sci-Fi", Description = "A computer hacker discovers that reality as he knows it is actually a simulated construct created by sentient machines to pacify humanity while using their bodies as an energy source. Neo must choose between the comfortable lie of the Matrix and the harsh truth of the real world." },
            new Movie { Id = 7, ReleaseDate = new DateOnly(1999, 10, 15), Length = 139, Title = "Fight Club", Director = "David Fincher", Genre = "Drama", Description = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into something much more dangerous. As the narrator becomes increasingly consumed by the violent subculture, his grip on reality begins to slip in this dark commentary on consumerism and masculinity." },
            new Movie { Id = 8, ReleaseDate = new DateOnly(2001, 12, 19), Length = 178, Title = "LOTR: Fellowship of the Ring", Director = "Peter Jackson", Genre = "Fantasy", Description = "A humble hobbit named Frodo Baggins inherits a powerful ring and must journey to Mount Doom to destroy it before the dark lord Sauron can reclaim it. He is joined by a fellowship of diverse heroes in this epic quest that will determine the fate of Middle-earth." },
            new Movie { Id = 9, ReleaseDate = new DateOnly(2002, 12, 18), Length = 179, Title = "LOTR: The Two Towers", Director = "Peter Jackson", Genre = "Fantasy", Description = "The fellowship has been broken, and the quest continues on multiple fronts as Frodo and Sam press toward Mordor with the treacherous Gollum as their guide. Meanwhile, Aragorn, Legolas, and Gimli must defend the kingdom of Rohan against Saruman's massive orc army at Helm's Deep." },
            new Movie { Id = 10, ReleaseDate = new DateOnly(2003, 12, 17), Length = 201, Title = "LOTR: Return of the King", Director = "Peter Jackson", Genre = "Fantasy", Description = "The final battle for Middle-earth begins as Aragorn embraces his destiny as king, Gandalf leads the defense of Minas Tirith against Sauron's forces, and Frodo reaches the fires of Mount Doom. The culmination of the epic trilogy delivers emotional payoffs and spectacular warfare on an unprecedented scale." },
            new Movie { Id = 11, ReleaseDate = new DateOnly(1993, 6, 11), Length = 127, Title = "Jurassic Park", Director = "Steven Spielberg", Genre = "Adventure", Description = "Dinosaurs are brought back to life through genetic engineering for an ambitious theme park, but the prehistoric creatures break free when the park's security systems fail. A paleontologist, paleobotanist, and mathematician must survive the island while protecting two young children from the rampaging dinosaurs." },
            new Movie { Id = 12, ReleaseDate = new DateOnly(1977, 5, 25), Length = 121, Title = "Star Wars: A New Hope", Director = "George Lucas", Genre = "Sci-Fi", Description = "A young farm boy named Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee, and two droids to rescue a rebel princess from the clutches of the evil Galactic Empire. His journey sets him on the path to becoming a legendary Jedi and ignites a galactic rebellion." },
            new Movie { Id = 13, ReleaseDate = new DateOnly(1980, 5, 21), Length = 124, Title = "The Empire Strikes Back", Director = "Irvin Kershner", Genre = "Sci-Fi", Description = "The Empire launches a devastating attack on the Rebel Alliance, scattering the heroes across the galaxy. Luke seeks training from the ancient Jedi Master Yoda while Han and Leia evade Imperial forces, leading to shocking revelations about Luke's heritage and the true nature of the Force." },
            new Movie { Id = 14, ReleaseDate = new DateOnly(1983, 5, 25), Length = 131, Title = "Return of the Jedi", Director = "Richard Marquand", Genre = "Sci-Fi", Description = "Luke Skywalker confronts Darth Vader in a final showdown before the Emperor while the Rebel Alliance launches a desperate attack on the second Death Star. The fate of the galaxy hangs in the balance as father and son face each other, and the dark side's hold on Anakin Skywalker is finally tested." },
            new Movie { Id = 15, ReleaseDate = new DateOnly(1995, 11, 22), Length = 81, Title = "Toy Story", Director = "John Lasseter", Genre = "Animation", Description = "Toys come to life when humans aren't around, and cowboy doll Woody faces an existential crisis when a high-tech space ranger named Buzz Lightyear threatens his position as Andy's favorite toy. Their rivalry turns to friendship as they must work together to escape the clutches of the toy-torturing neighbor, Sid." },
            new Movie { Id = 16, ReleaseDate = new DateOnly(2003, 5, 30), Length = 101, Title = "Finding Nemo", Director = "Andrew Stanton", Genre = "Animation", Description = "An overprotective clownfish named Marlin embarks on a perilous journey across the ocean to find his son Nemo, who was captured by divers and taken to a dentist's aquarium in Sydney. Along the way, Marlin teams up with the forgetful but optimistic Dory in this heartwarming tale of parental love and letting go." },
            new Movie { Id = 17, ReleaseDate = new DateOnly(1997, 12, 19), Length = 195, Title = "Titanic", Director = "James Cameron", Genre = "Romance", Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic. As Jack and Rose's forbidden romance blossoms during the ship's maiden voyage, tragedy strikes when the vessel collides with an iceberg, turning their love story into a desperate fight for survival." },
            new Movie { Id = 18, ReleaseDate = new DateOnly(2014, 11, 7), Length = 169, Title = "Interstellar", Director = "Christopher Nolan", Genre = "Sci-Fi", Description = "With Earth becoming uninhabitable, a team of astronauts travels through a wormhole near Saturn in search of a new home for humanity. Former pilot Joseph Cooper must leave his children behind to explore distant galaxies where time behaves strangely, racing against the clock to save the human race." },
            new Movie { Id = 19, ReleaseDate = new DateOnly(1990, 9, 12), Length = 146, Title = "Goodfellas", Director = "Martin Scorsese", Genre = "Crime", Description = "The rise and fall of Henry Hill through the ranks of the mob is chronicled from his early days as a teenager running errands for mobsters to his eventual downfall. Based on a true story, this visceral portrait of organized crime depicts the glamorous lifestyle and brutal violence of the Italian-American mafia in New York." },
            new Movie { Id = 20, ReleaseDate = new DateOnly(1991, 2, 14), Length = 118, Title = "The Silence of the Lambs", Director = "Jonathan Demme", Genre = "Thriller", Description = "FBI trainee Clarice Starling seeks the help of imprisoned cannibalistic serial killer Dr. Hannibal Lecter to catch another serial killer known as Buffalo Bill. As Clarice delves deeper into the twisted minds of psychopaths, she must face her own demons while racing against time to save Buffalo Bill's latest victim." },
            new Movie { Id = 21, ReleaseDate = new DateOnly(1998, 7, 24), Length = 169, Title = "Saving Private Ryan", Director = "Steven Spielberg", Genre = "War", Description = "Following the D-Day invasion, a group of U.S. soldiers goes behind enemy lines during World War II on a dangerous mission to retrieve a paratrooper whose three brothers have been killed in action. Captain John Miller and his squad question the morality of risking eight men to save one in this brutal and realistic portrayal of war." },
            new Movie { Id = 22, ReleaseDate = new DateOnly(2000, 5, 5), Length = 155, Title = "Gladiator", Director = "Ridley Scott", Genre = "Action", Description = "A powerful Roman general named Maximus is betrayed and his family murdered by the corrupt son of the Emperor. Sold into slavery and forced to become a gladiator, Maximus rises through the ranks of the arena, seeking vengeance against the man who destroyed his life while the fate of Rome hangs in the balance." },
            new Movie { Id = 23, ReleaseDate = new DateOnly(1985, 7, 3), Length = 116, Title = "Back to the Future", Director = "Robert Zemeckis", Genre = "Sci-Fi", Description = "A teenager is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his eccentric scientist friend. Marty McFly must ensure his teenage parents fall in love in 1955 or risk erasing his own existence, all while finding a way back to 1985 in this perfect blend of comedy and science fiction." },
            new Movie { Id = 24, ReleaseDate = new DateOnly(1995, 9, 22), Length = 127, Title = "Se7en", Director = "David Fincher", Genre = "Crime", Description = "Two detectives, a veteran and a rookie, hunt a serial killer who uses the seven deadly sins as his modus operandi. As Detective Somerset prepares for retirement and Detective Mills begins his career, they navigate a dark urban landscape pursuing a methodical killer whose crimes grow increasingly disturbing, leading to a devastating finale." },
            new Movie { Id = 25, ReleaseDate = new DateOnly(2001, 7, 20), Length = 125, Title = "Harry Potter and the Sorcerer's Stone", Director = "Chris Columbus", Genre = "Fantasy", Description = "An orphaned boy discovers on his eleventh birthday that he is actually a wizard and has been accepted to Hogwarts School of Witchcraft and Wizardry. Harry Potter learns about his famous past, makes lifelong friends, and confronts the dark wizard who murdered his parents in this magical adventure based on J.K. Rowling's beloved novel." },
            new Movie { Id = 26, ReleaseDate = new DateOnly(2019, 4, 26), Length = 181, Title = "Avengers: Endgame", Director = "Russo Brothers", Genre = "Superhero", Description = "After the devastating events of Infinity War where Thanos eliminated half of all life in the universe, the remaining Avengers assemble one final time to undo the Mad Titan's actions. Through a dangerous time heist, Earth's mightiest heroes attempt to restore those they've lost in this epic conclusion to the Infinity Saga." },
            new Movie { Id = 27, ReleaseDate = new DateOnly(2008, 6, 13), Length = 126, Title = "Iron Man", Director = "Jon Favreau", Genre = "Superhero", Description = "Billionaire industrialist Tony Stark is captured by terrorists and forced to build a devastating weapon. Instead, he creates a high-tech suit of armor to escape captivity. Returning home, Stark refines his invention and transforms himself into the armored superhero Iron Man, launching the Marvel Cinematic Universe." },
            new Movie { Id = 28, ReleaseDate = new DateOnly(2016, 2, 12), Length = 108, Title = "Deadpool", Director = "Tim Miller", Genre = "Action", Description = "A former Special Forces operative turned mercenary undergoes a rogue experiment that leaves him with accelerated healing powers and a twisted sense of humor. Wade Wilson adopts the alter ego Deadpool and hunts down the man who nearly destroyed his life, all while breaking the fourth wall in this irreverent superhero film." },
            new Movie { Id = 29, ReleaseDate = new DateOnly(2017, 3, 3), Length = 137, Title = "Logan", Director = "James Mangold", Genre = "Action", Description = "In a near future where mutants are nearly extinct, an aging and weary Logan lives a quiet life caring for a dying Professor X. When a young mutant girl with abilities similar to his own appears, Logan must protect her from dark forces hunting her down in this emotional and violent conclusion to Wolverine's story." },
            new Movie { Id = 30, ReleaseDate = new DateOnly(2023, 7, 21), Length = 114, Title = "Oppenheimer", Director = "Christopher Nolan", Genre = "Biography", Description = "The story of American scientist J. Robert Oppenheimer and his role in developing the atomic bomb during World War II. This biographical thriller explores the moral implications of creating such devastating weapons, Oppenheimer's security clearance hearing, and the personal cost of being known as the father of the atomic bomb." }
        );

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1,  Name = "admin", Hash = "f+sMn7oo7y6hh5wWoFvHwWIAAG07t07OUNEl5BQq8Q30FkVUdC6K40tLpVauz7jWBnlHwtphR6+/KwUmqTWbSw==", Salt = "kFaXeaDXHPtNRDPZd517LfrKlf6omU7XZ4E7lCxjFSDJLFhcgS+CV35kxMAJRGUxhRyAnGp9z676NK5Q7PQHYIV5lbYkrZ5X08CsF9ncRWQ8BoQaH4rTHmPrjtfCDzQF0HywBPGMbzsHqLBEDs3pCv+Bh9pt+7JAYg0RaH2QQA8=", Role = Roles.ADMIN },
            // Password = admin

            new User { Id = 2, Name = "testuser1", Hash = "Z9/FAsGUmTudmYKgEPaD4XLHnboaNWWAdHITiUN+/C4cIRsjTfpAw33hdKApFdS6gUAU9fhzIqNsr3k9cHJqaQ==", Salt = "2pJ8FMxMKYxuX6Wqsl5pXO2hh6HkHVXgW5TFFofNWQuwnENOYmXJG6aTUQDauKej+DDdx0JLI+QFwHWzQsUpetoR1+RBke0iAST3PuXjoovNerNdKqSBmxHOv4RzMMgVOV+D2pTIZxFgwmxFMRKTfj1xJPYBiDHFxftzPT+2ZtQ=", Role = Roles.AUTHENTICATEDUSER },
            // Password = testuser

            new User { Id = 3, Name = "testuser2", Hash = "TZb0yh2zN0xpqAqDFThoIwgoPLWhMrReKrRerEMmmLlByEWcwdd+tPwxebveCPhsAG7ImPm7gYdykROrKwruGw==", Salt = "ihJ5oN69oDLnSLVM4nMdAtfafKXeCTMT3+sYQHy1gi8z/XNwGM3ogE9kbeFZfZf91KMvqt+FDgtvhp0+T686Z74TeYA795VYxi7y72nTbceFBqBYiPzznh0udgzGz5eJRvW8ukF4tXHtK2ViIbydw3DGxthzUm2hcwK1N/xaHx4=", Role = Roles.AUTHENTICATEDUSER },
            // Password = testuser

            new User { Id = 4, Name = "testuser3", Hash = "LHlz0gscyF/ap2Ev0Ffzr2hxg0aVzqym7GFflmbvU9kQ02dfpqcjy2wyUtZebPOYaSxYK2S7UivHukKqYDwYcw==", Salt = "2QpGjfuIGtUBxUNAvpBJfaoQA8RHqSpq4PlyyfVkUbtauvcjI7E07ZckIbvjI9lMYqox/5VQDM3gTMKL5v07EVEF2RK09zcEuGyAzkpLldGd+ENDSjBImSWMMg6TMBvAHfBfzghe4+G4laKa7U3646seoawMnpoXLj2qHMSGT08=", Role = Roles.AUTHENTICATEDUSER }
            // Password = testuser
        );

        // Seed Posts
        modelBuilder.Entity<Post>().HasData(
            // Movie 2 - The Godfather (2 posts)
            new Post { Id = 1, Title = "A Masterpiece", Username = "admin", MovieId = 2, Content = "The Godfather sets the standard for crime dramas. Brando's performance is unforgettable." },
            new Post { Id = 2, Title = "Timeless Classic", Username = "testuser1", MovieId = 2, Content = "Every scene is perfectly crafted. This is cinema at its finest." },

            // Movie 3 - The Dark Knight (4 posts)
            new Post { Id = 3, Title = "Heath Ledger's Legacy", Username = "testuser2", MovieId = 3, Content = "Heath Ledger's Joker is one of the greatest villain performances ever captured on film." },
            new Post { Id = 4, Title = "Best Superhero Movie", Username = "testuser3", MovieId = 3, Content = "This transcends the superhero genre. It's a crime thriller first, superhero movie second." },
            new Post { Id = 5, Title = "Dark and Gripping", Username = "admin", MovieId = 3, Content = "Nolan created something truly special here. The tension never lets up." },
            new Post { Id = 6, Title = "Perfect Sequel", Username = "testuser1", MovieId = 3, Content = "Improves on Batman Begins in every way. Absolutely phenomenal." },

            // Movie 4 - Forrest Gump (3 posts)
            new Post { Id = 7, Title = "Emotional Journey", Username = "testuser2", MovieId = 4, Content = "This movie makes me cry every single time. Tom Hanks is perfect." },
            new Post { Id = 8, Title = "Life Lessons", Username = "testuser3", MovieId = 4, Content = "So many quotable moments and beautiful life lessons throughout." },
            new Post { Id = 9, Title = "Heartwarming", Username = "admin", MovieId = 4, Content = "A beautiful story about perseverance and kindness." },

            // Movie 5 - Inception (5 posts)
            new Post { Id = 10, Title = "Mind-Bending Masterpiece", Username = "testuser1", MovieId = 5, Content = "Nolan at his best. The layers of dreams within dreams are brilliantly executed." },
            new Post { Id = 11, Title = "Rewatchability", Username = "testuser2", MovieId = 5, Content = "I've seen this five times and still catch new details every viewing." },
            new Post { Id = 12, Title = "That Ending Though", Username = "testuser3", MovieId = 5, Content = "Still debating whether the top fell or not. Genius ambiguity." },
            new Post { Id = 13, Title = "Visual Spectacle", Username = "admin", MovieId = 5, Content = "The hallway fight scene alone is worth the price of admission." },
            new Post { Id = 14, Title = "Hans Zimmer Score", Username = "testuser1", MovieId = 5, Content = "The soundtrack perfectly complements the intensity of the film." },

            // Movie 6 - The Matrix (3 posts)
            new Post { Id = 15, Title = "Revolutionary", Username = "testuser2", MovieId = 6, Content = "Changed action movies forever. The bullet-time effects were groundbreaking." },
            new Post { Id = 16, Title = "Philosophical Depth", Username = "testuser3", MovieId = 6, Content = "So much deeper than just an action film. Makes you question reality." },
            new Post { Id = 17, Title = "Still Holds Up", Username = "admin", MovieId = 6, Content = "Watched it again recently and it's still as impressive as 1999." },

            // Movie 7 - Fight Club (2 posts)
            new Post { Id = 18, Title = "Twisted and Brilliant", Username = "testuser1", MovieId = 7, Content = "The twist still gets me even on rewatches. Fincher is a master." },
            new Post { Id = 19, Title = "Commentary on Consumerism", Username = "testuser2", MovieId = 7, Content = "The social commentary is even more relevant today than when it was released." },

            // Movie 8 - LOTR: Fellowship (4 posts)
            new Post { Id = 20, Title = "Epic Beginning", Username = "testuser3", MovieId = 8, Content = "The start of an incredible journey. Peter Jackson brought Middle-earth to life." },
            new Post { Id = 21, Title = "Perfect Adaptation", Username = "admin", MovieId = 8, Content = "Tolkien would be proud. This is how you adapt a beloved book." },
            new Post { Id = 22, Title = "The Fellowship", Username = "testuser1", MovieId = 8, Content = "The casting is absolutely perfect. Every character feels authentic." },
            new Post { Id = 23, Title = "Howard Shore's Music", Username = "testuser2", MovieId = 8, Content = "The soundtrack is hauntingly beautiful and elevates every scene." },

            // Movie 9 - LOTR: Two Towers (2 posts)
            new Post { Id = 24, Title = "Helm's Deep", Username = "testuser3", MovieId = 9, Content = "The Battle of Helm's Deep is one of the greatest battle sequences ever filmed." },
            new Post { Id = 25, Title = "Gollum's Performance", Username = "admin", MovieId = 9, Content = "Andy Serkis deserved an Oscar. Gollum is incredibly realistic and tragic." },

            // Movie 10 - LOTR: Return of the King (5 posts)
            new Post { Id = 26, Title = "Perfect Conclusion", Username = "testuser1", MovieId = 10, Content = "The ending had me in tears. What a way to finish the trilogy." },
            new Post { Id = 27, Title = "11 Oscars!", Username = "testuser2", MovieId = 10, Content = "Swept the Academy Awards and deserved every single one." },
            new Post { Id = 28, Title = "Epic Scale", Username = "testuser3", MovieId = 10, Content = "The Battle of Pelennor Fields is breathtaking in scope and execution." },
            new Post { Id = 29, Title = "Emotional Payoff", Username = "admin", MovieId = 10, Content = "After three films, seeing everyone's journey conclude is so satisfying." },
            new Post { Id = 30, Title = "Best of the Trilogy", Username = "testuser1", MovieId = 10, Content = "While all three are masterpieces, this one edges out the others for me." },

            // Movie 11 - Jurassic Park (3 posts)
            new Post { Id = 31, Title = "Childhood Favorite", Username = "testuser2", MovieId = 11, Content = "This movie made me fall in love with dinosaurs. Still magical today." },
            new Post { Id = 32, Title = "Spielberg Magic", Username = "testuser3", MovieId = 11, Content = "The T-Rex escape scene is pure cinematic perfection." },
            new Post { Id = 33, Title = "Practical Effects", Username = "admin", MovieId = 11, Content = "The mix of animatronics and CGI still looks better than many modern films." },

            // Movie 12 - Star Wars: A New Hope (4 posts)
            new Post { Id = 34, Title = "Started It All", Username = "testuser1", MovieId = 12, Content = "Changed cinema forever. The beginning of an incredible saga." },
            new Post { Id = 35, Title = "Iconic Characters", Username = "testuser2", MovieId = 12, Content = "Han, Luke, Leia - these characters defined a generation." },
            new Post { Id = 36, Title = "World-Building", Username = "testuser3", MovieId = 12, Content = "Lucas created such a rich and detailed universe. Amazing vision." },
            new Post { Id = 37, Title = "The Force", Username = "admin", MovieId = 12, Content = "The mythology and philosophy behind the Force is fascinating." },

            // Movie 13 - Empire Strikes Back (3 posts)
            new Post { Id = 38, Title = "Best Star Wars Film", Username = "testuser1", MovieId = 13, Content = "Darker, more complex, and emotionally resonant. The peak of the saga." },
            new Post { Id = 39, Title = "I Am Your Father", Username = "testuser2", MovieId = 13, Content = "One of the greatest plot twists in cinema history. Still gives me chills." },
            new Post { Id = 40, Title = "Yoda's Wisdom", Username = "testuser3", MovieId = 13, Content = "The Dagobah scenes with Yoda are the heart of this film." },

            // Movie 14 - Return of the Jedi (2 posts)
            new Post { Id = 41, Title = "Satisfying Ending", Username = "admin", MovieId = 14, Content = "The redemption of Vader makes the entire original trilogy worthwhile." },
            new Post { Id = 42, Title = "Ewoks!", Username = "testuser1", MovieId = 14, Content = "Say what you will, but the Ewoks are adorable and fun." },

            // Movie 15 - Toy Story (3 posts)
            new Post { Id = 43, Title = "Pixar's First", Username = "testuser2", MovieId = 15, Content = "The film that launched Pixar's incredible run. Revolutionary animation." },
            new Post { Id = 44, Title = "Adults Love It Too", Username = "testuser3", MovieId = 15, Content = "Not just a kids movie. The themes of friendship and jealousy are universal." },
            new Post { Id = 45, Title = "Timeless Story", Username = "admin", MovieId = 15, Content = "My kids love this just as much as I did when it came out." },

            // Movie 16 - Finding Nemo (2 posts)
            new Post { Id = 46, Title = "Beautiful Underwater World", Username = "testuser1", MovieId = 16, Content = "The ocean environments are gorgeously rendered. Pixar outdid themselves." },
            new Post { Id = 47, Title = "Father-Son Story", Username = "testuser2", MovieId = 16, Content = "The relationship between Marlin and Nemo is so touching." },

            // Movie 17 - Titanic (4 posts)
            new Post { Id = 48, Title = "Epic Romance", Username = "testuser3", MovieId = 17, Content = "Jack and Rose's love story still makes me cry. Cameron's masterpiece." },
            new Post { Id = 49, Title = "Historical Accuracy", Username = "admin", MovieId = 17, Content = "The attention to detail in recreating the ship is incredible." },
            new Post { Id = 50, Title = "Celine Dion", Username = "testuser1", MovieId = 17, Content = "My Heart Will Go On is forever tied to this film. Iconic." },
            new Post { Id = 51, Title = "The Sinking", Username = "testuser2", MovieId = 17, Content = "The second half is intense and heartbreaking. Amazingly done." },

            // Movie 18 - Interstellar (5 posts)
            new Post { Id = 52, Title = "Space Epic", Username = "testuser3", MovieId = 18, Content = "Nolan's most ambitious film. The scale is mind-boggling." },
            new Post { Id = 53, Title = "Emotional Core", Username = "admin", MovieId = 18, Content = "The father-daughter relationship gives the sci-fi real heart." },
            new Post { Id = 54, Title = "Scientific Accuracy", Username = "testuser1", MovieId = 18, Content = "Kip Thorne's involvement shows. The black hole visualization is scientifically grounded." },
            new Post { Id = 55, Title = "That Docking Scene", Username = "testuser2", MovieId = 18, Content = "The tension during the docking sequence had me holding my breath." },
            new Post { Id = 56, Title = "Zimmer's Score Again", Username = "testuser3", MovieId = 18, Content = "Hans Zimmer creates another unforgettable soundtrack. The organ is perfect." },

            // Movie 19 - Goodfellas (3 posts)
            new Post { Id = 57, Title = "Scorsese's Best", Username = "admin", MovieId = 19, Content = "Peak Scorsese. The energy and pacing are unmatched." },
            new Post { Id = 58, Title = "Ray Liotta", Username = "testuser1", MovieId = 19, Content = "Liotta's narration pulls you into the mob life. Fantastic performance." },
            new Post { Id = 59, Title = "That Tracking Shot", Username = "testuser2", MovieId = 19, Content = "The Copacabana scene is one continuous shot of brilliance." },

            // Movie 20 - Silence of the Lambs (2 posts)
            new Post { Id = 60, Title = "Hopkins is Terrifying", Username = "testuser3", MovieId = 20, Content = "Anthony Hopkins makes Hannibal Lecter unforgettable with minimal screen time." },
            new Post { Id = 61, Title = "Psychological Thriller", Username = "admin", MovieId = 20, Content = "The tension between Clarice and Lecter is electric. Masterful direction." },

            // Movie 21 - Saving Private Ryan (4 posts)
            new Post { Id = 62, Title = "Opening Scene", Username = "testuser1", MovieId = 21, Content = "The D-Day landing is the most realistic war sequence ever filmed. Brutal and powerful." },
            new Post { Id = 63, Title = "Spielberg's War Film", Username = "testuser2", MovieId = 21, Content = "This changed how war films are made. Unflinching and honest." },
            new Post { Id = 64, Title = "Ensemble Cast", Username = "testuser3", MovieId = 21, Content = "Every soldier feels like a real person with a story. Great casting." },
            new Post { Id = 65, Title = "Emotional Sacrifice", Username = "admin", MovieId = 21, Content = "The ending gets me every time. Earn this." },

            // Movie 22 - Gladiator (3 posts)
            new Post { Id = 66, Title = "Are You Not Entertained?", Username = "testuser1", MovieId = 22, Content = "Crowe commands the screen. One of the best action performances." },
            new Post { Id = 67, Title = "Roman Spectacle", Username = "testuser2", MovieId = 22, Content = "Ridley Scott brings ancient Rome to life. The Colosseum battles are incredible." },
            new Post { Id = 68, Title = "Revenge Story", Username = "testuser3", MovieId = 22, Content = "Maximus' journey from general to slave to gladiator is gripping." },

            // Movie 23 - Back to the Future (2 posts)
            new Post { Id = 69, Title = "Perfect Entertainment", Username = "admin", MovieId = 23, Content = "One of the most fun movies ever made. Never gets old." },
            new Post { Id = 70, Title = "Time Travel Done Right", Username = "testuser1", MovieId = 23, Content = "The paradoxes and timeline are handled brilliantly. Smart and funny." },

            // Movie 24 - Se7en (3 posts)
            new Post { Id = 71, Title = "Dark Masterpiece", Username = "testuser2", MovieId = 24, Content = "Fincher's vision is so bleak and captivating. The atmosphere is oppressive." },
            new Post { Id = 72, Title = "What's in the Box?", Username = "testuser3", MovieId = 24, Content = "The ending is devastating. One of cinema's darkest conclusions." },
            new Post { Id = 73, Title = "Pitt and Freeman", Username = "admin", MovieId = 24, Content = "The chemistry between the two leads drives the film. Perfect casting." },

            // Movie 25 - Harry Potter (4 posts)
            new Post { Id = 74, Title = "Magical Beginning", Username = "testuser1", MovieId = 25, Content = "The perfect introduction to the wizarding world. Captured the magic of the books." },
            new Post { Id = 75, Title = "Childhood Wonder", Username = "testuser2", MovieId = 25, Content = "Seeing Hogwarts for the first time was pure magic. Columbus nailed it." },
            new Post { Id = 76, Title = "The Trio", Username = "testuser3", MovieId = 25, Content = "Daniel, Emma, and Rupert were perfectly cast. They grew up with these roles." },
            new Post { Id = 77, Title = "John Williams", Username = "admin", MovieId = 25, Content = "Hedwig's Theme is iconic. Williams created magic with the music." },

            // Movie 26 - Avengers Endgame (5 posts)
            new Post { Id = 78, Title = "Epic Conclusion", Username = "testuser1", MovieId = 26, Content = "The culmination of 22 films. The Russo Brothers stuck the landing." },
            new Post { Id = 79, Title = "Avengers Assemble", Username = "testuser2", MovieId = 26, Content = "That portals scene gave me chills. Pure comic book joy brought to life." },
            new Post { Id = 80, Title = "Emotional Goodbye", Username = "testuser3", MovieId = 26, Content = "Tony's sacrifice had the entire theater in tears. I love you 3000." },
            new Post { Id = 81, Title = "Time Heist", Username = "admin", MovieId = 26, Content = "Revisiting past MCU moments was brilliant fan service done right." },
            new Post { Id = 82, Title = "Three Hour Epic", Username = "testuser1", MovieId = 26, Content = "Didn't feel like three hours. Every minute earned its place." },

            // Movie 27 - Iron Man (3 posts)
            new Post { Id = 83, Title = "Started the MCU", Username = "testuser2", MovieId = 27, Content = "RDJ was born to play Tony Stark. This launched everything." },
            new Post { Id = 84, Title = "Practical Suit", Username = "testuser3", MovieId = 27, Content = "The Mark I suit built in a cave is still one of my favorite sequences." },
            new Post { Id = 85, Title = "I Am Iron Man", Username = "admin", MovieId = 27, Content = "That ending changed superhero movies forever. No secret identity." },

            // Movie 28 - Deadpool (2 posts)
            new Post { Id = 86, Title = "R-Rated Perfection", Username = "testuser1", MovieId = 28, Content = "Finally a faithful Deadpool. Reynolds fought for this and it shows." },
            new Post { Id = 87, Title = "Fourth Wall Breaking", Username = "testuser2", MovieId = 28, Content = "The meta humor is perfectly balanced. Hilarious from start to finish." },

            // Movie 29 - Logan (4 posts)
            new Post { Id = 88, Title = "Wolverine's Swan Song", Username = "testuser3", MovieId = 29, Content = "Hugh Jackman's final performance as Logan is heartbreaking and perfect." },
            new Post { Id = 89, Title = "Western Superhero Film", Username = "admin", MovieId = 29, Content = "Mangold crafted something unique. Feels like a Western first, superhero film second." },
            new Post { Id = 90, Title = "Dark and Brutal", Username = "testuser1", MovieId = 29, Content = "The R-rating allows the violence and emotion to hit harder. Raw and real." },
            new Post { Id = 91, Title = "Laura", Username = "testuser2", MovieId = 29, Content = "Dafne Keen steals scenes. The relationship between Logan and Laura is beautiful." },

            // Movie 30 - Oppenheimer (5 posts)
            new Post { Id = 92, Title = "Nolan's Masterwork", Username = "testuser3", MovieId = 30, Content = "A three-hour character study that never drags. Nolan's most mature film." },
            new Post { Id = 93, Title = "Cillian Murphy", Username = "admin", MovieId = 30, Content = "Murphy finally gets the lead role he deserves. His Oppenheimer is haunting." },
            new Post { Id = 94, Title = "Practical Effects", Username = "testuser1", MovieId = 30, Content = "Nolan created the Trinity test without CGI. The commitment to practical filmmaking is admirable." },
            new Post { Id = 95, Title = "Historical Drama", Username = "testuser2", MovieId = 30, Content = "The exploration of Oppenheimer's legacy and guilt is fascinating and relevant." },
            new Post { Id = 96, Title = "IMAX Experience", Username = "testuser3", MovieId = 30, Content = "This film demands to be seen in IMAX. The visuals and sound are overwhelming." }
        );

        // Seed Replies
        modelBuilder.Entity<Reply>().HasData(
            // Replies to Post 1 (3 replies)
            new Reply { Id = 1, Username = "testuser2", Content = "Absolutely agree. The character development is masterful.", ParentPostId = 1 },
            new Reply { Id = 2, Username = "testuser3", Content = "The opening scene sets the tone perfectly.", ParentPostId = 1 },
            new Reply { Id = 3, Username = "testuser1", Content = "Have you seen the director's cut? Even better.", ParentPostId = 1 },

            // Replies to Post 2 (1 reply)
            new Reply { Id = 4, Username = "admin", Content = "The cinematography is stunning throughout.", ParentPostId = 2 },

            // Replies to Post 3 (3 replies)
            new Reply { Id = 5, Username = "testuser1", Content = "His commitment to the role was incredible. RIP.", ParentPostId = 3 },
            new Reply { Id = 6, Username = "admin", Content = "The interrogation scene is pure acting brilliance.", ParentPostId = 3 },
            new Reply { Id = 7, Username = "testuser3", Content = "He disappeared into that role completely.", ParentPostId = 3 },

            // Replies to Post 5 (1 reply)
            new Reply { Id = 8, Username = "testuser2", Content = "The moral complexity elevates it above typical superhero fare.", ParentPostId = 5 },

            // Replies to Post 7 (3 replies)
            new Reply { Id = 9, Username = "admin", Content = "The bench scenes with Jenny break my heart.", ParentPostId = 7 },
            new Reply { Id = 10, Username = "testuser1", Content = "Hanks deserved every award for this performance.", ParentPostId = 7 },
            new Reply { Id = 11, Username = "testuser3", Content = "Life is like a box of chocolates...", ParentPostId = 7 },

            // Replies to Post 8 (1 reply)
            new Reply { Id = 12, Username = "admin", Content = "Run, Forrest, run! Never gets old.", ParentPostId = 8 },

            // Replies to Post 10 (3 replies)
            new Reply { Id = 13, Username = "testuser3", Content = "The architecture of dreams is fascinating.", ParentPostId = 10 },
            new Reply { Id = 14, Username = "admin", Content = "Each layer adds another dimension to the story.", ParentPostId = 10 },
            new Reply { Id = 15, Username = "testuser2", Content = "The van sequence with different time scales is genius.", ParentPostId = 10 },

            // Replies to Post 12 (1 reply)
            new Reply { Id = 16, Username = "admin", Content = "I choose to believe it keeps spinning.", ParentPostId = 12 },

            // Replies to Post 14 (3 replies)
            new Reply { Id = 17, Username = "testuser2", Content = "The BRAAAM sound became iconic.", ParentPostId = 14 },
            new Reply { Id = 18, Username = "testuser3", Content = "Time by Hans Zimmer is incredible.", ParentPostId = 14 },
            new Reply { Id = 19, Username = "admin", Content = "Zimmer and Nolan are a perfect pairing.", ParentPostId = 14 },

            // Replies to Post 15 (1 reply)
            new Reply { Id = 20, Username = "testuser1", Content = "The slow-mo sequences were mind-blowing in 1999.", ParentPostId = 15 },

            // Replies to Post 17 (3 replies)
            new Reply { Id = 21, Username = "testuser1", Content = "The effects still look great today.", ParentPostId = 17 },
            new Reply { Id = 22, Username = "testuser2", Content = "I watch it every few years and it never disappoints.", ParentPostId = 17 },
            new Reply { Id = 23, Username = "testuser3", Content = "The green tint aesthetic is so iconic.", ParentPostId = 17 },

            // Replies to Post 18 (1 reply)
            new Reply { Id = 24, Username = "admin", Content = "The reveal is so well-constructed.", ParentPostId = 18 },

            // Replies to Post 20 (3 replies)
            new Reply { Id = 25, Username = "testuser1", Content = "The Shire looks exactly as I imagined reading the books.", ParentPostId = 20 },
            new Reply { Id = 26, Username = "testuser2", Content = "The practical effects and sets are incredible.", ParentPostId = 20 },
            new Reply { Id = 27, Username = "admin", Content = "New Zealand never looked so beautiful.", ParentPostId = 20 },

            // Replies to Post 22 (1 reply)
            new Reply { Id = 28, Username = "testuser3", Content = "Ian McKellen and Viggo Mortensen are standouts.", ParentPostId = 22 },

            // Replies to Post 24 (3 replies)
            new Reply { Id = 29, Username = "testuser1", Content = "The practical effects for that battle are astounding.", ParentPostId = 24 },
            new Reply { Id = 30, Username = "testuser2", Content = "The siege towers and ladders felt so real.", ParentPostId = 24 },
            new Reply { Id = 31, Username = "admin", Content = "Theoden's speech still gives me goosebumps.", ParentPostId = 24 },

            // Replies to Post 26 (1 reply)
            new Reply { Id = 32, Username = "testuser3", Content = "I wasn't ready for that many endings, but all were necessary.", ParentPostId = 26 },

            // Replies to Post 27 (3 replies)
            new Reply { Id = 33, Username = "testuser3", Content = "Tied with Ben-Hur and Titanic. Legendary.", ParentPostId = 27 },
            new Reply { Id = 34, Username = "admin", Content = "Best Picture was well-deserved.", ParentPostId = 27 },
            new Reply { Id = 35, Username = "testuser1", Content = "A clean sweep at the Oscars is so rare.", ParentPostId = 27 },

            // Replies to Post 29 (1 reply)
            new Reply { Id = 36, Username = "testuser2", Content = "The Grey Havens scene destroys me every time.", ParentPostId = 29 },

            // Replies to Post 31 (3 replies)
            new Reply { Id = 37, Username = "testuser1", Content = "The T-Rex reveal with the water ripples is iconic.", ParentPostId = 31 },
            new Reply { Id = 38, Username = "admin", Content = "This sparked my interest in paleontology.", ParentPostId = 31 },
            new Reply { Id = 39, Username = "testuser3", Content = "Welcome to Jurassic Park - chills!", ParentPostId = 31 },

            // Replies to Post 33 (1 reply)
            new Reply { Id = 40, Username = "testuser1", Content = "The combination was revolutionary for the time.", ParentPostId = 33 },

            // Replies to Post 34 (3 replies)
            new Reply { Id = 41, Username = "testuser3", Content = "The cultural impact cannot be overstated.", ParentPostId = 34 },
            new Reply { Id = 42, Username = "admin", Content = "It created modern blockbuster filmmaking.", ParentPostId = 34 },
            new Reply { Id = 43, Username = "testuser2", Content = "May the Force be with you - timeless.", ParentPostId = 34 },

            // Replies to Post 36 (1 reply)
            new Reply { Id = 44, Username = "admin", Content = "The cantina scene alone shows the depth of the universe.", ParentPostId = 36 },

            // Replies to Post 38 (3 replies)
            new Reply { Id = 45, Username = "testuser3", Content = "Kershner brought a different tone that elevated it.", ParentPostId = 38 },
            new Reply { Id = 46, Username = "admin", Content = "The Hoth battle is incredible.", ParentPostId = 38 },
            new Reply { Id = 47, Username = "testuser2", Content = "Cloud City is such a great setting.", ParentPostId = 38 },

            // Replies to Post 39 (1 reply)
            new Reply { Id = 48, Username = "admin", Content = "No one saw it coming. Perfect execution.", ParentPostId = 39 },

            // Replies to Post 41 (3 replies)
            new Reply { Id = 49, Username = "testuser2", Content = "The throne room confrontation is powerful.", ParentPostId = 41 },
            new Reply { Id = 50, Username = "testuser3", Content = "Vader's turn back to the light is earned.", ParentPostId = 41 },
            new Reply { Id = 51, Username = "testuser1", Content = "I never cry at movies, but that scene gets me.", ParentPostId = 41 },

            // Replies to Post 43 (1 reply)
            new Reply { Id = 52, Username = "admin", Content = "Who knew CGI could be this charming?", ParentPostId = 43 },

            // Replies to Post 45 (3 replies)
            new Reply { Id = 53, Username = "testuser1", Content = "Randy Newman's music is perfect.", ParentPostId = 45 },
            new Reply { Id = 54, Username = "testuser2", Content = "You've Got a Friend in Me makes me nostalgic.", ParentPostId = 45 },
            new Reply { Id = 55, Username = "admin", Content = "Watching it with kids adds a new dimension.", ParentPostId = 45 },

            // Replies to Post 47 (1 reply)
            new Reply { Id = 56, Username = "testuser3", Content = "Just keep swimming! That mantra helped me through tough times.", ParentPostId = 47 },

            // Replies to Post 48 (3 replies)
            new Reply { Id = 57, Username = "admin", Content = "DiCaprio and Winslet have incredible chemistry.", ParentPostId = 48 },
            new Reply { Id = 58, Username = "testuser1", Content = "I'll never let go, Jack - devastating.", ParentPostId = 48 },
            new Reply { Id = 59, Username = "testuser2", Content = "The king of the world scene is iconic.", ParentPostId = 48 },

            // Replies to Post 50 (1 reply)
            new Reply { Id = 60, Username = "testuser3", Content = "That song will forever be associated with this film.", ParentPostId = 50 },

            // Replies to Post 52 (3 replies)
            new Reply { Id = 61, Username = "admin", Content = "The visuals are stunning on IMAX.", ParentPostId = 52 },
            new Reply { Id = 62, Username = "testuser1", Content = "Miller's planet with the time dilation is terrifying.", ParentPostId = 52 },
            new Reply { Id = 63, Username = "testuser2", Content = "The wormhole scene is visually incredible.", ParentPostId = 52 },

            // Replies to Post 54 (1 reply)
            new Reply { Id = 64, Username = "testuser3", Content = "The tesseract scene bends my mind.", ParentPostId = 54 },

            // Replies to Post 56 (3 replies)
            new Reply { Id = 65, Username = "admin", Content = "No Time for Caution during the docking is perfection.", ParentPostId = 56 },
            new Reply { Id = 66, Username = "testuser1", Content = "The organ creates such an otherworldly feel.", ParentPostId = 56 },
            new Reply { Id = 67, Username = "testuser2", Content = "Zimmer outdid himself with this score.", ParentPostId = 56 },

            // Replies to Post 57 (1 reply)
            new Reply { Id = 68, Username = "testuser2", Content = "The editing and pacing are masterful.", ParentPostId = 57 },

            // Replies to Post 59 (3 replies)
            new Reply { Id = 69, Username = "testuser3", Content = "That shot is studied in film schools.", ParentPostId = 59 },
            new Reply { Id = 70, Username = "admin", Content = "The precision required for that is insane.", ParentPostId = 59 },
            new Reply { Id = 71, Username = "testuser1", Content = "Pure cinema magic in one take.", ParentPostId = 59 },

            // Replies to Post 60 (1 reply)
            new Reply { Id = 72, Username = "admin", Content = "The fava beans line is chilling.", ParentPostId = 60 },

            // Replies to Post 62 (3 replies)
            new Reply { Id = 73, Username = "testuser2", Content = "Changed my understanding of WWII films.", ParentPostId = 62 },
            new Reply { Id = 74, Username = "testuser3", Content = "The sound design makes you feel like you're there.", ParentPostId = 62 },
            new Reply { Id = 75, Username = "admin", Content = "I had to pause and collect myself after that scene.", ParentPostId = 62 },

            // Replies to Post 64 (1 reply)
            new Reply { Id = 76, Username = "testuser1", Content = "Tom Hanks leads an incredible cast.", ParentPostId = 64 },

            // Replies to Post 66 (3 replies)
            new Reply { Id = 77, Username = "admin", Content = "My name is Maximus Decimus Meridius...", ParentPostId = 66 },
            new Reply { Id = 78, Username = "testuser2", Content = "Crowe's intensity is palpable in every scene.", ParentPostId = 66 },
            new Reply { Id = 79, Username = "testuser3", Content = "The physicality he brought to the role was impressive.", ParentPostId = 66 },

            // Replies to Post 68 (1 reply)
            new Reply { Id = 80, Username = "admin", Content = "The fallen leaves at the beginning foreshadow his journey.", ParentPostId = 68 },

            // Replies to Post 70 (3 replies)
            new Reply { Id = 81, Username = "testuser2", Content = "The clockwork precision of the plot is remarkable.", ParentPostId = 70 },
            new Reply { Id = 82, Username = "testuser3", Content = "Every rewatch reveals new details.", ParentPostId = 70 },
            new Reply { Id = 83, Username = "admin", Content = "The screenplay is tight and clever.", ParentPostId = 70 },

            // Replies to Post 71 (1 reply)
            new Reply { Id = 84, Username = "testuser1", Content = "The rain-soaked aesthetic is perfect.", ParentPostId = 71 },

            // Replies to Post 72 (3 replies)
            new Reply { Id = 85, Username = "admin", Content = "That final scene haunts me.", ParentPostId = 72 },
            new Reply { Id = 86, Username = "testuser1", Content = "Brad Pitt's reaction is so raw.", ParentPostId = 72 },
            new Reply { Id = 87, Username = "testuser2", Content = "Ernest Hemingway said it best in the film.", ParentPostId = 72 },

            // Replies to Post 74 (1 reply)
            new Reply { Id = 88, Username = "testuser3", Content = "Seeing the Great Hall for the first time was magical.", ParentPostId = 74 },

            // Replies to Post 76 (3 replies)
            new Reply { Id = 89, Username = "admin", Content = "Watching them grow up on screen was special.", ParentPostId = 76 },
            new Reply { Id = 90, Username = "testuser1", Content = "They embodied their characters perfectly.", ParentPostId = 76 },
            new Reply { Id = 91, Username = "testuser2", Content = "The trio's friendship feels genuine.", ParentPostId = 76 },

            // Replies to Post 78 (1 reply)
            new Reply { Id = 92, Username = "testuser2", Content = "11 years of storytelling paid off beautifully.", ParentPostId = 78 },

            // Replies to Post 79 (3 replies)
            new Reply { Id = 93, Username = "testuser3", Content = "I stood up in the theater. Couldn't help myself.", ParentPostId = 79 },
            new Reply { Id = 94, Username = "admin", Content = "That moment justified every previous film.", ParentPostId = 79 },
            new Reply { Id = 95, Username = "testuser1", Content = "On your left - perfect callback.", ParentPostId = 79 },

            // Replies to Post 80 (1 reply)
            new Reply { Id = 96, Username = "admin", Content = "RDJ gave everything to that role.", ParentPostId = 80 },

            // Replies to Post 82 (3 replies)
            new Reply { Id = 97, Username = "testuser2", Content = "Not a wasted moment in the entire runtime.", ParentPostId = 82 },
            new Reply { Id = 98, Username = "testuser3", Content = "The pacing is masterful.", ParentPostId = 82 },
            new Reply { Id = 99, Username = "admin", Content = "I could watch another hour easily.", ParentPostId = 82 },

            // Replies to Post 83 (1 reply)
            new Reply { Id = 100, Username = "testuser3", Content = "No one else could have played Tony Stark.", ParentPostId = 83 },

            // Replies to Post 85 (3 replies)
            new Reply { Id = 101, Username = "testuser1", Content = "That press conference was a game-changer.", ParentPostId = 85 },
            new Reply { Id = 102, Username = "testuser2", Content = "The public identity made MCU unique.", ParentPostId = 85 },
            new Reply { Id = 103, Username = "testuser3", Content = "RDJ ad-libbed that line. Perfect.", ParentPostId = 85 },

            // Replies to Post 87 (1 reply)
            new Reply { Id = 104, Username = "testuser3", Content = "The opening credits scene is genius.", ParentPostId = 87 },

            // Replies to Post 88 (3 replies)
            new Reply { Id = 105, Username = "admin", Content = "17 years as Wolverine. What a legacy.", ParentPostId = 88 },
            new Reply { Id = 106, Username = "testuser1", Content = "The rawness of his performance is stunning.", ParentPostId = 88 },
            new Reply { Id = 107, Username = "testuser2", Content = "He went out on the highest note possible.", ParentPostId = 88 },

            // Replies to Post 90 (1 reply)
            new Reply { Id = 108, Username = "testuser3", Content = "Finally showing what Wolverine can really do.", ParentPostId = 90 },

            // Replies to Post 92 (3 replies)
            new Reply { Id = 109, Username = "admin", Content = "The structure with three timelines is brilliant.", ParentPostId = 92 },
            new Reply { Id = 110, Username = "testuser1", Content = "The black and white sequences are powerful.", ParentPostId = 92 },
            new Reply { Id = 111, Username = "testuser2", Content = "Nolan's restraint makes it more impactful.", ParentPostId = 92 },

            // Replies to Post 94 (1 reply)
            new Reply { Id = 112, Username = "admin", Content = "The commitment to in-camera effects is admirable.", ParentPostId = 94 },

            // Replies to Post 96 (3 replies)
            new Reply { Id = 113, Username = "admin", Content = "The sound design in IMAX is overwhelming.", ParentPostId = 96 },
            new Reply { Id = 114, Username = "testuser1", Content = "70mm film makes such a difference.", ParentPostId = 96 },
            new Reply { Id = 115, Username = "testuser2", Content = "This is Nolan's most IMAX-worthy film.", ParentPostId = 96 }
        );
    }
}