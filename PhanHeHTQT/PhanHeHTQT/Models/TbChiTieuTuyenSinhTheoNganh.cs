using System;
using System.Collections.Generic;
using PhanHeHTQT.Models.DM;

namespace PhanHeHTQT.Models;

public partial class TbChiTieuTuyenSinhTheoNganh
{
    public int IdChiTieuTuyenSinhTheoNganh { get; set; }

    public int? IdLoaiHinhDaoTao { get; set; }

    public int? IdNganhDaoTao { get; set; }

    public string? Nam { get; set; }

    public int? ChiTieu { get; set; }

    public virtual DmLoaiHinhDaoTao? IdLoaiHinhDaoTaoNavigation { get; set; }

    public virtual DmNganhDaoTao? IdNganhDaoTaoNavigation { get; set; }
}
