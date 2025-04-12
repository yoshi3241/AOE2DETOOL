using AOE2DETOOL.Tables.Master;
using System.Diagnostics.CodeAnalysis;
using static AOE2DETOOL.Definition.Enums;

namespace AOE2DETOOL.Models.Data
{
    internal class AOE2DEConvert
    {
        static public bool ToLocalUnitType(long aoe2deUnitId, [MaybeNullWhen(false)] out UnitType localUnitType) => Unit.AOE2DEUnitTypeToLocalUnitType.TryGetValue(aoe2deUnitId, out localUnitType);

        static public bool ToLocalUnitGroupType(UnitType localUnitType, out UnitGroupType unitGroupType)
        {
            var rtn = false;

            unitGroupType = UnitGroupType.None;

            if (Unit.UnitTypeToExpansionInfo.ContainsKey(localUnitType))
            {
                unitGroupType = (UnitGroupType)Unit.UnitTypeToExpansionInfo[localUnitType][Unit.UnitTypeIndex];
                rtn = true;
            }

            return rtn;
        }
    }
}
