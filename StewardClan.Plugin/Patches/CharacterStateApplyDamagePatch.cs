
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Steward_Clan.Plugin;

namespace Steward_Clan.Plugin.Patches
{
    [HarmonyPatch(typeof(CharacterState), "ApplyDamage", MethodType.Enumerator)]
    public static class CharacterStateApplyDamagePatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var found = false;

            for (int i = 0; i < codes.Count - 1; i++)
            {
                var code = codes[i];
                Plugin.Logger.LogDebug($"Instruction {i} is {code.opcode} {code.operand}");
                if (code.opcode == OpCodes.Callvirt &&
                    code.operand is MethodInfo method &&
                    method.Name == nameof(CharacterState.IsPyreHeart))
                {
                    if (!found)
                    {
                        found = true;
                        continue;
                    }
                    // Found the second IsPyreHeart() call
                    var branch = codes[i + 1];
                    if (branch.opcode != OpCodes.Brfalse_S || branch.opcode != OpCodes.Brfalse)
                        continue;

                    var skipLabel = (Label)branch.operand;

                    // Insert after IsPyreHeart but before Brfalse_S
                    var newInstructions = new List<CodeInstruction>
                {
                    new CodeInstruction(OpCodes.Dup), // Duplicate result of IsPyreHeart
                    new CodeInstruction(OpCodes.Brfalse_S, skipLabel), // if false, skip our code
                    new CodeInstruction(OpCodes.Ldarg_0), // Load 'this' (CharacterState)
                    new CodeInstruction(OpCodes.Ldarg_1), // Load damage amount
                    new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CharacterStateApplyDamagePatch), nameof(OnPyreDamage))),
                };

                    // Remove the Brfalse_S, replace it after our injection
                    codes.RemoveAt(i + 1);
                    codes.InsertRange(i + 1, newInstructions);
                    codes.Insert(i + 1 + newInstructions.Count, branch);

                    found = true;
                    return codes;
                }
            }

            Plugin.Logger.LogWarning("StewardClan: Failed to find IsPyreHeart() in CharacterState.ApplyDamage");
            return codes;
        }

        private static void OnPyreDamage(CharacterState character, int damage)
        {
            if (character != null && damage > 0)
            {
                // Custom logic here
                Plugin.Logger.LogInfo($"PyreHeart took {damage} damage!");
            }
        }
    }

}