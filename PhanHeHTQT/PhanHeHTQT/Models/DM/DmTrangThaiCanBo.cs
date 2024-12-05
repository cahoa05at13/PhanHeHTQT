using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models.DM;

public partial class DmTrangThaiCanBo
{
    public int IdTrangThaiCanBo { get; set; }

    public string? TrangThaiCanBo { get; set; }

    public virtual ICollection<TbCanBo> TbCanBos { get; set; } = new List<TbCanBo>();
}
