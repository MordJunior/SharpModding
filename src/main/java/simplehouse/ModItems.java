package simplehouse;

import net.minecraft.entity.LivingEntity;
import net.minecraft.item.Item;
import net.minecraft.item.ItemStack;
import net.minecraft.world.World;

public class ModItems {
    public class Simple_House extends Item {

        public Simple_House(Properties properties) {
            super(properties);
        }
        @Override
        public void onUse(World worldIn, LivingEntity livingEntityIn, ItemStack stack, int count){
            SimpleHouse.Logger.debug("use Item");
        }
    }
}
