using System;


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
                            public Collector map(Action<Supplier> p)
                            {
                                return default;
                            }
                            internal object map(Func<object, object> p)
                            {
                                throw new NotImplementedException();
                            }
                        }
                    }
                }
            }
        }
    }
}

