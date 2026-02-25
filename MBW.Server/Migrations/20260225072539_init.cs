using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MBW.Server.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentPostId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Genre", "Length", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Andy Dufresne, a quiet and analytical banker, is sentenced to life in Shawshank prison for the murder of his wife and her lover, a crime he steadfastly claims he did not commit. Thrust into a brutal and dehumanizing world ruled by routine, corruption, and violence, Andy initially struggles to survive the crushing weight of confinement. The prison is governed by rigid authority under Warden Norton and enforced by the sadistic Captain Hadley, men who cloak cruelty in righteousness. Amid this bleak environment, Andy forms an unlikely friendship with Ellis “Red” Redding, a seasoned inmate who knows how to obtain contraband and navigate prison politics. Through Red’s perspective, we witness Andy’s quiet resilience and refusal to surrender hope. Rather than allowing prison to define him, Andy slowly reshapes his world. He uses his financial expertise to assist the guards with taxes and later manages the warden’s illicit money-laundering operation, earning protection while secretly plotting his future. One of his most transformative acts is revitalizing the prison library, turning it into a sanctuary of knowledge and imagination for the inmates. These small rebellions against despair symbolize the power of hope as resistance. Andy’s famous belief that “hope is a good thing” becomes the emotional backbone of the story. Years pass, and the monotony of incarceration erodes many men’s spirits, yet Andy continues working patiently toward a hidden goal. When a fellow inmate reveals evidence that could prove Andy’s innocence, the warden suppresses the truth to protect his own interests, reinforcing the film’s themes of institutional corruption and moral hypocrisy. Realizing that justice will never come from the system, Andy executes a meticulously planned escape that he has been preparing for nearly two decades. His flight through a thunderstorm into freedom becomes one of cinema’s most triumphant moments. The film ultimately explores hope, friendship, perseverance, and the quiet power of inner freedom. It asks whether a person’s spirit can remain unbroken even when their body is confined. Through Red’s eventual redemption and reunion with Andy, the story closes on a note of liberation and renewal, proving that hope can endure even in the darkest places.", "Frank Darabont", "Drama", 142, new DateOnly(1994, 10, 14), "The Shawshank Redemption" },
                    { 2, "The story begins at the wedding of Connie Corleone, where family patriarch Vito Corleone presides over celebrations while quietly conducting the business of his powerful crime empire. Known as “The Godfather,” Vito commands loyalty through a mixture of generosity, calculated violence, and old-world honor. When rival families attempt to expand into the narcotics trade, Vito refuses, believing drugs will corrupt both the business and his political alliances. His decision triggers a brutal power struggle that reshapes the criminal underworld of New York. After Vito survives an assassination attempt, his youngest son Michael—once determined to live outside the family business—is pulled into the escalating conflict. Initially portrayed as a decorated war hero and moral outsider, Michael gradually reveals a cold strategic brilliance. In a pivotal moment, he assassinates a rival mob boss and a corrupt police captain, crossing a line from which there is no return. Forced into exile in Sicily, Michael experiences love and loss, deepening his transformation. Upon returning to America after his father’s death, Michael assumes control of the Corleone empire. What follows is a chilling consolidation of power, culminating in a montage of coordinated assassinations carried out during his nephew’s baptism. The sacred ritual contrasts starkly with the violence unfolding simultaneously, underscoring the film’s central theme: the collision of family, faith, and moral decay. Michael sacrifices innocence, love, and ultimately his own soul to protect the family legacy. The film examines power, loyalty, tradition, and the corrosive nature of ambition. It portrays crime not as chaos, but as an organized system bound by codes and betrayals. By the end, Michael is no longer the reluctant son but a hardened leader, closing the door—both literally and symbolically—on his former life. The transformation is complete, and the cost is immeasurable.", "Francis Ford Coppola", "Crime", 175, new DateOnly(1972, 3, 24), "The Godfather" },
                    { 3, "Gotham City teeters between hope and chaos as Batman works alongside Lieutenant Jim Gordon and District Attorney Harvey Dent to dismantle organized crime. For the first time, the city glimpses the possibility of lasting justice. But that fragile hope is shattered by the arrival of the Joker, a chaotic anarchist who seeks not money or power, but psychological collapse. Unlike traditional villains, the Joker thrives on unpredictability, exposing the moral weaknesses of both criminals and heroes alike. As the Joker orchestrates elaborate acts of terror, he challenges Batman’s belief in order and control. Each scheme escalates in brutality, from public assassinations to moral dilemmas that force citizens to choose who lives and dies. Harvey Dent, Gotham’s “White Knight,” becomes the emotional center of the conflict. When tragedy strikes and Dent descends into vengeance as Two-Face, the film explores how even the most virtuous individuals can fall under immense pressure. Batman is forced to confront the limits of his own moral code. Refusing to kill the Joker, he instead chooses to bear the blame for Dent’s crimes, sacrificing his reputation to preserve Gotham’s hope. The film delves into themes of chaos versus order, heroism versus vigilantism, and the heavy burden of sacrifice. It questions whether true justice can exist in a broken system. The Joker’s philosophy—that anyone can be corrupted—clashes with Batman’s unwavering resolve. In the end, Batman becomes the “Dark Knight,” a silent guardian willing to be hated for the greater good. The story is not just about a hero fighting a villain, but about the fragile line between righteousness and ruin.", "Christopher Nolan", "Action", 152, new DateOnly(2008, 7, 18), "The Dark Knight" },
                    { 4, "Forrest Gump, a kind-hearted man with a low IQ and boundless sincerity, recounts his extraordinary life story while sitting on a bench waiting for a bus. Despite being told he is limited, Forrest approaches life with unwavering optimism and honesty. His journey unfolds against the backdrop of major American historical events, from the Vietnam War to the civil rights movement and the rise of modern technology. Through chance and perseverance, Forrest finds himself at the center of moments that shape a generation. From becoming a football star to serving heroically in Vietnam, Forrest stumbles into success not through ambition but through loyalty and determination. His friendship with Bubba leads him to build a shrimping empire, fulfilling a promise made during wartime. His complicated love for Jenny, however, remains the emotional thread of the film. Jenny represents freedom and turbulence, constantly drifting in and out of Forrest’s life as she searches for meaning. The film explores themes of destiny, innocence, and unconditional love. Forrest’s famous observation that “life is like a box of chocolates” encapsulates the unpredictability of existence. While others chase wealth, fame, or rebellion, Forrest simply keeps moving forward, embodying resilience without cynicism. His sincerity becomes his strength in a world often driven by ego. Ultimately, the story suggests that greatness can arise from humility and kindness. Through triumph and heartbreak, Forrest remains steadfast, proving that a simple heart can endure extraordinary circumstances.", "Robert Zemeckis", "Drama", 154, new DateOnly(1994, 7, 6), "Forrest Gump" },
                    { 5, "Dom Cobb is a skilled extractor, a thief who infiltrates the subconscious minds of his targets to steal valuable secrets hidden within dreams. Haunted by the memory of his late wife Mal and burdened by guilt, Cobb lives in exile, unable to return home to his children. When a powerful businessman offers him a chance at redemption, Cobb is tasked not with stealing an idea, but planting one—a process known as inception. To accomplish this nearly impossible mission, Cobb assembles a team of specialists capable of navigating layered dreamscapes. The plan unfolds across multiple levels of reality, each with its own rules, gravity shifts, and time dilation. As the team descends deeper into shared dreams, the boundaries between illusion and reality blur. Mal’s projection, born from Cobb’s subconscious, threatens to sabotage the mission at every turn, symbolizing unresolved grief and self-destruction. Action sequences unfold in parallel across dream layers, building tension as time moves at different speeds. The film explores themes of memory, guilt, perception, and the fragile nature of reality. It asks whether an idea can define a person and whether truth matters if a lie brings peace. Cobb’s ultimate goal is not wealth, but reunion with his children and liberation from his past. The ambiguous ending, centered on a spinning top, leaves viewers questioning what is real. Rather than providing clear answers, the film invites interpretation, reinforcing its meditation on subjective reality. Inception becomes not just a heist thriller, but a deeply personal journey about letting go and choosing to believe in hope.", "Christopher Nolan", "Sci-Fi", 148, new DateOnly(2010, 7, 16), "Inception" },
                    { 6, "Thomas Anderson lives a double life. By day he is an unremarkable office worker, but by night he becomes Neo, a hacker searching for answers to a question that has haunted him for years: What is the Matrix? His search leads him to Morpheus, a mysterious rebel leader who reveals a staggering truth — the world Neo knows is a simulated reality created by intelligent machines. Humanity has been enslaved, its bodies harvested for energy while minds remain trapped inside an artificial dream. Awakening into the real world is brutal and disorienting. Neo discovers that his perceived life was an illusion, and the true Earth is a desolate wasteland ravaged by war between humans and machines. Morpheus believes Neo is “The One,” a prophesied figure capable of bending the rules of the Matrix and freeing humanity. Skeptical yet curious, Neo begins intense training within simulated programs that teach him martial arts, weapon mastery, and the ability to manipulate digital physics. Inside the Matrix, agents led by the relentless Agent Smith maintain order by hunting rebels who unplug minds from the system. These agents are nearly unstoppable, embodying the rigid control of the machine world. As Neo confronts them, he struggles with self-doubt and fear. Betrayal from within the crew further underscores the seductive comfort of illusion, as one member chooses the blissful lie of the Matrix over the harsh truth of reality. The film explores profound philosophical themes: the nature of reality, free will versus determinism, and the power of belief. It questions whether comfort is preferable to truth and whether destiny is chosen or imposed. Neo’s ultimate acceptance of his identity allows him to transcend the rules of the Matrix, stopping bullets and seeing the code beneath reality itself. In doing so, he becomes a symbol of awakening and liberation. The story blends action with existential inquiry, suggesting that freedom begins with the courage to question the world around you.", "Lana Wachowski, Lilly Wachowski", "Sci-Fi", 136, new DateOnly(1999, 3, 31), "The Matrix" },
                    { 7, "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into something much more dangerous. As the narrator becomes increasingly consumed by the violent subculture, his grip on reality begins to slip in this dark commentary on consumerism and masculinity.", "David Fincher", "Drama", 139, new DateOnly(1999, 10, 15), "Fight Club" },
                    { 8, "A humble hobbit named Frodo Baggins inherits a powerful ring and must journey to Mount Doom to destroy it before the dark lord Sauron can reclaim it. He is joined by a fellowship of diverse heroes in this epic quest that will determine the fate of Middle-earth.", "Peter Jackson", "Fantasy", 178, new DateOnly(2001, 12, 19), "LOTR: Fellowship of the Ring" },
                    { 9, "The fellowship has been broken, and the quest continues on multiple fronts as Frodo and Sam press toward Mordor with the treacherous Gollum as their guide. Meanwhile, Aragorn, Legolas, and Gimli must defend the kingdom of Rohan against Saruman's massive orc army at Helm's Deep.", "Peter Jackson", "Fantasy", 179, new DateOnly(2002, 12, 18), "LOTR: The Two Towers" },
                    { 10, "The final battle for Middle-earth begins as Aragorn embraces his destiny as king, Gandalf leads the defense of Minas Tirith against Sauron's forces, and Frodo reaches the fires of Mount Doom. The culmination of the epic trilogy delivers emotional payoffs and spectacular warfare on an unprecedented scale.", "Peter Jackson", "Fantasy", 201, new DateOnly(2003, 12, 17), "LOTR: Return of the King" },
                    { 11, "Dinosaurs are brought back to life through genetic engineering for an ambitious theme park, but the prehistoric creatures break free when the park's security systems fail. A paleontologist, paleobotanist, and mathematician must survive the island while protecting two young children from the rampaging dinosaurs.", "Steven Spielberg", "Adventure", 127, new DateOnly(1993, 6, 11), "Jurassic Park" },
                    { 12, "A young farm boy named Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee, and two droids to rescue a rebel princess from the clutches of the evil Galactic Empire. His journey sets him on the path to becoming a legendary Jedi and ignites a galactic rebellion.", "George Lucas", "Sci-Fi", 121, new DateOnly(1977, 5, 25), "Star Wars: A New Hope" },
                    { 13, "The Empire launches a devastating attack on the Rebel Alliance, scattering the heroes across the galaxy. Luke seeks training from the ancient Jedi Master Yoda while Han and Leia evade Imperial forces, leading to shocking revelations about Luke's heritage and the true nature of the Force.", "Irvin Kershner", "Sci-Fi", 124, new DateOnly(1980, 5, 21), "The Empire Strikes Back" },
                    { 14, "Luke Skywalker confronts Darth Vader in a final showdown before the Emperor while the Rebel Alliance launches a desperate attack on the second Death Star. The fate of the galaxy hangs in the balance as father and son face each other, and the dark side's hold on Anakin Skywalker is finally tested.", "Richard Marquand", "Sci-Fi", 131, new DateOnly(1983, 5, 25), "Return of the Jedi" },
                    { 15, "Toys come to life when humans aren't around, and cowboy doll Woody faces an existential crisis when a high-tech space ranger named Buzz Lightyear threatens his position as Andy's favorite toy. Their rivalry turns to friendship as they must work together to escape the clutches of the toy-torturing neighbor, Sid.", "John Lasseter", "Animation", 81, new DateOnly(1995, 11, 22), "Toy Story" },
                    { 16, "An overprotective clownfish named Marlin embarks on a perilous journey across the ocean to find his son Nemo, who was captured by divers and taken to a dentist's aquarium in Sydney. Along the way, Marlin teams up with the forgetful but optimistic Dory in this heartwarming tale of parental love and letting go.", "Andrew Stanton", "Animation", 101, new DateOnly(2003, 5, 30), "Finding Nemo" },
                    { 17, "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic. As Jack and Rose's forbidden romance blossoms during the ship's maiden voyage, tragedy strikes when the vessel collides with an iceberg, turning their love story into a desperate fight for survival.", "James Cameron", "Romance", 195, new DateOnly(1997, 12, 19), "Titanic" },
                    { 18, "With Earth becoming uninhabitable, a team of astronauts travels through a wormhole near Saturn in search of a new home for humanity. Former pilot Joseph Cooper must leave his children behind to explore distant galaxies where time behaves strangely, racing against the clock to save the human race.", "Christopher Nolan", "Sci-Fi", 169, new DateOnly(2014, 11, 7), "Interstellar" },
                    { 19, "The rise and fall of Henry Hill through the ranks of the mob is chronicled from his early days as a teenager running errands for mobsters to his eventual downfall. Based on a true story, this visceral portrait of organized crime depicts the glamorous lifestyle and brutal violence of the Italian-American mafia in New York.", "Martin Scorsese", "Crime", 146, new DateOnly(1990, 9, 12), "Goodfellas" },
                    { 20, "FBI trainee Clarice Starling seeks the help of imprisoned cannibalistic serial killer Dr. Hannibal Lecter to catch another serial killer known as Buffalo Bill. As Clarice delves deeper into the twisted minds of psychopaths, she must face her own demons while racing against time to save Buffalo Bill's latest victim.", "Jonathan Demme", "Thriller", 118, new DateOnly(1991, 2, 14), "The Silence of the Lambs" },
                    { 21, "Following the D-Day invasion, a group of U.S. soldiers goes behind enemy lines during World War II on a dangerous mission to retrieve a paratrooper whose three brothers have been killed in action. Captain John Miller and his squad question the morality of risking eight men to save one in this brutal and realistic portrayal of war.", "Steven Spielberg", "War", 169, new DateOnly(1998, 7, 24), "Saving Private Ryan" },
                    { 22, "A powerful Roman general named Maximus is betrayed and his family murdered by the corrupt son of the Emperor. Sold into slavery and forced to become a gladiator, Maximus rises through the ranks of the arena, seeking vengeance against the man who destroyed his life while the fate of Rome hangs in the balance.", "Ridley Scott", "Action", 155, new DateOnly(2000, 5, 5), "Gladiator" },
                    { 23, "A teenager is accidentally sent thirty years into the past in a time-traveling DeLorean invented by his eccentric scientist friend. Marty McFly must ensure his teenage parents fall in love in 1955 or risk erasing his own existence, all while finding a way back to 1985 in this perfect blend of comedy and science fiction.", "Robert Zemeckis", "Sci-Fi", 116, new DateOnly(1985, 7, 3), "Back to the Future" },
                    { 24, "Two detectives, a veteran and a rookie, hunt a serial killer who uses the seven deadly sins as his modus operandi. As Detective Somerset prepares for retirement and Detective Mills begins his career, they navigate a dark urban landscape pursuing a methodical killer whose crimes grow increasingly disturbing, leading to a devastating finale.", "David Fincher", "Crime", 127, new DateOnly(1995, 9, 22), "Se7en" },
                    { 25, "An orphaned boy discovers on his eleventh birthday that he is actually a wizard and has been accepted to Hogwarts School of Witchcraft and Wizardry. Harry Potter learns about his famous past, makes lifelong friends, and confronts the dark wizard who murdered his parents in this magical adventure based on J.K. Rowling's beloved novel.", "Chris Columbus", "Fantasy", 125, new DateOnly(2001, 7, 20), "Harry Potter and the Sorcerer's Stone" },
                    { 26, "After the devastating events of Infinity War where Thanos eliminated half of all life in the universe, the remaining Avengers assemble one final time to undo the Mad Titan's actions. Through a dangerous time heist, Earth's mightiest heroes attempt to restore those they've lost in this epic conclusion to the Infinity Saga.", "Russo Brothers", "Superhero", 181, new DateOnly(2019, 4, 26), "Avengers: Endgame" },
                    { 27, "Billionaire industrialist Tony Stark is captured by terrorists and forced to build a devastating weapon. Instead, he creates a high-tech suit of armor to escape captivity. Returning home, Stark refines his invention and transforms himself into the armored superhero Iron Man, launching the Marvel Cinematic Universe.", "Jon Favreau", "Superhero", 126, new DateOnly(2008, 6, 13), "Iron Man" },
                    { 28, "A former Special Forces operative turned mercenary undergoes a rogue experiment that leaves him with accelerated healing powers and a twisted sense of humor. Wade Wilson adopts the alter ego Deadpool and hunts down the man who nearly destroyed his life, all while breaking the fourth wall in this irreverent superhero film.", "Tim Miller", "Action", 108, new DateOnly(2016, 2, 12), "Deadpool" },
                    { 29, "In a near future where mutants are nearly extinct, an aging and weary Logan lives a quiet life caring for a dying Professor X. When a young mutant girl with abilities similar to his own appears, Logan must protect her from dark forces hunting her down in this emotional and violent conclusion to Wolverine's story.", "James Mangold", "Action", 137, new DateOnly(2017, 3, 3), "Logan" },
                    { 30, "The story of American scientist J. Robert Oppenheimer and his role in developing the atomic bomb during World War II. This biographical thriller explores the moral implications of creating such devastating weapons, Oppenheimer's security clearance hearing, and the personal cost of being known as the father of the atomic bomb.", "Christopher Nolan", "Biography", 114, new DateOnly(2023, 7, 21), "Oppenheimer" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "MovieId", "Title", "Username" },
                values: new object[,]
                {
                    { 1, "The Godfather sets the standard for crime dramas. Brando's performance is unforgettable.", 2, "A Masterpiece", "admin" },
                    { 2, "Every scene is perfectly crafted. This is cinema at its finest.", 2, "Timeless Classic", "testuser1" },
                    { 3, "Heath Ledger's Joker is one of the greatest villain performances ever captured on film.", 3, "Heath Ledger's Legacy", "testuser2" },
                    { 4, "This transcends the superhero genre. It's a crime thriller first, superhero movie second.", 3, "Best Superhero Movie", "testuser3" },
                    { 5, "Nolan created something truly special here. The tension never lets up.", 3, "Dark and Gripping", "admin" },
                    { 6, "Improves on Batman Begins in every way. Absolutely phenomenal.", 3, "Perfect Sequel", "testuser1" },
                    { 7, "This movie makes me cry every single time. Tom Hanks is perfect.", 4, "Emotional Journey", "testuser2" },
                    { 8, "So many quotable moments and beautiful life lessons throughout.", 4, "Life Lessons", "testuser3" },
                    { 9, "A beautiful story about perseverance and kindness.", 4, "Heartwarming", "admin" },
                    { 10, "Nolan at his best. The layers of dreams within dreams are brilliantly executed.", 5, "Mind-Bending Masterpiece", "testuser1" },
                    { 11, "I've seen this five times and still catch new details every viewing.", 5, "Rewatchability", "testuser2" },
                    { 12, "Still debating whether the top fell or not. Genius ambiguity.", 5, "That Ending Though", "testuser3" },
                    { 13, "The hallway fight scene alone is worth the price of admission.", 5, "Visual Spectacle", "admin" },
                    { 14, "The soundtrack perfectly complements the intensity of the film.", 5, "Hans Zimmer Score", "testuser1" },
                    { 15, "Changed action movies forever. The bullet-time effects were groundbreaking.", 6, "Revolutionary", "testuser2" },
                    { 16, "So much deeper than just an action film. Makes you question reality.", 6, "Philosophical Depth", "testuser3" },
                    { 17, "Watched it again recently and it's still as impressive as 1999.", 6, "Still Holds Up", "admin" },
                    { 18, "The twist still gets me even on rewatches. Fincher is a master.", 7, "Twisted and Brilliant", "testuser1" },
                    { 19, "The social commentary is even more relevant today than when it was released.", 7, "Commentary on Consumerism", "testuser2" },
                    { 20, "The start of an incredible journey. Peter Jackson brought Middle-earth to life.", 8, "Epic Beginning", "testuser3" },
                    { 21, "Tolkien would be proud. This is how you adapt a beloved book.", 8, "Perfect Adaptation", "admin" },
                    { 22, "The casting is absolutely perfect. Every character feels authentic.", 8, "The Fellowship", "testuser1" },
                    { 23, "The soundtrack is hauntingly beautiful and elevates every scene.", 8, "Howard Shore's Music", "testuser2" },
                    { 24, "The Battle of Helm's Deep is one of the greatest battle sequences ever filmed.", 9, "Helm's Deep", "testuser3" },
                    { 25, "Andy Serkis deserved an Oscar. Gollum is incredibly realistic and tragic.", 9, "Gollum's Performance", "admin" },
                    { 26, "The ending had me in tears. What a way to finish the trilogy.", 10, "Perfect Conclusion", "testuser1" },
                    { 27, "Swept the Academy Awards and deserved every single one.", 10, "11 Oscars!", "testuser2" },
                    { 28, "The Battle of Pelennor Fields is breathtaking in scope and execution.", 10, "Epic Scale", "testuser3" },
                    { 29, "After three films, seeing everyone's journey conclude is so satisfying.", 10, "Emotional Payoff", "admin" },
                    { 30, "While all three are masterpieces, this one edges out the others for me.", 10, "Best of the Trilogy", "testuser1" },
                    { 31, "This movie made me fall in love with dinosaurs. Still magical today.", 11, "Childhood Favorite", "testuser2" },
                    { 32, "The T-Rex escape scene is pure cinematic perfection.", 11, "Spielberg Magic", "testuser3" },
                    { 33, "The mix of animatronics and CGI still looks better than many modern films.", 11, "Practical Effects", "admin" },
                    { 34, "Changed cinema forever. The beginning of an incredible saga.", 12, "Started It All", "testuser1" },
                    { 35, "Han, Luke, Leia - these characters defined a generation.", 12, "Iconic Characters", "testuser2" },
                    { 36, "Lucas created such a rich and detailed universe. Amazing vision.", 12, "World-Building", "testuser3" },
                    { 37, "The mythology and philosophy behind the Force is fascinating.", 12, "The Force", "admin" },
                    { 38, "Darker, more complex, and emotionally resonant. The peak of the saga.", 13, "Best Star Wars Film", "testuser1" },
                    { 39, "One of the greatest plot twists in cinema history. Still gives me chills.", 13, "I Am Your Father", "testuser2" },
                    { 40, "The Dagobah scenes with Yoda are the heart of this film.", 13, "Yoda's Wisdom", "testuser3" },
                    { 41, "The redemption of Vader makes the entire original trilogy worthwhile.", 14, "Satisfying Ending", "admin" },
                    { 42, "Say what you will, but the Ewoks are adorable and fun.", 14, "Ewoks!", "testuser1" },
                    { 43, "The film that launched Pixar's incredible run. Revolutionary animation.", 15, "Pixar's First", "testuser2" },
                    { 44, "Not just a kids movie. The themes of friendship and jealousy are universal.", 15, "Adults Love It Too", "testuser3" },
                    { 45, "My kids love this just as much as I did when it came out.", 15, "Timeless Story", "admin" },
                    { 46, "The ocean environments are gorgeously rendered. Pixar outdid themselves.", 16, "Beautiful Underwater World", "testuser1" },
                    { 47, "The relationship between Marlin and Nemo is so touching.", 16, "Father-Son Story", "testuser2" },
                    { 48, "Jack and Rose's love story still makes me cry. Cameron's masterpiece.", 17, "Epic Romance", "testuser3" },
                    { 49, "The attention to detail in recreating the ship is incredible.", 17, "Historical Accuracy", "admin" },
                    { 50, "My Heart Will Go On is forever tied to this film. Iconic.", 17, "Celine Dion", "testuser1" },
                    { 51, "The second half is intense and heartbreaking. Amazingly done.", 17, "The Sinking", "testuser2" },
                    { 52, "Nolan's most ambitious film. The scale is mind-boggling.", 18, "Space Epic", "testuser3" },
                    { 53, "The father-daughter relationship gives the sci-fi real heart.", 18, "Emotional Core", "admin" },
                    { 54, "Kip Thorne's involvement shows. The black hole visualization is scientifically grounded.", 18, "Scientific Accuracy", "testuser1" },
                    { 55, "The tension during the docking sequence had me holding my breath.", 18, "That Docking Scene", "testuser2" },
                    { 56, "Hans Zimmer creates another unforgettable soundtrack. The organ is perfect.", 18, "Zimmer's Score Again", "testuser3" },
                    { 57, "Peak Scorsese. The energy and pacing are unmatched.", 19, "Scorsese's Best", "admin" },
                    { 58, "Liotta's narration pulls you into the mob life. Fantastic performance.", 19, "Ray Liotta", "testuser1" },
                    { 59, "The Copacabana scene is one continuous shot of brilliance.", 19, "That Tracking Shot", "testuser2" },
                    { 60, "Anthony Hopkins makes Hannibal Lecter unforgettable with minimal screen time.", 20, "Hopkins is Terrifying", "testuser3" },
                    { 61, "The tension between Clarice and Lecter is electric. Masterful direction.", 20, "Psychological Thriller", "admin" },
                    { 62, "The D-Day landing is the most realistic war sequence ever filmed. Brutal and powerful.", 21, "Opening Scene", "testuser1" },
                    { 63, "This changed how war films are made. Unflinching and honest.", 21, "Spielberg's War Film", "testuser2" },
                    { 64, "Every soldier feels like a real person with a story. Great casting.", 21, "Ensemble Cast", "testuser3" },
                    { 65, "The ending gets me every time. Earn this.", 21, "Emotional Sacrifice", "admin" },
                    { 66, "Crowe commands the screen. One of the best action performances.", 22, "Are You Not Entertained?", "testuser1" },
                    { 67, "Ridley Scott brings ancient Rome to life. The Colosseum battles are incredible.", 22, "Roman Spectacle", "testuser2" },
                    { 68, "Maximus' journey from general to slave to gladiator is gripping.", 22, "Revenge Story", "testuser3" },
                    { 69, "One of the most fun movies ever made. Never gets old.", 23, "Perfect Entertainment", "admin" },
                    { 70, "The paradoxes and timeline are handled brilliantly. Smart and funny.", 23, "Time Travel Done Right", "testuser1" },
                    { 71, "Fincher's vision is so bleak and captivating. The atmosphere is oppressive.", 24, "Dark Masterpiece", "testuser2" },
                    { 72, "The ending is devastating. One of cinema's darkest conclusions.", 24, "What's in the Box?", "testuser3" },
                    { 73, "The chemistry between the two leads drives the film. Perfect casting.", 24, "Pitt and Freeman", "admin" },
                    { 74, "The perfect introduction to the wizarding world. Captured the magic of the books.", 25, "Magical Beginning", "testuser1" },
                    { 75, "Seeing Hogwarts for the first time was pure magic. Columbus nailed it.", 25, "Childhood Wonder", "testuser2" },
                    { 76, "Daniel, Emma, and Rupert were perfectly cast. They grew up with these roles.", 25, "The Trio", "testuser3" },
                    { 77, "Hedwig's Theme is iconic. Williams created magic with the music.", 25, "John Williams", "admin" },
                    { 78, "The culmination of 22 films. The Russo Brothers stuck the landing.", 26, "Epic Conclusion", "testuser1" },
                    { 79, "That portals scene gave me chills. Pure comic book joy brought to life.", 26, "Avengers Assemble", "testuser2" },
                    { 80, "Tony's sacrifice had the entire theater in tears. I love you 3000.", 26, "Emotional Goodbye", "testuser3" },
                    { 81, "Revisiting past MCU moments was brilliant fan service done right.", 26, "Time Heist", "admin" },
                    { 82, "Didn't feel like three hours. Every minute earned its place.", 26, "Three Hour Epic", "testuser1" },
                    { 83, "RDJ was born to play Tony Stark. This launched everything.", 27, "Started the MCU", "testuser2" },
                    { 84, "The Mark I suit built in a cave is still one of my favorite sequences.", 27, "Practical Suit", "testuser3" },
                    { 85, "That ending changed superhero movies forever. No secret identity.", 27, "I Am Iron Man", "admin" },
                    { 86, "Finally a faithful Deadpool. Reynolds fought for this and it shows.", 28, "R-Rated Perfection", "testuser1" },
                    { 87, "The meta humor is perfectly balanced. Hilarious from start to finish.", 28, "Fourth Wall Breaking", "testuser2" },
                    { 88, "Hugh Jackman's final performance as Logan is heartbreaking and perfect.", 29, "Wolverine's Swan Song", "testuser3" },
                    { 89, "Mangold crafted something unique. Feels like a Western first, superhero film second.", 29, "Western Superhero Film", "admin" },
                    { 90, "The R-rating allows the violence and emotion to hit harder. Raw and real.", 29, "Dark and Brutal", "testuser1" },
                    { 91, "Dafne Keen steals scenes. The relationship between Logan and Laura is beautiful.", 29, "Laura", "testuser2" },
                    { 92, "A three-hour character study that never drags. Nolan's most mature film.", 30, "Nolan's Masterwork", "testuser3" },
                    { 93, "Murphy finally gets the lead role he deserves. His Oppenheimer is haunting.", 30, "Cillian Murphy", "admin" },
                    { 94, "Nolan created the Trinity test without CGI. The commitment to practical filmmaking is admirable.", 30, "Practical Effects", "testuser1" },
                    { 95, "The exploration of Oppenheimer's legacy and guilt is fascinating and relevant.", 30, "Historical Drama", "testuser2" },
                    { 96, "This film demands to be seen in IMAX. The visuals and sound are overwhelming.", 30, "IMAX Experience", "testuser3" }
                });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "Content", "ParentPostId", "Username" },
                values: new object[,]
                {
                    { 1, "Absolutely agree. The character development is masterful.", 1, "testuser2" },
                    { 2, "The opening scene sets the tone perfectly.", 1, "testuser3" },
                    { 3, "Have you seen the director's cut? Even better.", 1, "testuser1" },
                    { 4, "The cinematography is stunning throughout.", 2, "admin" },
                    { 5, "His commitment to the role was incredible. RIP.", 3, "testuser1" },
                    { 6, "The interrogation scene is pure acting brilliance.", 3, "admin" },
                    { 7, "He disappeared into that role completely.", 3, "testuser3" },
                    { 8, "The moral complexity elevates it above typical superhero fare.", 5, "testuser2" },
                    { 9, "The bench scenes with Jenny break my heart.", 7, "admin" },
                    { 10, "Hanks deserved every award for this performance.", 7, "testuser1" },
                    { 11, "Life is like a box of chocolates...", 7, "testuser3" },
                    { 12, "Run, Forrest, run! Never gets old.", 8, "admin" },
                    { 13, "The architecture of dreams is fascinating.", 10, "testuser3" },
                    { 14, "Each layer adds another dimension to the story.", 10, "admin" },
                    { 15, "The van sequence with different time scales is genius.", 10, "testuser2" },
                    { 16, "I choose to believe it keeps spinning.", 12, "admin" },
                    { 17, "The BRAAAM sound became iconic.", 14, "testuser2" },
                    { 18, "Time by Hans Zimmer is incredible.", 14, "testuser3" },
                    { 19, "Zimmer and Nolan are a perfect pairing.", 14, "admin" },
                    { 20, "The slow-mo sequences were mind-blowing in 1999.", 15, "testuser1" },
                    { 21, "The effects still look great today.", 17, "testuser1" },
                    { 22, "I watch it every few years and it never disappoints.", 17, "testuser2" },
                    { 23, "The green tint aesthetic is so iconic.", 17, "testuser3" },
                    { 24, "The reveal is so well-constructed.", 18, "admin" },
                    { 25, "The Shire looks exactly as I imagined reading the books.", 20, "testuser1" },
                    { 26, "The practical effects and sets are incredible.", 20, "testuser2" },
                    { 27, "New Zealand never looked so beautiful.", 20, "admin" },
                    { 28, "Ian McKellen and Viggo Mortensen are standouts.", 22, "testuser3" },
                    { 29, "The practical effects for that battle are astounding.", 24, "testuser1" },
                    { 30, "The siege towers and ladders felt so real.", 24, "testuser2" },
                    { 31, "Theoden's speech still gives me goosebumps.", 24, "admin" },
                    { 32, "I wasn't ready for that many endings, but all were necessary.", 26, "testuser3" },
                    { 33, "Tied with Ben-Hur and Titanic. Legendary.", 27, "testuser3" },
                    { 34, "Best Picture was well-deserved.", 27, "admin" },
                    { 35, "A clean sweep at the Oscars is so rare.", 27, "testuser1" },
                    { 36, "The Grey Havens scene destroys me every time.", 29, "testuser2" },
                    { 37, "The T-Rex reveal with the water ripples is iconic.", 31, "testuser1" },
                    { 38, "This sparked my interest in paleontology.", 31, "admin" },
                    { 39, "Welcome to Jurassic Park - chills!", 31, "testuser3" },
                    { 40, "The combination was revolutionary for the time.", 33, "testuser1" },
                    { 41, "The cultural impact cannot be overstated.", 34, "testuser3" },
                    { 42, "It created modern blockbuster filmmaking.", 34, "admin" },
                    { 43, "May the Force be with you - timeless.", 34, "testuser2" },
                    { 44, "The cantina scene alone shows the depth of the universe.", 36, "admin" },
                    { 45, "Kershner brought a different tone that elevated it.", 38, "testuser3" },
                    { 46, "The Hoth battle is incredible.", 38, "admin" },
                    { 47, "Cloud City is such a great setting.", 38, "testuser2" },
                    { 48, "No one saw it coming. Perfect execution.", 39, "admin" },
                    { 49, "The throne room confrontation is powerful.", 41, "testuser2" },
                    { 50, "Vader's turn back to the light is earned.", 41, "testuser3" },
                    { 51, "I never cry at movies, but that scene gets me.", 41, "testuser1" },
                    { 52, "Who knew CGI could be this charming?", 43, "admin" },
                    { 53, "Randy Newman's music is perfect.", 45, "testuser1" },
                    { 54, "You've Got a Friend in Me makes me nostalgic.", 45, "testuser2" },
                    { 55, "Watching it with kids adds a new dimension.", 45, "admin" },
                    { 56, "Just keep swimming! That mantra helped me through tough times.", 47, "testuser3" },
                    { 57, "DiCaprio and Winslet have incredible chemistry.", 48, "admin" },
                    { 58, "I'll never let go, Jack - devastating.", 48, "testuser1" },
                    { 59, "The king of the world scene is iconic.", 48, "testuser2" },
                    { 60, "That song will forever be associated with this film.", 50, "testuser3" },
                    { 61, "The visuals are stunning on IMAX.", 52, "admin" },
                    { 62, "Miller's planet with the time dilation is terrifying.", 52, "testuser1" },
                    { 63, "The wormhole scene is visually incredible.", 52, "testuser2" },
                    { 64, "The tesseract scene bends my mind.", 54, "testuser3" },
                    { 65, "No Time for Caution during the docking is perfection.", 56, "admin" },
                    { 66, "The organ creates such an otherworldly feel.", 56, "testuser1" },
                    { 67, "Zimmer outdid himself with this score.", 56, "testuser2" },
                    { 68, "The editing and pacing are masterful.", 57, "testuser2" },
                    { 69, "That shot is studied in film schools.", 59, "testuser3" },
                    { 70, "The precision required for that is insane.", 59, "admin" },
                    { 71, "Pure cinema magic in one take.", 59, "testuser1" },
                    { 72, "The fava beans line is chilling.", 60, "admin" },
                    { 73, "Changed my understanding of WWII films.", 62, "testuser2" },
                    { 74, "The sound design makes you feel like you're there.", 62, "testuser3" },
                    { 75, "I had to pause and collect myself after that scene.", 62, "admin" },
                    { 76, "Tom Hanks leads an incredible cast.", 64, "testuser1" },
                    { 77, "My name is Maximus Decimus Meridius...", 66, "admin" },
                    { 78, "Crowe's intensity is palpable in every scene.", 66, "testuser2" },
                    { 79, "The physicality he brought to the role was impressive.", 66, "testuser3" },
                    { 80, "The fallen leaves at the beginning foreshadow his journey.", 68, "admin" },
                    { 81, "The clockwork precision of the plot is remarkable.", 70, "testuser2" },
                    { 82, "Every rewatch reveals new details.", 70, "testuser3" },
                    { 83, "The screenplay is tight and clever.", 70, "admin" },
                    { 84, "The rain-soaked aesthetic is perfect.", 71, "testuser1" },
                    { 85, "That final scene haunts me.", 72, "admin" },
                    { 86, "Brad Pitt's reaction is so raw.", 72, "testuser1" },
                    { 87, "Ernest Hemingway said it best in the film.", 72, "testuser2" },
                    { 88, "Seeing the Great Hall for the first time was magical.", 74, "testuser3" },
                    { 89, "Watching them grow up on screen was special.", 76, "admin" },
                    { 90, "They embodied their characters perfectly.", 76, "testuser1" },
                    { 91, "The trio's friendship feels genuine.", 76, "testuser2" },
                    { 92, "11 years of storytelling paid off beautifully.", 78, "testuser2" },
                    { 93, "I stood up in the theater. Couldn't help myself.", 79, "testuser3" },
                    { 94, "That moment justified every previous film.", 79, "admin" },
                    { 95, "On your left - perfect callback.", 79, "testuser1" },
                    { 96, "RDJ gave everything to that role.", 80, "admin" },
                    { 97, "Not a wasted moment in the entire runtime.", 82, "testuser2" },
                    { 98, "The pacing is masterful.", 82, "testuser3" },
                    { 99, "I could watch another hour easily.", 82, "admin" },
                    { 100, "No one else could have played Tony Stark.", 83, "testuser3" },
                    { 101, "That press conference was a game-changer.", 85, "testuser1" },
                    { 102, "The public identity made MCU unique.", 85, "testuser2" },
                    { 103, "RDJ ad-libbed that line. Perfect.", 85, "testuser3" },
                    { 104, "The opening credits scene is genius.", 87, "testuser3" },
                    { 105, "17 years as Wolverine. What a legacy.", 88, "admin" },
                    { 106, "The rawness of his performance is stunning.", 88, "testuser1" },
                    { 107, "He went out on the highest note possible.", 88, "testuser2" },
                    { 108, "Finally showing what Wolverine can really do.", 90, "testuser3" },
                    { 109, "The structure with three timelines is brilliant.", 92, "admin" },
                    { 110, "The black and white sequences are powerful.", 92, "testuser1" },
                    { 111, "Nolan's restraint makes it more impactful.", 92, "testuser2" },
                    { 112, "The commitment to in-camera effects is admirable.", 94, "admin" },
                    { 113, "The sound design in IMAX is overwhelming.", 96, "admin" },
                    { 114, "70mm film makes such a difference.", 96, "testuser1" },
                    { 115, "This is Nolan's most IMAX-worthy film.", 96, "testuser2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Hash", "Name", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, "f+sMn7oo7y6hh5wWoFvHwWIAAG07t07OUNEl5BQq8Q30FkVUdC6K40tLpVauz7jWBnlHwtphR6+/KwUmqTWbSw==", "admin", "ADMIN", "kFaXeaDXHPtNRDPZd517LfrKlf6omU7XZ4E7lCxjFSDJLFhcgS+CV35kxMAJRGUxhRyAnGp9z676NK5Q7PQHYIV5lbYkrZ5X08CsF9ncRWQ8BoQaH4rTHmPrjtfCDzQF0HywBPGMbzsHqLBEDs3pCv+Bh9pt+7JAYg0RaH2QQA8=" },
                    { 2, "Z9/FAsGUmTudmYKgEPaD4XLHnboaNWWAdHITiUN+/C4cIRsjTfpAw33hdKApFdS6gUAU9fhzIqNsr3k9cHJqaQ==", "testuser1", "AUTHENTICATEDUSER", "2pJ8FMxMKYxuX6Wqsl5pXO2hh6HkHVXgW5TFFofNWQuwnENOYmXJG6aTUQDauKej+DDdx0JLI+QFwHWzQsUpetoR1+RBke0iAST3PuXjoovNerNdKqSBmxHOv4RzMMgVOV+D2pTIZxFgwmxFMRKTfj1xJPYBiDHFxftzPT+2ZtQ=" },
                    { 3, "TZb0yh2zN0xpqAqDFThoIwgoPLWhMrReKrRerEMmmLlByEWcwdd+tPwxebveCPhsAG7ImPm7gYdykROrKwruGw==", "testuser2", "AUTHENTICATEDUSER", "ihJ5oN69oDLnSLVM4nMdAtfafKXeCTMT3+sYQHy1gi8z/XNwGM3ogE9kbeFZfZf91KMvqt+FDgtvhp0+T686Z74TeYA795VYxi7y72nTbceFBqBYiPzznh0udgzGz5eJRvW8ukF4tXHtK2ViIbydw3DGxthzUm2hcwK1N/xaHx4=" },
                    { 4, "LHlz0gscyF/ap2Ev0Ffzr2hxg0aVzqym7GFflmbvU9kQ02dfpqcjy2wyUtZebPOYaSxYK2S7UivHukKqYDwYcw==", "testuser3", "AUTHENTICATEDUSER", "2QpGjfuIGtUBxUNAvpBJfaoQA8RHqSpq4PlyyfVkUbtauvcjI7E07ZckIbvjI9lMYqox/5VQDM3gTMKL5v07EVEF2RK09zcEuGyAzkpLldGd+ENDSjBImSWMMg6TMBvAHfBfzghe4+G4laKa7U3646seoawMnpoXLj2qHMSGT08=" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
