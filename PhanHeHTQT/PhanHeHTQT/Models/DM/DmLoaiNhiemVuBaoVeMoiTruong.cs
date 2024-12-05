using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models.DM;

public partial class DmLoaiNhiemVuBaoVeMoiTruong
{
    public int IdLoaiNhiemVuBaoVeMoiTruong { get; set; }

    public string? LoaiNhiemVuBaoVeMoiTruong { get; set; }

    public virtual ICollection<TbHoatDongBaoVeMoiTruong> TbHoatDongBaoVeMoiTruongs { get; set; } = new List<TbHoatDongBaoVeMoiTruong>();
}
