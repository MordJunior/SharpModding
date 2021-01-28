package simplehouse;

import net.minecraft.entity.LivingEntity;
import net.minecraft.entity.player.PlayerEntity;
import net.minecraft.item.Item;
import net.minecraft.item.ItemStack;
import net.minecraft.item.ItemUseContext;
import net.minecraft.item.UseAction;
import net.minecraft.util.ActionResult;
import net.minecraft.util.ActionResultType;
import net.minecraft.util.Hand;
import net.minecraft.util.text.TranslationTextComponent;
import net.minecraft.world.World;

public class ModItems {
    public static class Simple_House extends Item {

        public Simple_House(Properties properties) {
            super(properties);
        }

        @Override
        public ActionResultType onItemUse(ItemUseContext context) {
            if (context.getWorld().isRemote())
            {
                return ActionResultType.PASS;
            }

            SimpleHouse.Logger.debug("debug from onItemUse()");
            return ActionResultType.SUCCESS;
        }


        @Override
        public ActionResult<ItemStack> onItemRightClick(World worldIn, PlayerEntity playerIn, Hand handIn) {
            if (worldIn.isRemote())
            {
                return ActionResult.resultPass(playerIn.getHeldItem(handIn));
            }
            SimpleHouse.Logger.debug("debug from onItemRightClick()");
            playerIn.sendMessage(new TranslationTextComponent("hello"));
            if (this.isFood()) {
                ItemStack itemstack = playerIn.getHeldItem(handIn);
                if (playerIn.canEat(this.getFood().canEatWhenFull())) {
                    playerIn.setActiveHand(handIn);
                    return ActionResult.resultConsume(itemstack);
                } else {
                    return ActionResult.resultFail(itemstack);
                }
            } else {
                return ActionResult.resultPass(playerIn.getHeldItem(handIn));
            }
        }
    }
}
