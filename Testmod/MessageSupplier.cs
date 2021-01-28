namespace Net
{
    namespace Minecraftforge
    {
        namespace Fml
        {
            namespace Event
            {
                namespace Lifecycle
                {
                    public partial class InterModProcessEvent
                    {
                        public partial class Map
                        {
                            public class MessageSupplier
                            {
                                public static string Name { get; set; }
                                public string get()
                                {
                                    return Name;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

