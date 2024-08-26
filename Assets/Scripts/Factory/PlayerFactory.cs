using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory
{
    private PlayerController controller;
    private PlayerData playerData;
    private Animator profileUIAnimator;

    public PlayerFactory(PlayerController player, PlayerData data, Animator anim)
    {
        controller = player;
        playerData = data;
        profileUIAnimator = anim;
    }

    public PlayerController CreatePlayer()
    {
        controller.animator.runtimeAnimatorController = playerData.animatorController;
        controller.stat.SetBalloonNum(playerData.balloonNum, playerData.maxBalloonNum);
        controller.stat.SetSpeed(playerData.moveSpeed, playerData.maxSpeed);
        controller.stat.SetPopLength(playerData.popLength, playerData.maxPopLength);
        profileUIAnimator.runtimeAnimatorController = playerData.profileAnimatorController;
        return controller;            
    }
}
