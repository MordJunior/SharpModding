using System;
using Net.Minecraft.Block;
using Net.Minecraftforge.Common;
using Net.Minecraftforge.Event;
using Net.Minecraftforge.Eventbus.Api;
using Net.Minecraftforge.Fml;
using Net.Minecraftforge.Fml.Common;
using Net.Minecraftforge.Fml.Event.Lifecycle;
using Net.Minecraftforge.Fml.Event.Server;
using Net.Minecraftforge.Fml.Javafmlmod;
using Org.Apache.Logging.Log4j;
using Java.Util.Stream;
using System.Runtime.CompilerServices;



public class ExampleMod
{
    // Die übersetzung für @Mod("examplemod")
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    sealed class ModAttribute : Attribute
    {
        readonly string modName;

        public ModAttribute(string modName)
        {
            this.modName = modName;
        }

        public string ModName
        {
            get { return modName; }
        }

    }
    sealed class SubscribeEventAttribute : Attribute
    {

    }

    sealed class EventBusSubscriber : Attribute
    {
        readonly string mod;
    }

    public int NamedInt { get; set; }

    private readonly Logger LOGGER;


    public ExampleMod()
    {
        FMLJavaModLoadingContext.get().getModEventBus().addListener<FMLCommonSetupEvent>(this.setup);
        FMLJavaModLoadingContext.get().getModEventBus().addListener<InterModEnqueueEvent>(this.enqueueIMC);
        FMLJavaModLoadingContext.get().getModEventBus().addListener<InterModProcessEvent>(this.processIMC);
        FMLJavaModLoadingContext.get().getModEventBus().addListener<FMLClientSetupEvent>(this.doClientStuff);

        MinecraftForge.EVENT_BUS.register(this);
    }

    private void setup(FMLCommonSetupEvent @event)
    {
        LOGGER.info("HELLO FROM PREINIT");
        LOGGER.info("DIRT BLOCK >> {}", Blocks.DIRT.getRegistryName());
    }

    private void doClientStuff(FMLClientSetupEvent @event)
    {
        LOGGER.info("Got game settings {}", @event.getMinecraftSupplier().get().gameSettings);
    }

    private void enqueueIMC(InterModEnqueueEvent @event)
    {
        InterModComms.sendTo("examplemod", "helloworld", () => { LOGGER.info("Hello world from the MDK"); return "Hello world"; });
    }

    private void processIMC(InterModProcessEvent @event)
    {
        LOGGER.info("Got IMC {}", @event.getIMCStream().map(m => m.GetMessageSupplier().get()).collect(Collectors.toList()));
    }

    [SubscribeEvent]
    public void onServerStarting(FMLServerStartingEvent @event)
    {
        LOGGER.info("HELLO from server starting");
    }

    // @Mod.EventBusSubscriber(bus = Mod.EventBusSubscriber.Bus.MOD)
    //[EventBusSubscriber]

        [SubscribeEvent]
     public void onBlocksRegistry(RegistryEvent.Register<Block> blockRegistryEvent)
     {
         // register a new block here
         LOGGER.info("HELLO from Register Block");
     }
    
}

