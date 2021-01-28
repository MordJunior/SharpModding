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
                            public class Supplier
                            {
                                public MessageSupplier GetMessageSupplier()
                                {
                                    return new MessageSupplier();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

