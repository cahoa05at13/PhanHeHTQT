using System;
using System.Collections.Generic;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Models;

public partial class TbHoatDongTaiChinh
{
    public int IdHoatDongTaiChinh { get; set; }

    public int? IdLoaiHoatDongTaiChinh { get; set; }

    public string? NamTaiChinh { get; set; }

    public int? KinhPhi { get; set; }

    public string? NoiDung { get; set; }

    public virtual DmHoatDongTaiChinh? IdLoaiHoatDongTaiChinhNavigation { get; set; }
}
