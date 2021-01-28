namespace Net
{
    namespace Minecraft
    {
        namespace Block
        {
            public class Blocks
            {
                //public static object DIRT { get; internal set; }
                public class DIRT
                {
                    public static string Name { get; set; }
                    public static string getRegistryName()
                    {
                        return Name;
                    }
                }

            }
        }
    }
}

