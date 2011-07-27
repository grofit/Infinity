using Infinity.Lookups;

namespace Infinity.Tools.Helpers
{
    internal static class ResourceTypesHelper
    {
        public static string GetResourceStringFromType(int resourceType)
        {
            // Horrible but cant use readonly types in switch
            if (ResourceTypes.ACM == resourceType)          { return "Acm"; }
            if (ResourceTypes.ARE == resourceType)          { return "Are"; }
            if (ResourceTypes.BAM == resourceType)          { return "Bam"; }
            if (ResourceTypes.BCS == resourceType)          { return "Bcs"; }
            if (ResourceTypes.BIF == resourceType)          { return "Bif"; }
            if (ResourceTypes.BIK == resourceType)          { return "Bik"; }
            if (ResourceTypes.BMP == resourceType)          { return "Bmp"; }
            if (ResourceTypes.CHR == resourceType)          { return "Chr"; }
            if (ResourceTypes.CHU == resourceType)          { return "Chu"; }
            if (ResourceTypes.CRE == resourceType)          { return "Cre"; }
            if (ResourceTypes.DLG == resourceType)          { return "Dlg"; }
            if (ResourceTypes.EFF == resourceType)          { return "Eff"; }
            if (ResourceTypes.GAM == resourceType)          { return "Gam"; }
            if (ResourceTypes.GUI_SCRIPT == resourceType)   { return "Gui Script"; }
            if (ResourceTypes.IDS == resourceType)          { return "Ids"; }
            if (ResourceTypes.INI == resourceType)          { return "Ini"; }
            if (ResourceTypes.ITM == resourceType)          { return "Itm"; }
            if (ResourceTypes.MOS == resourceType)          { return "Mos"; }
            if (ResourceTypes.MUS == resourceType)          { return "Mus"; }
            if (ResourceTypes.MVE == resourceType)          { return "Mve"; }
            if (ResourceTypes.OGG == resourceType)          { return "Ogg"; }
            if (ResourceTypes.PLT == resourceType)          { return "Plt"; }
            if (ResourceTypes.PNG == resourceType)          { return "Png"; }
            if (ResourceTypes.PRO == resourceType)          { return "Sav"; }
            if (ResourceTypes.SCRIPT == resourceType)       { return "Script"; }
            if (ResourceTypes.SPL == resourceType)          { return "Spl"; }
            if (ResourceTypes.SRC == resourceType)          { return "Src"; }
            if (ResourceTypes.STO == resourceType)          { return "Sto"; }
            if (ResourceTypes.TIS == resourceType)          { return "Tis"; }
            if (ResourceTypes.TLK == resourceType)          { return "Tlk"; }
            if (ResourceTypes.TOH == resourceType)          { return "Toh"; }
            if (ResourceTypes.TOT == resourceType)          { return "Tot"; }
            if (ResourceTypes.TwoDA == resourceType)        { return "2da"; }
            if (ResourceTypes.VAR == resourceType)          { return "Var"; }
            if (ResourceTypes.VVC == resourceType)          { return "Vvc"; }
            if (ResourceTypes.WAV == resourceType)          { return "Wav"; }
            if (ResourceTypes.WED == resourceType)          { return "Wed"; }
            if (ResourceTypes.WFX == resourceType)          { return "Wfx"; }
            if (ResourceTypes.WMP == resourceType)          { return "Wmp"; }
            return "Unknown";
        }
    }
}