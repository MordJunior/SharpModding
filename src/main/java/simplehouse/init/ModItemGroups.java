package simplehouse.init;

import net.minecraft.item.Item;
import net.minecraft.item.ItemGroup;
import net.minecraft.item.ItemStack;
import net.minecraft.item.Items;
import net.minecraftforge.registries.ObjectHolder;
import simplehouse.SimpleHouse;
import sun.java2d.pipe.SpanShapeRenderer;

import java.util.function.Supplier;

public class ModItemGroups {
    public static class ModItemGroup extends ItemGroup {
        public final Supplier<ItemStack> iconSupplier;

        public ModItemGroup(final String name, final Supplier<ItemStack> iconSupplier) {
            super(name);
            this.iconSupplier = iconSupplier;
        }

        @Override
        public ItemStack createIcon() {
            return iconSupplier.get();
        }
    }
    public static final ItemGroup MOD_ITEM_GROUP = new ModItemGroup(
            SimpleHouse.MODID, () -> new ItemStack(ModItems.SIMPLE_HUT));

}

