using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models;

public partial class TbChiTietThuChi
{
    public int IdChiTietThuChi { get; set; }

    public int? IdLoaiThuChi { get; set; }

    public string? TenKhoanThuChi { get; set; }

    public string? NamTaiChinh { get; set; }

    public int? SoTien { get; set; }

    public virtual TbLoaiThuChi? IdLoaiThuChiNavigation { get; set; }
}
