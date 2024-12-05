using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models.DM;

public partial class DmViTriViecLam
{
    public int IdViTriViecLam { get; set; }

    public string? ViTriViecLam { get; set; }

    public virtual ICollection<TbThongTinViecLamSauTotNghiep> TbThongTinViecLamSauTotNghieps { get; set; } = new List<TbThongTinViecLamSauTotNghiep>();
}
