using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models.DM;

public partial class DmDangTaiLieu
{
    public int IdDangTaiLieu { get; set; }

    public string? DangTaiLieu { get; set; }

    public virtual ICollection<TbSachDaXuatBan> TbSachDaXuatBans { get; set; } = new List<TbSachDaXuatBan>();
}
