﻿namespace Xunkong.SnapMetadata;

public class SnapWeaponInfo
{
    public int Id { get; set; }
    public int PromoteId { get; set; }
    public int Sort { get; set; }
    public int WeaponType { get; set; }
    public int RankLevel { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public string AwakenIcon { get; set; }
    public List<WeaponGrowCurf> GrowCurves { get; set; }
    public Affix Affix { get; set; }
    public List<int> CultivationItems { get; set; }
}


public class Affix
{
    public string Name { get; set; }
    public List<SnapDescription> Descriptions { get; set; }
}

public class SnapDescription
{
    public int Level { get; set; }
    public string Description { get; set; }
}


public class WeaponGrowCurf
{
    public double InitValue { get; set; }
    public int Type { get; set; }
    public int Value { get; set; }
}

