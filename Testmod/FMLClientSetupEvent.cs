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
                    public partial class FMLClientSetupEvent
                    {
                        public FMLClientSetupEvent getMinecraftSupplier() { return new FMLClientSetupEvent(); }

                        public GameSettingsClass get() { return new GameSettingsClass(); }
                    }
                }
            }
        }
    }
}

