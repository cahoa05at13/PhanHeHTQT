using System;
using System.Collections.Generic;

namespace PhanHeHTQT.Models.DM;

public partial class DmDonViCapBang
{
    public int IdDonViCapBang { get; set; }

    public string? DonViCapBang { get; set; }

    public virtual ICollection<TbChuongTrinhDaoTao> TbChuongTrinhDaoTaos { get; set; } = new List<TbChuongTrinhDaoTao>();
}
