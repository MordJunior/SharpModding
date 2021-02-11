package simplehouse;

import net.minecraft.block.Block;
import net.minecraft.block.Blocks;
import net.minecraft.entity.LivingEntity;
import net.minecraft.entity.player.PlayerEntity;
import net.minecraft.item.Item;
import net.minecraft.item.ItemStack;
import net.minecraft.item.ItemUseContext;
import net.minecraft.item.UseAction;
import net.minecraft.util.ActionResult;
import net.minecraft.util.ActionResultType;
import net.minecraft.util.Hand;
import net.minecraft.util.Rotation;
import net.minecraft.util.math.BlockPos;
import net.minecraft.util.text.NBTTextComponent;
import net.minecraft.util.text.TranslationTextComponent;
import net.minecraft.world.World;

public class ModItems
{
    public static class Simple_House extends Item
    {
        public Simple_House(Properties properties) {
            super(properties);
        }

        @Override
        public ActionResultType onItemUse(ItemUseContext context) {
            if (context.getWorld().isRemote())
            {
                return ActionResultType.PASS;
            }
            BlockPos clickPosition = context.getPos();
            PlayerEntity player = context.getPlayer();

            //north: 3,4
            //east:  5,6
            //south: 7,0
            //west:  1,2
            //facing is a number from 0 to 7 representing the player facing direciton
            int yaw = (int)player.rotationYaw;
            if (yaw < 0){
                yaw += 360;
            }
            int facing = yaw/45;

            SimpleHouse.Logger.debug(facing);

            clickPosition =  clickPosition.add(0, 1, 0);
            clickPosition.rotate(Rotation.CLOCKWISE_180);

            context.getWorld().setBlockState(clickPosition, Blocks.OAK_DOOR.getDefaultState());
            SimpleHouse.Logger.debug("debug from onItemUse()");
            return ActionResultType.SUCCESS;
        }


        /*
        @Override
        public ActionResult<ItemStack> onItemRightClick(World worldIn, PlayerEntity playerIn, Hand handIn) {
            if (worldIn.isRemote())
            {
                return ActionResult.resultPass(playerIn.getHeldItem(handIn));
            }
            SimpleHouse.Logger.debug("debug from onItemRightClick()");
            playerIn.sendMessage(new TranslationTextComponent("hello"));
            playerIn.

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
        }*/
    }
}
