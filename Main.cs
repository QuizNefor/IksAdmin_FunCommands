using System.Reflection.Metadata.Ecma335;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Commands;
using IksAdminApi;

namespace IksAdmin_FunCommands;

public class Main : BasePlugin
{
    public override string ModuleName => "IksAdmin_FunCommands";
    public override string ModuleVersion => "1.0.0";
    private static PluginCapability<IIksAdminApi> _adminApiCapability = new("iksadmin:core");
    public IIksAdminApi? AdminApi;

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        AdminApi = _adminApiCapability.Get();
        AdminApi!.AddNewCommand(
            "clip_set",
            "Set ammo count",
            "css_clip_set <#UID/#SID/name> <ammo count>",
            2,
            "clip_set",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnClipSetCommand
            );
        AdminApi!.AddNewCommand(
            "slap",
            "Slap player",
            "css_slap <#UID/#SID/name> <damage>",
            2,
            "slap",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnSlapCommand
            );
        AdminApi!.AddNewCommand(
                "speed",
            "Set player move speed",
            "css_speed <#UID/#SID/name> <speed>",
            2,
            "speed",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnSpeedSetCommand
            );
        AdminApi!.AddNewCommand(
            "gravity",
            "Set player gravity",
            "css_gravity <#UID/#SID/name> <gravity>",
            2,
            "gravity",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnGravityCommand
        );
        AdminApi!.AddNewCommand(
            "set_money",
            "Set player money",
            "css_set_money <#UID/#SID/name> <money>",
            2,
            "set_money",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnSetMoneyCommand
        );
        AdminApi!.AddNewCommand(
            "hp",
            "Set player hp",
            "css_hp <#UID/#SID/name> <hp>",
            2,
            "hp",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnHpCommand
        );
        AdminApi!.AddNewCommand(
            "bury",
            "Bury player",
            "css_bury <#UID/#SID/name> <depth (default = 10)>",
            2,
            "bury",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnBuryCommand
        );
        AdminApi!.AddNewCommand(
            "unbury",
            "Unbury player",
            "css_unbury <#UID/#SID/name> <depth (default = 15)>",
            2,
            "unbury",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnUnBuryCommand
        );
        AdminApi!.AddNewCommand(
            "freeze",
            "Freeze player",
            "css_freeze <#UID/#SID/name>",
            1,
            "freeze",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnFreezeCommand
        );
        AdminApi!.AddNewCommand(
            "unfreeze",
            "Unfreeze player",
            "css_unfreeze <#UID/#SID/name>",
            1,
            "unfreeze",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnUnFreezeCommand
        );
        AdminApi!.AddNewCommand(
            "noclip",
            "Noclip for player",
            "css_noclip <#UID/#SID/name>",
            1,
            "noclip",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnNoclipCommand
        );
        AdminApi!.AddNewCommand(
            "teleportplayer",
            "Teleport to player",
            "css_teleportplayer <#UID/#SID/name> <#UID/#SID/name>",
            2,
            "teleportplayer",
            "b",
            CommandUsage.CLIENT_AND_SERVER,
            OnTeleportPlayerCommand
        );
    }

    private void OnTeleportPlayerCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        var target1 = XHelper.GetPlayerFromArg(args[0]);
        if (target1 == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target1.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.TeleportPlayer(target1);
    }

    private void OnNoclipCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.PlayerPawn.Value!.ToggleNoclip();
    }


    private void OnUnFreezeCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.PlayerPawn.Value!.Unfreeze();
    }

    private void OnFreezeCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.PlayerPawn.Value!.Freeze();
    }

    private void OnUnBuryCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.PlayerPawn.Value!.Unbury(int.Parse(args[1]));
    }

    private void OnBuryCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.PlayerPawn.Value!.Bury(int.Parse(args[1]));
    }

    private void OnHpCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.SetHp(int.Parse(args[1]));
    }

    private void OnSetMoneyCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.SetMoney(int.Parse(args[1]));
    }

    private void OnGravityCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.SetGravity(int.Parse(args[1]));
    }

    private void OnSpeedSetCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        target.SetSpeed(int.Parse(args[1]));
    }

    private void OnSlapCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }

        target.PlayerPawn.Value!.Slap(int.Parse(args[1]));
    }

    private void OnClipSetCommand(CCSPlayerController caller, Admin? admin, List<string> args, CommandInfo info)
    {
        var target = XHelper.GetPlayerFromArg(args[0]);
        if (target == null)
        {
            AdminApi.SendMessageToPlayer(caller, AdminApi.Localizer["NOTIFY_PlayerNotFound"]);
            return;
        }
        if (!target.PawnIsAlive)
        {
            AdminApi.SendMessageToPlayer(caller, Localizer["ERROR_PlayerNotAlive"]);
            return;
        }
        var weapon = target.PlayerPawn.Value!.WeaponServices!.ActiveWeapon;
        weapon.Value!.Clip1 = int.Parse(args[0]);
        weapon.Value!.VData!.MaxClip1 = int.Parse(args[0]);
        Utilities.SetStateChanged(target.PlayerPawn.Value, "CBasePlayerPawn", "m_pWeaponServices");
        
        
        
        
        
        
        
    }
}   
    